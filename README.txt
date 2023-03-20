
********************************************************************************

- The user has to manually insert both an ID and a name.
- The user can create Vessels and during the creation he has to assign them
to a Fleet.
- The user can create Containers and during the creation he has to load them
to a Vessel.
- There is no unique constraint on Fleet/Vessel/Container names.
- The CRUD operations which are not required, are not visible, but can still be accessed
from the address bar.
		example: https://localhost:44360/Fleet/Edit/1

---------------------------------------------------------------
IDE:
- Visual Studio community 2022

Project template:
- .NET Framework, MVC, C#

NuGet Packages:
- Entity Framework 

DB:
- SQL Server

schema file: FleetManagement.bacpac
--------------------------------------------------------
Steps:

- Created DB schema in MSSMS
- Connected Visual Studio with local SQL Server
- Generated DBContext, edmx diagram, Models using EF
- Generated Scaffolded Controlers for Fleet, Vessel, Container models
- Added code to satisfy the requirements
-----------------------------------------------------------
Possible improvements:

- User access levels/ roles
- UI
- exception handling
	
