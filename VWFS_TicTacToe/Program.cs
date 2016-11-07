// <copyright file="Program.cs" company="/">Daniel Dannewitz</copyright>
// <author>Daniel Dannewitz</author>
namespace VWFS_TicTacToe
{
    /// <summary>
    /// Application entry point.
    /// Serves as a dependency injector.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starting point.
        /// </summary>
        /// <param name="args">not used.</param>
        public static void Main(string[] args)
        {
            Board gameBoard = new Board();
            UserInteractor interactor = new UserInteractor();
            GameLogic game = new GameLogic(gameBoard, interactor);
            game.StartGame();
        }
    }
}
