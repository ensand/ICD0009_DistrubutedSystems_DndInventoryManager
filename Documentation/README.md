#Distributed Project Building

###Make sure tooling is up to date:
#####.NET sdk
#####dotnet tool [install/update] --global dotnet-aspnet-codegenerator
#####dotnet tool [install/update] --global dotnet-ef

###Create new Solution
ASP.NET Core Web Application;\
Type is Web App (Model - View - Controller);\
Individual authentication.

####In new project:
1. Delete the migrations folder and app.db
2. Create domain classes (class libraries are in .NETStandard v2.1)\
3.0. Move ApplicationDbContext to a separate project
3. Create new database:
~~~
dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp
dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp --context DAL.App.EF.AppDbContext
~~~
4. (Delete the old database and) update the database:
~~~
dotnet ef database drop --project DAL.App.EF --startup-project WebApp
dotnet ef database update --project DAL.App.EF --startup-project WebApp
~~~
5. Create HTML controllers in WEB APP folder to check that the database is okay:
 - needs Microsoft.VisualStudio.Web.CodeGeneration.Design;
 - needs Microsoft.EntityFrameworkCore.Design;
 - needs Microsoft.EntityFrameworkCore.SqlServer;
~~~
dotnet aspnet-codegenerator controller -name DndCharactersController     -actions -m DndCharacter    -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name WeaponsController           -actions -m Weapon          -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ArmorController             -actions -m Armor           -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MagicalItemsController      -actions -m MagicalItem     -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OtherEquipmentsController   -actions -m OtherEquipment  -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~

6. REST API controllers: 
~~~
dotnet aspnet-codegenerator controller -name PersonsController -actions -m Person -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ContactsController -actions -m Contact -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ContactTypesController -actions -m ContactType -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
~~~

7. Generate identity UI:
~~~
dotnet aspnet-codegenerator identity -dc DAL.App.EF.AppDbContext -f
~~~

#
####Packages
1. Min-max values: System.ComponentModel.Annotations;
2. Identity package: Microsoft.Extensions.Identity.Stores\
For making a domain object personal.
~~~
[MaxLength(255)]
public string IdentityUserId { get; set; }
public IdentityUser IdentityUser { get; set; }
~~~
AND on top of controllers:
~~~
[Authorize]
~~~

#
####Database
1. Pomelo.EntityFrameworkCore.MySql\
    Connection string: "server=alpha.akaver.com;database=student2018_ensand_[database_name];user=student2018;password=student2018"
2. MicroSoft SqlServer\
    Microsoft.EntityFrameworkCore.SqlServer\
    Connection string: "Server=alpha.akaver.com,1533;User Id=SA;Password=Admin.TalTech.1;Database=ensand_[database_name];MultipleActiveResultSets=true"

#
####Solution structure by the projects
We want to write our solutions with as much shared code as possible to avoid writing the same code again in the next solution.
So we need to separate our code into 2 parts: current app specific and common shared base.

What can we share?
* Primary key definitions;
* Definition for the base repository - those would repeat from one solution to the next;
* Interfaces (contracts) and their implementations.

Shared and common codebase in this solution:
* Contracts.DAL.Base - specs for domain metadata and PK in entities, specs for common base repository;
* DAL.Base - abstract implementation of interfaces for domain;
* DAL.Base.EF - implementation of the common base repository done in EF.

App specific codebase in this solution:
* Domain - domain objects, what is our business about;
* Contracts.DAL.App - specs for repositories;
* DAL.App.EF - implementation of repositories.