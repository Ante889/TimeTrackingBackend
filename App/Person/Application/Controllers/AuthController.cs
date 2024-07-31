using Microsoft.AspNetCore.Mvc;
using TimeTracking.App.Person.Domain.Interface;
using TimeTracking.App.Person.Application.Query;
using MediatR;
using TimeTracking.App.Person.Infrastructure.Service;

namespace TimeTracking.App.Person.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly PersonRepositoryInterface _personRepository;
    private readonly IMediator _mediator;
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(
        PersonRepositoryInterface personRepository,
        IMediator mediator,
        JwtTokenService jwtTokenService
    )
    {
        _personRepository = personRepository;
        _mediator = mediator;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        var query = new FindPersonByEmailQuery(loginModel.Email);
        var person = await _mediator.Send(query);

        if (person == null || !BCrypt.Net.BCrypt.Verify(loginModel.Password, person.Password))
        {
            return Unauthorized("Email or password are not correct.");
        }

        var token = _jwtTokenService.GetToken(person);
        return Ok(new { Token = token });
    }
}

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
