using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard
{
    public class Board
    {
        public int Size { get; set; }
        public Cell[,] Grid { get; set; }

        public Board(int iSize)
        {
            Size = iSize;

            //allocating space for a matrix, usually it's 8x8, but can be 26x26
            Grid = new Cell[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Grid[i, j] = new Cell(i, j);
                }
            }
        }
  

        public Boolean GetNextLegalMoves(string Piece, Cell currentCell)
        {
            //for simplicity define them here, since we are going to use them everywhere in this method
            int f, r, iLegalMoves = 0;

            //clear previous options
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Grid[i, j].LegalMove = false;
                }
            }

            switch (Piece)
            {
                //Queen can move any direction, any distance, same for take (bishop + rook)
                case "Queen":
                    //diagonal down and right
                    r = 1;
                    f = 1;
                    while (currentCell.File + f < Size & currentCell.Rank + r < Size)
                    {
                        if (Grid[currentCell.File + f, currentCell.Rank + r].Occupied && Grid[currentCell.File + f, currentCell.Rank + r].OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor)
                        {
                            Grid[currentCell.File + f, currentCell.Rank + r].LegalMove = true;
                            break;
                        }
                        else if (Grid[currentCell.File + f, currentCell.Rank + r].Occupied)
                        {
                            break;
                        }
                        Grid[currentCell.File + f, currentCell.Rank + r].LegalMove = true;
                        r++;
                        f++;
                    }

                    //diagonal down and left
                    r = 1;
                    f = 1;
                    while (currentCell.File + f < Size & currentCell.Rank - r >= 0)
                    {
                        if (Grid[currentCell.File + f, currentCell.Rank - r].Occupied && Grid[currentCell.File + f, currentCell.Rank - r].OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor)
                        {
                            Grid[currentCell.File + f, currentCell.Rank - r].LegalMove = true;
                            break;
                        }
                        else if (Grid[currentCell.File + f, currentCell.Rank - r].Occupied)
                        {
                            break;
                        }
                        Grid[currentCell.File + f, currentCell.Rank - r].LegalMove = true;
                        f++;
                        r++;
                    }

                    // diagonal up and left
                    r = 1;
                    f = 1;
                    while (currentCell.File - f >= 0 & currentCell.Rank - r >= 0)
                    {
                        if (Grid[currentCell.File - f, currentCell.Rank - r].Occupied && Grid[currentCell.File - f, currentCell.Rank - r].OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor)
                        {
                            Grid[currentCell.File - f, currentCell.Rank - r].LegalMove = true;
                            break;
                        }
                        else if (Grid[currentCell.File - f, currentCell.Rank - r].Occupied)
                        {
                            break;
                        }
                        Grid[currentCell.File - f, currentCell.Rank - r].LegalMove = true;
                        f++;
                        r++;
                    }

                    // diagonal up and right
                    r = 1;
                    f = 1;
                    while (currentCell.File - f >= 0 & currentCell.Rank + r < Size)
                    {
                        if (Grid[currentCell.File - f, currentCell.Rank + r].Occupied && Grid[currentCell.File - f, currentCell.Rank + r].OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor)
                        {
                            Grid[currentCell.File - f, currentCell.Rank + r].LegalMove = true;
                            break;
                        }
                        else if (Grid[currentCell.File - f, currentCell.Rank + r].Occupied)
                        {
                            break;
                        }
                        Grid[currentCell.File - f, currentCell.Rank + r].LegalMove = true;
                        f++;
                        r++;
                    }

                    //horizontal move right
                    r = 1;
                    while (currentCell.Rank + r < Size)
                    {
                        if (Grid[currentCell.File, currentCell.Rank + r].Occupied && Grid[currentCell.File, currentCell.Rank + r].OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor)
                        {
                            Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                            break;
                        }
                        else if (Grid[currentCell.File, currentCell.Rank + r].Occupied)
                        {
                            break;
                        }
                        Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                        r++;
                    }
                    //horizontal move left
                    r = 1;
                    while (currentCell.Rank - r >= 0)
                    {
                        if (Grid[currentCell.File, currentCell.Rank - r].Occupied && Grid[currentCell.File, currentCell.Rank - r].OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor)
                        {
                            Grid[currentCell.File, currentCell.Rank - r].LegalMove = true;
                            break;
                        }
                        else if (Grid[currentCell.File, currentCell.Rank - r].Occupied)
                        {
                            break;
                        }
                        Grid[currentCell.File, currentCell.Rank - r].LegalMove = true;
                        r++;
                    }
                    //vertical move down
                    f = 1;
                    while (currentCell.File + f < Size)
                    {
                        if (Grid[currentCell.File + f, currentCell.Rank].Occupied && Grid[currentCell.File + f, currentCell.Rank].OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor)
                        {
                            Grid[currentCell.File + f, currentCell.Rank].LegalMove = true;
                            break;
                        }
                        else if(Grid[currentCell.File + f, currentCell.Rank].Occupied)
                        {
                            break;
                        }
                        Grid[currentCell.File + f, currentCell.Rank].LegalMove = true;
                        f++;
                    }
                    //vertical move up
                    f = 1;
                    while (currentCell.File - f >= 0)
                    {
                        if (Grid[currentCell.File - f, currentCell.Rank].Occupied && Grid[currentCell.File - f, currentCell.Rank].OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor)
                        {
                            Grid[currentCell.File - f, currentCell.Rank].LegalMove = true;
                            break;
                        }
                        else if (Grid[currentCell.File - f, currentCell.Rank].Occupied)
                        {
                            break;
                        }
                        Grid[currentCell.File - f, currentCell.Rank].LegalMove = true;
                        f++;
                    }
                    break;
                //King moves 1 cell any direction, same for take
                //For king: check next move so king is not in danger on that cell
                case "King":
                    if (currentCell.File + 1 < Size & currentCell.Rank + 1 < Size && !(Grid[currentCell.File + 1, currentCell.Rank + 1].OccupiedBy.PieceColor == currentCell.OccupiedBy.PieceColor))
                    {
                        if (!CheckCheck(Grid[currentCell.File + 1, currentCell.Rank + 1], currentCell.OccupiedBy.PieceColor))
                        {
                            Grid[currentCell.File + 1, currentCell.Rank + 1].LegalMove = true;
                            iLegalMoves++;
                        }
                        
                    }
                    if (currentCell.File + 1 < Size && !(Grid[currentCell.File + 1, currentCell.Rank + 0].OccupiedBy.PieceColor == currentCell.OccupiedBy.PieceColor))
                    {
                        if (!CheckCheck(Grid[currentCell.File + 1, currentCell.Rank + 0], currentCell.OccupiedBy.PieceColor))
                        {
                            Grid[currentCell.File + 1, currentCell.Rank + 0].LegalMove = true;
                            iLegalMoves++;
                        }
                    }
                    if (currentCell.File + 1 < Size & currentCell.Rank - 1 >= 0 && !(Grid[currentCell.File + 1, currentCell.Rank - 1].OccupiedBy.PieceColor == currentCell.OccupiedBy.PieceColor))
                    {
                        if (!CheckCheck(Grid[currentCell.File + 1, currentCell.Rank - 1], currentCell.OccupiedBy.PieceColor))
                        {
                            Grid[currentCell.File + 1, currentCell.Rank - 1].LegalMove = true;
                            iLegalMoves++;
                        }
                    }
                    if (currentCell.File - 1 >= 0 && !(Grid[currentCell.File - 1, currentCell.Rank + 0].OccupiedBy.PieceColor == currentCell.OccupiedBy.PieceColor))
                    {
                        if (!CheckCheck(Grid[currentCell.File - 1, currentCell.Rank + 0], currentCell.OccupiedBy.PieceColor))
                        {
                            Grid[currentCell.File - 1, currentCell.Rank + 0].LegalMove = true;
                            iLegalMoves++;
                        }
                    }
                    if (currentCell.File - 1 >= 0 & currentCell.Rank - 1 >= 0 && !(Grid[currentCell.File - 1, currentCell.Rank - 1].OccupiedBy.PieceColor == currentCell.OccupiedBy.PieceColor))
                    {
                        if (!CheckCheck(Grid[currentCell.File - 1, currentCell.Rank - 1], currentCell.OccupiedBy.PieceColor))
                        {
                            Grid[currentCell.File - 1, currentCell.Rank - 1].LegalMove = true;
                            iLegalMoves++;
                        }
                    }
                    if (currentCell.File - 1 >= 0 & currentCell.Rank + 1 < Size && !(Grid[currentCell.File - 1, currentCell.Rank + 1].OccupiedBy.PieceColor == currentCell.OccupiedBy.PieceColor))
                    {
                        if (!CheckCheck(Grid[currentCell.File - 1, currentCell.Rank + 1], currentCell.OccupiedBy.PieceColor))
                        {
                            Grid[currentCell.File - 1, currentCell.Rank + 1].LegalMove = true;
                            iLegalMoves++;
                        }
                    }
                    if (currentCell.Rank + 1 < Size && !(Grid[currentCell.File + 0, currentCell.Rank + 1].OccupiedBy.PieceColor == currentCell.OccupiedBy.PieceColor))
                    {
                        if (!CheckCheck(Grid[currentCell.File + 0, currentCell.Rank + 1], currentCell.OccupiedBy.PieceColor))
                        {
                            Grid[currentCell.File + 0, currentCell.Rank + 1].LegalMove = true;
                            iLegalMoves++;
                        }
                    }
                    if (currentCell.Rank - 1 >= 0 && !(Grid[currentCell.File + 0, currentCell.Rank - 1].OccupiedBy.PieceColor == currentCell.OccupiedBy.PieceColor))
                    {
                        if (!CheckCheck(Grid[currentCell.File + 0, currentCell.Rank - 1], currentCell.OccupiedBy.PieceColor))
                        {
                            Grid[currentCell.File + 0, currentCell.Rank - 1].LegalMove = true;
                            iLegalMoves++;
                        }
                    }

                    break;
                //Bishop moves diagonal, same for take
                case "Bishop":
                    //diagonal down and right
                    r = 1;
                    f = 1;
                    while (currentCell.File + f < Size & currentCell.Rank + r < Size)
                    {
                        if (Grid[currentCell.File + f, currentCell.Rank + r].Occupied && Grid[currentCell.File + f, currentCell.Rank + r].OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor)
                        {
                            Grid[currentCell.File + f, currentCell.Rank + r].LegalMove = true;
                            break;
                        }
                        else if (Grid[currentCell.File + f, currentCell.Rank + r].Occupied)
                        {
                            break;
                        }
                        Grid[currentCell.File + f, currentCell.Rank + r].LegalMove = true;
                        r++;
                        f++;
                    }

                    //diagonal down and left
                    r = 1;
                    f = 1;
                    while (currentCell.File + f < Size & currentCell.Rank - r >= 0)
                    {
                        if (Grid[currentCell.File + f, currentCell.Rank - r].Occupied && Grid[currentCell.File + f, currentCell.Rank - r].OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor)
                        {
                            Grid[currentCell.File + f, currentCell.Rank - r].LegalMove = true;
                            break;
                        }
                        else if (Grid[currentCell.File + f, currentCell.Rank - r].Occupied)
                        {
                            break;
                        }
                        Grid[currentCell.File + f, currentCell.Rank - r].LegalMove = true;
                        f++;
                        r++;
                    }

                    // diagonal up and left
                    r = 1;
                    f = 1;
                    while (currentCell.File - f >= 0 & currentCell.Rank - r >= 0)
                    {
                        if (Grid[currentCell.File - f, currentCell.Rank - r].Occupied && Grid[currentCell.File - f, currentCell.Rank - r].OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor)
                        {
                            Grid[currentCell.File - f, currentCell.Rank - r].LegalMove = true;
                            break;
                        }
                        else if (Grid[currentCell.File - f, currentCell.Rank - r].Occupied)
                        {
                            break;
                        }
                        Grid[currentCell.File - f, currentCell.Rank - r].LegalMove = true;
                        f++;
                        r++;
                    }

                    // diagonal up and right
                    r = 1;
                    f = 1;
                    while (currentCell.File - f >= 0 & currentCell.Rank + r < Size)
                    {
                        if (Grid[currentCell.File - f, currentCell.Rank + r].Occupied && Grid[currentCell.File - f, currentCell.Rank + r].OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor)
                        {
                            Grid[currentCell.File - f, currentCell.Rank + r].LegalMove = true;
                            break;
                        }
                        else if (Grid[currentCell.File - f, currentCell.Rank + r].Occupied)
                        {
                            break;
                        }
                        Grid[currentCell.File - f, currentCell.Rank + r].LegalMove = true;
                        f++;
                        r++;
                    }
                    break;

                //Knight moves L shaped forms, same for take
                case "Knight":
                    if (currentCell.File + 2 < Size & currentCell.Rank + 1 < Size && !(Grid[currentCell.File + 2, currentCell.Rank + 1].OccupiedBy.PieceColor == currentCell.OccupiedBy.PieceColor))
                    {
                        Grid[currentCell.File + 2, currentCell.Rank + 1].LegalMove = true;
                    }
                    if (currentCell.File + 2 < Size & currentCell.Rank - 1 >= 0 && !(Grid[currentCell.File + 2, currentCell.Rank - 1].OccupiedBy.PieceColor == currentCell.OccupiedBy.PieceColor))
                    {
                        Grid[currentCell.File + 2, currentCell.Rank - 1].LegalMove = true;
                    }
                    if (currentCell.File - 2 >= 0 & currentCell.Rank + 1 < Size && !(Grid[currentCell.File - 2, currentCell.Rank + 1].OccupiedBy.PieceColor == currentCell.OccupiedBy.PieceColor))
                    {
                        Grid[currentCell.File - 2, currentCell.Rank + 1].LegalMove = true;
                    }
                    if (currentCell.File - 2 >= 0 & currentCell.Rank - 1 >= 0 && !(Grid[currentCell.File - 2, currentCell.Rank - 1].OccupiedBy.PieceColor == currentCell.OccupiedBy.PieceColor))
                    {
                        Grid[currentCell.File - 2, currentCell.Rank - 1].LegalMove = true;
                    }
                    if (currentCell.File + 1 < Size & currentCell.Rank + 2 < Size && !(Grid[currentCell.File + 1, currentCell.Rank + 2].OccupiedBy.PieceColor == currentCell.OccupiedBy.PieceColor))
                    {
                        Grid[currentCell.File + 1, currentCell.Rank + 2].LegalMove = true;
                    }
                    if (currentCell.File + 1 < Size & currentCell.Rank - 2 >= 0 && !(Grid[currentCell.File + 1, currentCell.Rank - 2].OccupiedBy.PieceColor == currentCell.OccupiedBy.PieceColor))
                    {
                        Grid[currentCell.File + 1, currentCell.Rank - 2].LegalMove = true;
                    }
                    if (currentCell.File - 1 >= 0 & currentCell.Rank + 2 < Size && !(Grid[currentCell.File - 1, currentCell.Rank + 2].OccupiedBy.PieceColor == currentCell.OccupiedBy.PieceColor))
                    {
                        Grid[currentCell.File - 1, currentCell.Rank + 2].LegalMove = true;
                    }
                    if (currentCell.File - 1 >= 0 & currentCell.Rank - 2 >= 0 && !(Grid[currentCell.File - 1, currentCell.Rank - 2].OccupiedBy.PieceColor == currentCell.OccupiedBy.PieceColor))
                    {
                        Grid[currentCell.File - 1, currentCell.Rank - 2].LegalMove = true;
                    }

                    break;
                //Rook moves horizontal and vertical, can take only horizontal and vertical
                case "Rook":
                    //horizontal move right
                    r = 1;
                    while (currentCell.Rank + r < Size)
                    {
                        if (Grid[currentCell.File, currentCell.Rank + r].Occupied && Grid[currentCell.File, currentCell.Rank + r].OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor)
                        {
                            Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                            break;
                        }
                        else if (Grid[currentCell.File, currentCell.Rank + r].Occupied)
                        {
                            break;
                        }
                        Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                        r++;
                    }
                    //horizontal move left
                    r = 1;
                    while (currentCell.Rank - r >= 0)
                    {
                        if (Grid[currentCell.File, currentCell.Rank - r].Occupied && Grid[currentCell.File, currentCell.Rank - r].OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor)
                        {
                            Grid[currentCell.File, currentCell.Rank - r].LegalMove = true;
                            break;
                        }
                        else if (Grid[currentCell.File, currentCell.Rank - r].Occupied)
                        {
                            break;
                        }
                        Grid[currentCell.File, currentCell.Rank - r].LegalMove = true;
                        r++;
                    }
                    //vertical move down
                    f = 1;
                    while (currentCell.File + f < Size)
                    {
                        if (Grid[currentCell.File + f, currentCell.Rank].Occupied && Grid[currentCell.File + f, currentCell.Rank].OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor)
                        {
                            Grid[currentCell.File + f, currentCell.Rank].LegalMove = true;
                            break;
                        }
                        else if (Grid[currentCell.File + f, currentCell.Rank].Occupied)
                        {
                            break;
                        }
                        Grid[currentCell.File + f, currentCell.Rank].LegalMove = true;
                        f++;
                    }
                    //vertical move up
                    f = 1;
                    while (currentCell.File - f >= 0)
                    {
                        if (Grid[currentCell.File - f, currentCell.Rank].Occupied && Grid[currentCell.File - f, currentCell.Rank].OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor)
                        {
                            Grid[currentCell.File - f, currentCell.Rank].LegalMove = true;
                            break;
                        }
                        else if (Grid[currentCell.File - f, currentCell.Rank].Occupied)
                        {
                            break;
                        }
                        Grid[currentCell.File - f, currentCell.Rank].LegalMove = true;
                        f++;
                    }
                    break;
                //Pawn moves front +2 or +1 if it's the first move (so to half of the board), can take only diagonal range of 1
                case "Pawn":
                    //only white go down
                    if (currentCell.OccupiedBy.PieceColor == "white")
                    {
                        //2 cells down to middle
                        if (currentCell.File + 2 <= Size / 2 && !(Grid[currentCell.File + 2, currentCell.Rank].Occupied || Grid[currentCell.File + 1, currentCell.Rank].Occupied))
                        {
                            Grid[currentCell.File + 2, currentCell.Rank].LegalMove = true;
                        }
                        //1 cells down
                        if (currentCell.File + 1 < Size && !Grid[currentCell.File + 1, currentCell.Rank].Occupied)
                        {
                            Grid[currentCell.File + 1, currentCell.Rank].LegalMove = true;
                        }
                        //white take definition: down right
                        if (currentCell.File + 1 < Size && currentCell.Rank + 1 < Size && Grid[currentCell.File + 1, currentCell.Rank + 1].OccupiedBy.PieceColor == "black")
                        {
                            Grid[currentCell.File + 1, currentCell.Rank + 1].LegalMove = true;
                        }
                        //down left
                        if (currentCell.File + 1 < Size && currentCell.Rank -1 >= 0 && Grid[currentCell.File + 1, currentCell.Rank - 1].OccupiedBy.PieceColor == "black")
                        {
                            Grid[currentCell.File + 1, currentCell.Rank - 1].LegalMove = true;
                        }
                    }
                    else
                    {
                        //2 cells up to middle
                        if (currentCell.File - 2 >= Size / 2 && !(Grid[currentCell.File - 1, currentCell.Rank].Occupied || Grid[currentCell.File - 2, currentCell.Rank].Occupied))
                        {
                            Grid[currentCell.File - 2, currentCell.Rank].LegalMove = true;
                        }
                        //1 cells down
                        if (currentCell.File - 1 >= 0 && !Grid[currentCell.File - 1, currentCell.Rank].Occupied)
                        {
                            Grid[currentCell.File - 1, currentCell.Rank].LegalMove = true;
                        }
                        //white take definition: down right
                        if (currentCell.File - 1 >= 0 && currentCell.Rank + 1 < Size && Grid[currentCell.File - 1, currentCell.Rank + 1].OccupiedBy.PieceColor == "white")
                        {
                            Grid[currentCell.File - 1, currentCell.Rank + 1].LegalMove = true;
                        }
                        //down left
                        if (currentCell.File - 1 >= 0 && currentCell.Rank - 1 >= 0 && Grid[currentCell.File - 1, currentCell.Rank - 1].OccupiedBy.PieceColor == "white")
                        {
                            Grid[currentCell.File - 1, currentCell.Rank - 1].LegalMove = true;
                        }
                    }
                    break;
                default:
                    break;

            }

            //for king, if no legal moves available will return false, this is basically checkmate
            if (iLegalMoves == 0 && currentCell.OccupiedBy.Piece == PieceOptions.King)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void PlaceInitialPieces()
        {
            //first clear everything
            for (int i=0; i<Size; i++)
            {
                for (int j=0;j<Size; j++)
                {
                    Grid[i, j].Occupied = false;
                    Grid[i, j].OccupiedBy.Piece = PieceOptions.Empty;
                    Grid[i, j].OccupiedBy.PieceColor = "";
                }
            }

            //can only hardcode positions for first file of each player
            //white pieces below
            Grid[0,0].Occupied = true;
            Grid[0, 0].OccupiedBy.Piece = PieceOptions.Rook;
            Grid[0, 0].OccupiedBy.PieceColor = "white";

            Grid[0, 1].Occupied = true;
            Grid[0, 1].OccupiedBy.Piece = PieceOptions.Knight;
            Grid[0, 1].OccupiedBy.PieceColor = "white";

            Grid[0, 2].Occupied = true;
            Grid[0, 2].OccupiedBy.Piece = PieceOptions.Bishop;
            Grid[0, 2].OccupiedBy.PieceColor = "white";

            Grid[0, 3].Occupied = true;
            Grid[0, 3].OccupiedBy.Piece = PieceOptions.Queen;
            Grid[0, 3].OccupiedBy.PieceColor = "white";

            Grid[0, 4].Occupied = true;
            Grid[0, 4].OccupiedBy.Piece = PieceOptions.King;
            Grid[0, 4].OccupiedBy.PieceColor = "white";

            Grid[0, 5].Occupied = true;
            Grid[0, 5].OccupiedBy.Piece = PieceOptions.Bishop;
            Grid[0, 5].OccupiedBy.PieceColor = "white";

            Grid[0, 6].Occupied = true;
            Grid[0, 6].OccupiedBy.Piece = PieceOptions.Knight;
            Grid[0, 6].OccupiedBy.PieceColor = "white";

            Grid[0, 7].Occupied = true;
            Grid[0, 7].OccupiedBy.Piece = PieceOptions.Rook;
            Grid[0, 7].OccupiedBy.PieceColor = "white";

            //black pieces below
            Grid[7, 0].Occupied = true;
            Grid[7, 0].OccupiedBy.Piece = PieceOptions.Rook;
            Grid[7, 0].OccupiedBy.PieceColor = "black";

            Grid[7, 1].Occupied = true;
            Grid[7, 1].OccupiedBy.Piece = PieceOptions.Knight;
            Grid[7, 1].OccupiedBy.PieceColor = "black";

            Grid[7, 2].Occupied = true;
            Grid[7, 2].OccupiedBy.Piece = PieceOptions.Bishop;
            Grid[7, 2].OccupiedBy.PieceColor = "black";

            //king and queen are on opposite sides so white king faces black queen
            Grid[7, 4].Occupied = true;
            Grid[7, 4].OccupiedBy.Piece = PieceOptions.Queen;
            Grid[7, 4].OccupiedBy.PieceColor = "black";

            Grid[7, 3].Occupied = true;
            Grid[7, 3].OccupiedBy.Piece = PieceOptions.King;
            Grid[7, 3].OccupiedBy.PieceColor = "black";

            Grid[7, 5].Occupied = true;
            Grid[7, 5].OccupiedBy.Piece = PieceOptions.Bishop;
            Grid[7, 5].OccupiedBy.PieceColor = "black";

            Grid[7, 6].Occupied = true;
            Grid[7, 6].OccupiedBy.Piece = PieceOptions.Knight;
            Grid[7, 6].OccupiedBy.PieceColor = "black";

            Grid[7, 7].Occupied = true;
            Grid[7, 7].OccupiedBy.Piece = PieceOptions.Rook;
            Grid[7, 7].OccupiedBy.PieceColor = "black";

            //pawns
            for (int i = 0; i < Size; i++)
            {
                //white pawns
                Grid[1, i].Occupied = true;
                Grid[1, i].OccupiedBy.Piece = PieceOptions.Pawn;
                Grid[1, i].OccupiedBy.PieceColor = "white";
                //black pawns
                Grid[6, i].Occupied = true;
                Grid[6, i].OccupiedBy.Piece = PieceOptions.Pawn;
                Grid[6, i].OccupiedBy.PieceColor = "black";
            }

        }

        public Boolean CheckCheck(Cell currentCell, string col)
        {
            //check if current king position is a legal move for any opposite color piece on the board
            //diagonal down and right
            int r, f;
            r = 1;
            f = 1;
            while (currentCell.File + f < Size & currentCell.Rank + r < Size)
            {
                if ((Grid[currentCell.File + f, currentCell.Rank + r].OccupiedBy.Piece == PieceOptions.Queen || Grid[currentCell.File + f, currentCell.Rank + r].OccupiedBy.Piece == PieceOptions.Bishop) && (Grid[currentCell.File + f, currentCell.Rank + r].OccupiedBy.PieceColor != col))
                {
                    return true;
                }
                else if (Grid[currentCell.File + f, currentCell.Rank + r].Occupied && Grid[currentCell.File + f, currentCell.Rank + r].Occupied && Grid[currentCell.File + f, currentCell.Rank + r].OccupiedBy.Piece != PieceOptions.Queen && Grid[currentCell.File + f, currentCell.Rank + r].OccupiedBy.Piece != PieceOptions.Bishop)
                {
                    break;
                }
                r++;
                f++;
            }

            //diagonal down and left
            r = 1;
            f = 1;
            while (currentCell.File + f < Size & currentCell.Rank + r < Size)
            {
                if ((Grid[currentCell.File + f, currentCell.Rank - r].OccupiedBy.Piece == PieceOptions.Queen || Grid[currentCell.File + f, currentCell.Rank - r].OccupiedBy.Piece == PieceOptions.Bishop) && (Grid[currentCell.File + f, currentCell.Rank - r].OccupiedBy.PieceColor != col))
                {
                    return true;
                }
                else if (Grid[currentCell.File + f, currentCell.Rank + r].Occupied && Grid[currentCell.File + f, currentCell.Rank + r].Occupied && Grid[currentCell.File + f, currentCell.Rank + r].OccupiedBy.Piece != PieceOptions.Queen && Grid[currentCell.File + f, currentCell.Rank + r].OccupiedBy.Piece != PieceOptions.Bishop)
                {
                    break;
                }
                f++;
                r++;
            }

            // diagonal up and left
            r = 1;
            f = 1;
            while (currentCell.File - f >= 0 & currentCell.Rank - r >= 0)
            {
                if ((Grid[currentCell.File - f, currentCell.Rank - r].OccupiedBy.Piece == PieceOptions.Queen || Grid[currentCell.File - f, currentCell.Rank - r].OccupiedBy.Piece == PieceOptions.Bishop) && (Grid[currentCell.File - f, currentCell.Rank - r].OccupiedBy.PieceColor != col))
                {
                    return true;
                }
                else if (Grid[currentCell.File - f, currentCell.Rank - r].Occupied && Grid[currentCell.File - f, currentCell.Rank - r].Occupied && Grid[currentCell.File - f, currentCell.Rank - r].OccupiedBy.Piece != PieceOptions.Queen && Grid[currentCell.File - f, currentCell.Rank - r].OccupiedBy.Piece != PieceOptions.Bishop)
                {
                    break;
                }
                f++;
                r++;
            }

            // diagonal up and right
            r = 1;
            f = 1;
            while (currentCell.File - f >= 0 & currentCell.Rank + r < Size)
            {
                if ((Grid[currentCell.File - f, currentCell.Rank + r].OccupiedBy.Piece == PieceOptions.Queen || Grid[currentCell.File - f, currentCell.Rank + r].OccupiedBy.Piece == PieceOptions.Bishop) && (Grid[currentCell.File - f, currentCell.Rank + r].OccupiedBy.PieceColor != col))
                {
                    return true;
                }
                else if (Grid[currentCell.File - f, currentCell.Rank + r].Occupied && Grid[currentCell.File - f, currentCell.Rank + r].Occupied && Grid[currentCell.File - f, currentCell.Rank + r].OccupiedBy.Piece != PieceOptions.Queen && Grid[currentCell.File - f, currentCell.Rank + r].OccupiedBy.Piece != PieceOptions.Bishop)
                {
                    break;
                }
                f++;
                r++;
            }

            //horizontal move right
            r = 1;
            while (currentCell.Rank + r < Size)
            {
                if ((Grid[currentCell.File, currentCell.Rank + r].OccupiedBy.Piece == PieceOptions.Queen || Grid[currentCell.File, currentCell.Rank + r].OccupiedBy.Piece == PieceOptions.Rook) && (Grid[currentCell.File, currentCell.Rank + r].OccupiedBy.PieceColor != col))
                {
                    return true;
                }
                else if (Grid[currentCell.File, currentCell.Rank + r].Occupied && Grid[currentCell.File, currentCell.Rank + r].Occupied && Grid[currentCell.File, currentCell.Rank + r].OccupiedBy.Piece != PieceOptions.Queen && Grid[currentCell.File, currentCell.Rank + r].OccupiedBy.Piece != PieceOptions.Bishop)
                {
                    break;
                }
                r++;
            }
            //horizontal move left
            r = 1;
            while (currentCell.Rank - r >= 0)
            {
                if ((Grid[currentCell.File, currentCell.Rank - r].OccupiedBy.Piece == PieceOptions.Queen || Grid[currentCell.File, currentCell.Rank - r].OccupiedBy.Piece == PieceOptions.Rook) && (Grid[currentCell.File, currentCell.Rank - r].OccupiedBy.PieceColor != col))
                {
                    return true;
                }
                else if (Grid[currentCell.File, currentCell.Rank - r].Occupied && Grid[currentCell.File, currentCell.Rank - r].Occupied && Grid[currentCell.File, currentCell.Rank - r].OccupiedBy.Piece != PieceOptions.Queen && Grid[currentCell.File, currentCell.Rank - r].OccupiedBy.Piece != PieceOptions.Bishop)
                {
                    break;
                }
                r++;
            }
            //vertical move down
            f = 1;
            while (currentCell.File + f < Size)
            {
                if ((Grid[currentCell.File + f, currentCell.Rank].OccupiedBy.Piece == PieceOptions.Queen || Grid[currentCell.File + f, currentCell.Rank].OccupiedBy.Piece == PieceOptions.Rook) && (Grid[currentCell.File + f, currentCell.Rank].OccupiedBy.PieceColor != col))
                {
                    return true;
                }
                else if (Grid[currentCell.File + f, currentCell.Rank].Occupied && Grid[currentCell.File + f, currentCell.Rank].Occupied && Grid[currentCell.File + f, currentCell.Rank].OccupiedBy.Piece != PieceOptions.Queen && Grid[currentCell.File + f, currentCell.Rank].OccupiedBy.Piece != PieceOptions.Bishop)
                {
                    break;
                }
                f++;
            }
            //vertical move up
            f = 1;
            while (currentCell.File - f >= 0)
            {
                if ((Grid[currentCell.File - f, currentCell.Rank].OccupiedBy.Piece == PieceOptions.Queen || Grid[currentCell.File - f, currentCell.Rank].OccupiedBy.Piece == PieceOptions.Rook) && (Grid[currentCell.File - f, currentCell.Rank].OccupiedBy.PieceColor != col))
                {
                    return true;
                }
                else if (Grid[currentCell.File - f, currentCell.Rank].Occupied && Grid[currentCell.File - f, currentCell.Rank].Occupied && Grid[currentCell.File - f, currentCell.Rank].OccupiedBy.Piece != PieceOptions.Queen && Grid[currentCell.File - f, currentCell.Rank].OccupiedBy.Piece != PieceOptions.Bishop)
                {
                    break;
                }
                f++;
            }

            //check for knight attacks
            if (currentCell.File + 2 < Size & currentCell.Rank + 1 < Size && Grid[currentCell.File + 2, currentCell.Rank + 1].OccupiedBy.Piece == PieceOptions.Knight && !(Grid[currentCell.File + 2, currentCell.Rank + 1].OccupiedBy.PieceColor == col))
            {
                return true;
            }
            if (currentCell.File + 2 < Size & currentCell.Rank - 1 >= 0 && Grid[currentCell.File + 2, currentCell.Rank - 1].OccupiedBy.Piece == PieceOptions.Knight && !(Grid[currentCell.File + 2, currentCell.Rank - 1].OccupiedBy.PieceColor == col))
            {
                return true;
            }
            if (currentCell.File - 2 >= 0 & currentCell.Rank + 1 < Size && Grid[currentCell.File - 2, currentCell.Rank + 1].OccupiedBy.Piece == PieceOptions.Knight && !(Grid[currentCell.File - 2, currentCell.Rank + 1].OccupiedBy.PieceColor == col))
            {
                return true;
            }
            if (currentCell.File - 2 >= 0 & currentCell.Rank - 1 >= 0 && Grid[currentCell.File - 2, currentCell.Rank - 1].OccupiedBy.Piece == PieceOptions.Knight && !(Grid[currentCell.File - 2, currentCell.Rank - 1].OccupiedBy.PieceColor == col))
            {
                return true;
            }
            if (currentCell.File + 1 < Size & currentCell.Rank + 2 < Size && Grid[currentCell.File + 1, currentCell.Rank + 2].OccupiedBy.Piece == PieceOptions.Knight && !(Grid[currentCell.File + 1, currentCell.Rank + 2].OccupiedBy.PieceColor == col))
            {
                return true;
            }
            if (currentCell.File + 1 < Size & currentCell.Rank - 2 >= 0 && Grid[currentCell.File + 1, currentCell.Rank - 2].OccupiedBy.Piece == PieceOptions.Knight && !(Grid[currentCell.File + 1, currentCell.Rank - 2].OccupiedBy.PieceColor == col))
            {
                return true;
            }
            if (currentCell.File - 1 >= 0 & currentCell.Rank + 2 < Size && Grid[currentCell.File - 1, currentCell.Rank + 2].OccupiedBy.Piece == PieceOptions.Knight && !(Grid[currentCell.File - 1, currentCell.Rank + 2].OccupiedBy.PieceColor == col))
            {
                return true;
            }
            if (currentCell.File - 1 >= 0 & currentCell.Rank - 2 >= 0 && Grid[currentCell.File - 1, currentCell.Rank - 2].OccupiedBy.Piece == PieceOptions.Knight && !(Grid[currentCell.File - 1, currentCell.Rank - 2].OccupiedBy.PieceColor == col))
            {
                return true;
            }

            //check for pawn attacks
            //for white color
            
            if (col == "white")
            {
                //pawn moving up left, so we check down right
                if (currentCell.File - 1 >= 0 && currentCell.Rank + 1 < Size && Grid[currentCell.File - 1, currentCell.Rank + 1].OccupiedBy.Piece == PieceOptions.Pawn && !(Grid[currentCell.File - 1, currentCell.Rank + 1].OccupiedBy.PieceColor == col))
                {
                    return true;
                }
                //pawn moving up right, so we check down left
                if (currentCell.File - 1 >= 0 && currentCell.Rank - 1 >= 0 && Grid[currentCell.File - 1, currentCell.Rank - 1].OccupiedBy.Piece == PieceOptions.Pawn && !(Grid[currentCell.File - 1, currentCell.Rank - 1].OccupiedBy.PieceColor == col))
                {
                    return true;
                }
            }
            //for black color
            else
            {
                //pawn moving down left, so we check up right
                if (currentCell.File + 1 < Size && currentCell.Rank + 1 < Size && Grid[currentCell.File + 1, currentCell.Rank + 1].OccupiedBy.Piece == PieceOptions.Pawn && !(Grid[currentCell.File + 1, currentCell.Rank + 1].OccupiedBy.PieceColor == col))
                {
                    return true;
                }
                //pawn moving down right, so we check up left
                if (currentCell.File + 1 < Size && currentCell.Rank - 1 >= 0 && Grid[currentCell.File + 1, currentCell.Rank - 1].OccupiedBy.Piece == PieceOptions.Pawn && !(Grid[currentCell.File + 1, currentCell.Rank - 1].OccupiedBy.PieceColor == col))
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean CheckMate(Cell currentCell)
        {
            if (currentCell.OccupiedBy.Piece == PieceOptions.King)
            {
                if (GetNextLegalMoves(currentCell.OccupiedBy.Piece.ToString(), currentCell) == false)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
