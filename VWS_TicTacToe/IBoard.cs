// <copyright file="IBoard.cs" company="/">Daniel Dannewitz</copyright>
// <author>Daniel Dannewitz</author>
namespace VWS_TicTacToe
{
    /// <summary>
    /// Interface definition for boards.
    /// </summary> 
    public interface IBoard
    {
        /// <summary>
        /// Initiates or resets a Board.
        /// </summary>
        void InitNewBoard();

        /// <summary>
        /// Tries to make a move.
        /// </summary>
        /// <param name = "position" > Position on the board.</param>
        /// <param name = "currentPlayer" > Current player.</param>
        /// <returns>True if move was made successfully, false otherwise.</returns>
        bool TryMakeMove(int position, int currentPlayer);

        /// <summary>
        /// Checks if there is a win state.
        /// </summary>
        /// <returns>True if game is won by current player, false otherwise.</returns>
        bool CheckForWin();

        /// <summary>
        /// Checks if there is a tie state.
        /// </summary>
        /// <returns>True if there are no more moves left, false otherwise.</returns>
        bool CheckForTie();
    }
}
