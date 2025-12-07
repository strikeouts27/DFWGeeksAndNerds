# Contact Manager - Project 14

## Overview

This is a complete ASP.NET Core MVC Contact Manager application with **Dependency Injection** and **Unit Testing** using the Moq framework.

## Project Structure

The solution contains two projects:

- **ContactManager** - Main ASP.NET Core MVC application
- **ContactManager.Tests** - xUnit test project with comprehensive unit tests

## Features

### Main Application

- Full CRUD operations for managing contacts
- Clean MVC architecture with proper separation of concerns
- Dependency Injection using IContactRepository interface
- Data validation with model annotations
- Responsive UI with Bootstrap 5
- TempData for success messages
- Professional views for all operations (List, Details, Add, Edit, Delete)

### Dependency Injection

- `IContactRepository` interface for repository abstraction
- `ContactRepository` implementation with in-memory data storage
- Registered as a singleton in `Program.cs`
- Both HomeController and ContactController receive dependencies via constructor injection
- Proper null validation in constructors

### Contact Model

The Contact model includes:

- ContactId (auto-generated)
- FirstName (required, max 50 characters)
- LastName (required, max 50 characters)
- Email (required, valid email format, max 100 characters)
- Phone (required, valid phone format, max 20 characters)
- Organization (optional, max 50 characters)
- FullName (computed property)

## Unit Tests

The test project includes **39 comprehensive unit tests** covering:

### HomeController Tests (10 tests)

- Index action with contacts and ViewBag data
- Empty contact list scenarios
- Privacy action
- Error action with proper HttpContext
- Constructor null validation
- Repository method call verification

### ContactController Tests (29 tests)

- **Index**: List display with empty and populated data
- **Details**: Success cases, NotFound scenarios, edge cases (zero, negative IDs)
- **Add (GET)**: View rendering
- **Add (POST)**: Valid/invalid model states, repository calls, TempData messages
- **Edit (GET)**: Success and NotFound cases
- **Edit (POST)**: Valid/invalid updates, field updates, TempData
- **Delete (GET)**: Success and NotFound cases
- **Delete (POST)**: Confirmation with existing/non-existing contacts
- **Edge Cases**: Null organization, large datasets (100 contacts), all field updates
- **Constructor**: Null validation

### Testing Technologies

- **xUnit** - Testing framework
- **Moq** - Mocking framework for creating fake repositories
- All tests properly isolate controllers from data sources
- Tests verify both successful and failure scenarios
- Edge cases and boundary conditions are tested

## Running the Application

### Build the Solution

```bash
dotnet build
```

### Run the Application

```bash
cd ContactManager
dotnet run
```

The application will start on `https://localhost:5001` or `http://localhost:5000`

### Run All Tests

```bash
dotnet test
```

Expected output: **Test Run Successful. Total tests: 39, Passed: 39**

### Run Tests with Verbose Output

```bash
dotnet test --verbosity normal
```

## Application Usage

1. **Home Page** - Shows contact count and recent contacts
2. **View All Contacts** - List of all contacts with action buttons
3. **Add Contact** - Form to add a new contact with validation
4. **Edit Contact** - Modify existing contact information
5. **Delete Contact** - Confirmation page before deletion
6. **Contact Details** - View complete contact information

## Key Implementation Details

### Dependency Injection Setup (Program.cs)

```csharp
builder.Services.AddSingleton<IContactRepository, ContactRepository>();
```

### Controller Constructors

Both controllers use constructor injection:

```csharp
public HomeController(ILogger<HomeController> logger, IContactRepository repository)
public ContactController(IContactRepository repository)
```

### Test Setup with Moq

```csharp
var mockRepository = new Mock<IContactRepository>();
var controller = new ContactController(mockRepository.Object);
```

## Technologies Used

- ASP.NET Core 8.0
- MVC Pattern
- Dependency Injection
- xUnit
- Moq 4.20.72
- Bootstrap 5
- Razor Views

## Project Compliance

✅ Solution contains two projects (ContactManager and ContactManager.Tests)  
✅ HomeController receives repository via dependency injection  
✅ ContactController receives repository via dependency injection  
✅ Unit tests thoroughly test all controller action methods  
✅ Moq framework used for fake repository objects  
✅ Tests isolate controllers from actual data sources  
✅ Edge cases and failure scenarios are tested  
✅ All tests pass successfully (39/39)

## Author

Project 14 - ASP.NET Core MVC with DI and Unit Testing
