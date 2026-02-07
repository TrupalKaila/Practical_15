# Practical-15

## Overview
This repository contains two ASP.NET MVC (Framework 4.7.2) applications that demonstrate authentication and authorization patterns for a practical assignment:

1. **Test 1 (Windows Authentication)** — uses Windows authentication and domain/user-based authorization.  
2. **Test 2 (Forms Authentication)** — uses Forms authentication with a custom role provider backed by database tables.  

Both projects share a similar Employee CRUD feature set and a database model built with Entity Framework Database-First (EDMX).【F:Practical_15_Test1/Web.config†L1-L65】【F:Practical_15Test2/Web.config†L1-L52】【F:Practical_15_Test1/Controllers/EmployeesController.cs†L1-L111】【F:Practical_15Test2/Controllers/EmployeesController.cs†L1-L122】

## Project Structure
- `Practical_15_Test1/` — Windows authentication project.
- `Practical_15Test2/` — Forms authentication project with custom role provider.
- `Practical_15.sln` — Visual Studio solution.

## Features
### Test 1 — Windows Authentication
- Uses Windows authentication and denies anonymous users in `Web.config`.【F:Practical_15_Test1/Web.config†L17-L31】
- Authorization is enforced on controller actions via `[Authorize(Roles = "...")]` with Windows domain accounts (example: `SIMFORM\admin`).【F:Practical_15_Test1/Controllers/EmployeesController.cs†L14-L90】
- Employee CRUD actions are protected by role checks: Admin can create/edit/delete, specific users can view/list.【F:Practical_15_Test1/Controllers/EmployeesController.cs†L14-L103】

### Test 2 — Forms Authentication
- Uses Forms authentication and redirects unauthenticated users to `/Accounts/Login`.【F:Practical_15Test2/Web.config†L17-L31】
- Custom `RoleProvider` loads roles from database tables and is wired up via `roleManager`.【F:Practical_15Test2/Web.config†L25-L36】【F:Practical_15Test2/Models/UserRoleProvider.cs†L1-L60】
- `CustomAuthorizeAttribute` shows a friendly Unauthorized page when authenticated users lack permissions.【F:Practical_15Test2/Filters/CustomAuthorizeAttribute.cs†L1-L28】【F:Practical_15Test2/Views/Shared/Unauthorized.cshtml†L1-L10】
- Employee CRUD permissions by role: 
  - **Admin**: create, edit, delete
  - **Manager**: edit
  - **Employee**: read-only
  【F:Practical_15Test2/Controllers/EmployeesController.cs†L14-L114】

## Database Model
Both projects use the `EmployeeDBContext` connection string and an EDMX model named `EmployeeDataModel`. The database contains (at minimum) the following tables/entities:

- **Employees** — `ID`, `Name`, `Designation`, `Salary` (CRUD target).【F:Practical_15_Test1/Models/Employee.cs†L13-L20】
- **Users** — login identities for Forms authentication.【F:Practical_15Test2/Models/User.cs†L13-L26】
- **RoleMasters** — role definitions (`Admin`, `Manager`, `Employee`).【F:Practical_15Test2/Models/RoleMaster.cs†L13-L25】
- **UserRolesMappings** — link table between users and roles.【F:Practical_15Test2/Models/UserRolesMapping.cs†L13-L23】

The default connection string targets LocalDB:
```
(LocalDB)\MSSQLLocalDB; initial catalog=MVCPractical15; integrated security=True;
```
【F:Practical_15_Test1/Web.config†L64-L64】【F:Practical_15Test2/Web.config†L46-L46】

## Getting Started
### Prerequisites
- Visual Studio with ASP.NET MVC tooling
- .NET Framework 4.7.2
- SQL Server LocalDB (or update the connection string)

### Setup
1. Open `Practical_15.sln` in Visual Studio.
2. Restore NuGet packages.
3. Ensure the database exists and the connection string in each project’s `Web.config` points to the correct SQL Server instance.【F:Practical_15_Test1/Web.config†L64-L64】【F:Practical_15Test2/Web.config†L46-L46】

### Running Test 1 (Windows Authentication)
1. Set `Practical_15_Test1` as the startup project.
2. Make sure Windows Authentication is enabled and Anonymous Authentication is disabled in IIS/IIS Express.
3. Update `[Authorize(Roles = "...")]` attributes to match your local domain users if necessary.【F:Practical_15_Test1/Controllers/EmployeesController.cs†L14-L103】

### Running Test 2 (Forms Authentication)
1. Set `Practical_15Test2` as the startup project.
2. Seed the `Users`, `RoleMasters`, and `UserRolesMappings` tables with credentials and role mappings.
3. Navigate to `/Accounts/Login` and sign in with a seeded user.【F:Practical_15Test2/Controllers/AccountsController.cs†L11-L44】

## Notes
- The Forms authentication role checks are backed by `UserRoleProvider.GetRolesForUser`, which joins Users, RoleMasters, and UserRolesMappings to return the role list for the logged-in user.【F:Practical_15Test2/Models/UserRoleProvider.cs†L26-L43】
- The Windows authentication project uses domain-qualified usernames (e.g., `DOMAIN\user`) for role checks. Update these to match your environment if needed.【F:Practical_15_Test1/Controllers/EmployeesController.cs†L14-L103】
