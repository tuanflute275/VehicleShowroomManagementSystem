# VehicleShowroomManagementSystem

## Introduction

**VehicleShowroomManagementSystem** is a comprehensive system designed to help manage vehicle details, customer information, orders, and processes in a vehicle showroom. It includes functionalities such as inventory management, sales orders, customer data management, and reporting. This system is ideal for car dealerships looking to streamline their operations.

## Installation

### Prerequisites

Before you start installing and running the application, make sure you have the following installed on your machine:

- **.NET 8 SDK**: The system is built on .NET 8. You can download it from [dotnet.microsoft.com](https://dotnet.microsoft.com/download).
- **SQL Server 2019**: The database backend for storing the showroom data. You can download it from [Microsoft](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
- **Visual Studio Code 2022**: This is used for editing and running the application. You can download it from [here](https://code.visualstudio.com/).
- **SQL Server Management Studio (SSMS)** or any other SQL client to manage your database.

### Steps to Install and Run the Application

1. **Clone the Repository**

   First, clone the repository to your local machine. Open your terminal or Git Bash and run:

   ```bash
   git clone https://github.com/tuanflute275/VehicleShowroomManagementSystem.git
   
2.Open the Project in Visual Studio Code

	Open Visual Studio Code. Navigate to File > Open Folder, and select the folder where you cloned the repository.

3.Install Required Extensions in Visual Studio Code

	Ensure you have the following extensions installed:

	C# by Microsoft.
	C# Extensions.
	SQL Server (mssql) for managing SQL Server connections.

4.Configure Database Connection

	Open the ``appsettings.json`` file located in the root of the project and configure the database connection string. Here's an example:

	json
	Copy code
	"ConnectionStrings": {
	  "DefaultConnection": "Server=localhost;Database=VehicleShowroomDB;User Id=yourusername;Password=yourpassword;"
	}
	
5. Run the Application
	
	Ctrl + F5
	
6. Access the Application

Once the application is running, open your browser and navigate to:

	http://localhost:5095