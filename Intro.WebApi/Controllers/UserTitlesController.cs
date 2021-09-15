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
    public class UserTitlesController : ControllerBase
    {
        private readonly ILogger<UserTitlesController> _logger;
        private readonly IUserTitleService _userTitleService;

        public UserTitlesController(ILogger<UserTitlesController> logger,
                                    IUserTitleService userTitleService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userTitleService = userTitleService ?? throw new ArgumentNullException(nameof(userTitleService));
        }

        /// <summary>
        /// Gets all user titles.
        /// </summary>
        /// <returns>A list of All user title DTOs</returns>
        [HttpGet]
        public async Task<ActionResult<List<UserTitleDTO>>> GetAllUserTitles()
        {
            try
            {
                List<UserTitleDTO> userTitleDtos = await _userTitleService.GetAllUserTitlesAsync();
                return Ok(userTitleDtos);
            }
            catch (Exception e)
            {
                _logger.LogError($"GetAllUserTypes - Error occured with the exception {e.Message}");
                return StatusCode(500);
            }
        }
    }
}