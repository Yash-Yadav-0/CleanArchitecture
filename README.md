# Clean Architecture Project

This project is designed following the principles of Clean Architecture, aiming for a modular, maintainable, and testable codebase. The project is divided into three main layers: Core, Infrastructure, and Presentation.

## Project Structure

### Core
The Core layer contains the essential business logic and is further divided into three main parts:

1. **Application**
   - **Dependencies**
   - **Bases**: Contains base classes and interfaces for the application layer.
        - Example: `BaseException.cs`,`BaseHandler.cs`,`BaseRule.cs`
   - **Behaviors**: Contains Validation Rule : `FluentValidation`
   - **Dtos** : Dtos of required entities for `.Application`
   - **Features**: Contains the features and use cases of the application, organized by domain areas.
        [`Commands`]
        [`Exceptions`]
        [`Queries`]
        [`Rules`]
   - **Helpers** : `EncodeHelper` for Tokens.
   - **Interfaces**: Contains interfaces for services and repositories used in the application layer.
            :`AutoMapper`,`MailService`,`Read and WriteRepository` , `LocalStorage`,
            `TokenService`,`UnitOfWorks`.
   - **Middlewares** : ` Exceptions: [ Middlewares,Models & Configuration ]`
   - **Registration.cs**: Handles the dependency injection registration for the application layer.

2. **Domain**
   - **Dependencies**
   - **Common**: Contains common domain logic and base classes.
     - Example: `EntityBase.cs`
   - **Entities**: Contains domain entities.
   - **Enums**: Contains enumerations used in the domain.
     - Example: `OrderType.cs`

3. **Mapper**
   - Contains mapping profiles and configurations for AutoMapper.

### Infrastructure
The Infrastructure layer contains the implementations of the interfaces defined in the Core layer and is divided into two main parts:

1. **CleanArchitecture.Infrastructure**
   - **Dependencies**
   - **Mail**: Contains mail service implementations.
     - Example: `MailService.cs`
   - **ScheduleServices**
   - **Storage**
     - Example: `LocalStorage.cs`
   - **Tokens**: Contains token service implementations.
     - Example: `TokenService.cs`, `TokenSettings.cs`
   - **InfrastructureRegistration.cs**: Handles the dependency injection registration for the infrastructure layer.

2. **CleanArchitecture.Persistence**
   - **Dependencies**
   - **Configuration**: Contains entity configurations for the database context.
     - Example: `BrandConfiguration.cs`, `CategoryConfiguration.cs`, `etc...`
   - **Context**: Contains the database context class.
   - **Migrations**: Contains database migration files.
     - Example: `20240715040908_initialDb.cs`, `20240717035333_Second_Migration.cs`
   - **Repositories**: Contains repository implementations.
     - Example: `ReadRepository.cs`, `WriteRepository.cs`
   - **UnitOfWorks**: Contains unit of work implementations.
     - Example: `UnitOfWorks.cs`
   - **PersistenceRegistration.cs**: Handles the dependency injection registration for the persistence layer.

### Presentation
The Presentation layer contains the API project which exposes the application functionalities via RESTful endpoints.

1. **CleanArchitecture.Api**

   - **Controllers**: Contains API controllers.
   - **wwwroot**: Contains static files for the web application.
   - **appsettings.json**: Contains configuration settings for the application.
   - **CleanArchitecture.Api.http**: HTTP file for testing the API endpoints.
   - **Program.cs**: Entry point of the application.

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
   dotnet run --project CleanArchitecture.Api 
   ```
``Let me know if you need any further changes or additions! ``
