# Maps

A map maps keys to values

The zero value of a map is `nil`. A `nil` map has no keys, nor can keys be added

The `make` function returns a map of the given type, initialized and ready for use

```go
package main

import "fmt"

type Vertex struct {
	Lat, Long float64
}

var m map[string]Vertex

func main() {
	m = make(map[string]Vertex)
	m["Bell Labs"] = Vertex{
		40.68433, -74.39967,
    }
	fmt.Println(m["Bell Labs"])
}
```

**Output**:

```zsh
{40.68433 -74.39967}
```

## Map literals

Map literals are like struct literals, but the keys are required

```go
package main

import "fmt"

type Vertex struct {
	Lat, Long float64
}

var m = map[string]Vertex{
	"Bell Labs": Vertex{
		40.68433, -74.39967,
	},
	"Google": Vertex{
		37.42202, -122.08408,
	},
}

func main() {
	fmt.Println(m)
}
```

**Output**:

```zsh
map[Bell Labs:{40.68433 -74.39967} Google:{37.42202 -122.08408}]
```

If the top-level type is just a type name, you can omit it from the elements of the literal

```go
package main

import "fmt"

type Vertex struct {
	Lat, Long float64
}

var m = map[string]Vertex{
	"Bell Labs": {40.68433, -74.39967},
	"Google":    {37.42202, -122.08408},
}

func main() {
	fmt.Println(m)
}
```

**Output**:

```zsh
map[Bell Labs:{40.68433 -74.39967} Google:{37.42202 -122.08408}]
```

## Mutating Maps

* Insert or update an element in map `m`:

```go
m[key] = elem
```

* Retrieve an element:

```go
elem = m[key]
```

* Delete an element:

```go
delete(m, key)
```

* Test that a key is present w/ a two-value assignment:

```go
elem, ok = m[key]
```

If `key` is in `m`, `ok` is `true`. If not, `ok` is `false`

If `key` is not in the map, then `elem` is the zero value for the map's element type

> [!NOTE]
> If `elem` or `ok` have not yet been declared you could use a short declaration form:

```go
elem, ok := m[key]
```

```go
package main

import "fmt"

func main() {
	m := make(map[string]int)
	
	m["Answer"] = 42
	fmt.Println("The value:", m["Answer"])

	m["Answer"] = 48
	fmt.Println("The value:", m["Answer"])

	delete(m, "Answer")
	fmt.Println("The value:", m["Answer"])
	
	v, ok := m["Answer"]
	fmt.Println("The value:", v, "Present?", ok)
}
```

**Output**:

```zsh
The value: 42
The value: 48
The value: 0
The value: 0 Present? false
```

## Exercise: Maps

Implement `WordCount`. It should return a map of the counts of each "word" in the string `s`. The `wc.Test` function runs a test suite against the provided function and prints success or failure

```go
package main

import (
	"code.google.com/p/go-tour/wc"
	"strings"
)

func WordCount(s string) map[string]int {
	m := make(map[string]int)
	a := strings.Fields(s)
	for _, v := range a {
		m[v]++
	}
	return m
}

func main() {
	wc.Test(WordCount)
}
```
