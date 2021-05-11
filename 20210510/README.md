* dotnet tool install --global dotnet-ef
* dotnet tool update --global dotnet-ef
* dotnet ef dbcontext scaffold "Data Source=.\sqlexpress;Database=Northwind;Integrated Security=SSPI;" Microsoft.EntityFrameworkCore.SqlServer -d -c NWContext
* dotnet add package Microsoft.EntityFrameworkCore.Design
* dotnet add package Microsoft.EntityFrameworkCore.SqlServer
* https://www.screencast.com/t/3fw4ij8u6
* https://docs.microsoft.com/en-us/ef/core/extensions/
