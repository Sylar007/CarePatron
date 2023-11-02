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
In the home exercise, I'm have created a backend service (.Net 6 with minimal API architecture) running on http://localhost:5044. This service can accepted a request from UI(http://localhost:3000/) which need to configure, from swagger UI (http://localhost:5044/swagger/index.html) or using PostMan.

The project solution built using a Minimal API approach in organizing its code into projects.
With the  Minimal API approach, its facilitate an API development using compact code syntax and help us develop a lightweight APIs quickly without much complexity.
Apart from that, it also to serve a develop who is coming from different background for e.g backend using Node JS. There are not much things to learn for them to get started. They no need to understand a more rigid architecture setup for e.g MVC and Web API

**How to run test**
- Open command prompt and go to Web project folder "CarePatron.Api"
- Type **dotnet restore**, **dotnet build** and then **dotnet run** 
- Once the project has been intiated, open browser and open this address: http://localhost:5044/swagger/index.html

**Additional Questions**
1. If time wasn't a constraint what else would you have done?
- I have added  Polly Retry mechanism to handle a flaky dependency which we have no control over. It is possible to add polly Circuit-breaker where if something goes wrong, hit the panic button that prevents any further attempts to repeat the operation. This is typically used when we have an extremely unreliable dependency. In this case, we want to stop calling it altogether, as additional attempts to call it might worsen the situation. An example of this might be an overloaded database. And also maybe i can also add more validation for Phone No based on Country Code and Numbers.
  
2. How was this test overall? I.e too hard, too easy, how long it took, etc
- Overall the test is good which candidate need to think about to handle the simulation on external flaky dependency. Also need to put some validation for Email Address which normally i used FluentValidation for Backend services. I took about 30mins for reading and configure in my local setup and then around 4 hours to complete the code. By the way, the information and requirements from the Wiki is easy to understand with a clear instructions.


