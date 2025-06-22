using ACommon.Objects;
using Asp.Versioning;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Shared.Objects.Account;
using System.Net.Mime;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace Module.Shared.Controllers.V1
{
    /// <summary>
    /// Test Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("V1/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Service for interacting with account-related Business Logic Layers
        /// </summary>
        private readonly ITestBllService _testBllService;
        public TestController(ITestBllService testBllService)
        {
            _testBllService = testBllService;
        }

        [HttpGet("Item/{testItemId}")]
        [ProducesResponseType(typeof(ApiResult<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<User>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResult<User>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ItemGet(int testItemId)
        {
            var result = await _testBllService.TestItemGet(testItemId);
            if (result.IsSuccessful)
                return Ok(result);

            return NotFound(result);
        }
    }
}
