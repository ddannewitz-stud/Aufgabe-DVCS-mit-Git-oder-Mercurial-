// <copyright file="Board.cs" company="/">Daniel Dannewitz</copyright>
// <author>Daniel Dannewitz</author>
namespace VWS_TicTacToe
{
    using System.Linq;

    /// <summary>
    /// Board representation. Holds board values and win conditions.
    /// </summary>
    public class Board : IBoard
    {
        /// <summary>
        /// Empty value for fields / spots on board.
        /// </summary>
        private const int FIELDEMPTY = -1;

        /// <summary>
        /// Player one value for fields / spots on board.
        /// </summary>
        private const int FIELDREPRP1 = 0;

        /// <summary>
        /// Player two value for fields / spots on board.
        /// </summary>
        private const int FIELDREPRP2 = 1;

        /// <summary>
        /// Player one token.
        /// </summary>
        private const string TOKENP1 = "X";

        /// <summary>
        /// Player two token.
        /// </summary>
        private const string TOKENP2 = "O";

        /// <summary>
        /// Top left field / spot of board.
        /// </summary>
        private const int TOPLEFT = 0;

        /// <summary>
        /// Top middle field / spot of board.
        /// </summary>
        private const int TOPMID = 1;

        /// <summary>
        /// Top right field / spot of board.
        /// </summary>
        private const int TOPRIGHT = 2;

        /// <summary>
        /// Middle left field / spot of board.
        /// </summary>
        private const int MIDLEFT = 3;

        /// <summary>
        /// Middle middle field / spot of board.
        /// </summary>
        private const int MIDMID = 4;

        /// <summary>
        /// Middle right field / spot of board.
        /// </summary>
        private const int MIDRIGHT = 5;

        /// <summary>
        /// Bottom left field / spot of board.
        /// </summary>
        private const int BOTLEFT = 6;

        /// <summary>
        /// Bottom middle field / spot of board.
        /// </summary>
        private const int BOTMID = 7;

        /// <summary>
        /// Bottom right field / spot of board.
        /// </summary>
        private const int BOTRIGHT = 8;

        /// <summary>
        /// First element (of a list for example)
        /// </summary>
        private const int FIRSTELEMENT = 0;

        /// <summary>
        /// Second element (of a list for example)
        /// </summary>
        private const int SECONDELEMENT = 1;

        /// <summary>
        /// Third element (of a list for example)
        /// </summary>
        private const int THIRDELEMENT = 2;

        /// <summary>
        /// Possible win formations.
        /// </summary>
        private readonly int[][] winFormations;

        /// <summary>
        /// Initializes a new instance of the <see cref="Board"/> class.
        /// Initiates win formations.
        /// </summary>
        public Board()
        {
            this.winFormations = new int[][]
            {
                new int[] { TOPLEFT, TOPMID, TOPRIGHT },
                new int[] { MIDLEFT, MIDMID, MIDRIGHT },
                new int[] { BOTLEFT, BOTMID, BOTRIGHT },
                new int[] { TOPLEFT, MIDLEFT, BOTLEFT },
                new int[] { TOPMID, MIDMID, BOTMID },
                new int[] { TOPRIGHT, MIDRIGHT, BOTRIGHT },
                new int[] { TOPLEFT, MIDMID, BOTRIGHT },
                new int[] { TOPRIGHT, MIDMID, BOTLEFT }
            };
        }

        /// <summary>
        /// Gets actual game board representation.
        /// </summary>
        public int[] GameBoard { get; private set; }

        /// <summary>
        /// Initiates game board with empty values.
        /// </summary>
        public void InitNewBoard()
        {
            this.GameBoard = new int[] 
            {
                FIELDEMPTY, FIELDEMPTY, FIELDEMPTY,
                FIELDEMPTY, FIELDEMPTY, FIELDEMPTY,
                FIELDEMPTY, FIELDEMPTY, FIELDEMPTY
            };
        }

        /// <summary>
        /// Tries to make a move.
        /// Uses <code>ValidateMove()</code> and <code>ExecuteMakeMove()</code> to do so.
        /// </summary>
        /// <param name="position">Position on game board.</param>
        /// <param name="currentPlayer">Current player.</param>
        /// <returns>True if move was made successfully, false otherwise.</returns>
        public bool TryMakeMove(int position, int currentPlayer)
        {
            bool isValidMove = false;

            isValidMove = this.ValidateMove(position);
            this.ExecuteMakeMove(isValidMove, position, currentPlayer);

            return isValidMove;
        }

        /// <summary>
        /// Checks for win state.
        /// </summary>
        /// <returns>True if current player has won, false otherwise.</returns>
        public bool CheckForWin()
        {
            bool isWon = false;

            isWon = this.winFormations.Any(line => 
            {
                return TakenBySamePlayer(line[FIRSTELEMENT], line[SECONDELEMENT], line[THIRDELEMENT]);
            });

            return isWon;
        }

        /// <summary>
        /// Checks for tie state.
        /// </summary>
        /// <returns>True if there are no more moves left, false otherwise.</returns>
        public bool CheckForTie()
        {
            bool isTie = !this.CheckForFreeFields();

            return isTie;
        }

        /// <summary>
        /// Builds and returns textual representation of board.
        /// </summary>
        /// <returns>Textual representation of board.</returns>
        public override string ToString()
        {
            string topLeft = this.GetPieceAt(TOPLEFT);
            string topMid = this.GetPieceAt(TOPMID);
            string topRight = this.GetPieceAt(TOPRIGHT);
            string midLeft = this.GetPieceAt(MIDLEFT);
            string midMid = this.GetPieceAt(MIDMID);
            string midRight = this.GetPieceAt(MIDRIGHT);
            string botLeft = this.GetPieceAt(BOTLEFT);
            string botMid = this.GetPieceAt(BOTMID);
            string botRight = this.GetPieceAt(BOTRIGHT);
            string fieldString = string.Empty;

            fieldString = string.Format(" {0} | {1} | {2}", topLeft, topMid, topRight);
            fieldString += "\n";
            fieldString += "---|---|---";
            fieldString += "\n";
            fieldString += string.Format(" {0} | {1} | {2}", midLeft, midMid, midRight);
            fieldString += "\n";
            fieldString += "---|---|---";
            fieldString += "\n";
            fieldString += string.Format(" {0} | {1} | {2}", botLeft, botMid, botRight);

            return fieldString;
        }

        /// <summary>
        /// Checks if there are 3 fields / spots in a row, col or diagonal taken by the same player.
        /// </summary>
        /// <param name="firstElement">First element of the line.</param>
        /// <param name="secondElement">Second element of the line.</param>
        /// <param name="thirdElement">Third element of the line.</param>
        /// <returns>True if there are win conditions fulfilled, false otherwise.</returns>
        private bool TakenBySamePlayer(int firstElement, int secondElement, int thirdElement)
        {
            bool isTakenBySamePlayer = false;

            isTakenBySamePlayer = this.GameBoard[firstElement] != FIELDEMPTY
                && this.GameBoard[firstElement] == this.GameBoard[secondElement]
                && this.GameBoard[firstElement] == this.GameBoard[thirdElement];

            return isTakenBySamePlayer;
        }

        /// <summary>
        /// Checks if there are still free fields / spots.
        /// </summary>
        /// <returns>True if there are still free fields / spots, false otherwise.</returns>
        private bool CheckForFreeFields()
        {
            bool hasFreeSpot = false;

            foreach (int field in this.GameBoard)
            {
                if (field == FIELDEMPTY)
                {
                    hasFreeSpot = true;
                    break;
                }
            }

            return hasFreeSpot;
        }

        /// <summary>
        /// Validates a move.
        /// </summary>
        /// <param name="position">Position on board.</param>
        /// <returns>True if move is valid, false otherwise.</returns>
        private bool ValidateMove(int position)
        {
            bool isValidMove = this.GameBoard[position] == FIELDEMPTY;

            return isValidMove;
        }

        /// <summary>
        /// Executes a move.
        /// </summary>
        /// <param name="isValidMove">Validation value.</param>
        /// <param name="position">Position on board.</param>
        /// <param name="currentPlayer">Current player.</param>
        private void ExecuteMakeMove(bool isValidMove, int position, int currentPlayer)
        {
            if (isValidMove)
            {
                this.GameBoard[position] = currentPlayer;
            }
        }

        /// <summary>
        /// Returns player token or field position at position.
        /// </summary>
        /// <param name="position">Position on board.</param>
        /// <returns>Player token or field / spot number.</returns>
        private string GetPieceAt(int position)
        {
            if (this.GameBoard[position] == FIELDREPRP1)
            {
                return TOKENP1;
            }
            else if (this.GameBoard[position] == FIELDREPRP2)
            {
                return TOKENP2;
            }
            else
            {
                return (position + 1).ToString();  // convert to 1..9 from 0..8
            }
        }
    }
}
