# Employee Leave Request

## Requirements
 - .NET 8 SDK 
 - IDE (Used JetBrains: Rider)
 - SQL Server Management Studio 

## Clone the Project 
 ```
 git clone https://github.com/Merve-1/Employee_Leave_Request.git
 cd EmployeeHR
 ```

## Database 
### Packages
```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet tool install --global dotnet-ef
```
### Configuration 
The application uses SQL Server 
Add this configuration in you appsettings.Development.json 

```
{ 
    "ConnectionStrings": { 
        "LeaveDatabase": "Server=localhost;Database=LeaveRequestsDb;Trusted_Connection=True;TrustServerCertificate=True;" 
    } 
}
```
### EFCore Migration (Create/ Update LeaveRequestsDb)
```
dotnet ef database update
```
![DB Definiation](db.png)
### DB After Seeding
![DB Seeding](db2.png)