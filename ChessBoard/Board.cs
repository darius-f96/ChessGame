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
  

        public void GetNextLegalMoves(string Piece, Cell currentCell)
        {
            //for simplicity define them here, since we are going to use them everywhere in this method
            int f, r;

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
                    //diagonal up and right
                    r = 0;
                    f = 0;
                    while (f < Size - 1 & r < Size - 1 & Grid[currentCell.File + f, currentCell.Rank + r].Occupied == false)
                    {
                        Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                        r++;
                        f++;
                    }
                    Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;

                    //diagonal up and left
                    r = currentCell.Rank;
                    f = 0;
                    while (f < Size - 1 & r > 0 & Grid[currentCell.File + f, currentCell.Rank + r].Occupied == false)
                    {
                        Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                        f++;
                        r--;
                    }
                    Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;

                    // diagonal down and left
                    r = currentCell.Rank;
                    f = currentCell.File;
                    while (f > 0 & r > 0 & Grid[currentCell.File + f, currentCell.Rank + r].Occupied == false)
                    {
                        Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                        f--;
                        r--;
                    }
                    Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;

                    // diagonal down and right
                    r = 0;
                    f = currentCell.File;
                    while (f > 0 & r < Size - 1 & Grid[currentCell.File + f, currentCell.Rank + r].Occupied == false)
                    {
                        Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                        f--;
                        r++;
                    }
                    Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                    //horizontal move+
                    r = 0;
                    while (r < Size - 1 & Grid[currentCell.File, currentCell.Rank + r].Occupied == false)
                    {
                        Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                        r++;
                    }
                    Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                    //horizontal move-
                    r = currentCell.Rank;
                    while (r > 0 & Grid[currentCell.File, currentCell.Rank - r].Occupied == false)
                    {
                        Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                        r--;
                    }
                    Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                    //vertical move+
                    f = 0;
                    while (f < Size - 1 & Grid[currentCell.File + f, currentCell.Rank].Occupied == false)
                    {
                        Grid[currentCell.File + f, currentCell.Rank].LegalMove = true;
                        f++;
                    }
                    Grid[currentCell.File + f, currentCell.Rank].LegalMove = true;
                    //vertical move-
                    f = currentCell.File;
                    while (f > 0 & Grid[currentCell.File + f, currentCell.Rank].Occupied == false)
                    {
                        Grid[currentCell.File + f, currentCell.Rank].LegalMove = true;
                        f--;
                    }
                    Grid[currentCell.File + f, currentCell.Rank].LegalMove = true;
                    break;
                //King moves 1 cell any direction, same for take
                case "King":
                    if (currentCell.File + 1 < Size & currentCell.Rank + 1 < Size)
                    {
                        Grid[currentCell.File + 1, currentCell.Rank + 1].LegalMove = true;
                    }
                    if (currentCell.File + 1 < Size)
                    {
                        Grid[currentCell.File + 1, currentCell.Rank + 0].LegalMove = true;
                    }
                    if (currentCell.File + 1 < Size & currentCell.Rank - 1 >= 0)
                    {
                        Grid[currentCell.File + 1, currentCell.Rank - 1].LegalMove = true;
                    }
                    if (currentCell.File - 1 >= 0)
                    {
                        Grid[currentCell.File - 1, currentCell.Rank + 0].LegalMove = true;
                    }
                    if (currentCell.File - 1 >= 0 & currentCell.Rank - 1 >= 0)
                    {
                        Grid[currentCell.File - 1, currentCell.Rank - 1].LegalMove = true;
                    }
                    if (currentCell.File - 1 >= 0 & currentCell.Rank + 1 < Size)
                    {
                        Grid[currentCell.File - 1, currentCell.Rank + 1].LegalMove = true;
                    }
                    if (currentCell.Rank + 1 < Size)
                    {
                        Grid[currentCell.File + 0, currentCell.Rank + 1].LegalMove = true;
                    }
                    if (currentCell.Rank - 1 >= 0)
                    {
                        Grid[currentCell.File + 0, currentCell.Rank - 1].LegalMove = true;
                    }

                    break;
                //Bishop moves diagonal, same for take
                case "Bisop":
                    //diagonal up and right
                    r = 0;
                    f = 0;
                    while (f < Size - 1 & r < Size - 1 & Grid[currentCell.File + f, currentCell.Rank + r].Occupied == false)
                    {
                        Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                        r++;
                        f++;
                    }
                    Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;

                    //diagonal up and left
                    r = currentCell.Rank;
                    f = 0;
                    while (f < Size - 1 & r > 0 & Grid[currentCell.File + f, currentCell.Rank + r].Occupied == false)
                    {
                        Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                        f++;
                        r--;
                    }
                    Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;

                    // diagonal down and left
                    r = currentCell.Rank;
                    f = currentCell.File;
                    while (f > 0 & r > 0 & Grid[currentCell.File + f, currentCell.Rank + r].Occupied == false)
                    {
                        Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                        f--;
                        r--;
                    }
                    Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;

                    // diagonal down and right
                    r = 0;
                    f = currentCell.File;
                    while (f > 0 & r < Size - 1 & Grid[currentCell.File + f, currentCell.Rank + r].Occupied == false)
                    {
                        Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                        f--;
                        r++;
                    }
                    Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;

                    break;

                //Knight moves L shaped forms, same for take
                case "Knight":
                    if (currentCell.File + 2 < Size & currentCell.Rank + 1 < Size)
                    {
                        Grid[currentCell.File + 2, currentCell.Rank + 1].LegalMove = true;
                    }
                    if (currentCell.File + 2 < Size & currentCell.Rank - 1 >= 0)
                    {
                        Grid[currentCell.File + 2, currentCell.Rank - 1].LegalMove = true;
                    }
                    if (currentCell.File - 2 >= 0 & currentCell.Rank + 1 < Size)
                    {
                        Grid[currentCell.File - 2, currentCell.Rank + 1].LegalMove = true;
                    }
                    if (currentCell.File - 2 >= 0 & currentCell.Rank - 1 >= 0)
                    {
                        Grid[currentCell.File - 2, currentCell.Rank - 1].LegalMove = true;
                    }
                    if (currentCell.File + 1 < Size & currentCell.Rank + 2 < Size)
                    {
                        Grid[currentCell.File + 1, currentCell.Rank + 2].LegalMove = true;
                    }
                    if (currentCell.File + 1 < Size & currentCell.Rank - 2 >= 0)
                    {
                        Grid[currentCell.File + 1, currentCell.Rank - 2].LegalMove = true;
                    }
                    if (currentCell.File - 1 >= 0 & currentCell.Rank + 2 < Size)
                    {
                        Grid[currentCell.File - 1, currentCell.Rank + 2].LegalMove = true;
                    }
                    if (currentCell.File - 1 >= 0 & currentCell.Rank - 2 >= 0)
                    {
                        Grid[currentCell.File - 1, currentCell.Rank - 2].LegalMove = true;
                    }

                    break;
                //Rook moves horizontal and vertical, can take only horizontal and vertical
                case "Rook":
                    //horizontal move+
                    r = 0;
                    while (r < Size - 1 & Grid[currentCell.File, currentCell.Rank + r].Occupied == false)
                    {
                        Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                        r++;
                    }
                    Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                    //horizontal move-
                    r = currentCell.Rank;
                    while (r > 0 & Grid[currentCell.File, currentCell.Rank - r].Occupied == false)
                    {
                        Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                        r--;
                    }
                    Grid[currentCell.File, currentCell.Rank + r].LegalMove = true;
                    //vertical move+
                    f = 0;
                    while (f < Size - 1 & Grid[currentCell.File + f, currentCell.Rank].Occupied == false)
                    {
                        Grid[currentCell.File + f, currentCell.Rank].LegalMove = true;
                        f++;
                    }
                    Grid[currentCell.File + f, currentCell.Rank].LegalMove = true;
                    //vertical move-
                    f = currentCell.File;
                    while (f > 0 & Grid[currentCell.File + f, currentCell.Rank].Occupied == false)
                    {
                        Grid[currentCell.File + f, currentCell.Rank].LegalMove = true;
                        f--;
                    }
                    Grid[currentCell.File + f, currentCell.Rank].LegalMove = true;
                    break;
                //Pawn moves front +2 or +1 if it's the first move (so to half of the board), can take only diagonal
                case "Pawn":
                    break;
                default:
                    break;

            }
        }
    }
}
