namespace ChessBoard
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlayerTake")]
    public partial class PlayerTake
    {
        public int Id { get; set; }

        public int TakerId { get; set; }

        public int TakenId { get; set; }

        [Required]
        [StringLength(10)]
        public string PieceNameTaken { get; set; }

        [StringLength(10)]
        public string Color { get; set; }

        public int GameId { get; set; }

        [StringLength(10)]
        public string PieceNameTaker { get; set; }

        public virtual Game Game { get; set; }

        public virtual Player Player { get; set; }
    }
}
