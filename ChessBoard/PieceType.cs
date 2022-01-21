using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard
{
    public enum PieceOptions
    {
        Rook,
        Knight,
        Bishop,
        Queen,
        King,
        Pawn,
        Empty
    }
    public class PieceType
    {
        public PieceOptions Piece { get; set; }
        public string PieceColor { get; set; }

        public PieceType(PieceOptions p, string col)
        {
            Piece = p;
            PieceColor = col;
        }

    }
}
