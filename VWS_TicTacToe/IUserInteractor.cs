// <copyright file="IUserInteractor.cs" company="/">Daniel Dannewitz</copyright>
// <author>Daniel Dannewitz</author>
namespace VWS_TicTacToe
{
    using System;

    /// <summary>
    /// Interface definition for User interactions.
    /// </summary>
    public interface IUserInteractor
    {
        /// <summary>
        /// Announces a text to the user(s).
        /// </summary>
        /// <param name="text">Text to announce.</param>
        void Announce(string text);

        /// <summary>
        /// Retrieves input from current player for match purposes.
        /// </summary>
        /// <param name="currentPlayer">Current player.</param>
        /// <returns>Position on the board.</returns>
        int GetMatchInput(int currentPlayer);

        /// <summary>
        /// Retrieves input from user to whether or not start a new match.
        /// </summary>
        /// <returns>Input key.</returns>
        ConsoleKey GetNewMatchInput();
    }
}