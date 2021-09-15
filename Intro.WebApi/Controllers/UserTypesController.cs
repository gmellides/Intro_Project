using Intro.Models.DTO;
using Intro.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intro.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypesController : ControllerBase
    {
        private readonly ILogger<UserTypesController> _logger;
        private readonly IUserTypeService _userTypeService;

        public UserTypesController(ILogger<UserTypesController> logger,
                                   IUserTypeService userTypeService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userTypeService = userTypeService ?? throw new ArgumentNullException(nameof(userTypeService));
        }

        /// <summary>
        /// Gets all user types.
        /// </summary>
        /// <returns>A List of User Types DTOs</returns>
        [HttpGet]
        public async Task<ActionResult<List<UsertTypeDTO>>> GetAllUserTypes()
        {
            try
            {
                List<UsertTypeDTO> userTypeDtos = await _userTypeService.GetAllUserTypesAsync();
                return Ok(userTypeDtos);
            }
            catch (Exception e)
            {
                _logger.LogError($"GetAllUserTypes - Error occured with the exception {e.Message}");
                return StatusCode(500);
            }
        }
    }
}