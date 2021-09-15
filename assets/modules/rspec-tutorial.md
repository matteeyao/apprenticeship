# RSpec Tutorial

## Equality/Identity Matchers

Matchers to test for object or value equality.

| Matcher | Description                              | Example                          |
|---------|------------------------------------------|----------------------------------|
| eq      | Passes when actual == expected           | expect(actual).to eq expected    |
| eql     | Passes when actual.eql?(expected)        | expect(actual).to eql expected   |
| be      | Passes when actual.equal?(expected)      | expect(actual).to be expected    |
| equal   | Also passes when actual.equal?(expected) | expect(actual).to equal expected |

Example:

```rb
describe "An example of the equality Matchers" do 

   it "should show how the equality Matchers work" do 
      a = "test string" 
      b = a 
      
      # The following Expectations will all pass 
      expect(a).to eq "test string" 
      expect(a).to eql "test string" 
      expect(a).to be b 
      expect(a).to equal b 
   end
   
end
```

## Comparison Matchers

Matchers for comparing to values.

| Matcher              | Description                                     | Example                                          |
|----------------------|-------------------------------------------------|--------------------------------------------------|
| >                    | Passes when actual > expected                   | expect(actual).to be > expected                  |
| >=                   | Passes when actual >= expected                  | expect(actual).to be >= expected                 |
| <                    | Passes when actual < expected                   | expect(actual).to be < expected                  |
| <=                   | Passes when actual <= expected                  | expect(actual).to be <= expected                 |
| be_between inclusive | Passes when actual is <= min and >= max         | expect(actual).to be_between(min, max).inclusive |
| be_between exclusive | Passes when actual is < min and > max           | expect(actual).to be_between(min, max).exclusive |
| match                | Passes when actual matches a regular expression | expect(actual).to match(/regex/)                 |

Example:

```rb
describe "An example of the comparison Matchers" do

   it "should show how the comparison Matchers work" do
      a = 1
      b = 2
      c = 3		
      d = 'test string'
      
      # The following Expectations will all pass
      expect(b).to be > a
      expect(a).to be >= a 
      expect(a).to be < b 
      expect(b).to be <= b 
      expect(c).to be_between(1,3).inclusive 
      expect(b).to be_between(1,3).exclusive 
      expect(d).to match /TEST/i 
   end
   
end
```

## Class/Type Matchers

| Matcher        | Description                                                                           | Example                                    |
|----------------|---------------------------------------------------------------------------------------|--------------------------------------------|
| be_instance_of | Passes when actual is an instance of the expected class.                              | expect(actual).to be_instance_of(Expected) |
| be_kind_of     | Passes when actual is an instance of the expected class or any of its parent classes. | expect(actual).to be_kind_of(Expected)     |
| respond_to     | Passes when actual responds to the specified method.                                  | expect(actual).to respond_to(expected)     |

Example:

```rb
describe "An example of the type/class Matchers" do
 
   it "should show how the type/class Matchers work" do
      x = 1 
      y = 3.14 
      z = 'test string' 
      
      # The following Expectations will all pass
      expect(x).to be_instance_of Fixnum 
      expect(y).to be_kind_of Numeric 
      expect(z).to respond_to(:length) 
   end
   
end
```

## True/False/Nil Matchers

Matchers for testing whether a value is true, false, or nil.

| Matcher   | Description                            | Example                     |
|-----------|----------------------------------------|-----------------------------|
| be true   | Passes when actual == true             | expect(actual).to be true   |
| be false  | Passes when actual == false            | expect(actual).to be false  |
| be_truthy | Passes when actual is not false or nil | expect(actual).to be_truthy |
| be_falsey | Passes when actual is false or nil     | expect(actual).to be_falsey |
| be_nil    | Passes when actual is nil              | expect(actual).to be_nil    |

Example:

```rb
describe "An example of the true/false/nil Matchers" do
   it "should show how the true/false/nil Matchers work" do
      x = true 
      y = false 
      z = nil 
      a = "test string" 
      
      # The following Expectations will all pass
      expect(x).to be true 
      expect(y).to be false 
      expect(a).to be_truthy 
      expect(z).to be_falsey 
      expect(z).to be_nil 
   end 
end
```

## Error Matchers

Matchers for testing, when a block of code raises an error.

| Matcher                                  | Description                                                                               | Example                                                   |
|------------------------------------------|-------------------------------------------------------------------------------------------|-----------------------------------------------------------|
| raise_error(ErrorClass)                  | Passes when the block raises an error of type ErrorClass.                                 | expect {block}.to raise_error(ErrorClass)                 |
| raise_error("error message")             | Passes when the block raise an error with the message “error message”.                    | expect {block}.to raise_error(“error message”)            |
| raise_error(ErrorClass, "error message") | Passes when the block raises an error of type ErrorClass with the message “error message” | expect {block}.to raise_error(ErrorClass,“error message”) |

Example:

```rb
describe "An example of the error Matchers" do 
   it "should show how the error Matchers work" do 
      
      # The following Expectations will all pass 
      expect { 1/0 }.to raise_error(ZeroDivisionError)
      expect { 1/0 }.to raise_error("divided by 0") 
      expect { 1/0 }.to raise_error("divided by 0", ZeroDivisionError) 
   end 
end
```

## Test Doubles

A Double is an object which can “stand in” for another object.

Let’s say you are building an application for a school and you have a class representing a classroom of students and another class for students, that is you have a Classroom class and a Student class. You need to write the code for one of the classes first, so let’s say that, start with the Classroom class −

```rb
class ClassRoom 
   def initialize(students) 
      @students = students 
   end 
   
   def list_student_names 
      @students.map(&:name).join(',') 
   end 
end
```

This is a simple class, it has one method list_student_names, which returns a comma delimited string of student names. Now, we want to create tests for this class but how do we do that if we haven’t created the `Student` class yet? We need a test Double.

Also, if we have a “dummy” class that behaves like a `Student` object then our `ClassRoom` tests will not depend on the `Student` class. We call this test isolation.

If our `ClassRoom` tests don’t rely on any other classes, then when a test fails, we can know immediately that there is a bug in our `ClassRoom` class and not some other class.

This is where RSpec Doubles (mocks) become useful. Our `list_student_names` method calls the name method on each `Student` object in its `@students` member variable. Therefore, we need a Double which implements a name method.

Here is the code for `ClassRoom` along with an RSpec Example (test), yet notice that there is no `Student` class defined −

```rb
class ClassRoom 
   def initialize(students) 
      @students = students 
   end
   
   def list_student_names 
      @students.map(&:name).join(',') 
   end 
end

describe ClassRoom do 
   it 'the list_student_names method should work correctly' do 
      student1 = double('student') 
      student2 = double('student') 
      
      allow(student1).to receive(:name) { 'John Smith'} 
      allow(student2).to receive(:name) { 'Jill Smith'} 
      
      cr = ClassRoom.new [student1,student2]
      expect(cr.list_student_names).to eq('John Smith,Jill Smith') 
   end 
end
```

As you can see, using a **test double** allows you to test your code even when it relies on a class that is undefined or unavailable. Also, this means that when there is a test failure, you can tell right away that it’s because of an issue in your class and not a class written by someone else.

## Stubs

In RSpec, a stub is often called a Method Stub, it’s a special type of method that “stands in” for an existing method, or for a method that doesn’t even exist yet.

Here is the code from the section on RSpec Doubles −

```rb
class ClassRoom 
   def initialize(students) 
      @students = students 
   End
   
   def list_student_names 
      @students.map(&:name).join(',') 
   end 
end 

describe ClassRoom do 
   it 'the list_student_names method should work correctly' do 
      student1 = double('student') 
      student2 = double('student') 
      
      allow(student1).to receive(:name) { 'John Smith'}
      allow(student2).to receive(:name) { 'Jill Smith'} 
      
      cr = ClassRoom.new [student1,student2]
      expect(cr.list_student_names).to eq('John Smith,Jill Smith') 
   end 
end
```

In our example, the `allow()` method provides the method stubs that we need to test the `ClassRoom` class. In this case, we need an object that will act just like an instance of the `Student` class, but that class doesn’t actually exist (yet). We know that the `Student` class needs to provide a `name()` method and we use `allow()` to create a method stub for `name()`.

## Hooks

The most common hooks used in RSpec are before and after hooks. They provide a way to define and run the setup and teardown code.

```rb
class SimpleClass 
   attr_accessor :message 
   
   def initialize() 
      puts "\nCreating a new instance of the SimpleClass class" 
      @message = 'howdy' 
   end 
   
   def update_message(new_message) 
      @message = new_message 
   end 
end 

describe SimpleClass do 
   before(:each) do 
      @simple_class = SimpleClass.new 
   end 
   
   it 'should have an initial message' do 
      expect(@simple_class).to_not be_nil
      @simple_class.message = 'Something else. . .' 
   end 
   
   it 'should be able to change its message' do
      @simple_class.update_message('a new message')
      expect(@simple_class.message).to_not be 'howdy' 
   end
end
```

When you pass the `:each` argument, you are instructing the `before` method to run before each example in your Example Group i.e. the two `it` blocks inside the describe block in the code above.

RSpec also has an after hook and both the before and after hooks can take `:all` as an argument. The after hook will run after the specified target. The `:all` target means that the hook will run before/after all of the Examples. Here is a simple example that illustrates when each hook is called.

```rb
describe "Before and after hooks" do 
   before(:each) do 
      puts "Runs before each Example" 
   end 
   
   after(:each) do 
      puts "Runs after each Example" 
   end 
   
   before(:all) do 
      puts "Runs before all Examples" 
   end 
   
   after(:all) do 
      puts "Runs after all Examples"
   end 
   
   it 'is the first Example in this spec file' do 
      puts 'Running the first Example' 
   end 
   
   it 'is the second Example in this spec file' do 
      puts 'Running the second Example' 
   end 
end
```

When you run the above code, you will see this output:

```zsh
Runs before all Examples 
Runs before each Example 
Running the first Example 
Runs after each Example 
.Runs before each Example 
Running the second Example 
Runs after each Example 
.Runs after all Examples
```

## Subjects

Consider this code:

```rb
class Person 
   attr_reader :first_name, :last_name 
   
   def initialize(first_name, last_name) 
      @first_name = first_name 
      @last_name = last_name 
   end 
end 

describe Person do 
   it 'create a new person with a first and last name' do
      person = Person.new 'John', 'Smith'
      
      expect(person).to have_attributes(first_name: 'John') 
      expect(person).to have_attributes(last_name: 'Smith') 
   end 
end
```

It’s actually pretty clear as is, but we could use RSpec’s subject feature to reduce the amount of code in the example. We do that by moving the person object instantiation into the describe line.

```rb
class Person 
   attr_reader :first_name, :last_name 
   
   def initialize(first_name, last_name) 
      @first_name = first_name 
      @last_name = last_name 
   end 
	
end 

describe Person.new 'John', 'Smith' do 
   it { is_expected.to have_attributes(first_name: 'John') } 
   it { is_expected.to have_attributes(last_name: 'Smith') }
end
```
