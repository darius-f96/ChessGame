namespace ChessBoard
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Game")]
    public partial class Game
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Game()
        {
            PlayerMoves = new HashSet<PlayerMove>();
            PlayerTakes = new HashSet<PlayerTake>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string GameName { get; set; }

        public int? WinnerId { get; set; }

        public int Player1Id { get; set; }

        public int Player2Id { get; set; }

        [StringLength(50)]
        public string Player1Color { get; set; }

        [StringLength(50)]
        public string Player2Color { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerMove> PlayerMoves { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerTake> PlayerTakes { get; set; }
    }
}
