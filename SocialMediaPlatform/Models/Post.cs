using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Post
{
    [Key]
    public int PostID { get; set; }
    public int UserID { get; set; }
    [ForeignKey("UserID")]
    public User User { get; set; } = null!;
    public string Content { get; set; } = string.Empty;
    public string MediaType { get; set; } = string.Empty;
    public string MediaURL { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
}

