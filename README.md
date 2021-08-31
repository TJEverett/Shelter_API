# Message Board API

#### _Track Animals in a Shelter, 08/31/2021_

#### By _**Tristen Everett**_

## Description

123456890

### Json Web Tokens (Further Exploration)

* What are Json Web Tokens
   * Json Web Tokens are an encrypted key that the API uses to authenticate users
* Why does this project use Json Web Tokens
   * Prevent unauthorized access to parts of the API
   * Require users to authenticate again every 180 minutes
* When does the Json Web Token need to be given
   * All POST, PUT, and DELETE routes
   * As Described under the [API Endpoints](#api-endpoints) section

## Setup/Installation Requirements

1. Clone this Repo
2. Run `dotnet ef database update` from within /ShelterAPI to create the database
3. You may need to update the file /ShelterAPI/appsettings.json to match the userID and password for the computer you're using
4. Run `dotnet restore` from within /ShelterAPI file location
5. Run `dotnet build` from within /ShelterAPI file location
6. Run `dotnet run` from within /ShelterAPI file location

### Current Bugs and Usage Limitations

* One default user will be added each time the command `dotnet ef database update` is ran. This user should be deleted after a new user is added to the database, or after each time the command needs to be re-run to keep shelter database secure.

### API Endpoints

* `api/login/login` (POST) - returns a Json Web Token for authenticated users
   * Body: `{"username": (string), "password": (string)}`
* `api/login/new` (POST) - adds a new user to the database if no other users with the username exist
   * Header: `Authorization` = `"Bearer (Token string)"`
   * Body: `{"username": (string), "password": (string)}`
* `api/login/delete` (DELETE) - removes a user from the database
   * Header: `Authorization` = `"Bearer (Token string)"`
   * Body: `{"username": (string), "password": (string)}`

* `api/cats` (GET) - returns a list of cats from the database, can be narrowed by using additional route parameters
   * `gender=(string)` - "male" will return only male cats, "female" will only return female cats (not case sensitive)
   * `isKitten=(bool)` - "true" will return cats under 1 year of age, "false" will return cats over 1 year of age
* `api/cats` (POST) - adds a cat to the database if the Token is still valid
   * Header: `Authorization` = `"Bearer (Token string)"`
   * Body: `{"name": (string), "weightkilo": (double), "isfemale": (bool), "birthday": (DateTime), "coloring": (string), "description": (string)}`
* `api/cats/{int id}` (GET) - return a specific cat from the database
* `api/cats/{int id}` (PUT) - update a specific cat from the database if the Token is still valid
   * Header: `Authorization` = `"Bearer (Token string)"`
   * Body: `{"name": (string), "weightkilo": (double), "isfemale": (bool), "birthday": (DateTime), "coloring": (string), "description": (string)}`
* `api/cats/{int id}` (DELETE) - delete a specific cat from the database if the Token is still valid
   * Header: `Authorization` = `"Bearer (Token string)"`

* `api/dogs` (GET) - returns a list of dogs from the database, can be narrowed by using additional route parameters
   * `gender=(string)` - "male" will return only male dogs, "female" will only return female dogs (not case sensitive)
   * `isPuppy=(bool)` - "true" will return dogs under 1 year of age, "false" will return dogs over 1 year of age
* `api/dogs` (POST) - adds a dog to the database if the Token is still valid
   * Header: `Authorization` = `"Bearer (Token string)"`
   * Body: `{"name": (string), "weightkilo": (double), "isfemale": (bool), "birthday": (DateTime), "coloring": (string), "description": (string), "breed": (string)}`
* `api/dogs/{int id}` (GET) - return a specific dog from the database
* `api/dogs/{int id}` (PUT) - update a specific dog in the database if the Token is still valid
   * Header: `Authorization` = `"Bearer (Token string)"`
   * Body: `{"name": (string), "weightkilo": (double), "isfemale": (bool), "birthday": (DateTime), "coloring": (string), "description": (string), "breed": (string)}`
* `api/dogs/{int id}` (DELETE) - delete a specific dog from the database if the Token is still vaid
   * Header: `Authorization` = `"Bearer (Token string)"`

## Technologies Used

* C#
* ASP.NET Core
* Entity Framework Core
* MYSQL

### License

This software is licensed under the MIT license

Copyright (c) 2021 **_Tristen Everett_**