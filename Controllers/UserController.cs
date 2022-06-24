using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoList;
namespace todoList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly Context _context;

    public UserController(ILogger<UserController> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        Console.WriteLine("IN GET METHOD I need change");
        return await _context.User.ToListAsync();
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<User>> GetOne(int id)
    {
        var newUser = await _context.User.FirstOrDefaultAsync(c => c.Id == id);
        if(newUser == null)
        {
            return NotFound();
        }
        return Ok(newUser);
    }

    [HttpPost]
    public async Task<ActionResult<ToDo>> PostUser(User user)
    {
        if (!ModelState.IsValid) 
        {
            return BadRequest();
        }
        _context.User.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOne), new { id = user.Id }, user );
    }
}
