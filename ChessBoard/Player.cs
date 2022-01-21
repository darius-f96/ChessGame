namespace ChessBoard
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Player")]
    public partial class Player
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Player()
        {
            PlayerMoves = new HashSet<PlayerMove>();
            PlayerTakes = new HashSet<PlayerTake>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string PlayerName { get; set; }

        public int? PlayerWins { get; set; }

        [StringLength(50)]
        public string LastPlayedColor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerMove> PlayerMoves { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerTake> PlayerTakes { get; set; }
    }
}
