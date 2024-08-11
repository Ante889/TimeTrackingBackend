using Microsoft.AspNetCore.Mvc;
using TimeTracking.App.Person.Domain.Interface;
using TimeTracking.App.Person.Application.Query;
using MediatR;
using TimeTracking.App.Person.Infrastructure.Service;
using TimeTracking.App.Person.Domain.Model;

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
        var person = await _mediator.Send(new FindPersonByEmailQuery(loginModel.Email));

        try
        {
            if (person == null || !BCrypt.Net.BCrypt.Verify(loginModel.Password, person.Password))
            {
                return Unauthorized("Email or password are not correct.");
            }
        }
        catch (BCrypt.Net.SaltParseException)
        {
            return Unauthorized("An error occurred while processing your password. Please try again later.");
        }

        var token = _jwtTokenService.GetToken(person);
        return Ok(new { Token = token });
    }
}