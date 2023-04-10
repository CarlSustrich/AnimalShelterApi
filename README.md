# Animal Shelter API

#### By **Carl Sustrich**

## Technologies Used

* C#
* .NET
* Entity Framework
* MySQL
* 

## Description

An API serving as a data management system for an imaginary animal shelter, including shelter locations and pets. All CUD functionality is authentication protected. 


## Setup Instructions

1. Ensure your computer has the appropriate software installed:
* System specific .NET SDK found [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* Dotnet ef tool 
  ```
  dotnet tool install --global dotnet-ef --version 6.0.0
  ```

2. Clone this repo.
3. Make a file named 'appsettings.json' within the Project folder directory, and populate it with the following code:
    ```json
    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*",
      "Jwt": {
        "Key": "79CgBk26TjJc2QVP",
        "Issuer": "http://localhost:47185",
        "Audience": "http://localhost:47185"
      },
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Port=3306;database=[DATABASE NAME];uid=[USERNAME];pwd=[PASSWORD];"
      }
    }
    ```
4. Make a file named 'appsettings.Development.json' within the Project folder directory, and populate it with the following code:
    ```json
    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft": "Trace",
          "Microsoft.AspNetCore": "Information",
          "Microsoft.Hosting.Lifetime": "Information"
        }
      }
    }
    ```
5. Run the following, while in the Project Folder:
    ```
    dotnet ef database update
    ```
6. In the command line, run the command "dotnet run" to compile and execute the application. Your default browser should automatically open a tab at the address 'https://localhost:7201/swagger/index.html', but you may do so manually as well.
7. If your connection is refused, you may fix this by either clicking the 'advanced' button that accompanies the refuesed message, then click 'connect anyway', or you can trust the security certificate by running the terminal command 'dotnet dev-certs https --trust'.

## Available Endpoints

*Login:
```
POST http://localhost:7201/api/logins
```

*Shelters:
```
GET http://localhost:7201/api/shelters/
GET http://localhost:7201/api/shelters/{id}
POST http://localhost:7201/api/shelters/
PUT http://localhost:7201/api/shelters/{id}
DELETE http://localhost:7201/api/shelters/{id}
```

*Animals:
```
GET http://localhost:7201/api/animals/  (see note)
GET http://localhost:7201/api/animals/{id}
POST http://localhost:7201/api/animals/
PUT http://localhost:7201/api/animals/{id}
DELETE http://localhost:7201/api/animals/{id}
```
*Users:
```
GET http://localhost:7201/api/shelters/
GET http://localhost:7201/api/shelters/{id}
POST http://localhost:7201/api/shelters/
PUT http://localhost:7201/api/shelters/{id}
DELETE http://localhost:7201/api/shelters/{id}
```
Note: `{id}` is a variable and it should be replaced with the id number of the animal you want to GET, PUT, or DELETE.

#### Optional Query String Parameters for Animals GET Request

GET requests to `http://localhost:7201/api/animals/` can optionally include query strings to filter or search animals.

| Parameter   | Type        |  Required    | Description |
| ----------- | ----------- | -----------  | ----------- |
| name     | String      | not required | Returns animals with a matching name value |
| species        | String      | not required | Returns animals with a matching species value, 'other' returns all non dog/cat options |
| breed  | String      | not required | Returns animals with a matching breed value|
| shelterId     | String      | not required | Returns animals residing at a specific shelter |

You can include multiple query strings by separating them with an `&`:

```
GET http://localhost:7201/api/animals?species=other&shelterId=2
```

#### Additional Requirements for POST Request

When making a POST request, you need to include a **body**. Here's an example body in JSON:

```json
{
  "shelterName": "Portland Humane Society",
  "location": "123 Sample St"
}
```
or 
```json
{
  "animalName": "Lucky",
  "species": "fish",
  "breed": "beta",
  "dateAcquired": "2023-03-31T21:01:05.900Z",
  "shelterId": 1,
}
```

#### Additional Requirements for PUT Request

When making a PUT request, you need to include a **body** that includes the entry's Id property. Ensure the Id matches, between the body and request path. Here's an example body in JSON:

```json
{
  "shelterId": 1,
  "shelterName": "Portland Humane Society",
  "location": "123 Updated St"
}
```

## Instructions for use

Read privilege is accessible to all users. To access any Create, Update or Destroy pathways, a user must log in to authenticate themselves. Currently the program is only set up to use a seeded user log in, not accept new registrations.

To log in:
* Send a post request to the log in endpoint specified above, containing the following JSON format message body:
```json
{
    "username": "athena_admin",
    "password": "1"
}
```

* This method will return a bearer token. To access Create, Update or Destroy pathways, this bearer token will need to be attached to your request as a header. If using Postman, select the authorization tab for your request, then from the dropdown, choose "Bearer Token". The token you recieved from logging in should be entered here. 
* Each token is valid for 60 minutes from the time recieved. 

## Known Bugs

Currently uses an unencrypted username and password. I plan on updating this to include EF Code Identity protection in the future.

## License

MIT License

Copyright (c) [2023] [Carl Sustrich]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
