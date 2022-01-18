using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard
{
    public class Cell
    {
        public int Rank { get; set; }
        public int File { get; set; }
        public bool Occupied { get; set; }
        public bool LegalMove { get; set; }

        public Cell (int f, int r)
        {
            Rank = r;
            File = f;
        }
    }
}
