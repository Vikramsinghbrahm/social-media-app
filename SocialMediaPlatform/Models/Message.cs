using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Message
{
    [Key]
    public int MessageID { get; set; }
    public int SenderID { get; set; }
    [ForeignKey("SenderID")]
    public User Sender { get; set; } = null!;
    public int ReceiverID { get; set; }
    [ForeignKey("ReceiverID")]
    public User Receiver { get; set; } = null!;
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
