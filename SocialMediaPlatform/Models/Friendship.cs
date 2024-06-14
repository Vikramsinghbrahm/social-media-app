using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Friendship
{
    [Key]
    public int FriendshipID { get; set; }
    public int UserID1 { get; set; }
    [ForeignKey("UserID1")]
    public User User1 { get; set; } = null!;
    public int UserID2 { get; set; }
    [ForeignKey("UserID2")]
    public User User2 { get; set; } = null!;
    public DateTime Timestamp { get; set; }
}

