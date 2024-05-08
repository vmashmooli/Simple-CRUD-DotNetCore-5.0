using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services.Service;
using Domain.Common.Dto.User;
using Domain.Common.Helper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDto add, CancellationToken cancellationToken)
        {
            var result = await _service.AddService(add, cancellationToken);
            return new Response<bool>().ResponseSending(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserDto edit, CancellationToken cancellationToken)
        {
            var result = await _service.EditService(edit, cancellationToken);
            return new Response<bool>().ResponseSending(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int userId, CancellationToken cancellationToken)
        {
            var result = await _service.DeleteService(userId, cancellationToken);
            return new Response<bool>().ResponseSending(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _service.GetService(cancellationToken);
            return new Response<List<UserDto>>().ResponseSending(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(int userId, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdService(userId, cancellationToken);
            return new Response<UserDto>().ResponseSending(result);
        }
    }
}