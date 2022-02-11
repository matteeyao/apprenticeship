# Command

Command is a behavioral design pattern that turns a request into a stand-alone object that contains all information about the request.

The conversion allows deferred or remote execution of commands, storing command history, etc.

This transformation lets you pass requests as a set of method arguments, delay or queue a request's execution, and support undoable operations.

## Problem

When you implement context menus, shortcuts, and other stuff, you have to either duplicate the operation's code in many classes or make menus dependent on buttons, which is an even worse option.

## Solution

Good software design is often based on the *principle of separation of concerns*, which usually results in breaking an app into layers.

The most common example: a layer fro the graphical user interface and another layer for the business logic.

The GUI layer is responsible for rendering a beautiful picture on the screen, capturing any input and showing results of what the user and the app are doing.

However, when it comes to doing something important, like calculating the trajectory of the moon or composing an annual report, the GUI layer delegates the work to the underlying layer of business logic.

In the code it might look like this: a GUI object calls a method of a business logic object, passing it some arguments. This process is usually described as one object sending another a request.

![The GUI object may access the business logic objects directly.](../img/command-1.png)

The Command pattern suggests that GUI objects shouldn't send these requests directly.

Instead, you should extract all of the request details, such as the object being called, the name of the method and the list of arguments into a separate *command* class w/ a single method that triggers this request.

Command objects serve as links btwn various GUI and business logic objects.

From now on, the GUI object doesn't need to know what business logic object will receive the request and how it'll be processed. The GUI object just triggers the command, which handles all the details.

![Accessing the business logic layer via a command.](../img/command-2.png)

The next step is to make your commands implement the same interface. Usually it has just a single execution method that takes no parameters.

This interface lets you use various commands w/ the same request sender, w/o coupling it to concrete classes of commands.

As a bonus, now you can switch command objects linked to the sender, effectively changing the sender's behavior at runtime.

You might have noticed one missing piece of the puzzle, which is the request parameters.

A GUI object might have supplied the business-layer object w/ some parameters. Since the command execution method doesn't have any parameters, how would we pass the request details to the receiver?

It turns out the command should be either pre-configured w/ this data, or capable of getting it on its own.

![The GUI objects delegate the work to commands.](../img/command-3.png)

Let's get back to our text editor. After we apply the Command pattern, we no longer need all those button subclasses to implement various click behaviors.

It's enough to put a single field into the base `Button` class that stores a reference to a command object and make the button execute that command on a click.

You'll implement a bunch of command classes for every possible operation and link them w/ particular buttons, depending on the buttons' intended behavior.

Other GUI elements, such as menus, shortcuts or entire dialogs, can be implemented in the same way.

They'll be linked to a command which gets executed when a user interacts w/ the GUI element.

As you've probably guessed by now, the elements related to the same operations will be linked to the same commands, preventing any code duplication.

As a result, commands become a convenient middle layer that reduce coupling btwn the GUI and business logic layers.

## Structure

![](../img/command-4.png)

1. The **Sender** class (aka *invoker*) is responsible for initiating requests. This class must have a field for storing a reference to a command object. The sender triggers that command instead of sending the request directly to the receiver. Note that the sender isn't responsible for creating the command object. Usually, it gets a pre-created command from the client via the constructor.

2. The **Command** interface usually declares just a single method for executing the command.

3. **Concrete Commands** implement various kinds of requests. A concrete command isn't supposed to perform the work on its own, but rather to pass the call to one of the business logic objects. However, for the sake of simplifying the code, these classes can be merged.

Parameters required to execute a method on a receiving object can be declared as fields in the concrete command.

You can make command objects immutable by only allowing the initialization of these fields via the constructor.

4. The **Receiver** class contains some business logic. Almost any object may act as a receiver. Most commands only handle the details of how a request is passed to the receiver, while the receiver itself does the actual work.

5. The **Client** creates and configures concrete objects. The client must pass all of the request parameters, including a receiver instance, into the command's constructor. After that, the resulting command may be associated w/ one or multiple senders.

## #Pseudcode

In this example, the *Command* pattern helps to track the history of executed operations and makes it possible to revert an operation if needed.

![Undoable operations in a text editor.](../img/command-5.png)

Commands which result in changing the state of the editor (e.g., cutting and pasting) make a backup copy of the editor’s state before executing an operation associated with the command.

After a command is executed, it’s placed into the command history (a stack of command objects) along with the backup copy of the editor’s state at that point.

Later, if the user needs to revert an operation, the app can take the most recent command from the history, read the associated backup of the editor’s state, and restore it.

The client code (GUI elements, command history, etc.) isn’t coupled to concrete command classes because it works with commands via the command interface.

This approach lets you introduce new commands into the app without breaking any existing code.

```c#
/* The base command class defines the common interface for all
concrete commands. */
abstract class Command is
    protected field app: Application
    protected field editor: Editor
    protected field backup: text
    
    constructor Command(app: Application, editor: Editor) is
        this.app = app
        this.editor = editor
        
    // Make a backup of the editor's state.
    method saveBackup() is
        backup = editor.text
        
    // Restore the editor's state.
    method undo() is
        editor.text = backup
        
    /* The execution method is declared abstract ot force all
    concrete commands to provide their own implementations.
    The method must return true or false depending on whether
    the command changes the editor's state. */
    abstract method execute()
    
// The concrete commands go here.
class CopyCommand extends Command is
    /* The copy command isn't saved to the history since it
    doesn't change the editor's state. */
    method execute() is
        app.clipboard = editor.getSelection()
        return false
        
class CutCommand extends Command is
    /* The cut command does change the editor's state, therfore
    it must be saved to the history. And it'll be saved as
    long as the method returns true. */
    method execute() is
        saveBackup()
        app.clipboard = editor.getSelection()
        editor.deleteSelection()
        return true
        
class PasteCommand extends Command is
    method execute() is
        saveBackup()
        editor.replaceSelection(app.clipboard)
        return true
        
// The undo operation is also a command.
class UndoCommand extends Command is
    method execute() is
        app.undo()
        return false
        
// The global command history is just a stack.
class CommandHistory is
    private field history: array of Command
    
    // Last in...
    method push(c: Command) is
        // Push the command to the end of the history array.
        
    // ...first out
    method pop():Command is
        // Get the most recent command from the history.
        
/* The editor class has actual text editing operations. It plays
the role of a receiver: all commands end up delegating
execution to the editor's methods. */
class Editor is
    field text: string
    
    method getSelection() is
        // Return selected text.
        
    method deleteSelection() is
        // Delete selected text.
        
    method replaceSelection(text) is
        /* Insert the clipboard's contents at the current
        position. */
        
/* The application class sets up object relations. It acts as a
sender: when something needs to be done, it creates a command
object and executes it. */
class Application is
    field clipboard: string
    field editors: array of Editors
    field activeEditor: Editor
    field history: CommandHistory
    
    /* The code which assigns command to UI objects may look
    like this. */
    method createUI() is
        // ...
        copy = function() { executeCommand(
            new CopyCommand(this, activeEditor)) }
        copyButton.setCommand(copy)
        shortcuts.onKeyPress("Ctrl+C", copy)
        
        cut = function() { executeCommand(
            new CutCommand(this, activeEditor)) }
        cutButton.setCommand(cut)
        shortcuts.onKeyPress("Ctrl+X", cut)

        paste = function() { executeCommand(
            new PasteCommand(this, activeEditor)) }
        pasteButton.setCommand(paste)
        shortcuts.onKeyPress("Ctrl+V", paste)

        undo = function() { executeCommand(
            new UndoCommand(this, activeEditor)) }
        undoButton.setCommand(undo)
        shortcuts.onKeyPress("Ctrl+Z", undo)

    /* Execute a command and check whether it has to be added to
    the history. */
    method executeCommand(command) is
        if (command.execute)
            history.push(command)
            
    /* Take the most recent command from the history and run its
    undo method. Note that we don't know the class of that
    command. But we don't have to, since the command knows how
    to undo its own action. */
    method undo() is
        command = history.pop()
        if (command != null)
            command.undo()
```

## Applicability

**Use the Command pattern when you want to parameterize objects w/ operations.**

The Command pattern can turn a specific method call into a stand-alone object. This change opens up a lot of interesting uses: you can pass commands as method arguments, store them inside other objects, switch linked commands at runtime, etc.

Here's an example: you're developing a GUI component such as a context menu, and you want users to be able to configure items that trigger operations when an end user clicks an item.

---

**Use the Command pattern when you want to queue operations, schedule their execution, or execute them remotely.**

As w/ any other object, a command can be serialized, which means converting it to a string that can be easily written to a file or a database.

Later, the string can be restored as the initial command object.

Thus, you can delay and schedule command execution.

In the same way, you can queue, log or send commands over the network.

---

**Use the Command pattern when you want to implement reversible operations.**

Although there are many ways to implement undo/redo, the Command pattern is perhaps the most popular of all.

To be able to revert operations, you need to implement the history of performed operations.

The command history is a stack that contains all executed command objects along w/ related backups of the application's state.

This method has two drawbacks. First, it isn't easy to save an application's state b/c some of it can be private. This problem can be mitigated w/ the **Memento** pattern.

Second, the state backups may consume quite a lot of RAM. Therefore, sometimes you can resort to an alternative implementation: instead of restoring the past state, the command performs the inverse operation. The reverse operation also has a price: it may turn out to be hard or even impposible to implement.

## How to Implement

1. Declare the command interface w/ a single execution method.

2. Start extracting requests into concrete command classes that implement the command interface. Each class must have a set of field for storing the request arguments along w/ a reference to the actual receiver object. All the values must be initialized via the command's constructor.

3. Identify classes that will act as *senders*. Add the fields for storing commands into these classes. Senders should communicate w/ their commands only via the command interface. Senders usually don't create command objects as their own, but rather get them from the client code.

4. Change the senders so they execute the command instead of sending a request to the receiver directly.

5. The client should initialize objects in the following order:

    * Create receivers.
  
    * Create commands, and associate them w/ receivers if needed.

    * Create senders, and associate them w/ specific commands.

## Relations w/ Other Patterns

* **Chain of Responsibility**, **Command**, **Mediator**, and **Observer** address various ways of connecting senders and receivers of requests:

  * *Chain of Responsibility* passes a request sequentially along a dynamic chain of potential receivers until one of them handles it.

  * *Command* establishes unidirectional connections between senders and receivers.

  * *Mediator* eliminates direct connections between senders and receivers, forcing them to communicate indirectly via a mediator object.

  * *Observer* lets receivers dynamically subscribe to and unsubscribe from receiving requests.

* Handlers in **Chain of Responsibility** can be implemented as **Commands**. In this case, you can execute a lot of different operations over the same context object, represented by a request.

However, there's another approach, where the request itself is a *Command* object. In this case, you can execute the same operation in a series of different contexts linked into a chain.

* You can use **Command** and **Memento** together when implementing "undo". In this case, commands are responsible for performing various operations over a target object, while mementos save the state of that object just before a command gets executed.

* **Command** and **Strategy** may look similar b/c you can use both to parameterize an object w/ some action. However, they have very different intents.

  * You can use *Command* to convert any operation into an object. The operation's parameters become fields of that object. The conversion lets you defer execution of the operation, queue it, store the history of commands, send commands to remote services, etc.

  * On the other hand, *Strategy* usually describes different ways of doing the same thing, letting you swap these algorithms within a single conext class.

* **Prototype** can help when you need to save copies of **Commands** into history.

* You can treat **Visitor** as a powerful version of the **Command** pattern. Its objects can execute operations over various objects of different classes.

## Usage of the pattern in C#

**Identification**: The Command pattern is recognizable by behavioral methods in an abstract/interface type (sender) which invokes a method in an implementation of a different abstract/interface type (receiver) which has been encapsulated by the command implementation during its creation.

Command classes are usually limited to specific actions.

## Conceptual Example

This example illustrates the structure of the **Command** design pattern. It focuses on answering these questions:

* What classes does it consist of?

* What roles do these classes play?

* In what way the elements of the pattern are related?

### Program.cs: Conceptual example

```c#
using System;

namespace RefactoringGuru.DesignPatterns.Command.Conceptual
{
    // The command interface declares a method executing a command.
    public interface ICommand
    {
        void Execute();
    }
    
    // Some commands can implement simple operations on their own.
    class SimpleCommand : ICommand
    {
        private string _payload = string.Empty;
        
        public SimpleCommand(string payload)
        {
            this._payload = payload;
        }
        
        public void Execute()
        {
            Console.WriteLine($"SimpleCommand: See, I can do simple things like printing ({this._payload})");
        }
    }
    
    /* However, some commands can delegate more complex operations to other 
    objects, called "receivers." */
    class ComplexCommand : ICommand
    {
        private Receiver _receiver;
        
        // Context data, required for launching the receiver's methods.
        private string _a;
        
        private string _b;
        
        /* Complex commands can accept one or several receiver objects along
        w/ any context data via the constructor. */
        public ComplexCommand(Receiver receiver, string a, string b)
        {
            this._receiver = receiver;
            this._a = a;
            this._b = b;
        }
        
        // Commands can delegate to any methods of a receiver
        public void Execute()
        {
            Console.WriteLine("ComplexCommand: Complex stuff should be done by a receiver object.");
            this._receiver.DoSomething(this._a);
            this._receiver.DoSomethingElse(this._b);
        }
    }
    
    /* The Receiver classes contain some important business logic. They know how
    to perform all kinds of operations, associated w/ carrying out a request.
    In fact, and class may serve as a Receiver. */
    class Receiver
    {
        public void DoSomething(string a)
        {
            Console.WriteLine($"Receiver: Working on ({a}.)");
        }
        
        public void DoSomethingElse(string b)
        {
            Console.WriteLine($"Receiver: Also working on ({b}.)");
        }
    }
    
    /* The Invoker is associated w/ one or several commands. It sends a 
    request to the command. */
    class Invoker
    {
        private ICommand _onStart;
        
        private ICommand _onFinish;
        
        // Initialize commands.
        public void SetOnStart(ICommand command)
        {
            this._onStart = command;
        }
        
        public void SetOnFinish(ICommand command)
        {
            this._onFinish = command;
        }
        
        /* The Invoker does not depend on concrete command or receiver classes.
        The Invoker passes a request to a receiver indirectly, by executing a
        command. */
        public void DoSomethingImportant()
        {
            Console.WriteLine("Invoker: Does anybody want something done before I begin?");
            if (this._onStart is ICommand)
            {
                this._onStart.Execute();
            }
            
            Console.WriteLine("Invoker: ...doing something really important...");
            
            Console.WriteLine("Invoker: Does anybody want something done after I finish?");
            if (this._onFinish is ICommand)
            {
                this._onFinish.Execute();
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // The client code can parameterize an invoker with any commands.
            Invoker invoker = new Invoker();
            invoker.SetOnStart(new SimpleCommand("Say Hi!"));
            Receiver receiver = new Receiver();
            invoker.SetOnFinish(new ComplexCommand(receiver, "Send email", "Save report"));

            invoker.DoSomethingImportant();
        }
    }
}
```

### Output.txt: Execution result

```zsh
Invoker: Does anybody want something done before I begin?
SimpleCommand: See, I can do simple things like printing (Say Hi!)
Invoker: ...doing something really important...
Invoker: Does anybody want something done after I finish?
ComplexCommand: Complex stuff should be done by a receiver object.
Receiver: Working on (Send email.)
Receiver: Also working on (Save report.)
```
