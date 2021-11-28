# Pointers

## Pointer receivers

You can declare methods w/ pointer receivers

This means the receiver type has the literal syntax `*T` for some type `T`. (Also, `T` cannot itself be a pointer such as `*int`)

For example, the `Scale` method here is defined on `*Vertex`

Methods w/ pointer receivers can modify the value to which the receiver points (as `Scale` does here). Since methods often need to modify their receiver, pointer receivers are more common than value receivers

Try removing the `*` from the declaration of the `Scale` function on the line `func (v *Vertex) Scale(f float64) {` and observe how the program's behavior changes

W/ a value receiver, the `Scale` method operates on a copy of the original `Vertex` value (this is the same behavior as for any other function argument). The `Scale` method must have a pointer receiver to change the `Vertex` value declared in the `main` function

```go
package main

import (
	"fmt"
	"math"
)

type Vertex struct {
	X, Y float64
}

func (v Vertex) Abs() float64 {
	return math.Sqrt(v.X*v.X + v.Y*v.Y)
}

func (v *Vertex) Scale(f float64) {
	v.X = v.X * f
	v.Y = v.Y * f
}

func main() {
	v := Vertex{3, 4}
	v.Scale(10)
	fmt.Println(v.Abs())
}
```

**Output**:

```zsh
50
```

## Pointers and functions

Here we see the `Abs` and `Scale` methods rewritten as functions

Again, try removing the `*` from line `func Scale(v *Vertex, f float64) {`. Can you see why the behavior changes? What else did you need to change for the example to compile?

```go
package main

import (
	"fmt"
	"math"
)

type Vertex struct {
	X, Y float64
}

func Abs(v Vertex) float64 {
	return math.Sqrt(v.X*v.X + v.Y*v.Y)
}

func Scale(v Vertex, f float64) {
	v.X = v.X * f
	v.Y = v.Y * f
}

func main() {
	v := Vertex{3, 4}
	Scale(&v, 10)
	fmt.Println(Abs(v))
}
```

```zsh
5
```

## Methods and pointer indirection

Comparing the previous two programs, you might notice that functions w/ a pointer argument must take a pointer:

```go
var v Vertex
ScaleFunc(v, 5)  // Compile error!
ScaleFunc(&v, 5) // OK
```

while methods w/ pointer receivers take either a value or a pointer as the receiver when they are called:

```go
var v Vertex
v.Scale(5)  // OK
p := &v
p.Scale(10) // OK
```

For the statement `v.Scale(5)`, even though `v` is a value and not a pointer, the method w/ the pointer is called automatically. That is, as a convenience, Go interprets the statement `v.Scale(5)` as `(&v).Scale(5)` since the `Scale` method has a pointer receiver

```go
package main

import "fmt"

type Vertex struct {
	X, Y float64
}

func (v *Vertex) Scale(f float64) {
	v.X = v.X * f
	v.Y = v.Y * f
}

func ScaleFunc(v *Vertex, f float64) {
	v.X = v.X * f
	v.Y = v.Y * f
}

func main() {
	v := Vertex{3, 4}
	v.Scale(2)
	ScaleFunc(&v, 10)

	p := &Vertex{4, 3}
	p.Scale(3)
	ScaleFunc(p, 8)

	fmt.Println(v, p)
}
```

**Output**:

```zsh
{60 80} &{96 72}
```

The equivalent thing happens in the reverse direction

Functions that take a value argument must take a value of that specific type:

```go
var v Vertex
fmt.Println(AbsFunc(v))  // OK
fmt.Println(AbsFunc(&v)) // Compile error!
```

while methods w/ value receivers take either a value or a pointer as the receiver when they are called:

```go
var v Vertex
fmt.Println(AbsFunc(v))  // OK
fmt.Println(AbsFunc(&v)) // Compile error!
```

In this case, the method call `p.Abs()` is interpreted as `(*p).Abs()`

```go
package main

import (
	"fmt"
	"math"
)

type Vertex struct {
	X, Y float64
}

func (v Vertex) Abs() float64 {
	return math.Sqrt(v.X*v.X + v.Y*v.Y)
}

func AbsFunc(v Vertex) float64 {
	return math.Sqrt(v.X*v.X + v.Y*v.Y)
}

func main() {
	v := Vertex{3, 4}
	fmt.Println(v.Abs())
	fmt.Println(AbsFunc(v))

	p := &Vertex{4, 3}
	fmt.Println(p.Abs())
	fmt.Println(AbsFunc(*p))
}
```

**Output**:

```zsh
5
5
5
5
```

## Choosing a value or pointer receiver

There are two reasons to use a pointer receiver

The first is so that the method can modify the value that its receiver points to

The second is to avoid copying the value on each method call. This can be more efficient if the receivers is a large struct, for example

In this example, both `Scale` and `Abs` are w/ receiver type `*Vertex`, even though the `Abs` method needn't modify its receiver

In general, all methods on a given type should have either value or pointer receivers, but not a mixture of both

```go
package main

import (
	"fmt"
	"math"
)

type Vertex struct {
	X, Y float64
}

func (v *Vertex) Scale(f float64) {
	v.X = v.X * f
	v.Y = v.Y * f
} 

func (v *Vertex) Abs() float64 {
	return math.Sqrt(v.X*v.X + v.Y*v.Y)
}

func main() {
	v := &Vertex{3, 4}
	fmt.Printf("Before scaling: %+v, Abs: %v\n", v, v.Abs())
	v.Scale(5)
	fmt.Printf("After scaling: %+v, Abs: %v\n", v, v.Abs())
}
```

**Output**:

```zsh
Before scaling: &{X:3 Y:4}, Abs: 5
After scaling: &{X:15 Y:20}, Abs: 25
```
