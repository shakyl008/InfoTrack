run the createDB.sql script
run npm install

connection string in api/secrets.config - the local server will be different for you

Things i ommited from this test to keep the scope manageable:
authentication, rate limiting, retry policies, logging, 
kept the frontend simple, and avoided setting up axios, redux, routes, auth0, etc




Steps I followed:
-step up a stand alone asp.net core api project
-created a simple database
-used scaffold db to create the dbContext and entity framework models
-configured the db context to get the connection string from a file or environment variable
-created the structure for the classes controller/service/data/models/dtos
-wrote a DTOs to separate logic from the EntityFramework models created by scaffold - as the models may be overriten in any future database migration
-added dependency injection (Depdencies.cs)
-wrote the mvp for controller/service/data, manually tested to see if it was working
-got the xpath out google searches and configured htmlagilitypack to accordingly
-loop -> trying to break the functionality, breaking, fixing

-created a simple component to test if axios would get the results correctly
-set up cors
-installed matrial ui and refactored the connection stings to files
-refactored the search component to be more modular

-added SearchEnginesDTO to handle different search engine url
-got bing to work, duckduckgo doesn't respond well to HttpClient

-refactored ui with error displays

-created the getsearchresults endpoint and the corresponding react component
-created a very simple scoring metric -> score = sum(1/x)

-althought not specifically asked for, i thought it would be nice to add some unit tests