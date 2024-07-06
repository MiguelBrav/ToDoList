# ToDoList
To Do List using .net 

### ToDoDatabase ###
For this project, we use SQL Server as the database. The database schema includes tables for users, tasks, and language support.

## Usage/Examples for ToDoList.API

This ToDo List API allows users to manage their tasks. It is built with .NET 6 and includes features for user authorization, adding tasks, marking tasks as completed, and deleting tasks,etc.  
The available languages ​​are es-mx (Spanish - Español) and en-us (English - Ingles)
YGO Client
```
curl -X 'GET' \
  'https://todolist.application-service.work/TaskTiers/es-mx' \
  -H 'accept: */*'
```
![App Screenshot](https://res.cloudinary.com/imgresd/image/upload/v1720242430/Github/astygcvwwvmbeh38ezin.png)

### Create an user
```
{
  "email": "test@testuser.com",
  "name": "Miguel S Test",
  "password": "Abc12313451$",
  "gender": 1,
  "birthDate": "1998-11-26"
}
```
You can successfully register, log in, and interact with the ToDo List API using your authentication token.

![App Screenshot](https://res.cloudinary.com/imgresd/image/upload/v1720242462/Github/hrm3rfmypzpivkvieh55.png)

You can consume the API using this URL: "https://todolist.application-service.work/".

For more information, you can check this swagger doc online about the ygo client. [ToDoListApi - Swagger Documentation](https://todolist.application-service.work/swagger/index.html)

## Running Tests

To run tests, run AccountTests.cs from project ToDoList.XUnit

## Feedback

If you have any feedback, please reach out to me.

## Coming Soon
PDF Reports: Generate and download task reports in PDF format.  
More Documentation: Expanded documentation with detailed guides and examples.
