# ASP.NET Core 2 + MongoDB
An example of using the MongoDB database with ASP.NET Core (Web API). 

## Features:

This app can serve as basic backend REST API with common behaviour:
  - get all items (GET: api/[controller]);
  - get an item (GET: api/[controller]/id);
  - create an item (POST: api/[controller]);
  - update an item (PUT: api/[controller]/id) // by replacing item
  - update/modify an item (PATCH: api/[controller]/id) // by patching item
  - delete an item (DELETE: api/[controller]/id)

Connection to MongoDB database implemented as extension method to IServiceCollection (AddMongoDbContext). I like to keep my model classes free from dependencies, so for mapping between models and BSON documents I used BsonClassMap API. It also implemented as extension method (AddBsonMapper).

Access to the database occurs through repository in an asynchronous way.

## Technology stack:
- ASP.NET Core 2.2
- MongoDB Driver 2.7.2
- Microsoft.AspNetCore.JsonPatch v2.2.0 (for implementation HttpPatch method)
