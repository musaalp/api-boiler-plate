# Order API
Order API is a sample application build using 
* Asp.Net Core 3.1,
* MySQL
* Dapper 2.0.78
* MediatR

The architecture and design of the project is explained below


## Prerequisites
You will need the following tools:

* [Visual Studio Code or Visual Studio 2019](https://visualstudio.microsoft.com/vs/) (version 16.3 or later)
* [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet/3.1)
* [MySQL](https://www.mysql.com)


## Setup
Follow these steps to get your development environment set up:

## Create Database and Tables
* You should specify mysql server, uid and pwd information in appsettings.json file at OrderDbConnection section.
* At the `/db` directory, there are two sql files. To ceate database and tables execute the `CreateTables.sql` and to fill Products table with sample data execute `InsertProducts.sql` in any executable environment like Workbench, Mysql cli etc.
* A sample request `sample-order-request.json` for create order can be found at root directory. 


## To Make Ready Web API
* At the `src/Api` directory, restore required packages by running:
 
     ```
     dotnet restore
     ```
	 
* Next, build the solution by running:
 
     ```
     dotnet build
     ```	 
	 
* Launch the Web API by running:
 
     ```
     dotnet run
     ```	 
	 
* It should be hosted at http://localhost:5000
* Api documentation can be found at http://localhost:5000/swagger/index.html

### Run Unit Tests

1. To run tests go to each following directories
	
	`test/Data.Tests`
	`test/Service.Tests`

	then execute the line code blow. 

	 ```
	 dotnet test
	 ```


Application mainly consist of 3 layers. 
* **Api Layer** : Responsible to communication with clients.
* **Service Layer** : Basically where all the business rules live, be a mediator between data and api layer.
* **Data Layer** : Responsible to store and present the data.

* **Sdk Nuget Package** : Includes common objects that every microservice may need it. Like exceptions, entities, helper etc.

### Explanation of solutions
Application build based on microservice architecture. It's very performance, easy to extend and lightweight.

HttpGet `OrdersController/{orderId}` end point takes orderId as a parameter from route. 
Controller create an instance of GetOrderByOrderIdServiceRequest and by using IMediatr interface's send metod it dispatch to GetOrderByOrderIdServiceRequestHandler.
Every business logics perform here if everything goes well it communicate with data layer with anohter Mediatr request which is GetOrderByOrderIdDataRequest. This request get handled by GetOrderByOrderIdDataRequestHandler.
Sql queries executes by dapper extension metods as async and this handler returns data to service layer and than api layer.

HttpPost `OrdersController/` end point takes AddOrderModel as a parameter from body. 
Controller create an instance of AddOrderServiceRequest and by using IMediatr interface's send metod it dispatch to AddOrderServiceRequestHandler.
Handler checks required point and than communicate with data layer by using AddOrderDataRequest. AddOrderServiceDataHandler handles this request and execute sql query in a signle transaction by using Dapper.
If everything goes well data layer returns to service layer, service layer returns api layer with expected informations.


#### Used techs and libraries

* Dapper 2.0.78
* FluentValidation 9.5.3
* FluentValidation.AspNetCore 9.5.3
* MediatR 9.0.0
* MediatR.Extensions.Microsoft.DependencyInjection 9.0.0
* Microsoft.Extensions.DependencyInjection 5.0.1
* MySql.Data 8.0.23
* Swashbuckle.AspNetCore 6.1.1
* Microsoft.NET.Test.Sdk 16.5.0
* xunit 2.4.0
* xunit.runner.visualstudio 2.4.0
* coverlet.collector 1.2.0


#### Key Concepts
* **Dapper** : I choose Dapper over Entity Framework or Nhibernate because of performance. Dapper is very usefull and lightwight.
* **Mediatr** : Supports request/response, commands, queries, notifications and events, synchronous and async with intelligent dispatching via C# generic variance.
* **RequestValidationPipelineBehavior** : It's a Mediatr pipeline behavior. It get executed while request is dispatching to handler. If any rule not satisfied then it throw exception.
* **CustomExceptionHandlerMiddleware** :  Global exception handler. It stands top of the Http request pipeline. Everything is operated inside it if any error happen it catch.
* **Sdk** : The intention of this layer is serve it as a nuget package in real life example. Thereby it can be used by other microservices. For simplicity I left it as a class library.
* **Swagger** : For api documentation I used swagger. It can be accessed at http://localhost:5000/swagger/index.html. Default page of api is bring you to swagger page.
* **Dependencies** : Each layer is responsible to register its dependencies by ServiceCollectionExtensions.
