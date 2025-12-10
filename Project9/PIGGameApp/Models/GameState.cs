namespace PIGGameApp.Models
{
    /// <summary>
    /// Model class that represents the state of the PIG game
    /// </summary>
    public class GameState
    {
        // Player scores
        public int Player1Score { get; set; } = 0;
        public int Player2Score { get; set; } = 0;

        // Current turn total
        public int TurnTotal { get; set; } = 0;

        // Current die value
        public int DieValue { get; set; } = 0;

        // Which player's turn (1 or 2)
        public int CurrentPlayer { get; set; } = 1;

        // Game status
        public bool GameOver { get; set; } = false;

        // Winner (if game is over)
        public int Winner { get; set; } = 0;

        // Points needed to win
        public int PointsToWin { get; set; } = 100;

        /// <summary>
        /// Resets the game to initial state
        /// </summary>
        public void ResetGame()
        {
            Player1Score = 0;
            Player2Score = 0;
            TurnTotal = 0;
            DieValue = 0;
            CurrentPlayer = 1;
            GameOver = false;
            Winner = 0;
        }

        /// <summary>
        /// Switches to the other player's turn
        /// </summary>
        public void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == 1 ? 2 : 1;
            TurnTotal = 0;
        }

        /// <summary>
        /// Adds the turn total to the current player's score and checks for winner
        /// </summary>
        public void Hold()
        {
            if (CurrentPlayer == 1)
            {
                Player1Score += TurnTotal;
                if (Player1Score >= PointsToWin)
                {
                    GameOver = true;
                    Winner = 1;
                }
            }
            else
            {
                Player2Score += TurnTotal;
                if (Player2Score >= PointsToWin)
                {
                    GameOver = true;
                    Winner = 2;
                }
            }

            // Switch players if game is not over
            if (!GameOver)
            {
                SwitchPlayer();
            }
        }

        /// <summary>
        /// Rolls the die and updates game state
        /// </summary>
        /// <returns>True if turn continues, False if turn ends</returns>
        public bool RollDie()
        {
            Random random = new Random();
            DieValue = random.Next(1, 7); // Random number between 1 and 6

            // If rolled a 1, turn ends and turn total is lost
            if (DieValue == 1)
            {
                TurnTotal = 0;
                SwitchPlayer();
                return false;
            }
            else
            {
                // Add die value to turn total
                TurnTotal += DieValue;
                return true;
            }
        }
    }
}
