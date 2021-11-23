/* Exercise: rot13Reader

A common pattern is an `io.Reader that wraps another `io.Reader`, modifying the stream in some way

For example, the `gzip.NewReader` function takes an `io.Reader` (a stream of compressed data) and returns a
`*gzip.Reader` that also implements `io.Reader` (a stream of the decompressed data)

Implement a `rot13Reader` that implements `io.Reader` and reads from an `io.Reader`, modifying the stream
by applying the `rot13` substitution cipher to all alphabetical characters

The `rot13Reader` type is provided for you. Make it an `io.Reader` by implementing its `Read` method
 */

package main

import (
	"io"
	"os"
	"strings"
)

type rot13Reader struct {
	r io.Reader
}

func (rot *rot13Reader) Read(p []byte) (n int, err error) {
	n, err = rot.r.Read(p)
	for i := 0; i < len(p); i++ {
		if p[i] >= 'A' && p[i] < 'Z' {
			p[i] = 65 + (((p[i] - 65) + 13) % 26)
		} else if p[i] >= 'a' && p[i] <= 'z' {
			p[i] = 97 + (((p[i] - 97) + 13) % 26)
		}
	}
	return
}

func main() {
	s := strings.NewReader("Lbh penpxrq gur pbqr!")
	r := rot13Reader{s}
	io.Copy(os.Stdout, &r)
}
