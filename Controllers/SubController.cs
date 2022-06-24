using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using todoList;
namespace todoList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubController : ControllerBase
{
    private readonly ILogger<SubController> _logger;
    private readonly Context _context;

    public SubController(ILogger<SubController> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubTd>>> Get()
    {
        return await _context.SubTd.ToListAsync();
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<SubTd>> GetOne(int id)
    {
        var newSub = await _context.SubTd.FirstOrDefaultAsync(c => c.Id == id);
        if(newSub == null)
        {
            return NotFound();
        }
        return Ok(newSub);
    }

    [HttpPost]
    public async Task<ActionResult<SubTd>> PostSub(SubTd sub)
    {
        if (!ModelState.IsValid) 
        {
            return BadRequest();
        }
        _context.SubTd.Add(sub);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOne), new { id = sub.Id }, sub );
    }
}

//usercontroller -> create, get
//todolist controller -> create, get
//subtodo controller -> create, get