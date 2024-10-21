using Microsoft.AspNetCore.Mvc;
using TimeTracking.App.Person.Domain.Interface;
using MediatR;
using TimeTracking.App.Person.Infrastructure.Service;
using TimeTracking.App.Person.Application.Command;
using TimeTracking.App.Person.Domain.Model;
using TimeTracking.App.Person.Application.Query;
using TimeTracking.App.Base.Controllers;

namespace TimeTracking.App.Person.Application.Controllers;

[ApiController]
[Route("api/v1/")]
public class RegistrationController : ControllerBase
{
    private readonly PersonRepositoryInterface _personRepository;
    private readonly IMediator _mediator;
    private readonly JwtTokenService _jwtTokenService;
    private readonly SafeExecutorInterface _safeExecutor;

    public RegistrationController(
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

    [HttpPost("registration")]
    public Task<IActionResult> Register([FromBody] RegisterModel registerModel)
    {
        return _safeExecutor.ExecuteSafe(async () =>
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (registerModel.Password != registerModel.ConfirmPassword) return BadRequest("Passwords do not match");

            var existingPerson = await _mediator.Send(new FindPersonByEmailQuery(registerModel.Email));
            if (existingPerson != null) return BadRequest("Email already in use");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerModel.Password);

            var person = await _mediator.Send(new RegisterCommand(
                registerModel.Email,
                hashedPassword,
                registerModel.FirstName,
                registerModel.LastName
            ));

            var token = _jwtTokenService.GetToken(person);

            return Ok(new
            {
                Token = token,
                firstName = person.FirstName,
                lastName = person.LastName,
                id = person.Id
            });
        });
    }
}
