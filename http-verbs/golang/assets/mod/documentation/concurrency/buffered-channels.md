# 8.4.4 Buffered Channels

Channels can be *buffered*. Provide the buffer length as the second argument to `make` to initialize a buffered channel:

```go
ch := make(chan int, 100)
```

Sends to a buffered channel block only when the buffer is full. Receives block when the buffer is empty.

Modify the example to overfill the buffer and see what happens.

```go
package main

import "fmt"

func main() {
	ch := make(chan int, 2)
	ch <- 1
	ch <- 2
	fmt.Println(<-ch)
	fmt.Println(<-ch)
}
```

**Output**:

```zsh
1
2
```

A buffered channel has a queue of elements. The queue's maximum size is determined when it is created, by the capacity argument to `make`. The statement below creates a buffered channel capable of holding three `string` values. Figure 8.2 is a graphical representation of `ch` and the channel to which it refers:

```go
ch = make(chan string, 3)
```

![Figure 8.2. An empty buffered channel.](../../../img/documentation/concurrency/empty-buffered-channel.png)

A send operation on a buffered channel inserts an element at the back of the queue, and a receive operation removes an element from the front. If the channel is full, the send operation blocks its goroutine until space is made available by another goroutine's receive. Conversely, if the channel is empty, a receive operation blocks until a value is sent by another goroutine.

We can send up to three values on this channel w/o the goroutine blocking:

```go
ch <- "A"
ch <- "B"
ch <- "C"
```

At this point, the channel is full (Figure 8.3), and a fourth send statement would block:

![Figure 8.3. A full buffered channel.](../../../img/documentation/concurrency/full-buffered-channel.png)

If we receive one value,

```go
fmt.Println(<-ch) // "A"
```

The channel is neither full nor empty (Figure 8.4), so either a send operation or a receive operation could proceed w/o blocking. In this way, the channel's buffer decouples the sending and receiving goroutines

![Figure 8.4. A partially full buffered channel.](../../../img/documentation/concurrency/partially-full-buffered-channel.png)

In the unlikely event that a program needs to know the channel's buffer capacity, it can be obtained by calling the built-in `cap` function:

```go
fmt.Println(cap(ch)) // "3"
```

When applied to a channel, the built-in `len` function returns the number of elements currently buffered. Since in a concurrent program this information is likely to be stale as soon as it is received, its value is limited, but it could conceivably be useful during fault diagnosis or performance optimization.

```go
fmt.Println(len(ch)) // "2"
```

After two more receive operations the channel is empty again, and a fourth would block:

```go
fmt.Println(<-ch) // "B"
fmt.Println(<-ch) // "C"
```

In this example, the send and receive operations were all performed by the same goroutine, but in real programs they are usually executed by different goroutines. Novices are sometimes tempted to use buffered channels within a single goroutine as a queue, lured by their pleasingly simple syntax, but this is a mistake. Channels are deeply connected to goroutine scheduling, and w/o another goroutine receiving from the channel, a sender—and perhaps the whole program—risks becoming blocked forever. If all you need is a simple queue, make one using a slice.

The example below shows an application of a buffered channel. It makes parallel requests to three *mirrors*, that is, equivalent but geographically distributed servers. It sends their responses over a buffered channel, then receives and returns only the first response, which is the quickest one to arrive. Thus `mirrored-Query` returns a result even before the two slower servers have responded. (Incidentally, it's quite normal for several goroutines to send values to the same channel concurrently, as in this example, or to receive from the same channel).

```go
func mirroredQuery() string {
    responses := make(chan string, 3)
    go func() { responses <- request("asia.gopl.io") }()
    go func() { responses <- request("europe.gopl.io") }()
    go func() { responses <- request("americas.gopl.io") }()
    return <-responses // return the quickest response
}

func request(hostname string) (response string) { /* ... */ }
```

Had we used an unbuffered channel, the two slower goroutines would have gotten stuck trying to send their responses on a channel from which no goroutine will ever receive. This situation, called a *goroutine leak*, would be a bug. Unlike garbage variables, leaked goroutines are not automatically collected, so it is important to make sure that goroutines terminate themselves when no longer needed.

The choice btwn unbuffered and buffered channels, and the choice of a buffered channel's capacity, may both affect the correctness of a program. Unbuffered channels give stronger synchronization guarantees b/c every send operation is synchronized w/ its corresponding received; w/ buffered channels, these operations are decoupled. Also, when we know an upper bound on the number of values that will be sent on a channel, it's not unusual to create a buffered channel of that size and perform all the sends before the first value is received. Failure to allocate sufficient buffer capacity would cause the program to deadlock.

Channel buffering may also affect program performance. Imagine three cooks in a cake shop, one baking, one icing, and one inscribing each cake before passing it on to the next cook in the assembly line. In a kitchen w/ little space, each cook that has finished a cake must wait for the next cook to become ready to accept it; this rendezvous is analogous to communication over an unbuffered channel.

If there is space for one cake btwn each cook, a cook may place a finished cake there and immediately start work on the next; this is analogous to a buffered channel w/ capacity 1. So long as the cooks work at about the same rate on average, most of these handovers proceed quickly, smoothing out transient differences in their respective rates. More space btwn cooks—larger buffers—can smooth out bigger transient variations in their rates w/o stalling the assembly line, such as happens when one cook takes a short break, then later rushes to catch up.

On the other hand, if an earlier stage of the assembly line is consistently faster than the following stage, the bugger btwn them will spend most of its time full. Conversely, if the later stage is faster, the buffer will usually be empty. A buffer provides no benefit in this case.

The assembly line metaphor is a useful one for channels and goroutines. For example, if the second stage is more elaborate, a single cook may not be able to keep up w/ the supply from the first cook or meet the demand from the third. To solve the problem, we could hire another cook to help the second, performing the same task but working independently. This is analogous to creating another goroutine communicating over the same channels.

We don't have space to show it here, but the `gopl.io/ch8/cake` package simulates this cake shop, with several parameters you can vary. It includes benchmarks for a few of the scenarios described above
