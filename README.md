# 🐾 Capybara Pet App

Welcome to the Capybara Pet App! This application allows users to adopt and nurture virtual capybara pets, interact with them, and earn achievements through various activities. Built with .NET 8 and following modern architectural patterns, this project provides a robust and maintainable codebase for a fun and engaging virtual pet experience.

## Features

- **User Management**: Secure authentication, profile management, and user settings
- **Virtual Capybaras**: Adopt, name, and customize your capybara pets
- **Interactive Gameplay**:
  - Feed, play with, and care for your capybara
  - Monitor happiness, hunger, and energy levels
  - Watch your capybara grow and develop
- **Virtual Economy**:
  - Earn in-game currency through activities
  - Purchase items, food, and accessories
  - Trade items with other users
- **Achievement System**:
  - Unlock achievements for various milestones
  - Earn rewards and special items
  - Track your progress and compete with friends

## Architecture

This application is built using a combination of modern architectural patterns to ensure scalability, maintainability, and performance.

### Clean Architecture

The solution is structured following Clean Architecture principles, with clear separation of concerns across four main layers:

1. **API Layer** (`CapybaraPetApp.Api`)
   - Handles HTTP requests and responses
   - Implements authentication and authorization
   - Validates input and formats output

2. **Application Layer** (`CapybaraPetApp.Application`)
   - Contains business logic and use cases
   - Implements CQRS pattern
   - Manages transactions and coordinates domain objects

3. **Domain Layer** (`CapybaraPetApp.Domain`)
   - Contains enterprise-wide business rules
   - Defines entities, value objects, aggregates, and domain events
   - Enforces business invariants

4. **Infrastructure Layer** (`CapybaraPetApp.Infrastructure`)
   - Implements data access using Entity Framework Core
   - Handles external services and integrations
   - Manages cross-cutting concerns like logging and caching

### CQRS Implementation

The application implements the Command Query Responsibility Segregation (CQRS) pattern to optimize read and write operations:

- **Commands**: Handle state changes (writes) using Entity Framework Core for transaction management
- **Queries**: Optimized read operations using Dapper for complex queries
- **A similar approach like MediatR** is used to implement the mediator pattern, decoupling commands and queries from their handlers

### Domain-Driven Design (DDD)

Key DDD concepts implemented:
- **Aggregate Roots**: Well-defined boundaries for consistency
- **Value Objects**: Immutable objects representing domain concepts
- **Domain Events**: For side effects and eventual consistency
- **Repositories**: Abstracting persistence concerns
- **Specification Pattern**: For complex querying

### Data Access

- **Entity Framework Core**: Used for write operations and simple queries
- **Dapper**: Used for complex read operations and reporting
- **Migrations**: Code-first database migrations for schema management
- **Repository Pattern**: Abstracting data access details from the domain

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- SQL Server 2019+ (or Azure SQL Database)
- (Optional) Docker and Docker Compose for containerized development
- IDE (Visual Studio 2022, JetBrains Rider, or VS Code with C# extensions)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/dduq/capybara-pet-app.git
   cd capybara-pet-app
   ```

2. Configure the database connection string in `appsettings.json`

3. Apply database migrations:
   ```bash
   dotnet ef database update --project CapybaraPetApp.Infrastructure --startup-project CapybaraPetApp.Api
   ```

4. Run the application:
   ```bash
   dotnet run --project CapybaraPetApp.Api
   ```

## Project Structure

```
CapybaraPetApp/
├── CapybaraPetApp.Api/          # API Controllers and Middleware
├── CapybaraPetApp.Application/  # Application Services, Commands, Queries
├── CapybaraPetApp.Domain/       # Domain Models, Interfaces, Events
├── CapybaraPetApp.Infrastructure/ # Data Access, External Services
└── CapybaraPetApp.Tests/        # Unit and Integration Tests
```

## API Documentation

Once the application is running, you can access the Swagger UI at `https://localhost:5001/swagger` for interactive API documentation and testing.

## License

This project is licensed under the GNU Affero General Public License v3.0 - see the [LICENSE](LICENSE) file for details. This license ensures that any modifications or distributions of this software must also be open source and available under the same license.

## Acknowledgments

- Inspired by virtual pet games and the adorable nature of capybaras
- Built with .NET 8 and modern C# features
- Employs Clean Architecture and DDD principles for maintainability

1. Clone the repository:

   ```bash
   git clone https://github.com/dduq/capybara-pet-app.git
   cd capybara-pet-app
