using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Comment
{
    [Key]
    public int CommentID { get; set; }
    public int PostID { get; set; }
    [ForeignKey("PostID")]
    public Post Post { get; set; } = null!;
    public int UserID { get; set; }
    [ForeignKey("UserID")]
    public User User { get; set; } = null!;
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public ICollection<Like> Likes { get; set; } = new List<Like>();
}
