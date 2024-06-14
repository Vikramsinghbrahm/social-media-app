using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class FriendshipsController : ControllerBase
{
    private readonly SocialMediaContext _context;

    public FriendshipsController(SocialMediaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Friendship>>> GetFriendships()
    {
        return await _context.Friendships.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Friendship>> GetFriendship(int id)
    {
        var friendship = await _context.Friendships.FindAsync(id);

        if (friendship == null)
        {
            return NotFound();
        }

        return friendship;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutFriendship(int id, Friendship friendship)
    {
        if (id != friendship.FriendshipID)
        {
            return BadRequest();
        }

        _context.Entry(friendship).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FriendshipExists(id))
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
    public async Task<ActionResult<Friendship>> PostFriendship(Friendship friendship)
    {
        _context.Friendships.Add(friendship);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetFriendship", new { id = friendship.FriendshipID }, friendship);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFriendship(int id)
    {
        var friendship = await _context.Friendships.FindAsync(id);
        if (friendship == null)
        {
            return NotFound();
        }

        _context.Friendships.Remove(friendship);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool FriendshipExists(int id)
    {
        return _context.Friendships.Any(e => e.FriendshipID == id);
    }
}
