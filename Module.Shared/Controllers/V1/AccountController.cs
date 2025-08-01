﻿using ACommon.Objects;
using Asp.Versioning;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Objects.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace Module.Shared.Controllers.V1
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V1/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Service for interacting with account-related Business Logic Layers
        /// </summary>
        private readonly IAccountBllService _accountBllService;
        public AccountController(IAccountBllService accountBllService)
        {
            _accountBllService = accountBllService;
        }

        [HttpPost("Register")]
        [ProducesResponseType(typeof(ApiResult<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<User>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResult<User>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] Register user)
        {
            var bllResult = await _accountBllService.Register(user);
            switch (bllResult.HttpStatusCode)
            {
                case ACommon.Objects.StatusCodes.Status200Ok:
                    return Ok(bllResult);
                case ACommon.Objects.StatusCodes.Status400BadRequest:
                    return BadRequest(bllResult);
                case ACommon.Objects.StatusCodes.Status500InternalServerError:
                    return StatusCode(StatusCodes.Status500InternalServerError, bllResult);
                default:
                    return NotFound();
            }

        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(ApiResult<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<User>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResult<User>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] Authenticate user)
        {
            var bllResult = await _accountBllService.Authenticate(user);
            switch (bllResult.HttpStatusCode)
            {
                case ACommon.Objects.StatusCodes.Status200Ok:
                    return Ok(bllResult);
                case ACommon.Objects.StatusCodes.Status400BadRequest:
                    return BadRequest(bllResult);
                default:
                    return NotFound();
            }
        }

        //[Authorize]
        //[HttpGet("UserGetById")]
        //[ProducesResponseType(typeof(ApiResult<User>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ApiResult<User>), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ApiResult<User>), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> UserGetById([FromQuery] UserGetById user)
        //{
        //    return NotFound();
        //}
        //
        //[Authorize]
        //[HttpGet("UsersGetByIds")]
        //[ProducesResponseType(typeof(ApiResult<List<User>>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ApiResult<List<User>>), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ApiResult<List<User>>), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> UsersGetByIds([FromQuery] List<UserGetById> user)
        //{
        //    return NotFound();
        //}

        //[Authorize]
        //[HttpPut("User")]
        //[ProducesResponseType(typeof(ApiResult<User>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ApiResult<User>), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ApiResult<User>), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> UserUpdate([FromQuery] UserUpdate user)
        //{
        //    return NotFound();
        //}
    }
}
