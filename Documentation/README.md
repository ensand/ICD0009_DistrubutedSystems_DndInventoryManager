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
2. Create domain classes (class libraries are in .NETStandard v2.1)
3.0. Move ApplicationDbContext to a separate project
3. Create new database:
~~~
dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp
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
dotnet aspnet-codegenerator controller -name DndCharactersController            -actions -m DndCharacter            -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name WeaponsController                  -actions -m Weapon                  -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ArmorController                    -actions -m Armor                   -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MagicalItemsController             -actions -m MagicalItem             -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OtherEquipmentsController          -actions -m OtherEquipment          -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CharactersWeaponsController        -actions -m CharactersWeapons       -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CharactersArmorController          -actions -m CharactersArmor         -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CharactersMagicalItemsController   -actions -m CharactersMagicalItems  -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CharactersEquipmentsController     -actions -m CharactersEquipment     -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~

6. REST API controllers: 
~~~
dotnet aspnet-codegenerator controller -name PersonsController          -actions -m Person          -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ContactsController         -actions -m Contact         -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ContactTypesController     -actions -m ContactType     -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
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
1. Pomelo.EntityFrameworkCore.MySql
2. Connection string: "server=alpha.akaver.com;database=student2018_ensand_[database_name];user=student2018;password=student2018"