using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoList;
namespace todoList.Controllers;

[ApiController]
[Route("[controller]")]
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
        return await _context.ToDos.ToListAsync();
    }
}
