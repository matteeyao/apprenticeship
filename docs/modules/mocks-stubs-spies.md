# The difference between Mocks, Stubs, and Spies

## Mocks aren't Stubs

`Mock` objects encourage testing based on behavior verification

### Regular Tests

Example: We want to take an order object and fill it from a warehouse object. The order is very simple, w/ only one product and a quantity. The warehouse holds inventories of different products. When we ask an order to fill itself from a warehouse there are two possible responses. If there's enough product in the warehouse to fill the order, the order becomes filled and the warehouse's amount of the product is reduced by the appropriate amount. If there isn't enough product in the warehouse then the order isn't filled and nothing happens in the warehouse.

These two behaviors imply a couple of conventional JUnit tests:

```java
public class OrderStateTester extends TestCase {
  private static String TALISKER = "Talisker";
  private static String HIGHLAND_PARK = "Highland Park";
  private Warehouse warehouse = new WarehouseImpl();

  protected void setUp() throws Exception {
    warehouse.add(TALISKER, 50);
    warehouse.add(HIGHLAND_PARK, 25);
  }

  public void testOrderIsFilledIfEnoughInWarehouse() {
    Order order = new Order(TALISKER, 50);
    order.fill(warehouse);
    assertTrue(order.isFilled());
    assertEquals(0, warehouse.getInventory(TALISKER));
  }

  public void testOrderDoesNotRemoveIfNotEnough() {
    Order order = new Order(TALISKER, 51);
    order.fill(warehouse);
    assertFalse(order.isFilled());
    assertEquals(50, warehouse.getInventory(TALISKER));
  }
```

xUnit tests follow a typical four phase sequence: setup, exercise, verify, teardown. In this case the setup phase is done partly in the `setUp` method (setting up the warehouse) and partly in the test method (setting up the order). The call to `order.fill` is the exercise phase. This is where the object is prodded to do the thing that we want to test. The assert statements are then the verification stage, checking to see if the exercised method carried out its task correctly. In this case, there's not explicit teardown phase, the garbage collector does this for us implicitly.

During setup there are two kinds of object that we are putting together. `Order` is the class that we are testing, but for `Order.fill` to work we also need an instance of `Warehouse`. In this situation `Order` is the object that we are focused on testing.

So for this test I need the SUT (`Order`) and one collaborator (`warehouse`). The warehouse is required for two reasons: one is to get the tested behavior to work (since `Order.fill` calls warehouse's methods) and secondly we need it for verification (since once of the results of the of `Order.fill` is a potential change to te state of the warehouse). There is a lot of the distinction between SUT and collaborators-SUT as the "primary object" and collaborators as "secondary objects".

This style of testing uses **state verification**: which means that we determine whether the exercised method worked correctly by examining the state of the SUT and its collaborators after the method was exercised. As we'll see, mock objects enable a different approach to verification.

### Tests w/ Mock Objects

jMock library for defining mocks. jMock is a java mock object library.

```java
public class OrderInteractionTester extends MockObjectTestCase {

  private static String TALISKER = "Talisker";

  public void testFillingRemovesInventoryIfInStock() {
    //setup - data
    Order order = new Order(TALISKER, 50);
    Mock warehouseMock = new Mock(Warehouse.class);
    
    //setup - expectations
    warehouseMock.expects(once()).method("hasInventory")
        .with(eq(TALISKER),eq(50))
        .will(returnValue(true));
    warehouseMock.expects(once()).method("remove")
        .with(eq(TALISKER), eq(50))
        .after("hasInventory");

    //exercise
    order.fill((Warehouse) warehouseMock.proxy());
    
    //verify
    warehouseMock.verify();
    assertTrue(order.isFilled());
  }

  public void testFillingDoesNotRemoveIfNotEnoughInStock() {
    Order order = new Order(TALISKER, 51);    
    Mock warehouse = mock(Warehouse.class);
      
    warehouse.expects(once()).method("hasInventory")
      .withAnyArguments()
      .will(returnValue(false));

    order.fill((Warehouse) warehouse.proxy());

    assertFalse(order.isFilled());
  }
}
```

Concentrate on `testFillingRemovesInventoryIfInStock` first, as I've taken a couple of shortcuts w/ the later test.

To begin w/, the setup phase is very different. For a start, it's divided into two parts: data and expectations. The data part sets up the objects we are interested in working w/, in that sense it's similar to the traditional setup. The difference is in the objects that are created. The SUT is the same - an order. However, the collaborator isn't a warehouse object, instead it's a mock warehouse-technically an instance of the class `Mock`.

The second part of the setup creates expectations on the mock object. The expectations indicate which methods should be called on the mocks when the SUT is exercised.

Once all the expectations are in place I exercise the SUT. After the exercise, I then do verification, which has two aspects. We run asserts against the SUT - much as before. However we also verify the mocks - checking that they were called according to their expectations.

The key difference here is how we verify that the order did the right thing in its interaction w/ the warehouse. W/ state verification, we do this by asserts against the warehouse's state. Mocks use **behavior verification**, where we instead check to see if the order made the correct calls on the warehouse. We do this check by telling the mock what to expect during setup and asking the mock to verify itself during verification. Only the order is checked using asserts, and if the method doesn't change the state of the order, there's no asserts at all.

In the second test we do a couple of different things. Firstly, we create the mock differently, using the `mock` method in `MockObjectTestCase` rather than the constructor. This is a convenience method in the jMock library that means that we don't need to explicitly call verify later on, any mock created w/ the convenience method is automatically verified at the end of the test. We could have done this in the first test too, but we wanted to show the verification more explicitly to show how testing w/ mocks works.

The second different thing in the second case is that we've relaxed the constraints on the expectation by using `withAnyArguments`. The reason for this is that the first test checks that the number is passed to the warehouse, so the second test need not repeat that element of the test. If the logic of the order needs to be changed later, then only one test will fail, easing the effort of migrating the tests. As it turns out I could have left `withAnyArguments` out entirely, as that is the default.

### Using EasyMock

EasyMock also enables behavior verification, but has a couple of differences in style w/ jMock which are worth discussing. Here are the familiar tests again:

```java
public class OrderEasyTester extends TestCase {
  private static String TALISKER = "Talisker";
  
  private MockControl warehouseControl;
  private Warehouse warehouseMock;
  
  public void setUp() {
    warehouseControl = MockControl.createControl(Warehouse.class);
    warehouseMock = (Warehouse) warehouseControl.getMock();    
  }

  public void testFillingRemovesInventoryIfInStock() {
    //setup - data
    Order order = new Order(TALISKER, 50);
    
    //setup - expectations
    warehouseMock.hasInventory(TALISKER, 50);
    warehouseControl.setReturnValue(true);
    warehouseMock.remove(TALISKER, 50);
    warehouseControl.replay();

    //exercise
    order.fill(warehouseMock);
    
    //verify
    warehouseControl.verify();
    assertTrue(order.isFilled());
  }

  public void testFillingDoesNotRemoveIfNotEnoughInStock() {
    Order order = new Order(TALISKER, 51);    

    warehouseMock.hasInventory(TALISKER, 51);
    warehouseControl.setReturnValue(false);
    warehouseControl.replay();

    order.fill((Warehouse) warehouseMock);

    assertFalse(order.isFilled());
    warehouseControl.verify();
  }
}
```

EasyMock uses a record/replay metaphor for setting expectations. For each object you wish to mock you create a control and mock object. The mock satisfied the interface of the secondary object, the control provides additional features. To indicate an expectation you call the method, w/ the arguments you expect on the mock. You follow this w/ a call to the control if you want a return value. Once you've finished setting expectations you call `replay` on the control - at which point the mock finishes the recording and is ready to respond to the primary object. Once done, you can call verify on the control.

The record/replay metaphor has an advantage over the constraints of jMock in that you are making actual method calls to the mock rather than specifying method names in strings. This means you get to use code-completion in your IDE and any refactoring of method names will automatically update the tests. The downside is that you can't have the looser constraints.

### The difference btwn Mocks and Stubs

In the two styles of testing shown above, the first case uses a real warehouse object and the second case uses a mock warehouse, which of course isn't a real warehouse object. Using mocks is one way to not use a real warehouse in the test, but there are other forms of unreal objects used in testing like this.

* **`Test Double`**: generic term for any case where you replace a production object for testing purposes.

  * **`Dummy`**: objects are passed around but never actually used. Usually they are just used to fill parameter lists.

  * **`Fake`**: objects actually have working implementations, but usually take some shortcut which makes them not suitable for production (an in memory database for instance)

  * **`Stubs`**: provide canned answers to calls made during the test, usually not responding at all to anything outside what's programmed in for the test

  * **`Spies`**: are stubs that also record some information based on how they were called. One form of this might be an email service that records how many messages it was sent.

  * **`Mocks`**: objects pre-programmed w/ expectations which form a specification of the calls they are expected to receive

Of these kinds of doubles, only mocks insist upon behavior verification. The other doubles can, and usually do, use state verification. Mocks actually do behave like other doubles during the exercise phase, as they need to make the SUT believe it's talking w/ its real collaborators-but mocks differ in the setup and the verification phases.

A common case for a test double would be if we said that we wanted to send an email message if we failed to fill an order. The problem is that we don't to send actual email messages out to customers during testing. So instead we create a test double for our email system, one that we can control and manipulate.

Here we can begin to see the difference between mocks and stubs. If we were writing a test for this mailing behavior, we might write a simple stub like this.

```java
public interface MailService {
  public void send (Message msg);
}
public class MailServiceStub implements MailService {
  private List<Message> messages = new ArrayList<Message>();
  public void send (Message msg) {
    messages.add(msg);
  }
  public int numberSent() {
    return messages.size();
  }
}                                 
```

We can then use state verification on the stub like so:

```java
class OrderStateTester...

  public void testOrderSendsMailIfUnfilled() {
    Order order = new Order(TALISKER, 51);
    MailServiceStub mailer = new MailServiceStub();
    order.setMailer(mailer);
    order.fill(warehouse);
    assertEquals(1, mailer.numberSent());
  }
```

Of course this is a very simple test - only that a message has been sent. We've not tested it was sent to the right person, or with the right contents, but it will do to illustrate the point.

Using mocks this test would look quite different.

```java
class OrderInteractionTester...

  public void testOrderSendsMailIfUnfilled() {
    Order order = new Order(TALISKER, 51);
    Mock warehouse = mock(Warehouse.class);
    Mock mailer = mock(MailService.class);
    order.setMailer((MailService) mailer.proxy());

    mailer.expects(once()).method("send");
    warehouse.expects(once()).method("hasInventory")
      .withAnyArguments()
      .will(returnValue(false));

    order.fill((Warehouse) warehouse.proxy());
  }
}
```

In both cases, we use a test double instead of the real mail service. There is a difference in that the stub uses state verification while the mock uses behavior verification.

In order to use state verification on the stub, we need to make some extra methods on the stub to help w/ verification. As a result, the stub implements `MailService` but adds extra test methods.

Mock objects always use behavior verification, a stub can go either way. Meszaros refers to stubs that use behavior verification as a Test Spy. The difference is in how exactly the double runs and verifies.

## See the difference w/ RSpec

**Stubs** are useful if some method depends on a third-party API. We can avoid calling the API w/ stubs.

```rb
class SomeClient
  def request
    body = some_lib.request
    JSON.parse body
  end

  private

  def some_lib
    @some_lib || SomeLib.new
  end
end
```

```rb
describe 'SomeClient#request'
  before do
    allow(client).to receive(:some_lib).with(no_args).and_return(some_lib)
    allow(some_lib).to receive(:request).with(no_args).and_return(expected_boby)
  end

  let(:client) { SomeClient.new }
  let(:some_lib) { instance_double(SomeLib) }
  let(:expected_body) { ... }
  let(:expected_json) { JSON.parse expected_body }

  subject { client.request }

  it { is_expected.to eq expected_json }
end
```

**Mocks**: in the above stub example, if the client actually calls the API many times, it's completely unexpected. If we want to ensure whether the `some_lib` method is invoked expectedly, using mocks is more suitable.

```rb
describe `SomeClient#request`
  before { allow(client).to receive(:some_lib).with(no_args).and_return(some_lib) }

  let(:client) { SomeClient.new }
  let(:some_lib) { instance_double(SomeLib) }
  let(:expected_body) { ... }
  let(:expected_json) { JSON.parse expected_body }

  subject { client.request }

  it do
    expect(some_lib).to receive(:request).with(no_args).and_return(expected_body).once # Ensure it's really called expectedly
    is_expected.to eq expected_json
  end
```

If `SomeLib#request` is called unexpectedly (wrong args, called many times, etc.), the test will fail.

**Spies** are difficult to distinguish from mocks

RSpec documentation says:

Message expectations put an example's expectations at the start, before you've invoked the code-under-test. Many developers prefer using an arrange-act-assert (or given-when-then) pattern for structuring tests. Spies are an alternate type of test double that support this pattern by allowing you to expect that a message has been received after the fact, using `have_received`.

W/ spies:

```rb
describe 'SomeClient#request'
  before { allow(client).to receive(:some_lib).with(no_args).and_return(some_lib) }

  let(:client) { SomeClient.new }
  let(:some_lib) { spy(SomeLib) } # It's OK to use 'instance_double' instead
  let(:expected_body) { ... }
  let(:expected_json) { JSON.parse expected_body }

  subject { client.request }

  it do
    is_expected.to eq expected_json
    expect(some_lib).to have_received(:request).with(no_args).and_return(expected_boby).once
  end
end
```

After `subject` is called, the test checks whether `SomeLib#request` is really called expectedly.

`spy(SomeLib)` is similar to `instance_double(SomeLib)`. The difference is:

* `spy`

    * Mix stubs and real objects

    * If some real method is not stubbed, the real one would be called.

* `instance_double`

    * Can stub methods

    * If you try to stub the undefined method, it would raise an error.
    