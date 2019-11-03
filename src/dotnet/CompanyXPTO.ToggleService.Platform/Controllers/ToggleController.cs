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
    /// Defines the toogle service which version's apis is v1.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/v1/Toggle")]
    public class ToggleController : ApiController
    {
        private readonly IToggleBusinessManager _toggleBusinessManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleController"/> class.
        /// </summary>
        /// <param name="toggleBusinessManager">The toggle business manager.</param>
        public ToggleController(IToggleBusinessManager toggleBusinessManager)
        {
            _toggleBusinessManager = toggleBusinessManager;
        }

        /// <summary>
        /// Gets the applications asynchronous.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(Response<IEnumerable<ToggleDto>>))]
        public async Task<IHttpActionResult> GetTogglesAsync()
        {
            try
            {
                var result = await _toggleBusinessManager.GetTogglesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // in some cases the exception could not be sent and it is only written in log system 
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Gets the application asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The toggle.</returns>
        [HttpGet]
        [Route("{Id}")]
        [AllowAnonymous]
        [ResponseType(typeof(Response<ToggleDto>))]
        public async Task<IHttpActionResult> GetToggleAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    // if application id is null the request is wrong
                    return BadRequest();
                }
                var result = await _toggleBusinessManager.GetToggleAsync(id);

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

        /// <summary>
        /// Puts a toggle asynchronous.
        /// </summary>
        /// <param name="toogDto">The toog dto.</param>
        /// <returns>The toggle.</returns>
        [HttpPut]
        [AllowAnonymous]
        [ResponseType(typeof(Response<ToggleDto>))]
        public async Task<IHttpActionResult> PutToggleAsync(ToggleDto toogDto)
        {
            try
            {
                if (toogDto == null)
                {
                    // if application id is null the request is wrong
                    return BadRequest();
                }
                var result = await _toggleBusinessManager.PutToggleAsync(toogDto);

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

        /// <summary>
        /// Posts a toggle asynchronous.
        /// </summary>
        /// <param name="toogDto">The toog dto.</param>
        /// <returns>The toggle.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ResponseType(typeof(Response<ToggleDto>))]
        public async Task<IHttpActionResult> PostToggleAsync(ToggleDto toogDto)
        {
            try
            {
                if (toogDto==null)
                {
                    // if application id is null the request is wrong
                    return BadRequest();
                }
                var result = await _toggleBusinessManager.PostToggleAsync(toogDto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // in some cases the exception could not be sent and it is only written in log system 
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Deletes the toggle asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The result if void.</returns>
        [HttpDelete]
        [Route("{Id}")]
        [ResponseType(typeof(void))]
        [AllowAnonymous]
        public IHttpActionResult DeleteToggle(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    // if application id is null the request is wrong
                    return BadRequest();
                }
                _toggleBusinessManager.DeleteToggle(id);
                return Ok();
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