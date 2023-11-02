# **Home Exercise - CarePatron API**
The goal of this exercise is to write a backend service app with below requirements
## 1. Create a client
All fields are required
Send an email and sync documents after a client is created (using the mock repositories provided)
## 2. Edit a client
All fields are required
If the email has changed, send an email and sync documents after a client is updated (using the mock repositories provided)
## 3. Search for a client
Searching in the "search field" should filter the list of clients by their first or last name
Example: John Stevens and Steven Smith should both show up if a user searches "steven"
Example: John Stevens should show up if a user searches "john"

## Name: Mohd Diah A.Karim
In the home exercise, I'm have created a backend service (.Net 6 with minimal API architecture) running on http://localhost:5044. This service can accept a request from UI(http://localhost:3000/) which need to configure, from swagger UI (http://localhost:5044/swagger/index.html) or using PostMan.
![image](https://github.com/Sylar007/CarePatron/assets/5510879/0d5bfa5d-0d81-422f-a0be-7871ae97f07e)

The project solution built using a Minimal API approach in organizing its code into projects.
With the  Minimal API approach, its facilitate an API development using compact code syntax and help us develop a lightweight APIs quickly without much complexity.
Apart from that, it also to really help a developer who is coming from different background for e.g backend using Node JS. There are not much things to learn for them to get started. They no need to understand a more rigid architecture setup for e.g MVC, Blazor and Web API

**How to run test**
- Open command prompt and go to Web project folder "CarePatron.Api"
- Type **dotnet restore**, **dotnet build** and then **dotnet run** 
- Once the project has been intiated, open browser and open this address: http://localhost:5044/swagger/index.html

**Additional Questions**
1. If time wasn't a constraint what else would you have done?
- I have added  Polly Retry mechanism to handle a flaky dependency which we have no control over. It is possible to add polly Circuit-breaker where if something goes wrong, hit the panic button that prevents any further attempts to repeat the operation. This is typically used when we have an extremely unreliable dependency. In this case, we want to stop calling it altogether, as additional attempts to call it might worsen the situation. An example of this might be an overloaded database. And also maybe i can also add more validation for Phone No based on Country Code and Numbers. Apart from that maybe can add some authentication and authorisation to determine who can access the API and execute based on their permission levels (read, write)
  
2. How was this test overall? I.e too hard, too easy, how long it took, etc
- Overall the test is good which candidate need to think about to handle the simulation on external flaky dependency. Also need to put some validation for Email Address which normally i used FluentValidation for Backend services. I took about 30mins for reading and configure in my local setup and then around 4 hours to complete the code. By the way, the information and requirements from the Wiki is easy to understand with a clear instructions.

**Extras**
1. Quality and best practices
- To maintain a clean code which is really important for easy future enhancement and our code not easily to break i'm usually will follow some standard practise such as SOLID principles, Design patterns and Dry. Apart from that in good project implementation we do need a BDD (i'm familiar with SpecFlow and Cucumber) so that we can automate the testing from CICD pipeline. Therefore we can mnimise the effort from QA. Usually i will create a Unit Test and Integration Test for the project but i'm not sure if it's part of the requirement for this home exercise.
2. Can this submission's code architecture easily scale to a codebase with 20 developers?
- Yes, definitely. The one thing that i'm handling is spliting the Routing from the program.cs to specific Folder (Endpoint). So if Developer wants to add a new APIs related to different models, they can simply create a new endpoint file. Apart from that i'm also returning a standard Response object so it easier for Front End to see the Result(Data), Status Code, An array of error messages and IsSuccess flag. 
3. How can you ensure data integrity in case of failures?
- For ensuring data integrity we can utilise a key feature from Entity Framework which is transaction. A transaction is a sequence of one or more database operations (such as insert, update, delete or select) that are executed as a single unit of work. Transactions allow multiple users to work on a database concurrently without corrupting the data or causing inconsistency. This is really going to help Developer to implement, handling errors and rollbacks, transaction isolation levels, and performance considerations. 
4. How can you ensure the API behaves as you intend it to?
- For ensuring the API behaves, we need to add more control on exception handling (argument exception, nullreferenceException, argumentOutOfRange ans so on). We also can return a relevant status code according to the positive and negative flow. For example status code 200, 400, 401, 403, 500 and etc) 
5. How can you improve the performance of this?
- Usually what my team did before is do a benchmarking. A benchmark is a measurement or a set of measurements related to the performance of a piece of code in an application. We use BenchmarkDotNet library where we can determine the IO, Memory usage and Garbage collector utilisation. And for Database performance we usually have as session using SQL Profiler to configure, run and observe all query to Database. After the findings, we going to do some DB optimising such re-indexing, partition tables and so on. Also to determine our app capacity we need to run a load testing. I have an experience creating JMeter scripts to simulate the test so that we can determine what's throughput before our app performance will start to degraded
