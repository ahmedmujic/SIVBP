using Microsoft.EntityFrameworkCore;

namespace SIVBP_Keširanje.Models
{
    public partial class SIVBPContext : DbContext
    {
        public SIVBPContext()
        {
        }

        public SIVBPContext(DbContextOptions<SIVBPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostLink> PostLinks { get; set; } = null!;
        public virtual DbSet<PostType> PostTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=127.0.0.1,5434;Database=SIVBP;User Id=sa;Password=b9L9!D8Q*f9wYbPR;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Text).HasMaxLength(700);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Comments_Users");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CommunityOwnedDate).HasColumnType("datetime");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.LastActivityDate).HasColumnType("datetime");

                entity.Property(e => e.LastEditDate).HasColumnType("datetime");

                entity.Property(e => e.LastEditorDisplayName).HasMaxLength(40);

                entity.Property(e => e.Tags).HasMaxLength(150);

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.HasOne(d => d.OwnerUser)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.OwnerUserId)
                    .HasConstraintName("fk_posts_user");
            });

            modelBuilder.Entity<PostLink>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PostType>(entity =>
            {
                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DisplayName).HasMaxLength(40);

                entity.Property(e => e.EmailHash).HasMaxLength(40);

                entity.Property(e => e.LastAccessDate).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(100);

                entity.Property(e => e.WebsiteUrl).HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
