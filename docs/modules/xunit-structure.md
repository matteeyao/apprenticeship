* Setup: this where you put the object under test in the necessary state for the behavior you want to check;

* Exercise: when you send a message to your object;

* Verify: here, you should check if the object under test behaved the way you expected;

* Teardown: basically where you clean up stuff in order to get your system back to the initial state.

```rb
describe Stack do
    describe "#push" do
        it "puts an element at the top of the stack" do
            # setup
            stack = Stack.new

            # exercise
            stack.push(1)
            stack.push(2)

            # verify
            expect(stack.top).to eq(2)
        end
    end
end
```

About the comments, no, we don't need them. Let's remove them and keep this structure:

```rb
describe Stack do
  describe "#push" do
    it "puts an element at the top of the stack" do
      stack = Stack.new

      stack.push(1)
      stack.push(2)

      expect(stack.top).to eq(2)
    end
  end
end
```

Above is just the *how*, not the *what*. The *what* is improving test readability. The *how* is: structuring the code based on the xUnit four-phase standard, by adding two line breaks.

Using a standard structure to ease the communication of an idea is not something new.

## Why care about test readability?

Test readability is important in a lot of situations. Like when a test gets red, someone needs to fix it. In order to do that, one needs to understand what the test is about. If the test is well structured and easy to read, they can fix it faster.

Also, if you think about your tests as examples of how to use your code, someone that is trying to use a class that you wrote, can see how it’s done in the tests. The test readability will be equally important here too.
