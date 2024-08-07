﻿using Microsoft.AspNetCore.Mvc;
using TimeTracking.App.Person.Domain.Interface;
using TimeTracking.App.Person.Application.Query;
using MediatR;
using TimeTracking.App.Person.Infrastructure.Service;
using TimeTracking.App.Person.Application.Command;
using TimeTracking.App.Person.Domain.Model;

namespace TimeTracking.App.Person.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly PersonRepositoryInterface _personRepository;
    private readonly IMediator _mediator;
    private readonly JwtTokenService _jwtTokenService;

    public RegistrationController(
        PersonRepositoryInterface personRepository,
        IMediator mediator,
        JwtTokenService jwtTokenService
    )
    {
        _personRepository = personRepository;
        _mediator = mediator;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (registerModel.Password != registerModel.ConfirmPassword) return BadRequest("Passwords do not match");

        var person = await _mediator.Send(
            new RegisterCommand(
               registerModel.Email,
               registerModel.Password,
               registerModel.FirstName,
               registerModel.LastName
            )
        );

        var token = _jwtTokenService.GetToken(person);

        return Ok(
            new { 
                Token = token,
                FirstName = person.FirstName,
                LastName = person.LastName
            }
        );
    }
}

