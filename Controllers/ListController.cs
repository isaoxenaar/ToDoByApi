using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoList;
namespace todoList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListController : ControllerBase
{
    private readonly ILogger<ListController> _logger;
    private readonly Context _context;

    public ListController(ILogger<ListController> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TdList>>> Get()
    {
        return await _context.TdList.ToListAsync();
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<TdList>> GetOne(int id)
    {
        var newList = await _context.TdList.FirstOrDefaultAsync(c => c.Id == id);
        if(newList == null)
        {
            return NotFound();
        }
        return Ok(newList);
    }

    [HttpPost]
    public async Task<ActionResult<TdList>> PostList(TdList list)
    {
        if (!ModelState.IsValid) 
        {
            return BadRequest();
        }
        _context.TdList.Add(list);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOne), new { id = list.Id }, list );
    }
}

//usercontroller -> create, get
//todolist controller -> create, get
//subtodo controller -> create, get