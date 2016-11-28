// <copyright file="UserInteractor.cs" company="/">Daniel Dannewitz</copyright>
// <author>Daniel Dannewitz</author>
namespace VWS_TicTacToe
{
    using System;

    /// <summary>
    /// Interacts with the user(s).
    /// </summary>
    public class UserInteractor : IUserInteractor
    {
        /// <summary>
        /// Announce a text to the user(s).
        /// </summary>
        /// <param name="text">Text to be announced.</param>
        public void Announce(string text)
        {
            Console.WriteLine(text);
        }

        /// <summary>
        /// Retrieves input for match purposes (field / spot number).
        /// </summary>
        /// <param name="currentPlayer">Current player.</param>
        /// <returns>Position on board.</returns>
        public int GetMatchInput(int currentPlayer)
        {
            bool isValidInput = false;
            int printableCurrPlayer = currentPlayer + 1; // convert from {0, 1} to {1, 2}
            ConsoleKeyInfo input;

            do
            {
                this.Announce(string.Format("Player {0}, enter your move (number): ", printableCurrPlayer));
                input = this.RetrieveActualInput();
                isValidInput = this.ValidateOneToNineInput(input.Key);
            }
            while (!isValidInput);

            return input.KeyChar - '1';
        }

        /// <summary>
        /// Retrieves input for new match purposes (yes or no).
        /// </summary>
        /// <returns>Input key.</returns>
        public ConsoleKey GetNewMatchInput()
        {
            bool isValidInput = false;
            ConsoleKeyInfo input;

            do
            {
                this.Announce("Do you want to play again? (Y/N)");
                input = this.RetrieveActualInput();
                isValidInput = this.ValidateYesInput(input.Key);
            }
            while (!isValidInput);

            return input.Key;
        }

        /// <summary>
        /// Actual input retrieval.
        /// </summary>
        /// <returns>Key info instance.</returns>
        private ConsoleKeyInfo RetrieveActualInput()
        {
            ConsoleKeyInfo input = Console.ReadKey();
            Console.WriteLine(); // keeping console clean

            return input;
        }

        /// <summary>
        /// Validates input from 1 to 9.
        /// </summary>
        /// <param name="inputKey">Input key.</param>
        /// <returns>True if input is valid, false otherwise.</returns>
        private bool ValidateOneToNineInput(ConsoleKey inputKey)
        {
            bool isValid = (inputKey >= ConsoleKey.D1 && inputKey <= ConsoleKey.D9)
                || (inputKey >= ConsoleKey.NumPad1 && inputKey <= ConsoleKey.NumPad9);

            return isValid;
        }

        /// <summary>
        /// Validates yes / no input.
        /// </summary>
        /// <param name="inputKey">Input key.</param>
        /// <returns>True if input is valid, false otherwise.</returns>
        private bool ValidateYesInput(ConsoleKey inputKey)
        {
            bool isValid = inputKey == ConsoleKey.Y
                || inputKey == ConsoleKey.N;

            return isValid;
        }
    }
}
