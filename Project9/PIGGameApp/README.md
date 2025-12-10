# PIG Game App

A one-page web application implementing the classic PIG dice game, built with ASP.NET Core MVC and session state management.

## Game Rules

- Players take turns rolling a single die
- On each turn, a player can:
  - **Roll**: Add the die value to their turn total
  - **Hold**: Add their turn total to their score and end their turn
- **Rolling a 1**: Lose all points for that turn and end your turn
- **Winning**: First player to reach 100 points wins!

## Features

- Session state persistence to maintain game state between requests
- Real-time score tracking for two players
- Visual feedback for active player
- Die animations with color-coded results
- Responsive design for mobile and desktop
- Clear game rules and instructions

## Project Structure

```
PIGGameApp/
├── Controllers/
│   └── GameController.cs       # Handles all game actions
├── Models/
│   └── GameState.cs            # Game state model with logic
├── Views/
│   ├── Game/
│   │   └── Index.cshtml        # Main game view
│   ├── Shared/
│   │   └── _Layout.cshtml      # Layout template
│   ├── _ViewImports.cshtml
│   └── _ViewStart.cshtml
├── wwwroot/
│   ├── css/
│   │   └── site.css            # Game styling
│   └── js/
│       └── site.js
├── Program.cs                   # App configuration with session support
├── appsettings.json
└── PIGGameApp.csproj
```

## How to Run

1. Navigate to the project directory:

   ```bash
   cd PIGGameApp
   ```

2. Restore dependencies and run the application:

   ```bash
   dotnet run
   ```

3. Open your browser and navigate to:
   ```
   https://localhost:5001
   ```
   or
   ```
   http://localhost:5000
   ```

## Technical Implementation

### Session State

- Uses ASP.NET Core Session middleware to persist game state
- Game state is serialized to JSON and stored in session
- Session timeout set to 30 minutes

### Game Logic

- `GameState` model encapsulates all game logic
- Methods for rolling, holding, switching players, and checking win conditions
- Controller retrieves and saves game state from/to session

### Key Components

**GameController.cs**

- `Index()`: Displays the game page
- `NewGame()`: Resets game state
- `Roll()`: Handles die rolling logic
- `Hold()`: Adds turn total to score

**GameState.cs**

- Tracks player scores, turn total, current player
- `RollDie()`: Generates random number and updates state
- `Hold()`: Adds turn total to score and checks for winner
- `SwitchPlayer()`: Changes active player

## Requirements Met

✅ New game initialization sets all values to zero  
✅ Roll and Hold buttons enabled at start  
✅ Roll generates random number 1-6  
✅ Rolling a 1 ends turn with zero points  
✅ Rolling 2-6 adds to turn total  
✅ Hold adds turn total to score and ends turn  
✅ Win detection at 100 points  
✅ Buttons disabled when game ends  
✅ Winner display  
✅ Session state persistence  
✅ Well-organized, commented code

## Technologies Used

- ASP.NET Core 8.0 MVC
- C# 12
- Razor Views
- Session State Management
- CSS3 with Flexbox/Grid
- Responsive Web Design

---

_Project 9 - Dallas College ASP.NET Course_
