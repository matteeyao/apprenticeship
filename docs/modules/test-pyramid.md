# Practical Test Pyramid

## Integration Tests

Test the integration of your application w/ all the parts that live outside of your application.

For your automated tests this means you don't just need to run your own application but also the component you're integrating with.

If you're testing the integration with a database you need to run a database when running your tests.

For testing that you can read files from a disk you need to save a file to your disk and load it in your integration test.

Together with contract testing and running contract tests against test doubles as well as the real implementations you can come up with integration tests that are faster, more independent and usually easier to reason about.

Narrow integration tests live at the boundary of your service. Conceptually they're always about triggering an action that leads to integrating with the outside part (filesystem, database, separate service).

![Figure 6: A database integration test integrates your code with a real database](../images/db-integration-test.png)

1. start a database

2. connect your application to the database

3. trigger a function within your code that writes data to the database

4. check that the expected data has been written to the database by reading the data from the database

Another example, testing that your service integrates with a separate service via a REST API could look like this:

![Figure 7: This kind of integration test checks that your application can communicate with a separate service correctly](../images/http-integration-test.png)

1. start your application

2. start an instance of the separate service (or a test double with the same interface)

3. trigger a function within your code that reads from the separate service's API

4. check that your application can parse the response correctly

Your integration tests - like unit tests - can be fairly `whitebox`. Some frameworks allow you to start your application while still being able to mock some other parts of your application so that you can check that the correct interactions have happened.

Write integration tests for all pieces of code where you either *serialize* or *deserialize* data. This happens more often than you might think. Think about:

* Calls to your services' REST API

* Reading from and writing to databases

* Calling other application's APIs

* Reading from and writing to queues

* Writing to the filesystem

Writing integration tests around these boundaries ensures that writing data to and reading data from these external collaborators works fine.

When writing *narrow integration tests* you should aim to run your external dependencies locally: spin up a local MySQL database, test against a local ext4 filesystem.

If you're integrating with a separate service either run an instance of that service locally or build and run a fake version that mimics the behavior of the real service.

If there's no way to run a third-party service locally you should opt for running a dedicated test instance and point at this test instance when running your integration tests.

Avoid integrating with the real production system in your automated tests.

Integrating with a service over the network is a typical characteristic of a *broad integration test* and makes your tests slower and usually harder to write.

With regards to the test pyramid, integration tests are on a higher level than your unit tests.

Integrating slow parts like filesystems and databases tends to be much slower than running unit tests with these parts stubbed out.

They can also be harder to write than small and isolated unit tests, after all you have to take care of spinning up an external part as part of your tests.

Still, they have the advantage of giving you the confidence that your application can correctly work with all the external parts it needs to talk to.

## Database Integration

The *PersonRepository* is the only repository class in the codebase. It relies on *Spring Data* and has no actual implementation. 

It just extends the *CrudRepository* interface and provides a single method header. The rest is Spring magic.

```java
public interface PersonRepository extends CrudRepository<Person, String> {
    Optional<Person> findByLastName(String lastName);
}
```

With the `CrudRepository` interface Spring Boot offers a fully functional CRUD repository w/ `findOne`, `findAll`, `save`, `update` and `delete` methods. 

Our custom method definition (`findByLastName()`) extends this basic functionality and gives us a way to fetch `Persons` by their last name.

Spring Data analyses the return type of the method and its method name and checks the method name against a naming convention to figure out what it should do.

Although Spring Data does the heavy lifting of implementing database repositories I still wrote a database integration test.

First it tests that our custom `findByLastName` method actually behaves as expected.

Secondly it proves that our repository used Spring's wiring correctly and can connect to the database.

To make it easier for you to run the tests on your machine (without having to install a PostgreSQL database) our test connects to an in-memory `H2` database.

I've defined `H2` as a test dependency in the `build.gradle` file.

The `application.properties` in the test directory doesn't define any `spring.datasource` properties. This tells Spring Data to use an in-memory database. As it finds `H2` on the classpath it simply uses `H2` when running our tests.

When running the real application with the `int` profile (e.g. by setting `SPRING_PROFILES_ACTIVE=int` as environment variable) it connects to a PostgreSQL database as defined in the `application-int.properties`.

Enough explanation already, here's a simple integration test that saves a Person to the database and finds it by its last name:

```java
@RunWith(SpringRunner.class)
@DataJpaTest
public class PersonRepositoryIntegrationTest {
    @Autowired
    private PersonRepository subject;

    @After
    public void tearDown() throws Exception {
        subject.deleteAll();
    }

    @Test
    public void shouldSaveAndFetchPerson() throws Exception {
        Person peter = new Person("Peter", "Pan");
        subject.save(peter);

        Optional<Person> maybePeter = subject.findByLastName("Pan");

        assertThat(maybePeter, is(Optional.of(peter)));
    }
}
```

## Integration with Separate Services

We want to ensure that our service sends requests and parses the responses correctly from our microservice.

We want to avoid hitting the real servers when running automated tests. The real reason is *decoupling*. Our tests should run independently.

Using [`Wiremock`](http://wiremock.org/), we can avoid hitting the real servers by running our own, fake server while running our integration tests.

```java
@RunWith(SpringRunner.class)
@SpringBootTest
public class WeatherClientIntegrationTest {

    @Autowired
    private WeatherClient subject;

    @Rule
    public WireMockRule wireMockRule = new WireMockRule(8089);

    @Test
    public void shouldCallWeatherService() throws Exception {
        wireMockRule.stubFor(get(urlPathEqualTo("/some-test-api-key/53.5511,9.9937"))
                .willReturn(aResponse()
                        .withBody(FileLoader.read("classpath:weatherApiResponse.json"))
                        .withHeader(CONTENT_TYPE, MediaType.APPLICATION_JSON_VALUE)
                        .withStatus(200)));

        Optional<WeatherResponse> weatherResponse = subject.fetchWeather();

        Optional<WeatherResponse> expectedResponse = Optional.of(new WeatherResponse("Rain"));
        assertThat(weatherResponse, is(expectedResponse));
    }
}
```

To use *Wiremock* we instantiate a `WireMockRule` on a fixed port (8089).

Using the DSL we can set up the Wiremock server, define the endpoints it should listen on and set canned responses it should respond w/.

Next we call the method we want to test, the one that calls the third-party service and check if the result is parsed correctly.

It's important to understand how the test knows that it should call the fake Wiremock server instead of the real API. 

The secret is in our `application.properties` file contained in `src/test/resources`. This is the properties file Spring loads when running tests. In this file we override configuration like API keys and URLs with values that are suitable for our testing purposes, e.g. calling the fake Wiremock server instead of the real one: `weather.url = http://localhost:8089`

Note that the port defined here has to be the same we define when instantiating the `WireMockRule` in our test. Replacing the real weather API's URL with a fake one in our tests is made possible by injecting the URL in our `WeatherClient` class' constructor:

```java
@Autowired
public WeatherClient(final RestTemplate restTemplate,
                     @Value("${weather.url}") final String weatherServiceUrl,
                     @Value("${weather.api_key}") final String weatherServiceApiKey) {
    this.restTemplate = restTemplate;
    this.weatherServiceUrl = weatherServiceUrl;
    this.weatherServiceApiKey = weatherServiceApiKey;
}
```

This way we tell our WeatherClient to read the `weatherUrl` parameter's value from the `weather.url` property we define in our application properties.

Writing *narrow integration tests* for a separate service is quite easy with tools like `Wiremock`. Unfortunately there's a downside to this approach: How can we ensure that the fake server we set up behaves like the real server?

Right now we're merely testing that our `WeatherClient` can parse the responses that the fake server sends. That's a start but it's very brittle.

Using *end-to-end* tests and running the tests against a test instance of the real service instead of using a fake service would solve this problem but would make us reliant on the availability of the test service.

Fortunately, there's a better solution to this dilemma: Running contract tests against the fake and the real server ensures that the fake we use in our integration tests is a faithful test double. 

Let's see how this works next.

## Contract Tests

Splitting your system into many small services often means that these services need to communicate with each other via certain (hopefully well-defined, sometimes accidentally grown) interfaces.

Interfaces between different applications can come in different shapes and technologies. Common ones are

* REST and JSON via HTTPS

* RPC using something like gRPC

* building an event-driven architecture using queues

For each interface there are two parties involved: the **provider** and the **consumer**. The **provider** serves data to consumers. The consumer processes data obtained from a **provider**.

In a REST world a provider builds a REST API with all required endpoints; a consumer makes calls to this REST API to fetch data or trigger changes in the other service.

In an asynchronous, event-driven world, a provider (often rather called **publisher**) publishes data to a queue; a consumer (often called **subscriber**) subscribes to these queues and reads and processes data.

![Figure 8: Each interface has a providing (or publishing) and a consuming (or subscribing) party. The specification of an interface can be considered a contract.](../images/contract-tests.png)

Automated [contact tests](https://martinfowler.com/bliki/ContractTest.html) make sure that the implementations on the consumer and provider side still stick to the defined contract. They serve as a good regression test suite and make sure that deviations from the contract will be noticed early.

**Consumer-Driven Contract tests** (CDC tests) let the [consumers drive the implementation of a contract](https://martinfowler.com/articles/consumerDrivenContracts.html).

Using CDC, consumers of an interface write tests that check the interface for all data they need from that interface. The consuming team then publishes these tests so that the publishing team can fetch and execute these tests easily.

The providing team can now develop their API by running the CDC tests. Once all tests pass they know they have implemented everything the consuming team needs.

![Figure 9: Contract tests ensure that the provider and all consumers of an interface stick to the defined interface contract. With CDC tests consumers of an interface publish their requirements in the form of automated tests; the providers fetch and execute these tests continuously](../images/cdc-tests.png)

The team providing the interface should fetch and run these CDC tests continuously (in their build pipeline) to spot any breaking changes immediately.

If they break the interface their CDC tests will fail, preventing breaking changes to go live.

As long as the tests stay green the team can make any changes they like without having to worry about other teams.

The Consumer-Driven Contract approach would leave you with a process looking like this:

* The consuming team writes automated tests with all consumer expectations

* They publish the tests for the providing team

* The providing team runs the CDC tests continuously and keeps them green

* Both teams talk to each other once the CDC tests break

A naive implementation of CDC tests can be as simple as firing requests against an API and assert that the responses contain everything you need. You then package these tests as an executable (.gem, .jar, .sh) and upload it somewhere the other team can fetch it (e.g. an artifact repository like [Artifactory](https://www.jfrog.com/artifactory/)).

Several tools been build to make writing and exchanging CDC's easier.

[Pact](https://github.com/realestate-com-au/pact) is probably the most prominent one these days. It has a sophisticated approach of writing tests for the consumer and the provider side, gives you stubs for separate services out of the box and allows you to exchange CDC tests with other teams. Pact has been ported to a lot of platforms and can be used with JVM languages, Ruby, .NET, JavaScript and many more.

## Consumer Test (our team)

Our microservice consumes the weather API. So it's our responsibility to write a **consumer test** that defines our expectations for the contract (the API) between our microservice and the weather service.

First we include a library for writing pact consumer tests in our `build.gradle`:

```java
testCompile('au.com.dius:pact-jvm-consumer-junit_2.11:3.5.5')
```

Thanks to this library we can implement a consumer test and use pact's mock services:

```java
@RunWith(SpringRunner.class)
@SpringBootTest
public class WeatherClientConsumerTest {

    @Autowired
    private WeatherClient weatherClient;

    @Rule
    public PactProviderRuleMk2 weatherProvider =
            new PactProviderRuleMk2("weather_provider", "localhost", 8089, this);

    @Pact(consumer="test_consumer")
    public RequestResponsePact createPact(PactDslWithProvider builder) throws IOException {
        return builder
                .given("weather forecast data")
                .uponReceiving("a request for a weather request for Hamburg")
                    .path("/some-test-api-key/53.5511,9.9937")
                    .method("GET")
                .willRespondWith()
                    .status(200)
                    .body(FileLoader.read("classpath:weatherApiResponse.json"),
                            ContentType.APPLICATION_JSON)
                .toPact();
    }

    @Test
    @PactVerification("weather_provider")
    public void shouldFetchWeatherInformation() throws Exception {
        Optional<WeatherResponse> weatherResponse = weatherClient.fetchWeather();
        assertThat(weatherResponse.isPresent(), is(true));
        assertThat(weatherResponse.get().getSummary(), is("Rain"));
    }
}
```

If you look closely, you'll see that the `WeatherClientConsumerTest` is very similar to the `WeatherClientIntegrationTest`. Instead of using Wiremock for the server stub we use Pact this time. In fact the consumer test works exactly as the integration test, we replace the real third-party server with a stub, define the expected response and check that our client can parse the response correctly. In this sense the `WeatherClientConsumerTest` is a narrow integration test itself. The advantage over the wiremock-based test is that this test generates a *pact file* (found in target/pacts/&pact-name>.json) each time it runs. This pact file describes our expectations for the contract in a special JSON format. This pact file can then be used to verify that our stub server behaves like the real server. We can take the pact file and hand it to the team providing the interface. They take this pact file and write a provider test using the expectations defined in there. This way they test if their API fulfils all our expectations.

You see that this is where the *consumer-driven* part of CDC comes from. 

The consumer drives the implementation of the interface by describing their expectations. 

The provider has to make sure that they fulfil all expectations and they're done.

Getting the pact file to the providing team can happen in multiple ways.

A simple one is to check them into version control and tell the provider team to always fetch the latest version of the pact file.

A more advanced one is to use an artifact repository, a service like Amazon's S3 or the pact broker.

Using pact has the benefit that you automatically get a pact file with the expectations to the contract that other teams can use to easily implement their provider tests.

Of course this only makes sense if you can convince the other team to use pact as well. If this doesn't work, using the integration test and Wiremock combination is a decent plan b.

## Provider Test (the other team)

The providing team gets the pact file and runs it against their providing service.

To do so they implement a provider test that reads the pact file, stubs out some test data and runs the expectations defined in the pact file against their service.

The pact folks have written several libraries for implementing provider tests. 

Their main [GitHub repo](https://github.com/DiUS/pact-jvm) gives you a nice overview which consumer and which provider libraries are available. Pick the one that best matches your tech stack.

For simplicity let's assume that the darksky API is implemented in Spring Boot as well.

In this case they could use the [Spring pact provider](https://github.com/DiUS/pact-jvm/tree/master/pact-jvm-provider-spring) which hooks nicely into Spring's MockMVC mechanisms. A hypothetical provider test that the microservice team would implement could look like this:

```java
@RunWith(RestPactRunner.class)
@Provider("weather_provider") // same as the "provider_name" in our clientConsumerTest
@PactFolder("target/pacts") // tells pact where to load the pact files from
public class WeatherProviderTest {
    @InjectMocks
    private ForecastController forecastController = new ForecastController();

    @Mock
    private ForecastService forecastService;

    @TestTarget
    public final MockMvcTarget target = new MockMvcTarget();

    @Before
    public void before() {
        initMocks(this);
        target.setControllers(forecastController);
    }

    @State("weather forecast data") // same as the "given()" in our clientConsumerTest
    public void weatherForecastData() {
        when(forecastService.fetchForecastFor(any(String.class), any(String.class)))
                .thenReturn(weatherForecast("Rain"));
    }
}
```

You see that all the provider test has to do is to load a pact file (e.g. by using the `@PactFolder` annotation to load previously downloaded pact files) and then define how test data for pre-defined states should be provided (e.g. using Mockito mocks).

There's no custom test to be implemented.

These are all derived from the pact file.

It's important that the provider test has matching counterparts to the `provider name` and `state` declared in the consumer test.

## Provider Test (our team)

Consuming teams send us their Pacts that we can use to implement our provider tests for our REST API.

Let's first add the Pact provider library for Spring to our project:

`testCompile('au.com.dius:pact-jvm-provider-spring_2.12:3.5.5')`

Implementing the provider test follows the same pattern as described before.

For the sake of simplicity I simply checked the pact file from our [simple consumer](https://github.com/hamvocke/spring-testing-consumer) into our service's repository.

This makes it easier for our purpose, in a real-life scenario you're probably going to use a more sophisticated mechanism to distribute your pact files.

```java
@RunWith(RestPactRunner.class)
@Provider("person_provider")// same as in the "provider_name" part in our pact file
@PactFolder("target/pacts") // tells pact where to load the pact files from
public class ExampleProviderTest {

    @Mock
    private PersonRepository personRepository;

    @Mock
    private WeatherClient weatherClient;

    private ExampleController exampleController;

    @TestTarget
    public final MockMvcTarget target = new MockMvcTarget();

    @Before
    public void before() {
        initMocks(this);
        exampleController = new ExampleController(personRepository, weatherClient);
        target.setControllers(exampleController);
    }

    @State("person data") // same as the "given()" part in our consumer test
    public void personData() {
        Person peterPan = new Person("Peter", "Pan");
        when(personRepository.findByLastName("Pan")).thenReturn(Optional.of
                (peterPan));
    }
}
```

The shown `ExampleProviderTest` needs to provide state according to the pact file we're given, that's it. 

Once we run the provider test, Pact will pick up the pact file and fire HTTP request against our service that then responds according to the state we've set up.

## UI Tests

*UI tests* test that the user interface of your application works correctly.

User input should trigger the right actions, data should be presented to the user, the UI state should change as expected.

![](../images/ui-tests.png)

Testing your application end-to-end often means driving your tests through the user interface. The inverse, however, is not true.

Testing your user interface doesn't have to be done in an end-to-end fashion.

Depending on the technology you use, testing your user interface can be as simple as writing some unit tests for your frontend javascript code with your backend stubbed out.

With traditional web applications testing the user interface can be achieved with tools like [Selenium](http://docs.seleniumhq.org/).

If you consider a REST API to be your user interface you should have everything you need by writing proper integration tests around your API.

With web interfaces there's multiple aspects that you probably want to test around your UI: behaviour, layout, usability or adherence to your corporate design are only a few.

Fortunately, testing the *behavior* of your user interface is pretty simple.

You click here, enter data there and want the state of the user interface to change accordingly.

If you roll your own frontend implementation using vanilla javascript you can use your regular testing tools like [Jasmine](https://jasmine.github.io/) or [Mocha](http://mochajs.org/). With a more traditional, server-side rendered application, Selenium-based tests will be your best choice.

Testing that your web application's *layout* remains intact is a little harder.

Depending on your application and your users' needs you may want to make sure that code changes don't break the website's layout by accident.

There are some tools to try if you want to automatically check your web application's design in your build pipeline. Most of these tools utilise Selenium to open your web application in different browsers and formats, take screenshots and compare these to previously taken screenshots. If the old and new screenshots differ in an unexpected way, the tool will let you know.

[Galen](http://galenframework.com/) is one of these tools.

Once you want to test for usability and a "looks good" factor you leave the realms of automated testing.

This is the area where you should rely on [exploratory testing](https://en.wikipedia.org/wiki/Exploratory_testing), usability testing (this can even be as simple as [hallway testing](https://en.wikipedia.org/wiki/Usability_testing#Hallway_testing)) and showcases with your users to see if they like using your product and can use all features without getting frustrated or annoyed.

## End-to-End Tests

Testing your deployed application via its user interface is the most end-to-end way you could test your application.

The previously described, webdriver driven UI tests are a good example of end-to-end tests.

![Figure 11: End-to-end tests test your entire, completely integrated system](../images/end-to-end-tests.png)

End-to-end tests (also called [Broad Stack Tests](https://martinfowler.com/bliki/BroadStackTest.html)) give you the biggest confidence when you need to decide if your software is working or not.

[Selenium](http://docs.seleniumhq.org/) and the [WebDriver Protocol](https://www.w3.org/TR/webdriver/) allow you to automate your tests by automatically driving a (headless) browser against your deployed services, performing clicks, entering data and checking the state of your user interface. 

You can use Selenium directly or use tools that are build on top of it, [Nightwatch](http://nightwatchjs.org/) being one of them.

End-to-End tests come with their own kind of problems. They are notoriously flaky and often fail for unexpected and unforeseeable reasons. Quite often their failure is a false positive. The more sophisticated your user interface, the more flaky the tests tend to become. Browser quirks, timing issues, animations and unexpected popup dialogs are only some of the reasons that got me spending more of my time with debugging than I'd like to admit.

In a microservices world there's also the big question of who's in charge of writing these tests. Since they span multiple services (your entire system) there's no single team responsible for writing end-to-end tests.

Due to their high maintenance cost you should aim to reduce the number of end-to-end tests to a bare minimum.

Think about the high-value interactions users will have with your application. Try to come up with user journeys that define the core value of your product and translate the most important steps of these user journeys into automated end-to-end tests.

Remember: you have lots of lower levels in your test pyramid where you already tested all sorts of edge cases and integrations with other parts of the system.

There's no need to repeat these tests on a higher level.

High maintenance effort and lots of false positives will slow you down and cause you to lose trust in your tests, sooner rather than later.

## User Interface End-to-End Test

For end-to-end tests [Selenium](http://docs.seleniumhq.org/) and the [WebDriver](https://www.w3.org/TR/webdriver/) protocol are the tool of choice for many developers.

With Selenium you can pick a browser you like and let it automatically call your website, click here and there, enter data and check that stuff changes in the user interface.

Selenium needs a browser that it can start and use for running its tests. There are multiple so-called *'drivers'* for different browsers that you could use.

[Pick one](https://www.mvnrepository.com/search?q=selenium+driver) (or multiple) and add it to your `build.gradle`.

Whatever browser you choose, you need to make sure that all devs in your team and your CI server have installed the correct version of the browser locally.

For Java, there's a nice little library called [webdrivermanager](https://github.com/bonigarcia/webdrivermanager) that can automate downloading and setting up the correct version of the browser you want to use.

Add these two dependencies to your build.gradle and you're good to go:

`testCompile('org.seleniumhq.selenium:selenium-chrome-driver:2.53.1')`
`testCompile('io.github.bonigarcia:webdrivermanager:1.7.2')`


Running a fully-fledged browser in your test suite can be a hassle. Especially when using continuous delivery the server running your pipeline might not be able to spin up a browser including a user interface (e.g. because there's no X-Server available). You can take a workaround for this problem by starting a virtual X-Server like [xvfb](https://en.wikipedia.org/wiki/Xvfb).

A more recent approach is to use a *headless* browser (i.e. a browser that doesn't have a user interface) to run your webdriver tests - [Chromium](https://developers.google.com/web/updates/2017/04/headless-chrome) for instance.

A simple end-to-end test that fires up Chrome, navigates to our service and checks the content of the website looks like this:

```java
@RunWith(SpringRunner.class)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
public class HelloE2ESeleniumTest {

    private WebDriver driver;

    @LocalServerPort
    private int port;

    @BeforeClass
    public static void setUpClass() throws Exception {
        ChromeDriverManager.getInstance().setup();
    }

    @Before
    public void setUp() throws Exception {
        driver = new ChromeDriver();
    }

    @After
    public void tearDown() {
        driver.close();
    }

    @Test
    public void helloPageHasTextHelloWorld() {
        driver.get(String.format("http://127.0.0.1:%s/hello", port));

        assertThat(driver.findElement(By.tagName("body")).getText(), containsString("Hello World!"));
    }
}
```

Note that this test will only run on your system if you have Chrome installed on the system you run this test on (your local machine, your CI server).

The test is straightforward. It spins up the entire Spring application on a random port using `@SpringBootTest`. We then instantiate a new Chrome webdriver, tell it to go navigate to the `/hello` endpoint of our microservice and check that it prints "Hello World!" on the browser window.

## REST API End-to-End Test

Avoiding a graphical user interface when testing your application can be a good idea to come up with tests that are less flaky than full end-to-end tests while still covering a broad part of your application's stack.

This can come in handy when testing through the web interface of your application is particularly hard.

Maybe you don't even have a web UI but serve a REST API instead (because you have a single page application somewhere talking to that API, or simply because you despise everything that's nice and shiny).

Either way, a [Subcutaneous Test]() that tests just beneath the graphical user interface and can get you really far without compromising on confidence too much.

Just the right thing if you're serving a REST API like we do in our example code:

```java
@RestController
public class ExampleController {
    private final PersonRepository personRepository;

    // shortened for clarity

    @GetMapping("/hello/{lastName}")
    public String hello(@PathVariable final String lastName) {
        Optional<Person> foundPerson = personRepository.findByLastName(lastName);

        return foundPerson
             .map(person -> String.format("Hello %s %s!",
                     person.getFirstName(),
                     person.getLastName()))
             .orElse(String.format("Who is this '%s' you're talking about?",
                     lastName));
    }
}
```

Let me show you one more library that comes in handy when testing a service that provides a REST API.

[REST-assured](https://github.com/rest-assured/rest-assured) is a library that gives you a nice DSL for firing real HTTP requests against an API and evaluating the responses you receive.

First things first: Add the dependency to your `build.gradle`.

`testCompile('io.rest-assured:rest-assured:3.0.3')`

With this library at our hands we can implement an end-to-end test for our REST API:

```java
@RunWith(SpringRunner.class)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
public class HelloE2ERestTest {

    @Autowired
    private PersonRepository personRepository;

    @LocalServerPort
    private int port;

    @After
    public void tearDown() throws Exception {
        personRepository.deleteAll();
    }

    @Test
    public void shouldReturnGreeting() throws Exception {
        Person peter = new Person("Peter", "Pan");
        personRepository.save(peter);

        when()
            .get(String.format("http://localhost:%s/hello/Pan", port))
        .then()
            .statusCode(is(200))
            .body(containsString("Hello Peter Pan!"));
    }
}
```

Again, we start the entire Spring application using `@SpringBootTest`. In this case we `@Autowire` the `PersonRepository` so that we can write test data into our database easily.

When we now ask the REST API to say "hello" to our friend "Mr Pan" we're being presented with a nice greeting. Amazing! And more than enough of an end-to-end test if you don't even sport a web interface.

## Acceptance Tests - Do Your Features Work Correctly?

The higher you move up in your test pyramid the more likely you enter the realms of testing whether the features you're building work correctly from a user's perspective. You can treat your application as a black box and shift the focus in your tests from

```
when I enter the values x and y, the return value should be z
```

towards

```
given there's a logged in user

and there's an article "bicycle"

when the user navigates to the "bicycle" article's detail page

and clicks the "add to basket" button

then the article "bicycle" should be in their shopping basket
```

Sometimes you'll hear the terms [**functional test**](https://en.wikipedia.org/wiki/Functional_testing) or [**acceptance test**](https://en.wikipedia.org/wiki/Acceptance_testing#Acceptance_testing_in_extreme_programming) for these kinds of tests. 

Sometimes people will tell you that functional and acceptance tests are different things. Sometimes the terms are conflated. Sometimes people will argue endlessly about wording and definitions. 

Often this discussion is a pretty big source of confusion.

Here's the thing: At one point you should make sure to test that your software works correctly from a *user*'s perspective, not just from a technical perspective.

BDD tools: [Cucumber](https://cucumber.io/) and [chai.js](http://chaijs.com/guide/styles/#should), which allow you to write assertions with should-style keywords that can make your tests read more BDD-like. And even if you don't use a library that provides this notation, clever and well-factored code will allow you to write user behavior focused tests.

Some helper methods/functions can get you a very long way:

```py
# a sample acceptance test in Python

def test_add_to_basket():
    # given
    user = a_user_with_empty_basket()
    user.login()
    bicycle = article(name="bicycle", price=100)

    # when
    article_page.add_to_.basket(bicycle)

    # then
    assert user.basket.contains(bicycle)
```

Acceptance tests can come in different levels of granularity. 

Most of the time they will be rather high-level and test your service through the user interface. 

However, it's good to understand that there's technically no need to write acceptance tests at the highest level of your test pyramid.

If your application design and your scenario at hand permits that you write an acceptance test at a lower level, go for it.

Having a low-level test is better than having a high-level test.

The concept of acceptance tests - proving that your features work correctly for the user - is completely orthogonal to your test pyramid.

## Putting Tests Into Your Deployment Pipeline

If you're using Continuous Integration or Continuous Delivery, you'll have a [Deployment Pipeline](https://martinfowler.com/bliki/DeploymentPipeline.html) in place that will run automated tests every time you make a change to your software.

Usually this pipeline is split into several stages that gradually give you more confidence that your software is ready to be deployed to production.

To answer this you should just think about one of the very foundational values of Continuous Delivery (indeed one of the core [values of Extreme Programming](http://www.extremeprogramming.org/values.html) and agile software development): **Fast Feedback**.

Put the fast running tests in the earlier stages of your pipeline.

Conversely you put the longer running tests - usually the ones with a broader scope - in the later stages to not defer the feedback from the fast-running tests.

You see that defining the stages of your deployment pipeline is not driven by the types of tests but rather by their speed and scope.

With that in mind it can be a very reasonable decision to put some of the really narrowly-scoped and fast-running integration tests in the same stage as your unit tests - simply b/c they give you faster feedback and not because you want to draw the line along the formal type of your tests.

## Avoid Test Duplication

In the context of implementing your test pyramid you should keep two rules of thumb in mind:

1. If a higher-level test spots an error and there's no lower-level test failing, you need to write a lower-level test

2. Push your tests as far down the test pyramid as you can

The first rule is important because lower-level tests allow you to better narrow down errors and replicate them in an isolated way. They'll run faster and will be less bloated when you're debugging the issue at hand. And they will serve as a good regression test for the future.

The second rule is important to keep your test suite fast. If you have tested all conditions confidently on a lower-level test, there's no need to keep a higher-level test in your test suite.

*If a higher-level test gives you more confidence that your application works correctly, you should have it.*

Writing a unit test for a Controller class helps to test the logic within the Controller itself. Still, this won't tell you whether the REST endpoint this Controller provides actually responds to HTTP requests. So you move up the test pyramid and add a test that checks for exactly that - but nothing more.

You don't test all the conditional logic and edge cases that your lower-level tests already cover in the higher-level test again.

Make sure that the higher-level test focuses on the part that the lower-level tests couldn't cover.

I replace higher-level tests with lower-level tests if possible.

## Writing Clean Test Code

1. Test code is as important as production code. Give it the same level of care and attention. "this is only test code" is not a valid excuse to justify sloppy code

2. Test one condition per test. This helps you to keep your tests short and easy to reason about

3. Readability matters. Don't try to be overly DRY . Duplication is okay, if it improves readability. Try to find a balance between DRY and DAMP code

4. When in doubt use the [Rule of Three](https://blog.codinghorror.com/rule-of-three/) to decide when to refactor. *Use before reuse*

## Conclusion

[sample code](https://github.com/hamvocke/spring-testing)
