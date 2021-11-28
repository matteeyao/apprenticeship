# Goroutines

## The Go Programming Language

In Go, each concurrently executing activity is called a *goroutine*. Consider a program that has two functions, one that does some computation and one that writes some output, and assume that neither function calls the other. A sequential program may call one function and then call the other, but in a *concurrent* program w/ two or more goroutines, calls to *both* functions can be active at the same time. We'll see such a program in a moment

If you have used operating system threads or threads in other languages, then you can assume for now that a goroutine is similar to a thread, and you'll be able to write correct programs

When a program starts, its only goroutine is the one that calls the `main` function, so we call it the `main goroutine`. New goroutines are created by the `go` stmt. Syntactically, a `go` stmt is an ordinary function or method call prefixed by the keyword `go`. A `go` stmt causes the function to be called in a newly created goroutine. The `go` stmt itself completes immediately:

```go
f()     // call f(); wait for it to return
go f()  // create a new goroutine that calls f(); don't wait
```

In the example below, the main goroutine computes the 45th Fibonacci number. Since it uses the terribly inefficient recursive algorithm, it runs for an appreciable time, during which we'd like to provide the user w/ a visual indication that the program is still running, by displaying an animated textual "spinner"

```go
func main() {
	go spinner(100 * time.Millesecond)
	const n = 45
	fibN := fib(n)  // slow
	fmt.Printf("\rFibonacci(%d) = %d\n", n, fibN)
}

func spinner(delay time.Duration) {
	for {
	    for _, r := range `-\|/` {
            fmt.Printf("\r%c", r)
            time.Sleep(delay)
        }
    }
}

func fib(x int) int {
    if x < 2 {
        return x
    }
    return fib(x-1) + fib(x-2)
}
```

After several seconds of animation, the `fib(45)` returns and the `main` function prints its result:

```zsh
/
Fibonacci(45) = 1134903170
```

The `main` function then returns. When this happens, all goroutines are abruptly terminated and the program exits. Other than by returning from `main` or exiting the program, there is no programmatic way for one goroutine to stop another, but as we will see later, there are ways to communicate w/ a goroutine to request that it stop itself

Notice how the program is expressed as the composition of two autonomous activities, spinning and Fibonacci computation. Each is written as a separate function but both make progress concurrently

## Go Tour

A *goroutine* is a lightweight thread managed by the Go runtime

```go
go f(x, y, z)
```

starts a new goroutine running

```go
f(x, y, z)
```

The evaluation of `f`, `x`, `y`, and `z` happens in the current goroutine and the execution of `f` happens in the new goroutine

Goroutines run in the same address space, so access to shared memory must be synchronized. The `sync` package provides useful primitives, although you won't need them much in Go as there are other primitives

```go
package main

import (
	"fmt"
	"time"
)

func say(s string) {
	for i := 0; i < 5; i++ {
		time.Sleep(100 * time.Millisecond)
		fmt.Println(s)
    }
}

func main() {
	go say("world")
	say("hello")
}
```

**Output**:

```zsh
world
hello
hello
world
world
hello
hello
world
world
hello
```
