# MultipleUsersWebApp

Data storage using SQL Server Express LocalDB, using a schema of your own design but 
with some requirements:
o Hold user data of first name, last name, age, date, country, province, city 
o Optimization and storage is one key aspect that will be reviewed. Performance is 
very important; what if the DB holds millions of records?
o Preload the database will large volume of data.
o Do not use EF. We want you to build the schema, not Entity Framework.
- An endpoint to create up to 1000 users at a time
- An endpoint to get users with pagination support
o Can filter and get users by a certain age or certain country

Installation
To run this project locally, you will need to follow these steps:

Prerequisites
&bull; .NET 6 SDK
&bull; Visual Studio or Visual Studio Code

## Steps. 
1- Clone the repository to your local machine</br>

2- Configure the database connection in the appsettings.json file to point to your local database server. Update the connection string:</br>
"ConnectionStrings": {
  "DefaultConnection": "YourConnectionString"
}</br>


## BackEnd Side technologies
&bull; C# .NET 6.0 Web API</br>
&bull; ADO.NET</br>
&bull; System.Runtime.Caching 

## Patterns:
&bull; DDD (Domain-Driven Designr)</br>
&bull; Dependency Injection</br>


