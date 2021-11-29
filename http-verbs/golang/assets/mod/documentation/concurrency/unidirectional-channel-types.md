# 8.4.3 Unidirectional Channel Types

```go
func main() {
    naturals := make(chan int)
    squares := make(chan int)

    // Counter
    go func() {
        for x := 0; x < 100; x++ {
            naturals <- x
        }
        close(naturals)
    }()

    // Squarer
    go func() {
        for x := range naturals {
            squares <- x * x
        }
        close(squares)
    }()

    // Printer (in main goroutine)
    for x := range squares {
        fmt.Println(x)
    }
}
```

As programs grow, it is natural to break up large functions into smaller pieces. Our previous example used three goroutines, communicating over two channels, which were local variables of `main`. The program naturally divides into three functions:

```go
func counter(out chan int)
func squarer(out, in chan int)
func printer(in chan int)
```

The `squarer` function, sitting in the middle of the pipeline, takes two parameters, the input channel and the output channel. Both have the same type, but their intended uses are opposite: `in` is only received from, and `out` is only to be sent to. The names `in` and `out` convey this intention, but still, nothing prevents `squarer` from sending to `in` or receiving from `out`.

This arrangement is typical. When a channel is supplied as a function parameter, it is nearly always w/ the intent that it be used exclusively for sending or exclusively for receiving.

To document this intent and prevent misuse, the Go type system provides *unidirectional* channel types that expose only one or the other of the send and receive operations. The type `chan<-int`, a *send-only* channel of `int`, allows sends but not receives. Conversely, the type `<-chan int`, a *receive-only* channel of `int`, allows receives but not sends. (The position of the `<-` arrow relative to the `chan` keyword is a mnemonic). Violations of this discipline are detected at compile time.

Since the `close` operation asserts that no more sends will occur on a channel, only the sending goroutine is in a position to call it, and for this reason it is a compile-time error to attempt to close a receive-only channel.

Here's the squaring pipeline once more, this time w/ unidirectional channel types:

*gopl.io/ch8/pipeline3*

```go
func counter(out chan<- int) {
    for x := 0; x < 100; x++ {
        out <- x
    }
    close(out)
}

func squarer(out chan<- int, in <-chan int) {
    for v := range in {
        out <- v * v
    }
    close(out)
}

func printer(in <-chan int) {
    for v := range in {
        fmt.Println(v)
    }
}

func main() {
    naturals := make(chan int)
    squares := make(chan int)

    go counter(naturals)
    go squarer(squares, naturals)
    printer(squares)
}
```

The call `counter(naturals)` implicitly converts `naturals`, a value of type `chan int`, to the type of the parameter, `chan<- int`. The `printer(squares)` call does a similar implicit conversion to `<-chan int`. Conversions from bidirectional to unidirectional channel types are permitted in any assignment. There is no going back, however: once you have a value of a unidirectional type such as `chan<- int`, there is no way to obtain from it a value of type `chan int` that refers to the same channel data structure.
