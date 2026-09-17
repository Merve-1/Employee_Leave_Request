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
![DB Definiation](readme-images/01_db.png)
### DB After Seeding
![DB Seeding](readme-images/02_db2.png)

## DB using Script 
You can also create the db schema and seed the initial leave requests using the database/SQLQuery2.sql and will get the same output just change the name of the db in the appsettings.Development.json to the same db name in the script 
```
{ 
     "ConnectionStrings": { 
        "LeaveDatabase": "Server=localhost;Database=leaveRequestScript;Trusted_Connection=True;TrustServerCertificate=True;" 
    } 
}
```
![script](readme-images/06_script.png)
![scriptdb](readme-images/05_scriptdb.png)
## End Points 

### Get Employees
![Get All Employees](readme-images/03_GetAllEmployees.png)
### Employee Search
![Search](readme-images/04_SearchForEmployee.png)
### Get All Leave Requests
![Get All LR](readme-images/07_LR.png)
### Get Leave Request with Filter 
![Filtered LR](readme-images/08_FilteredLR.png)
### Get Leave Request by ID
![LR by ID](readme-images/09_leaveRequestByID.png)
### Create Leave Request 
![Create LeaveRequest](readme-images/10_PostLR.png)
![Create LeaveRequest](readme-images/11_PostLR.png)
![DB After Post Request](readme-images/12_db.png)
### Update Pending status 
![valid update](readme-images/13_validUpdate.png)
![invalid update](readme-images/14_invalidUpdate.png)
### Delete Leave Request
![delete LR](15_DeleteLR.png)