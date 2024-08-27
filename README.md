# BookManagementAPI
The Book Management project is structured using the principles of Clean Architecture, which ensures that the system is both flexible and maintainable. This project provides the users to add details of Books, Publisher, Category and Author and store the data in MYSQL server database. Here’s a brief description:

## 1. Clean Architecture Layers:

### Domain Layer:
- Contains the core business logic and entities like Book, Author, Publisher, and Category.
- Independent of any external frameworks or technology specifics.

### Application Layer:
- Houses the service interfaces and use cases that define the business processes.
- This layer orchestrates how data flows between entities, repositories, and other services.

### Infrastructure Layer:
- Deals with the actual implementation of external dependencies like databases, file systems, or external APIs.
- Contains the DbContext for interacting with the database using Entity Framework Core, repositories, and data mapping configurations.

### API Layer:
- The entry point for the application, exposing endpoints for managing books, authors, publishers, and categories.
- This layer handles HTTP requests and responses, interacting with the Application layer to perform operations.

## 2. Project Components:

### Entity Models:
- Book, Author, Publisher, and Category are represented as entity models, defining their attributes and relationships.

### Repositories:
- Repositories are used to abstract data access and provide a clean way to interact with the data source. Each entity typically has a corresponding repository.

### Services:
- Services are implemented in the Application layer to encapsulate business logic and interact with repositories.

### API Endpoints:
- CRUD operations (Create, Read, Update, Delete) are exposed through API controllers, allowing external clients to manage books, authors, publishers, and categories.

## 3. Configuration Files:
- appsettings.json:
Stores general configuration settings such as connection strings, logging levels, and third-party API keys.

- launchsettings.json:
Defines how the application should be run during development, including settings for HTTP/HTTPS URLs, environment variables, and browser launch behavior.

- appsettings.Development.json:
Holds environment-specific settings for development, typically overriding or adding to the configurations in appsettings.json.

## 4. Key Features:
- Book Management:
Add, update, delete, and retrieve books, with relationships to authors, publishers, and categories.

- Author, Publisher, and Category Management:
Each of these entities can be managed independently, with relationships handled using foreign keys and navigation properties.
- Entity Relationships:
Relationships like Book-Author, Book-Publisher, and Book-Category are managed with options for cascading deletes or setting foreign keys to null on deletion.

- Authorization and Authentication:
The project includes simple login mechanisms, ensuring secure access to the API using API key authentication and JWT Bearer Authentication.
