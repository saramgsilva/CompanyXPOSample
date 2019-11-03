using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyXPTO.ToggleService.Core.Interfaces;
using CompanyXPTO.ToggleService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyXPTO.ToggleService.Platform.Controllers
{
    [ApiController]
    [Route("api/v1/ToggleConfig")]
    public class ToggleConfigController : ControllerBase
    {
        private readonly IApplicationBusinessManager _applicationBusinessManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleConfigController"/> class.
        /// </summary>
        /// <param name="applicationBusinessManager">The application business manager.</param>
        public ToggleConfigController(IApplicationBusinessManager applicationBusinessManager)
        {
            _applicationBusinessManager = applicationBusinessManager;
        }

        /// <summary>
        /// Gets the toggles asynchronous.
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <returns>The response with list of ToggleServiceConfigDto</returns>
        [HttpGet]
        [Route("{applicationId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<IEnumerable<ToggleServiceConfigDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTogglesAsync(string applicationId)
        {
            try
            {
                // the application id is always required
                if (string.IsNullOrEmpty(applicationId))
                {
                    // if application id is null the request is wrong
                    return BadRequest();
                }

                // get toggles configured in database from the applicationid provided
                var result = await _applicationBusinessManager.GetTogglesAsync(applicationId);

                return Ok(result);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // in some cases the exception could not be sent and it is only written in log system 
                return StatusCode(500, ex);
            }
        }
    }
}