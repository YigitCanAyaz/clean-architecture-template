using CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Presentation.Controllers;

public sealed class AuthsController : ApiController
{
    public AuthsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register(RegisterCommand request, CancellationToken cancellationToken)
    {
        MessageResponse response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
