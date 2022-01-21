using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ChessBoard
{
    public partial class ChessGameEntitiesModel : DbContext
    {
        public ChessGameEntitiesModel()
            : base("name=ChessGameEntitiesModel")
        {
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerMove> PlayerMoves { get; set; }
        public virtual DbSet<PlayerTake> PlayerTakes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasMany(e => e.PlayerTakes)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.TakerId);

            modelBuilder.Entity<PlayerMove>()
                .Property(e => e.PieceName)
                .IsFixedLength();

            modelBuilder.Entity<PlayerTake>()
                .Property(e => e.PieceNameTaken)
                .IsFixedLength();

            modelBuilder.Entity<PlayerTake>()
                .Property(e => e.Color)
                .IsFixedLength();

            modelBuilder.Entity<PlayerTake>()
                .Property(e => e.PieceNameTaker)
                .IsFixedLength();
        }
    }
}
