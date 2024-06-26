using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly SocialMediaContext _context;

    public MessagesController(SocialMediaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
    {
        return await _context.Messages.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Message>> GetMessage(int id)
    {
        var message = await _context.Messages.FindAsync(id);

        if (message == null)
        {
            return NotFound();
        }

        return message;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMessage(int id, Message message)
    {
        if (id != message.MessageID)
        {
            return BadRequest();
        }

        _context.Entry(message).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MessageExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Message>> PostMessage(Message message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMessage", new { id = message.MessageID }, message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMessage(int id)
    {
        var message = await _context.Messages.FindAsync(id);
        if (message == null)
        {
            return NotFound();
        }

        _context.Messages.Remove(message);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MessageExists(int id)
    {
        return _context.Messages.Any(e => e.MessageID == id);
    }
}
