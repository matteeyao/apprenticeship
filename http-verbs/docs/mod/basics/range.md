# Range

The `range` form of the `for` loop iterates over a slice or map

When ranging over a slice, two values are returned for each iteration. The first is the index, and the second is a copy of the element at that index

```go
package main

import "fmt"

var pow = []int{1, 2, 4, 8, 16, 32, 64, 128}

func main() {
	for i, v := range pow {
		fmt.Printf("2**%d = %d\n", i, v)
    }
}
```

**Output**:

```zsh
2**0 = 1
2**1 = 2
2**2 = 4
2**3 = 8
2**4 = 16
2**5 = 32
2**6 = 64
2**7 = 128
```

You can skip the index or value by assigning to `_`

```go
for i, _ := range pow
for _, value := range pow
```

If you only want the index, you can omit the second variable:

```go
for i := range pow
```

```go
package main

import "fmt"

func main() {
	pow := make([]int, 10)
	for i := range pow {
		pow[i] = 1 << uint(i) // == 2**i
    }
	for _, value := range pow {
		fmt.Printf("%d\n", value)
    }
}
```

**Output**:

```zsh
1
2
4
8
16
32
64
128
256
512
```

## Exercise: Slices

Implement `Pic`. It should return a slice of length `dy`, each element of which is a slice of `dx` 8-bit unsigned integers. When you run the program, it will display your picture, interpreting the integers as grayscale (well, bluescale) values

The choice of image is up to you. Interesting functions include `(x+y)/2`, `x*y`, and `x^y`

(You need to use a loop to allocate each `[]uint8` inside the `[][]uint8`)

(Use `uint8(intValue)` to convert btwn types)

```go
package main

import "golang.org/x/tour/pic"

func Pic(dx, dy int) [][]uint8 {
	
	pic := make([][]uint8, dy)
	
	for y := range pic {
		
		pic[y] = make([]uint8, dx)
		
		for x := range pic[y] {
			pic[y][x] = uint8( (x+y)/2 )
        }
    }
	
	return pic
}

func main() {
	pic.Show(Pic)
}
```
