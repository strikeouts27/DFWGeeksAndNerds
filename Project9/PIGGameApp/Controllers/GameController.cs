using Microsoft.AspNetCore.Mvc;
using PIGGameApp.Models;
using System.Text.Json;

namespace PIGGameApp.Controllers
{
    /// <summary>
    /// Controller that handles all game actions for the PIG dice game
    /// </summary>
    public class GameController : Controller
    {
        private const string SessionKey = "GameState";

        /// <summary>
        /// Displays the main game page
        /// </summary>
        public IActionResult Index()
        {
            var gameState = GetGameState();
            return View(gameState);
        }

        /// <summary>
        /// Starts a new game by resetting the game state
        /// </summary>
        [HttpPost]
        public IActionResult NewGame()
        {
            var gameState = new GameState();
            gameState.ResetGame();
            SaveGameState(gameState);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Handles the Roll action - rolls the die and updates game state
        /// </summary>
        [HttpPost]
        public IActionResult Roll()
        {
            var gameState = GetGameState();

            // Only allow rolling if game is not over
            if (!gameState.GameOver)
            {
                gameState.RollDie();
                SaveGameState(gameState);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Handles the Hold action - adds turn total to player's score
        /// </summary>
        [HttpPost]
        public IActionResult Hold()
        {
            var gameState = GetGameState();

            // Only allow holding if game is not over
            if (!gameState.GameOver)
            {
                gameState.Hold();
                SaveGameState(gameState);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Retrieves the game state from session or creates a new one
        /// </summary>
        private GameState GetGameState()
        {
            var sessionData = HttpContext.Session.GetString(SessionKey);
            
            if (string.IsNullOrEmpty(sessionData))
            {
                // Create new game state if none exists
                var newGameState = new GameState();
                SaveGameState(newGameState);
                return newGameState;
            }

            return JsonSerializer.Deserialize<GameState>(sessionData) ?? new GameState();
        }

        /// <summary>
        /// Saves the game state to session
        /// </summary>
        private void SaveGameState(GameState gameState)
        {
            var sessionData = JsonSerializer.Serialize(gameState);
            HttpContext.Session.SetString(SessionKey, sessionData);
        }
    }
}
