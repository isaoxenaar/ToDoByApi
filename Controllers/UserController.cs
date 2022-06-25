using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoList.Data;
using todoList.DTO;
using todoList.Jwt;
namespace todoList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly Context _context;
    private readonly IUserRepository _repository;
    private readonly JwtService _jwtservice;

    public UserController(ILogger<UserController> logger, Context context, IUserRepository repository, JwtService jwtService)
    {
        _logger = logger;
        _context = context;
        _repository = repository;
        _jwtservice = jwtService;
    }

    [HttpPost("register")]

    public IActionResult Register(RegisterDto dto)
    {
        System.Console.WriteLine("hello from register");
        var user = new User {

            Name = dto.Name,
            Email = dto.Email,
            Password =BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };
        _repository.Create(user);
        return Created("succes", _repository.Create(user));
    }

    [HttpPost("login")]

    public IActionResult Login(LoginDto dto)
    {
        var user = _repository.GetByEmail(dto.Email);

        if(user == null)
            return BadRequest(new {message="user does not exist"});

        if(!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            return BadRequest(new {message="wrong password"});
        }

        var jwt = _jwtservice.Generate(user.Id);

        CookieOptions cookieOptions = new CookieOptions
        {
            HttpOnly = true
        };
        
        Response.Cookies.Append("jwt", jwt, cookieOptions);

        return Ok(new {
            message = "succes"
        });
        
    }

    [HttpGet("user")]
    public IActionResult User()
    {
        try{
            var jwt = Request.Cookies["jwt"];
            var token = _jwtservice.Verify(jwt);
            int userId = int.Parse(token.Issuer);
            var user = _repository.GetById(userId);
            return Ok(user);
        }
        catch(Exception e){
            return Unauthorized();
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");
        return Ok(new {
            message = "logged out"
        });
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
