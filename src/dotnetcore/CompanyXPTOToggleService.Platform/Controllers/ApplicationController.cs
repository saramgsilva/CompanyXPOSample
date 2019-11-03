using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompanyXPTO.ToggleService.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using CompanyXPTO.ToggleService.Dtos;

namespace CompanyXPTO.ToggleService.Platform.Controllers
{
    /// <summary>
    /// Defines the application service which version's apis is v1.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [ApiController]
    [Route("api/v1/Application")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationBusinessManager _applicationBusinessManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationController"/> class.
        /// </summary>
        /// <param name="applicationBusinessManager">The application business manager.</param>
        public ApplicationController(IApplicationBusinessManager applicationBusinessManager)
        {
            _applicationBusinessManager = applicationBusinessManager;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<IEnumerable<ApplicationDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetApplicationsAsync()
        {
            try
            {
                var result = await _applicationBusinessManager.GetApplicationsAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // in some cases the exception could not be sent and it is only written in log system 
                return StatusCode(500, ex);
            }
        }
        
        [HttpGet]
        [Route("{applicationId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<IEnumerable<ApplicationDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetApplicationAsync(string applicationId)
        {
            try
            {
                if (string.IsNullOrEmpty(applicationId))
                {
                    // if application id is null the request is wrong
                    return BadRequest();
                }
                var result = await _applicationBusinessManager.GetApplicationAsync(applicationId);

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