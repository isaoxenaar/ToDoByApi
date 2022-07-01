using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoList.Data;
using todoList.DTO;
using todoList.Jwt;
namespace todoList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly Context _context;
    private readonly IUserRepository _repository;
    private readonly JwtService _jwtservice;

    public AuthController(ILogger<AuthController> logger, Context context, IUserRepository repository, JwtService jwtService)
    {
        _logger = logger;
        _context = context;
        _repository = repository;
        _jwtservice = jwtService;
    }

    [HttpPost("register")]

    public async Task<ActionResult<User>> Register(RegisterDto dto)
    {
        var user = new User {

            Name = dto.Name,
            Email = dto.Email,
            Password =BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };
        _context.User.Add(user);
        await _context.SaveChangesAsync();
        _repository.Create(user);
        return Created("succes", user);
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
            message = jwt
        });
    }

    [HttpGet]
    public IActionResult getUser()
    {
        try{
            var jwt = Request.Cookies["jwt"];
            if(jwt == null) {
                return BadRequest();
            }
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
}

    