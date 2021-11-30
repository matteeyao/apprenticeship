# 8.7 Multiplexing with `select`

## Select

The `select` statement lets a goroutine wait on multiple communication operations.

A `select` blocks until one of its cases can run, then it executes that case. It chooses one at random if multiple are ready.

```go
package main

import "fmt"

func fibonacci(c, quit chan int) {
	x, y := 0, 1
	for {
		select {
		case c <- x:
			x, y = y, x+y
		case <-quit:
			fmt.Println("quit")
			return
		}
	}
}

func main() {
	c := make(chan int)
	quit := make(chan int)
	go func() {
		for i := 0; i < 10; i++ {
			fmt.Println(<-c)
		}
		quit <- 0
	}()
	fibonacci(c, quit)
}
```

**Output**:

```zsh
0
1
1
2
3
5
8
13
21
34
quit
```

## 8.7 Multiplexing with `select`

The program below does the countdown for a rocket launch. The `time.Tick` function returns a channel on which it sends events periodically, acting like a metronome. The value of each event is a timestamp, but it is rarely as interesting as the fact of its delivery:

*gopl.io/ch8/countdown1*

```go
func main() {
    fmt.Println("Commencing countdown.")
    tick := time.Tick(1 * time.Second)
    for countdown := 10; countdown > 0; countdown-- {
        fmt.Println(countdown)
        <-tick
    }
    launch()
}
```

Now let's add the ability to abort the launch sequence by pressing the return key during the countdown. First, we start a goroutine that tries to read a single byte from the standard input and, if it succeeds, sends a value on a channel called `abort`.

*gopl.io/ch8/countdown2*

```go
abort := make(chan struct{})
go func() {
    os.Stdin.Read(make([]byte, 1)) // read a single byte
    abort <- struct{}{}
}()
```

Now each iteration of the countdown loop needs to wait for an event to arrive on one of the two channels: the ticker channel if everything is fine ("nominal" in NASA jargon) or an abort event if there was an "anomaly." We can't just receive from each channel b/c whichever operation we try first will block until completion. We need to *multiplex* these operations, and to do that, we need a *select statement*:

```go
select {
case <-ch1:
    // ...
case x := <-ch2:
    // ...use x...
case ch3 <- y:
    // ...
default:
    // ...
}
```

The general form of a select stmt is shown above. Like a switch stmt, it has a number of cases and an optional `default`. Each case specifies a *communication* (a send or receive operation on some channel) and an associated block of statements. A receive expression may appear on its own, as in the first case, or within a short variable declaration, as in the second case; the second form lets you refer to the received value.

A `select` waits until a communication for some case is ready to proceed. It then performs that communication and executes the case's associated statements; the other communications do not happen. A `select` w/ no cases, `select{}`, waits forever.

Let's return to our rocket launch program. The `time.After` function immediately returns a channel, and starts a new goroutine that sends a single value on that channel after the specified time. The select statement below waits until the first of two events arrives, either an abort event or the event indicating that 10 seconds have elapsed. If 10 seconds go by w/ no abort, the launch proceeds.

```go
func main() {
    // ...create abort channel...

    fmt.Println("Commencing countdown.  Press return to abort.")
    select {
    case <-time.After(10 * time.Second):
        // Do nothing.
    case <-abort:
        fmt.Println("Launch aborted!")
        return
    }
    launch()
}
```

The example below is more subtle. The channel `ch`, whose buffer size is 1, is alternately empty then full, so only one of the cases can proceed, either the send when `i` is even, or the receive when `i` is odd. It always prints `0 2 4 6 8`.

```go
ch := make(chan int, 1)
for i := 0; i < 10; i++ {
    select {
    case x := <-ch:
        fmt.Println(x) // "0" "2" "4" "6" "8"
    case ch <- i:
    }
}
```

If multiple cases are ready, `select` picks one at random, which ensures that every channel has an equal chance of being selected. Increasing the buffer size of the previous example makes its output nondeterministic, b/c when the buffer is neither full nor empty, the select statement figuratively tosses a coin.

Let's make our launch program print the countdown. The select statement below causes each iteration of the loop to wait up to 1 second for an abort, but no longer.

*gopl.io/ch8/countdown3*

```go
func main() {
    // ...create abort channel...

    fmt.Println("Commencing countdown.  Press return to abort.")
    tick := time.Tick(1 * time.Second)
    for countdown := 10; countdown > 0; countdown-- {
        fmt.Println(countdown)
        select {
        case <-tick:
            // Do nothing.
        case <-abort:
            fmt.Println("Launch aborted!")
            return
        }
    }
    launch()
}
```

The `time.Tick` function behaves as if it creates a goroutine that calls `time.Sleep` in a loop, sending an event each time it wakes up. When the countdown function above returns, it stops receiving events from `tick`, but the ticker goroutine is still there, trying in vain to send on a channel from which no goroutine is receiving—a goroutine leak (§8.4.4).

The `Tick` function is convenient, but it’s appropriate only when the ticks will be needed throughout the lifetime of the application. Otherwise, we should use this pattern:

```go
ticker := time.NewTicker(1 * time.Second)

<-ticker.C // receive from the ticker's channel

ticker.Stop() // cause the ticker's goroutine to terminate
```

Sometimes we want to try to send or receive on a channel but avoid blocking if the channel is not ready—a *non-blocking* communication. A `select` statement can do that too. A `select` may have a `default`, which specifies what to do when none of the other communications can proceed immediately.

The select statement below receives a value from the `abort` channel if there is one to receive; otherwise it does nothing. This is a non-blocking receive operation; doing it repeatedly is called *polling* a channel.

```go
select {
case <-abort:
    fmt.Printf("Launch aborted!\n")
    return
default:
    // do nothing
}
```

The zero value for a channel is `nil`. Perhaps surprisingly, nil channels are sometimes useful. Because send and receive operations on a nil channel block forever, a case in a select statement whose channel is nil is never selected. This lets us use `nil` to enable or disable cases that correspond to features like handling timeouts or cancellation, responding to other input events, or emitting output. We’ll see an example in the next section.
