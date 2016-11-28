// <copyright file="GameLogic.cs" company="/">Daniel Dannewitz</copyright>
// <author>Daniel Dannewitz</author>
namespace VWS_TicTacToe
{
    using System;

    /// <summary>
    /// Actual management unit.
    /// Manages complete game behavior.
    /// </summary>
    public class GameLogic
    {
        /// <summary>
        /// Maximum players.
        /// </summary>
        private const int MAXPLAYERS = 2;

        /// <summary>
        /// Instance of game board.
        /// </summary>
        private IBoard gameBoard;

        /// <summary>
        /// User interaction instance.
        /// </summary>
        private IUserInteractor userInteractor;

        /// <summary>
        /// Game running value.
        /// </summary>
        private bool isGameRunning;

        /// <summary>
        /// Match ongoing value.
        /// </summary>
        private bool isMatchRunning;

        /// <summary>
        /// Current player value.
        /// </summary>
        private int currentPlayer;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        /// <param name="gameBoard">Board instance.</param>
        /// <param name="userInteractor">Interaction instance.</param>
        public GameLogic(IBoard gameBoard, IUserInteractor userInteractor)
        {
            this.gameBoard = gameBoard;
            this.userInteractor = userInteractor;
            this.isGameRunning = true;
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public void StartGame()
        {
            while (this.isGameRunning)
            {
                ConsoleKey inputKey;

                this.InitNewGame();
                this.StartMatch();
                inputKey = this.userInteractor.GetNewMatchInput();
                this.SwitchGameRunning(inputKey);
            }
        }

        /// <summary>
        /// Initializes a new game.
        /// Clears board, resets Current player.
        /// </summary>
        private void InitNewGame()
        {
            this.gameBoard.InitNewBoard();
            this.isMatchRunning = true;
            this.currentPlayer = 0;
        }

        /// <summary>
        /// Starts a match.
        /// </summary>
        private void StartMatch()
        {
            while (this.isMatchRunning)
            {
                this.PrintBoard();
                this.MakeAMove();
                this.CheckGameState();
                this.SwitchCurrentPlayer();
            }
        }

        /// <summary>
        /// Prints board representation.
        /// </summary>
        private void PrintBoard()
        {
            string gameBoardRepr = this.gameBoard.ToString();

            Console.Clear();
            this.userInteractor.Announce(gameBoardRepr);
        }

        /// <summary>
        /// Initializes a move.
        /// </summary>
        private void MakeAMove()
        {
            int position = 0;
            bool isMoveMade = false;

            do
            {
                position = this.userInteractor.GetMatchInput(this.currentPlayer);
                isMoveMade = this.gameBoard.TryMakeMove(position, this.currentPlayer);
            }
            while (!isMoveMade);
        }

        /// <summary>
        /// Checks the current game state (win/tied).
        /// </summary>
        private void CheckGameState()
        {
            bool isWon = this.gameBoard.CheckForWin();
            bool isTie = this.gameBoard.CheckForTie();
            bool isStillRunning = !(isWon || isTie);

            this.AnnounceWinOrTie(isWon, isTie);
            this.SwitchMatchRunning(isStillRunning);
        }

        /// <summary>
        /// Announce win or tie.
        /// </summary>
        /// <param name="isWon">Win value.</param>
        /// <param name="isTie">Tie value.</param>
        private void AnnounceWinOrTie(bool isWon, bool isTie)
        {
            if (isWon)
            {
                int printableCurrPlayer = this.currentPlayer + 1; // convert from {0, 1} to {1, 2}
                string hasWonString = string.Format("Player {0}, has Won!", printableCurrPlayer);
                this.PrintBoard();
                this.userInteractor.Announce(hasWonString);
            }
            else if (isTie)
            {
                string tiedString = "Tied game";
                this.PrintBoard();
                this.userInteractor.Announce(tiedString);
            }
        }

        /// <summary>
        /// Switches the current match ongoing value.
        /// </summary>
        /// <param name="isStillRunning">Still running value.</param>
        private void SwitchMatchRunning(bool isStillRunning)
        {
            this.isMatchRunning = isStillRunning;
        }

        /// <summary>
        /// Switches current player.
        /// </summary>
        private void SwitchCurrentPlayer()
        {
            this.currentPlayer = ++this.currentPlayer % MAXPLAYERS;
        }

        /// <summary>
        /// Switches game running value.
        /// </summary>
        /// <param name="inputKey">User input key.</param>
        private void SwitchGameRunning(ConsoleKey inputKey)
        {
            this.isGameRunning = inputKey == ConsoleKey.Y;
        }
    }
}
