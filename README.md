 
# Driver License Issuance and Management System

## Introduction
The Driver License Issuance and Management System is a desktop application built using **C# (Windows Forms)** with **SQL Server**. The system aims to manage and organize the process of issuing, renewing, replacing lost or damaged licenses, as well as managing users and tracking various operations.

## Key Features
- Issue various types of driver licenses.
- Manage requests (First-time issuance, Renewal, Replacement of lost or damaged license, etc.).
- Manage individuals (Add, Edit, Delete).
- Manage tests (Theory, Visual, Practical).
- License reservation and unblocking.
- Track changes and actions within the system with the ability to undo.

## Requirements
- **Visual Studio 2022** or newer.
- **SQL Server 2019** or newer.
- .NET Framework (installed automatically with Visual Studio if not already present).

## Project Setup
### 1. Import the Database
1. Open **SQL Server Management Studio**.
2. Import the database file  `DVLD.bak` in path : "..\DVLD Project Final\Project\Database\DVLD.bak".
3. Ensure the database is connected correctly.

### 2. Set up the Connection String
- Open the connection string settings file (`app.config`) in path : ..\DVLD Project Final\Project\DVLD\App.config.
- Modify the `Connection String` to reflect your SQL Server connection details:
  ```xml
  <connectionStrings>
    <add name="DVLD" connectionString="Server=YOUR_SERVER_NAME;Database=LicenseManagement;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;" providerName="System.Data.SqlClient" />
  </connectionStrings>
  

### 3. Run the Project
1. Open the project in **Visual Studio**  in  path "..\DVLD Project Final\Project\DVLD\DVLD.sln".
2. Restore any missing packages if they are not automatically installed.
3. Run the application using the **Start** button or press `F5`.

## Services Provided by the System
### License Management:
- Issue a first-time license.
- Renew a driver’s license.
- Issue a replacement for lost or damaged licenses.
- Issue an international license.

### Request Management:
- View current requests by status.
- Modify request status.
- Query requests by national ID number.

### Person Management:
- Add a new person.
- Search, edit, and delete.
- Ensure there are no duplicate entries.

### User Management:
- Create system users.
- Assign permissions.
- Manage account status (freeze, delete).

### Process Tracking:
- Record all actions (add, modify, delete).
- Undo changes if necessary.

## Additional Information
- Fees for requests and tests vary depending on the service provided.
- The system automatically verifies the requirements for each license category.
- A manual scheduling system is available for tests.

## Contributing
We welcome any contributions to improve the project! Please follow these steps:
1. Fork the project.
2. Make your changes in a new branch.
3. Create a Pull Request with a clear description of your changes.

## License
This project is open-source and uses the **MIT License**.
```

### Feel free to let me know if you need any modifications or additions! 😊
