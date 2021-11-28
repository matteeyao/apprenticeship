# Methods

Go does not have classes. However, you can define methods on types

A method is a function w/ a special *receiver* argument

The receiver appears in its own argument list btwn the `func` keyword and the method name

In this example, the `Abs` method has a receiver of type `Vertex` named `v`

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
	return math.Sqrt(v.X*v.X + v.Y* v.Y)
}

func main() {
	v := Vertex{3, 4}
	fmt.Println(v.Abs())
}
```

**Output**:

```zsh
5
```

## Methods are functions

Remember: a method is just a function w/ a receiver argument

Here's `Abs` written as a regular function w/ no change in functionality

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

func main() {
	v := Vertex{3, 4}
	fmt.Println(Abs(v))
}
```

**Output**:

```zsh
5
```

You can declare a method on non-struct types, too

In this example we see a numeric type `MyFloat` w/ an `Abs` method

You can only declare a method w/ a receiver whose type is defined in the same package as the method. You cannot declare a method w/ a receiver whose type is defined in another package (which includes the built-in types such as `int`)

```go
package main

import (
	"fmt"
	"math"
)

type MyFloat float64

func (f MyFloat) Abs() float64 {
	if f < 0 {
		return float64(-f)
    }
	return float64(f)
}

func main() {
	f := MyFloat(-math.Sqrt2)
	fmt.Println(f.Abs())
}
```

**Output**:

```zsh
1.4142135623730951
```
