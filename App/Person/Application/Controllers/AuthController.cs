using Microsoft.AspNetCore.Mvc;
using TimeTracking.App.Person.Domain.Interface;
using TimeTracking.App.Person.Application.Query;
using MediatR;
using TimeTracking.App.Person.Infrastructure.Service;
using TimeTracking.App.Person.Domain.Model;
using TimeTracking.App.Base.Controllers;

namespace TimeTracking.App.Person.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly PersonRepositoryInterface _personRepository;
    private readonly IMediator _mediator;
    private readonly JwtTokenService _jwtTokenService;
    private readonly SafeExecutorInterface _safeExecutor;

    public AuthController(
        PersonRepositoryInterface personRepository,
        IMediator mediator,
        JwtTokenService jwtTokenService,
        SafeExecutorInterface safeExecutor
    )
    {
        _personRepository = personRepository;
        _mediator = mediator;
        _jwtTokenService = jwtTokenService;
        _safeExecutor = safeExecutor;
    }

    [HttpPost("login")]
    public Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        return _safeExecutor.ExecuteSafe(async () =>
        {
            var person = await _mediator.Send(new FindPersonByEmailQuery(loginModel.Email));

            if (person == null || !BCrypt.Net.BCrypt.Verify(loginModel.Password, person.Password))
            {
                return Unauthorized("Email or password are not correct.");
            }

            var token = _jwtTokenService.GetToken(person);
            return Ok(new { Token = token });
        });
    }
}
