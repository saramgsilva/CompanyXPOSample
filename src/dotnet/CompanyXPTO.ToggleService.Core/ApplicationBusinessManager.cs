using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CompanyXPTO.ToggleService.Core.Interfaces;
using CompanyXPTO.ToggleService.DataAccess.Repositories.Interfaces;
using CompanyXPTO.ToggleService.Dtos;
using CompanyXPTO.ToggleService.Model;

namespace CompanyXPTO.ToggleService.Core
{
    /// <summary>
    /// Defines the business logic related with application entity.
    /// </summary>
    /// <seealso cref="CompanyXPTO.ToggleService.Core.Interfaces.IApplicationBusinessManager" />
    public class ApplicationBusinessManager : IApplicationBusinessManager
    {
        private readonly IUnitOfWork<DbContext> _unitOfWork;
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationBusinessManager"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        public ApplicationBusinessManager(IUnitOfWork<DbContext> unitOfWork, IRepositoryFactory repositoryFactory)
        {
            _unitOfWork = unitOfWork;
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Gets the toggles asynchronous.
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <returns>The list of ToggleServiceConfigDto</returns>
        /// <exception cref="System.ArgumentNullException">applicationId</exception>
        /// <exception cref="System.NotImplementedException">It was not possible to access the data.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">applicationId</exception>
        public async Task<Response<IEnumerable<ToggleServiceConfigDto>>> GetTogglesAsync(string applicationId)
        {
            if (string.IsNullOrEmpty(applicationId))
            {
                throw new ArgumentNullException(nameof(applicationId));
            }

            var applicationRepository = GetApplicationRepository();
            var application = await GetApplication(applicationId, applicationRepository);
            if (!application.IsToggleServiceAllowed)
            {
                return new Response<IEnumerable<ToggleServiceConfigDto>> { IsValid = false , ErrorCode = "102", Message = $"{application.Name} does not have permission to use toggle service."};
            }
            var items = application.Configs?.Select(config => new ToggleServiceConfigDto
            {
                //●	When the application/service request their toggles, they must only provide their id and version.
                Id = config.ToggleId,
                Version = config.Version

            }).ToList() ?? new List<ToggleServiceConfigDto>();

            return new Response<IEnumerable<ToggleServiceConfigDto>> { Result = items, IsValid = true };
        }

        /// <summary>
        /// Gets the application asynchronous.
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <returns>The application</returns>
        /// <exception cref="ArgumentNullException">applicationId</exception>
        /// <exception cref="NotImplementedException">It was not possible to access the data.</exception>
        /// <exception cref="ArgumentOutOfRangeException">applicationId</exception>
        public async Task<Response<ApplicationDto>> GetApplicationAsync(string applicationId)
        {
            if (string.IsNullOrEmpty(applicationId))
            {
                throw new ArgumentNullException(nameof(applicationId));
            }
            var applicationRepository = GetApplicationRepository();
            var application = await GetApplication(applicationId, applicationRepository);
            var dto = new ApplicationDto
            {
                Id = application.Id,
                Name = application.Name
            };
            return new Response<ApplicationDto> {IsValid = true, Result = dto};
        }

        /// <summary>
        /// Gets the applications asynchronous.
        /// </summary>
        /// <returns>The list of applications</returns>
        /// <exception cref="NotImplementedException">It was not possible to access the data.</exception>
        public async Task<Response<IEnumerable<ApplicationDto>>> GetApplicationsAsync()
        {
            var applicationRepository = GetApplicationRepository();

            var applications = await applicationRepository.GetAsync();
            var stos= applications.Select(application => new ApplicationDto
            {
                Id = application.Id,
                Name = application.Name
            });

            return new Response<IEnumerable<ApplicationDto>> { IsValid = true, Result = stos };
        }

        /// <summary>
        /// Gets the application repository.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">It was not possible to access the data.</exception>
        private IRepository<Application> GetApplicationRepository()
        {
            var applicationRepository = _repositoryFactory.CreateRepository<Application>(_unitOfWork);
            return applicationRepository;
        }

        /// <summary>
        /// Gets the application.
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="applicationRepository">The application repository.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">applicationId</exception>
        private static async Task<Application> GetApplication(string applicationId, IRepository<Application> applicationRepository)
        {
            var application = await applicationRepository.FindIdAsync(applicationId);
            if (application == null)
            {
                throw new ArgumentOutOfRangeException(nameof(applicationId));
            }
            return application;
        }
    }
}