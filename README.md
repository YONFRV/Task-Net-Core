# REST API to manage a task list in Net Core(To-Do List)
This project is a REST API developed in .NET Core to manage a To-Do List. Each task is made up of a title, a description and a status that can be pending or completed. A MySQL database is used to store and manage tasks. The API allows you to perform the following operations:

## Features
-) Create a new task: You can create a task by specifying its title, description, and status.

-) Get a list of all tasks: This operation gives you a list of all the tasks registered in the database.

-) Get a task by its ID: You can get the details of a specific task by providing its unique ID.

-) Update the status of a task (pending/completed): You can change the status of a task from pending to completed and vice versa.

-)Delete a task by its ID: If you want to delete a task, you can do so by providing its unique ID.

## Requirements
Before you start working with this API, make sure you have the following installed:

-) .NET Core SDK

-) MySQL Server

-) An IDE or code editor (such as Visual Studio Code)

## Setting

Make sure you configure the connection to the MySQL database in the file
'appsettings.json':

    {
      "ConnectionStrings": {
         "CadenaConexionMysql": "server=localhost,xxxx;database=xxx;uid=xx;password=x;"
      },
      "Logging": {
         "LogLevel": {
         "Default": "Information",
         "Microsoft": "Warning",
         "Microsoft.Hosting.Lifetime": "Information"
         }
      },
      "AllowedHosts": "*"
    }
Make sure to replace:

-) xxxx:Port of your MySQL database

-) xxx: Name of your MySQL database

-) xx: User of your MySQL database

-) x:Password of your MySQL database

## Execution
1. Clone this repository to your local machine.
2. Open a terminal in the project folder and run the following command to start the application:

       dotnet run
3. This will start the application at http://localhost:5000 (or whatever port you have configured).
4. Use tools like Postman or curl to interact with the API and perform the operations mentioned above.

## Contributions
If you would like to contribute to this project, we welcome you! Feel free to open issues or submit pull requests.
## License

This project is licensed under the MIT License.