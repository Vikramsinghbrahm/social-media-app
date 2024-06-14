using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Like
{
    [Key]
    public int LikeID { get; set; }
    public int? PostID { get; set; }
    [ForeignKey("PostID")]
    public Post? Post { get; set; }
    public int? CommentID { get; set; }
    [ForeignKey("CommentID")]
    public Comment? Comment { get; set; }
    public int UserID { get; set; }
    [ForeignKey("UserID")]
    public User User { get; set; } = null!;
    public DateTime Timestamp { get; set; }
}


