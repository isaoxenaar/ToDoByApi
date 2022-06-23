using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoList;
namespace todoList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    private readonly ILogger<ToDoController> _logger;
    private readonly Context _context;

    public ToDoController(ILogger<ToDoController> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDo>>> Get()
    {
        Console.WriteLine("IN GET METHOD I need change");
        return await _context.ToDo.ToListAsync();
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<ToDo>> GetOne(int id)
    {
        var newTodo = await _context.ToDo.FirstOrDefaultAsync(c => c.Id == id);
        if(newTodo == null)
        {
            return NotFound();
        }
        return Ok(newTodo);
    }

    [HttpPost]
    public async Task<ActionResult<ToDo>> PostToDo(ToDo todo)
    {
        if (!ModelState.IsValid) 
        {
            return BadRequest();
        }
        _context.ToDo.Add(todo);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOne), new { id = todo.Id }, todo );
    }
}
