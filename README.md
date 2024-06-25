# NetMessaging API with .NET

# The main architectural patterns and styles that guide this solution are

- Port and Adapter Architecture
- CQRS (Command Query Responsibility Segregation)

# Technical specifications:

- Ready to containerize with Docker.
- Entity Framework Core 6
- Generic Repository (very useful with aggregate management)
- Shadow Properties on entities: Properties that are added to domain entities without "poisoning" the entity's own definition in that layer.
- Automatic Domain Services injection using "[DomainService]" annotation.
- MediaTR : register command handlers and queries automatically (via reflection does scan of the assembly)
- Global Exception Handler
- Logs : Console
- Swagger

### Project structure:

Solution for VisualStudio(.sln) composed of the following folders :

- Api : Api Rest, entry point of the application
- Application : Domain Services Orchestration Layer; Ports, Commands, Queries, Handlers
- Infrastructure : Adapters
- Domain : Entities, Value Objects, Ports, Domain Services, Aggregates
- Function : Message reading function

# Screenshots

## 1. Web api running on docker desktop
<img src="https://github.com/klmeir/NetMessaging/blob/main/docs/1.png" />

## 2. Request from postman
<img src="https://github.com/klmeir/NetMessaging/blob/main/docs/2.png" />

## 3. Displaying the message in the queue with Service Bus Explorer
<img src="https://github.com/klmeir/NetMessaging/blob/main/docs/3.png" />

## 4. Visualizing the azure function obtaining the message and storing it in a database in xml format.
<img src="https://github.com/klmeir/NetMessaging/blob/main/docs/4.png" />

## 5. Displaying the message stored in sql server in xml format
<img src="https://github.com/klmeir/NetMessaging/blob/main/docs/5.png" />

## 6. We delete the PaymentResponse table to simulate an error later when saving the message
<img src="https://github.com/klmeir/NetMessaging/blob/main/docs/6.png" />

## 7. We send a new message in postman
<img src="https://github.com/klmeir/NetMessaging/blob/main/docs/7.png" />

## 8. The azure functión cannot store a message and catches exceptions.
<img src="https://github.com/klmeir/NetMessaging/blob/main/docs/8.png" />

## 9. We display the log file with the traces of the error occurred
<img src="https://github.com/klmeir/NetMessaging/blob/main/docs/9.png" />

## 10. Display of message that could not be stored in the queue with Service Bus Explorer
<img src="https://github.com/klmeir/NetMessaging/blob/main/docs/10.png" />

## Build & Run

# Visual Studio 2022

To run the project open the solution in visual studio, check the database connection string and run the script to create tables, sp and test data

# Docker and Docker Compose

The execution of docker compose from visual studio is functional, at the moment we are working to execute it from command line...

In progress...
To startup the whole solution, execute the following command:

```
docker-compose build --no-cache
docker-compose up -d
```

Then the following containers should be running on `docker ps`:

| Application      | URL                                                                                |
| ---------------- | ---------------------------------------------------------------------------------- |
| NetMessaging API | https://localhost:8080                                                             |
| SQL Server       | Server=localhost;User Id=sa;Password=<YourStrong!Passw0rd>;Database=NetMessaging; |


Browse to [http://localhost:8000](http://localhost:8000) and view the swagger documentation
