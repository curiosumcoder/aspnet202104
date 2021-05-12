* dotnet tool install --global dotnet-ef
* dotnet tool update --global dotnet-ef
* dotnet ef dbcontext scaffold "Data Source=.\sqlexpress;Database=Northwind;Integrated Security=SSPI;" Microsoft.EntityFrameworkCore.SqlServer -d -c NWContext
* dotnet add package Microsoft.EntityFrameworkCore.Design
* dotnet add package Microsoft.EntityFrameworkCore.SqlServer
* dotnet add package Microsoft.EntityFrameworkCore.Proxies

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Microsoft.EntityFrameworkCore.Proxies
            optionsBuilder.UseLazyLoadingProxies();

            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Database=Northwind;Integrated Security=SSPI;");
            }
        }

* https://docs.microsoft.com/en-us/ef/core/extensions/
