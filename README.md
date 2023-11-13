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
