# Range and Close

A sender can `close` a channel to indicate that no more values will be sent. Receivers can test whether a channel has been closed by assigning a second parameter to the receiving expression: after

```go
v, ok := <-ch
```

`ok` is `false` if there are no more values to receive and the channel is closed

The loop `for i := range c` receives values from the channel repeatedly until it is closed.


> [!NOTE]
> Only the sender should close a channel, never the receiver. Sending on a closed channel will cause a panic.

> [!NOTE]
> Channels aren't like files; you don't usually need to close them. Closing is only necessary when the receiver must be told there are no more values coming, such as to terminate a range loop.

```go
package main

import (
	"fmt"
)

func fibonacci(n int, c chan int) {
	x, y := 0, 1
	for i := 0; i < n; i++ {
		c <- x
		x, y = y, x+y
	}
	close(c)
}

func main() {
	c := make(chan int, 10)
	go fibonacci(cap(c), c)
	for i := range c {
		fmt.Println(i)
	}
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
```
