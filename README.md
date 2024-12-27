# Dayt: Event with Image Support

## Overview

This project was enhanced to include functionality for managing images associated with events using Entity Framework. The following steps and approaches were undertaken to complete this task:

---

## Changes Made

### **Data Layer**
1. **Entities and Relationships:**
   - Created `EventImageEntity` and established a one-to-many relationship with `EventEntity` for storing event images.
   - Introduced `ImageTypeEnum` to categorize image types (e.g., Thumbnail, Gallery, etc.) in `EventImageEntity`.

2. **Models:**
   - Updated `EventModel` to include a collection of `EventImageModel` for mapping data to the domain layer.

3. **Exception Handling:**
   - Added custom exceptions (`NotFoundException`, `ValidationException`) to provide meaningful error responses.

4. **Fluent API Configuration:**
   - Added a dedicated configuration file (`EventImageEntityConfiguration`) in the `Configurations` folder to define entity relationships and constraints.

---

### **Logic Layer**
1. **Repositories:**
   - Extended the functionality of `EventRepository` to save event images as a collection with the event, ensuring both are saved in a single call.
   - Added `EventImageRepository` to handle specific scenarios of saving and fetching event images separately when required.
   - Implemented proper exception handling for repository methods.

2. **Unit Tests:**
   - Updated existing unit tests to validate the saving and fetching of events along with associated images.
   - Added new unit tests in a dedicated `EventImageRepositoryTests` class to ensure the correctness of the `EventImageRepository`.

---

## Unit Test Highlights
- Verified that events are saved without images and retrieved correctly.
- Ensured that events with images are saved and fetched along with associated images.
- Validated exception handling when trying to fetch or save images for non-existing events.
- Confirmed proper retrieval of images associated with events.

---

## Observations and Suggestions

### Current Observations
The project structure is functional and well-organized. However, with project growth, maintaining scalability and testability could become challenging. Below are suggestions for improvements:

### Suggested Improvements
1. **Adopt Domain-Driven Design (DDD):**
   - Introduce the concept of aggregates (e.g., Event and EventImage as a single aggregate root).
   - Ensure that domain logic resides within the aggregate to enforce business rules.
   - Benefits:
     - Centralized domain logic ensures consistency.
     - Simplifies the management of complex relationships between entities.

2. **Implement Clean Architecture:**
   - Separate core business logic, infrastructure, and application layers to achieve modularity.
   - Benefits:
     - Improves maintainability and scalability.
     - Promotes testability by decoupling dependencies.

3. **Introduce CQRS:**
   - Separate commands (mutating operations) from queries (read operations) to optimize performance.
   - Benefits:
     - Enhances scalability for read-heavy applications.
     - Allows independent scaling and optimization of queries.

4. **Refactor for Abstraction and Loose Coupling:**
   - Move repository interfaces to the `Data` layer and create services in the `Logic` layer for better separation of concerns.
   - Avoid coupling core business logic with infrastructure concerns.

5. **Use AutoMapper:**
   - Simplifies transformations between entities and models.

6. **Apply Design Patterns:**
   - Incorporate patterns like:
     - **Unit of Work:** Ensures atomic transactions across multiple repositories.
     - **Dependency Injection:** Enhances modularity and testability.
     - **Result Pattern:** Standardizes error and success handling.

---

## Clean Architecture and DDD Proposal

### Suggested Project Structure (Clean Architecture with DDD Principles)

```plaintext
- src/
  - Core/
    - Entities/
      - Event.cs
      - EventImage.cs
    - Enums/
      - ImageTypeEnum.cs
    - Interfaces/
      - IEventRepository.cs
      - IEventImageRepository.cs
    - Aggregates/
      - EventAggregate.cs
  - Application/
    - Models/
      - EventModel.cs
      - EventImageModel.cs
    - Services/
      - EventService.cs
      - EventImageService.cs
    - DTOs/
      - EventDto.cs
      - EventImageDto.cs
  - Infrastructure/
    - Data/
      - DbContext/
        - EventDbContext.cs
      - Configurations/
        - EventEntityConfiguration.cs
        - EventImageEntityConfiguration.cs
      - Repositories/
        - EventRepository.cs
        - EventImageRepository.cs
    - Exceptions/
      - NotFoundException.cs
      - ValidationException.cs
  - Tests/
    - UnitTests/
      - EventRepositoryTests.cs
      - EventImageRepositoryTests.cs
```

---

## Pull Request and Notes

- A pull request has been created to merge these changes into the main branch.