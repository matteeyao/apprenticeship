# Variables

The `var` stmt declares a list of variables; as in function argument lists, the type is last

A `var` stmt can be at package or function level

```go
package main

import "fmt"

var c, python, java, bool

func main() {
	var i int
	fmt.Println(i, c, python, java)
}
```

**Output**:

```zsh
0 false false false
```

## Variables w/ initializers

A `var` declaration can include initializers, one per variable

If an initializer is present, the type can be omitted; the variable will take the type of the initializer

```go
package main

import "fmt"

var i, j int = 1, 2

func main() {
	var c, python, java = true, false, "no!"
	fmt.Println(i, j, c, python, java)
}
```

**Output**:

```zsh
0 false false false
```

## Short variable declarations

Inside a function, the `:=` short assignment stmt can be used in place of a `var` declaration w/ implicit type

Outside a function, every stmt begins w/ a keyword (`var`, `func`, and so on) and so the `:=` construct is not available

```go
package main

import "fmt"

func main() {
	var i, j int = 1, 2
	k := 3
	c, python, java := true, false, "no!"
	
	fmt.Println(i, j, k, c, python, java)
}
```

**Output**:

```zsh
1 2 3 true false no!
```

## Basic types

Go's basic types are

```go
bool

string

int     int8    in16    int32   int64
uint    uint8   uint16  uint32  uint64  uintptr

byte    // alias for uint8
rune    // alias for int32
        // represents a Unicode code point
		
float32 float64
complex64   complex128
```

The example shows variables of several types, and also that variable declarations may be "factored" into blocks, as w/ import statements

The `int`, `uint`, `uintptr` types are usually 32 bits wide on 32-bit systems and 64 bits wide on 64-bit systems. When you need an integer value you should use `int` unless you have a specific reason to use a sized or unsigned integer type

```go
package main

import (
	"fmt"
	"math/cmplx"
)

var (
	ToBe    bool        = false
	MaxInt  uint64      = 1<<64 - 1
	z       complex128  = cmplx.Sqrt(-5 + 12i)
)

func main() {
	fmt.Printf("Type: %T Value: %v\n", ToBe, ToBe)
	fmt.Printf("Type: %T Value: %v\n", MaxInt, MaxInt)
	fmt.Printf("Type: %T Value: %v\n", z, z)
}
```

**Output**:

```zsh
Type: bool Value: false
Type: uint64 Value: 18446744073709551615
Type: complex128 Value: (2+3i)
```

## Zero values

Variables declared w/p an explicit initial value are given their *zero value*

The zero value is:

* `0` for numeric types

* `false` for the boolean type, and

* `""` (the empty string) for strings

```go
package main

import "fmt"

func main() {
	var i int
	var f float64
	var b bool
	var s string
	fmt.Printf("%v %v %v %q\n", i, f, b, s)
}
```

**Output**:

```zsh
0 0 false ""
```

## Type conversions

The expression `T(v)` converts the  value `v` to the type `T`

Some numeric conversions:

```go
var i int = 42
var f float64 = float64(i)
var u uint = uint(f)
```

Or, put more simply:

```go
i := 42
f := float64(i)
u := uint(f)
```

Unlike in C, in Go assignment btwn items of different type requires an explicit conversion. Try removing the `float64` or `uint` conversions in the example and see what happens:

```go
package main

import {
	"fmt"
	"math"
}

func main() {
	var x, y int = 3, 4
	var f float64 = math.Sqrt(float64(x*x + y*y))
	var z uint = uint(f)
	fmt.Println(x, y, z)
}
```

**Output**:

```zsh
3 4 5
```

## Type inference

When declaring a variable w/o specifying an explicit type (either by using the `:=` syntax or `var =` expression syntax), the variable's type is inferred from the value on the right hand side

When the right hand side of the declaration is typed, the new variable is of that same type:

```go
var i int
j := i // j is an int
```

But when the right hand side contains an untyped numeric constant, the new variable may be an `int`, `float64`, or `complex128` depending on the precision of the constant:

```go
i := 42             // int
f := 3.142          // float64
g := 0.867 + 0.5i   // complex128
```

Try changing the initial value of `v` in the example code and observe how its type is affected:

```go
package main

import "fmt"

func main() {
	v := 42 // change me!
	fmt.Printf("v is of type %T\n", v)
}
```

**Output**:

```zsh
v is of type int
```
