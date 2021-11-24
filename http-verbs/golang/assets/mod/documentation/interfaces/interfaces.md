# Interfaces

An *interface type* is defined as a set of method signatures

A value of interface type can hold any value that implements those methods

> [!NOTE]
> There is an error in the example code on line `a = v`. `Vertex` (the value type) doesn't implement `Abser` b/c the `Abs` method is defined only on `*Vertex` (the pointer type)

```go
package main

import (
	"fmt"
	"math"
)

type Abser interface {
	Abs() float64
}

func main() {
	var a Abser
	f := MyFloat(-math.Sqrt2)
	v := Vertex{3, 4}
	
	a = f   // a MyFloat implements Abser
	a = &v  // a *Vertex implements Abser
	
	// In the following line, v is a Vertex (not *Vertex)
	// and does NOT implement Abser
	a = v
	
	fmt.Println(a.Abs())
}

type MyFloat float64

func (f MyFloat) Abs() float64 {
	if f < 0 {
		return float64(-f)
    }
	return float64(f)
}

type Vertex struct {
	X, Y float64
}

func (v *Vertex) Abs() float64 {
	return math.Sqrt(v.X*v.X + v.Y*v.Y)
}
```

**Output**:

```zsh
./main.go:22:4: cannot use v (type Vertex) as type Abser in assignment:
        Vertex does not implement Abser (Abs method has pointer receiver)
```

## Interfaces are implemented implicitly

A type implements an interface by implementing its methods. There is no explicit declaration of intent, no "implements" keyword

Implicit interfaces decouple the definition of an interface from its implementation, which could then appear in any package w/o prearrangement

```go
package main

import "fmt"

type I interface {
	M()
}

type T struct {
	S string
}

// This method means type T implements the interface I,
// but we don't need to explicitly declare that it does so.
func (t T) M() {
	fmt.Println(t.S)
}

func main() {
	var i I = T{"hello"}
	i.M()
}
```

**Output**:

```zsh
hello
```

## Interface values

Under the hood, interface values can be thought of as a tuple of a value and a concrete type:

```go
(value, type)
```

An interface value holds a value of a specific underlying concrete type

Calling a method on an interface value executes the method of the same name on its underlying type

```go
package main

import (
	"fmt"
	"math"
)

type I interface {
	M()
}

type T struct {
	S string
}

func (t *T) M() {
	fmt.Println(t.S)
}

type F float64

func (f F) M() {
	fmt.Println(f)
}

func main() {
	var i I
	
	i = &T{"Hello"}
	describe(i)
	i.M()
	
	i = F(math.Pi)
	describe(i)
	i.M()
}

func describe(i I) {
	fmt.Printf("(%v, %T)\n", i, i)
}
```

**Output**:

```zsh
(&{Hello}, *main.T)
Hello
(3.141592653589793, main.F)
3.141592653589793
```

## Interface values w/ nil underlying values

If the concrete value inside the interface itself is `nil`, the method will be called w/ a nil receiver

In some languages, this would trigger a null pointer exception, but in Go it is common to write methods that gracefully handle being called w/ a nil receiver (as w/ the method `M` in this example)

> [!NOTE]
> An interface value that holds a nil concrete value is itself non-nil.

```go
package main

import "fmt"

type I interface {
	M()
}

func (t *T) M() {
	if t == nil {
		fmt.Println("<nil>")
		return
    }
	fmt.Println(t.S)
}

func main() {
	var i I
	
	var t *T
	i = t
	describe(i)
	i.M()
	
	i = &T{"hello"}
	describe(i)
	i.M()
}

func describe(i I) {
	fmt.Printf("(%v, %T)\n", i, i)
}
```

**Output**:

```zsh
(<nil>, *main.T)
<nil>
(&{hello}, *main.T)
hello
```

## Nil interface values

A `nil` interface value holds neither value nor concrete type

Calling a method on a `nil` interface is a run-time error b/c there is no type inside the interface tuple to indicate which *concrete* method to call

```go
package main

import "fmt"

type I interface {
	M()
}

func main() {
	var i I
	describe(i)
	i.M()
}

func describe(i I) {
	fmt.Printf("(%v, %T)\n", i, i)
}
```

**Output**:

```zsh
(<nil>, <nil>)
panic: runtime error: invalid memory address or nil pointer dereference
[signal SIGSEGV: segmentation violation code=0x1 addr=0x0 pc=0x47f7c7]

goroutine 1 [running]:
main.main()
        /home/myao/web-service-gin/main.go:12 +0x67
exit status 2
```

## The empty interface

The interface type that specifies zero methods is known as the *empty interface*:

```
interface{}
```

An empty interface may hold values of any type (every type implements at least zero methods)

Empty interfaces are used by code that handles values of unknown type. For example, `fmt.Print` takes any number of arguments of type `interface{}`

```go
package main

import "fmt"

func main() {
	var i interface{}
	describe(i)
	
	i = 42
	describe(i)
	
	i = "hello"
	describe(i)
}

func describe(i interface{}) {
	fmt.Printf("(%v, %T)\n", i, i)
}
```

**Output**:

```zsh
(<nil>, <nil>)
(42, int)
(hello, string)
```

## Type assertions

A *type assertion* provides access to an interface value's underlying concrete value

```go
t := i.(T)
```

This statement asserts that the interface value `i` holds the concrete type `T` and assigns the underlying `T` value to the variable `t`

If `i` does not hold a `T`, the stmt will trigger a panic

To `test` whether an interface value holds a specific type, a type assertion can return two values: the underlying value and a boolean value that reports whether the assertion succeeded

```go
t, ok := i.(T)
```

If `i` holds a `T`, then `t` will be the underlying value and `ok` will be `true`

If not, `ok` will be `false` and `t` will be the zero value of type `T`, and no panic occurs

Note the similarity btwn this syntax and that of reading from a map

```go
package main

import "fmt"

func main() {
	var i interface{} = "hello"
	
	s := i.(string)
	fmt.Println(s)
	
	s, ok := i.(string)
	fmt.Println(s, ok)
	
	f, ok := i.(float64)
	fmt.Println(f, ok)
	
	f = i.(float64) // panic
	fmt.Println(f)
}
```

**Output**:

```zsh
hello
hello true
0 false
panic: interface conversion: interface {} is string, not float64

goroutine 1 [running]:
main.main()
        /home/myao/web-service-gin/main.go:17 +0x165
exit status 2
```

## Type switches

A *type switch* is a construct that permits several type assertions in series

A type switch is like a regular switch stmt, but the cases in a type switch specify types (not values), and those values are compared against the type of the value held by the given interface value

```go
switch v := i.(type) {
case T:
	// here v has type T
case S:
	// here v has type S
default:
	// no match; here v has the same type as i
}
```

The declaration in a type switch has the same syntax as a type assertion `i.(T)`, but the specific type `T` is replaced w/ the keyword `type`

The switch stmt tests whether the interface value `i` holds a value of type `T` or `S`. In each of the `T` and `S` cases, the variable `v` will be of type `T` or `S` respectively and hold the value held by `i`. In the default case (where there is no match), the variable `v` is of the same interface type and value as `i`

```go
package main

import "fmt"

func do(i interface{}) {
	switch v := i.(type) {
	case int:
		fmt.Printf("Twice %v is %v\n", v, v*2)
	case string:
		fmt.Printf("%q is %v bytes long\n", v, len(v))
	default:
		fmt.Printf("I don't know about type %T!\n", v)
	}
}

func main() {
	do(21)
	do("hello")
	do(true)
}
```

**Output**:

```zsh
Twice 21 is 42
"hello" is 5 bytes long
I don't know about type bool!
```

## Stringers

One of the most ubiquitous interfaces is `Stringer` defined by the `fmt` package

```go
type Stringer interface {
	String() string
}
```

A `Stringer` is a type that can describe itself as a string. The `fmt` package (and many others) look for this interface to print values

```go
package main

import "fmt"

type Person struct {
	Name string
	Age int
}

func (p Person) String() string {
	return fmt.Sprintf("%v (%v years)", p.Name, p.Age)
}

func main() {
	a := Person{"Arthur Dent", 42}
	z := Person{"Zaphod Beeblebrox", 9001}
	fmt.Println(a, z)
}
```

**Output**:

```zsh
Arthur Dent (42 years) Zaphod Beeblebrox (9001 years)
```
