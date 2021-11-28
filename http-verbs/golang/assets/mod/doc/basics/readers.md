# Readers

The `io` package specifies the `io.Reader` interface, which represents the read end of a stream of data

The Go standard library contains many implementations of this interface, including files, network connections, compressors, ciphers, and others

The `io.Reader` interface has a `Read` method:

```go
func (T) Read(b []byte) (n int, err error)
```

`Read` populates the given byte slice w/ data and returns the number of bytes populated and an error value. It returns an `io.EOF` error when the stream ends

The example code creates a `strings.Reader` and consumes its output 8 bytes at a time

```go
package main

import (
	"fmt"
	"io"
	"strings"
)

func main() {
	r := strings.NewReader("Hello, Reader!")
	
	b := make([]byte, 8)
	for {
		n, err := r.Read(b)
		fmt.Printf("n = %v err = %v b = %v\n", n, err, b)
		fmt.Printf("b[:n] = %q\n", b[:n])
		if err == io.EOF {
			break
        }
    }
}
```

**Output**:

```zsh
n = 8 err = <nil> b = [72 101 108 108 111 44 32 82]
b[:n] = "Hello, R"
n = 6 err = <nil> b = [101 97 100 101 114 33 32 82]
b[:n] = "eader!"
n = 0 err = EOF b = [101 97 100 101 114 33 32 82]
b[:n] = ""
```


