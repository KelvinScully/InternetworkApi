using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using ACommon.Objects;
using ACommon.Objects.Account;
using BusinessLogicLayer.Interfaces;
using Module.Shared.Objects.Account;
using Module.Shared.ManualMappings.Account;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace Module.Shared.Controllers.V1
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Route("V1/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Service for interacting with account-related Business Logic Layers
        /// </summary>
        private readonly IAccountBllService _accountBllService;
        
        
        /// <summary>
        /// Controller Constuctor
        /// </summary>
        public AccountController(IAccountBllService accountBllService)
        {
            _accountBllService = accountBllService;
        }

        [HttpGet("User")]
        [ProducesResponseType(typeof(ApiResult<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<UserDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResult<UserDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UserGet([FromQuery] UserGet user)
        {
            var result = await _accountBllService.UserGet(ManualMapping.ToDto(user));
            if (result.IsSuccessful)
                return Ok(result);

            return NotFound(result);
        }

     //   [HttpGet("User/{userId}")]
     //   [ProducesResponseType(typeof(ApiResult<UserDto>), StatusCodes.Status200OK)]
     //   [ProducesResponseType(typeof(ApiResult<UserDto>), StatusCodes.Status400BadRequest)]
     //   [ProducesResponseType(typeof(ApiResult<UserDto>), StatusCodes.Status404NotFound)]
     //   public async Task<IActionResult> UserGet(int userId)
     //   {
     //       var user = new UserGet() { UserId = userId };
     //       var result = await _accountBllService.UserGet(ManualMapping.ToDto(user));
     //       if (result.IsSuccessful)
     //           return Ok(result);
     //
     //       return NotFound(result);
     //   }

        [HttpPost("User")]
        [ProducesResponseType(typeof(ApiResult<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<UserDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResult<UserDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UserInsert([FromQuery] UserInsert user)
        {
            //var result = await _accountBllService.UserGet(ManualMapping.ToDto(user));
            //if (result.IsSuccessful)
            //    return Ok(result);

            return NotFound();
        }

        [HttpPut("User")]
        [ProducesResponseType(typeof(ApiResult<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<UserDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResult<UserDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UserUpdate([FromQuery] UserUpdate user)
        {
            //var result = await _accountBllService.UserGet(ManualMapping.ToDto(user));
            //if (result.IsSuccessful)
            //    return Ok(result);

            return NotFound();
        }
    }
}
