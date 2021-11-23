# Structs

## Pointers

Go has pointers. A pointer holds the memory address of a value

The type `*T` is a pointer to a `T` value. Its zero value is `nil`

```go
var p *int
```

The `&` operator generates a pointer to its operand

```go
i := 42
p = &i
```

The `*` operator denotes the pointer's underlying value:

```go
fmt.Println(*p) // read i through the pointer p
*p = 21         // set i through the pointer p
```

This is known as "dereferencing" or "indirecting"

Unlike C, Go has no pointer arithmetic

```go
package main

import "fmt"

func main() {
	i, j := 42, 2701
	
	p := &i         // point to i
	fmt.Println(*p) // read i through the pointer
	*p = 21         // set i through the pointer
	fmt.Println(i)  // see the new value of i
	
	p = &j          // point to j
	*p = *p / 37    // divide j through the pointer
	fmt.Println(j)  // see the new value of j
}
```

**Output**:

```zsh
42
21
73
```

## Structs

A `stuct` is a collection of fields

```go
package main

import "fmt"

type Vertex struct {
	X int
	Y int
}

func main() {
	fmt.Println(Vertex{1, 2})
}
```

**Output**:

```zsh
{1 2}
```

## Struct Fields

Struct fields are accessed using a dot

```go
package main

import "fmt"

type Vertex struct {
	X int
	Y int
}

func main() {
	v := Vertex{1, 2}
	v.X = 4
	fmt.Println(v.X)
}
```

**Output**:

```zsh
4
```

## Pointers to structs

Struct fields can be accessed through a struct pointer

To access the field `X` of a struct when we have the struct pointer `p` we could write `(*p).X`. However, that notation is cumbersome, so the language permits us instead to write just `p.X`, w/o the explicit dereference

```go
package main

import "fmt"

type Vertex struct {
	X int
	Y int
}

func main() {
	v := Vertex{1, 2}
	p := &v             // point to vertex v
	p.X = 1e9
	fmt.Println(v)
}
```

## Struct Literals

A struct literal denotes a newly allocated struct value by listing the values of its fields

You can list just a subset of fields by using the `Name:` syntax (and the order of named fields is irrelevant)

The special prefix `&` returns a pointer to the struct value

```go
package main

import "fmt"

type Vertex struct {
	X, Y int
}

var (
	v1 = Vertex{1, 2}  // has type Vertex
	v2 = Vertex{X: 1}  // Y:0 is implicit
	v3 = Vertex{}      // X:0 and Y:0
	p  = &Vertex{1, 2} // has type *Vertex
)

func main() {
	fmt.Println(v1, p, v2, v3)
}
```

**Output**:

```zsh
{1 2} &{1 2} {1 0} {0 0}
```
