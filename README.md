# Clean Architecture Project

This project is designed following the principles of Clean Architecture, ensuring a modular, maintainable, and testable codebase. Below is the reference structure showing how different layers and projects depend on each other.

## Project References

### Core
The Core layer is split into Application, Domain, and Mapper, each having specific responsibilities and dependencies.

1. **Application**
   - **References**: 
     - **Domain**: Contains essential business logic and domain entities.
   - **Contents**:
     - Bases
     - Behaviors
     - Dtos
     - Features
     - Helpers
     - Interfaces
     - Middlewares

2. **Domain**
   - **Contents**:
     - Common
     - Entities
     - Enums

3. **Mapper**
   - **References**:
     - **Application**: Contains mapping configurations to convert between different layers.
   - **Contents**:
     - AutoMapper profiles and configurations.

### Infrastructure
The Infrastructure layer provides implementations for the interfaces defined in the Core layer, focusing on external services and database interactions.

1. **CleanArchitecture.Infrastructure**
   - **References**:
     - **Application**: Uses application services and interfaces.
     - **Persistence**: Contains database-related services and implementations.
   - **Contents**:
     - Dependencies
     - Mail
     - ScheduleServices
     - Storage
     - Tokens

2. **CleanArchitecture.Persistence**
   - **References**:
     - **Application**: Uses application services and interfaces.
     - **Domain**: Contains domain entities and logic.
   - **Contents**:
     - Dependencies
     - Configuration
     - Context
     - Migrations
     - Repositories
     - UnitOfWorks

### Presentation
The Presentation layer contains the API project, which exposes the application's functionalities via RESTful endpoints.

1. **CleanArchitecture.Api**
   - **References**:
     - **Infrastructure**: Uses infrastructure services and implementations.
     - **Mapper**: Uses mapping configurations.
     - **Persistence**: Uses database-related services and implementations.
   - **Contents**:
     - Connected Services
     - Dependencies
     - Properties
     - Controllers
     - wwwroot
     - appsettings.json
     - Program.cs
## Getting Started

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)


### Running the Application
1. Clone the repository:
   ```sh
   git clone <repository-url>

2. Navigate to the project directory:

   ```sh
   cd <project-directory>

3.Build the solution:

   ```sh
   dotnet restore
   dotnet build
   ```
4.Apply migrations to the database:

   ```sh
   dotnet ef database update  
   ```
5.Run the application:

   ```sh
   dotnet run --project CleanArchitecture.Api ```

   ```
   .................... 
   ```
`Let me know if you need any further changes or additions! `
