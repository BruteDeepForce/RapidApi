Web API Project. What does this API do?


-Depends on MSSQL but will be pushed to Azure Cloud.


-Importing data from the database model


-To ensure the application of requests (Ip and Key) limited to 15 minutes. 
Detection with middleware while the request is in the pipeline before it reaches the endpoint.


-Generates the API key in SHA1 cryptography for confidentiality and adds it to the database simultaneously (checking for the same key value)


-Sends an email to the user when the key is generated.


-Allows specifying endpoint request duration


-Can also request to pull data from any API server.
