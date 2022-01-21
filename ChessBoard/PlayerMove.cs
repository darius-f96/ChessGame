namespace ChessBoard
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlayerMove")]
    public partial class PlayerMove
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public int? FromFile { get; set; }

        public int? FromRank { get; set; }

        public int? ToFile { get; set; }

        public int? ToRank { get; set; }

        public int GameId { get; set; }

        [StringLength(10)]
        public string PieceName { get; set; }

        public virtual Game Game { get; set; }

        public virtual Player Player { get; set; }

    }
}
