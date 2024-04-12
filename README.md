Web API Project. What does this API do?
-Depends on MSSQL but will be pushed to Azure Cloud.
-Importing data from the database model
-To ensure the application of requests (Ip and Key) limited to 15 minutes
-Generates API key for privacy and adds it to database synchronously (checks for the same key value)
-Sends an email to the user when the key is generated.
-Allows specifying endpoint request duration
-Can also request to pull data from any API server.
