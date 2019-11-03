using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CompanyXPTO.ToggleService.Core.Interfaces;
using CompanyXPTO.ToggleService.Dtos;

namespace CompanyXPTO.ToggleService.Platform.Controllers
{
    /// <summary>
    /// Defines the application service which version's apis is v1.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/v1/Application")]
    public class ApplicationController : ApiController
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
        [ResponseType(typeof(Response<IEnumerable<ApplicationDto>>))]
        public async Task<IHttpActionResult> GetApplicationsAsync()
        {
            try
            {
                var result = await _applicationBusinessManager.GetApplicationsAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // in some cases the exception could not be sent and it is only written in log system 
                return InternalServerError(ex);
            }
        }
        
        [HttpGet]
        [Route("{applicationId}")]
        [AllowAnonymous]
        [ResponseType(typeof(Response<IEnumerable<ApplicationDto>>))]
        public async Task<IHttpActionResult> GetApplicationAsync(string applicationId)
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
                return InternalServerError(ex);
            }
        }
    }
}