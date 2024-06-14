using Microsoft.EntityFrameworkCore;

public class SocialMediaContext : DbContext
{
    public SocialMediaContext(DbContextOptions<SocialMediaContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Friendship> Friendships { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User-Friendship relationships
        modelBuilder.Entity<Friendship>()
            .HasOne(f => f.User1)
            .WithMany(u => u.Friendships)
            .HasForeignKey(f => f.UserID1)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Friendship>()
            .HasOne(f => f.User2)
            .WithMany()
            .HasForeignKey(f => f.UserID2)
            .OnDelete(DeleteBehavior.Restrict);

        // User-Post relationship
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        // User-Comment relationship
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserID)
            .OnDelete(DeleteBehavior.Restrict);

        // Post-Comment relationship
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostID)
            .OnDelete(DeleteBehavior.Cascade);

        // User-Like relationship
        modelBuilder.Entity<Like>()
            .HasOne(l => l.User)
            .WithMany(u => u.Likes)
            .HasForeignKey(l => l.UserID)
            .OnDelete(DeleteBehavior.Restrict);

        // Post-Like relationship
        modelBuilder.Entity<Like>()
            .HasOne(l => l.Post)
            .WithMany(p => p.Likes)
            .HasForeignKey(l => l.PostID)
            .OnDelete(DeleteBehavior.Restrict); // Changed from Cascade to Restrict

        // Comment-Like relationship
        modelBuilder.Entity<Like>()
            .HasOne(l => l.Comment)
            .WithMany(c => c.Likes)
            .HasForeignKey(l => l.CommentID)
            .OnDelete(DeleteBehavior.Cascade);

        // User-Message relationship
        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Receiver)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(m => m.ReceiverID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
