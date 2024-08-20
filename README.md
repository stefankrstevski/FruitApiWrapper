# FruitApiWrapper

## Description

This project is a wrapper around the third-party [Fruityvice API](https://www.fruityvice.com/), extended with CRUD operations for custom metadata provided by the consumer. Data from the Fruityvice API is cached for 24 hours, as it doesn't change frequently. Exception handling is implemented throughout the project.

The architecture follows Clean Architecture principles inspired by Ardalis. Consumers can add or update metadata by providing a valid JSON format with key-value pairs. This allows flexibility in the type and amount of metadata added. The fruit name is used as a selector to associate metadata with the fruit. Metadata is included when fetching fruit data and removed entirely when requested.

## API Endpoints

- **GET /api/fruits/{name}**: Fetches data for a specific fruit from the Fruityvice API, including any associated metadata.
- **POST /api/fruits/metadata/{name}**: Adds or updates metadata for a fruit. The request body should be a JSON object containing key-value pairs.
- **DELETE /api/fruits/metadata/{name}**: Removes all metadata associated with the specified fruit.

## Prerequisites

- .NET 8 SDK
- Visual Studio 

## How to Start the Project

1. **Clone the Project**
2. **Add Connection String**: Update your connection string in either `appsettings.json` or user secrets with the following format:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YourServerName; Database=YourDatabaseName; Trusted_Connection=True; TrustServerCertificate=True;"
   }
3. **Set the Startup Project**: Ensure Presentation.Api is set as the startup project in Visual Studio.
4. **Run the Project**
