# Duck Typing

## Dynamic vs Static Languages

"...languages use the term "type" to describe the category of the contents of a variable... knowledge of the category of contents of a variable or its type allows an application to have an expectation about how those contents will behave."

*Statically typed languages such as Java type-check before run time*.

*Dynamically typed languages such as Ruby type-check during runtime*.

## Polymorphism

Static Polymorphism aka compile time polymorphism in languages such as Java have interfaces that have special contracts on how to interact with other classes.

One implementation of polymorphism is called method overloading which allows a class to have more than one method with the same name.

Another implementation is called method overriding, which allows a method declaration in a sub class contained inside a parent class.

Methods in static languages only work correctly if they know the classes and types of their arguments.

Dynamic Polymorphism aka runtime polymorphism in languages such as ruby refer to the ability of different objects to respond to the same message signature (method name and specified parameters).

One example of dynamic polymorphism in an object oriented language is called "duck typing."

## Duck Typing

Duck typing is a programming method that allows any object to be passed into a method that has the method signatures expected in order to run.

Not important what an object *is*, rather what it *does*.

Objects of different types can respond to the same messages as long as they have the specific method behavior.

Relying on public interfaces rather than object types allows for flexibility and change over time.

```rb
class Cook
    def prepare_recipe(recipes)
        recipes.each do |recipe|
            case recipe
            when Pizza
                recipe.sprinkle_cheese(self)
            when Macaroni
                recipe.stir_cheese(self)
            when GrilledCheese
                recipe.melt_cheese(self)
            end
        end
    end
end
```

```rb
class Cook
    def prepare(recipes)
        recipes.each{ |recipe|
            recipe.prepare_recipe(self) }
    end
end

class Pizza
    def prepare_recipe(recipe)
        puts "spread the sauce"
        puts "sprinkle the cheese"
    end
end

class Macaroni
    def prepare_recipe(recipe)
        puts "cook the noodles"
        puts "stir in the cheese"
    end
end

class GrilledCheese
    def prepare_recipe(recipe)
        puts "grill the bread"
        puts "melt the cheese"
    end
end
```

TDD Implementation

```rb
class FakeConsole
    arrt_reader :last_string_printed
    
    def stub_get_input(input)
        @input = input
    end
    
    def get_input
        @input
    end
    
    def print_message(message)
        @last_string_printed = message
    end
end

describe PrimeFactorsUnner do
    describe 'generate_primes' do
        it 'returns a list of primes for a given input' do
            console = FakeConsole.new
            console.stub_get_input("8")
            runner = PrimeFactorsRunner.new(console)
            
            primes = runner.generate_primes
            
            expect(primes).to eq([2, 2, 2])
        end
    end
end
```
Instead of relying on user input in the console I added a Console class and a FakeConsole class.

Each class contained the same methods signatures so they would behave the same when testing the `generate_primes` method.
