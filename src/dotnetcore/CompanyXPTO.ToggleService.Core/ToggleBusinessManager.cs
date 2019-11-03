using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CompanyXPTO.ToggleService.Core.Interfaces;
using CompanyXPTO.ToggleService.DataAccess.Data;
using CompanyXPTO.ToggleService.DataAccess.Repositories;
using CompanyXPTO.ToggleService.DataAccess.Repositories.Interfaces;
using CompanyXPTO.ToggleService.Dtos;
using CompanyXPTO.ToggleService.Model;

namespace CompanyXPTO.ToggleService.Core
{
    /// <summary>
    /// Defines the toggle business manager entity.
    /// </summary>
    /// <seealso cref="CompanyXPTO.ToggleService.Core.Interfaces.IToggleBusinessManager" />
    public class ToggleBusinessManager : IToggleBusinessManager
    {
        private readonly IUnitOfWork<DbContext> _unitOfWork;
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleBusinessManager"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        public ToggleBusinessManager(IUnitOfWork<DbContext> unitOfWork, IRepositoryFactory repositoryFactory)
        {
            _unitOfWork = unitOfWork;
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Gets the toggle asynchronous.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>The toggle</returns>
        /// <exception cref="ArgumentNullException">Id</exception>
        /// <exception cref="NotImplementedException">It was not possible to access the data.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Id</exception>
        public async Task<Response<ToggleDto>> GetToggleAsync(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var toggleRepository = GetToggleRepository();

            var toggle = await toggleRepository.FindIdAsync(Id);
            if (toggle == null)
            {
                throw new ArgumentOutOfRangeException(nameof(Id));
            }
            return new Response<ToggleDto> { IsValid = true, Result = ToToggleDto(toggle) };
        }

        /// <summary>
        /// Gets the applications asynchronous.
        /// </summary>
        /// <returns>The list of applications</returns>
        /// <exception cref="NotImplementedException">It was not possible to access the data.</exception>
        public async Task<Response<IEnumerable<ToggleDto>>> GetTogglesAsync()
        {
            var toggleRepository = GetToggleRepository();

            var toggles = await toggleRepository.GetAsync();

            var stos = toggles.Select(toggle => ToToggleDto(toggle));

            return new Response<IEnumerable<ToggleDto>> { IsValid = true, Result = stos };
        }

        /// <summary>
        /// Posts a toggle asynchronous.
        /// </summary>
        /// <param name="toogDto">The toog dto.</param>
        /// <returns>The toggle,</returns>
        /// <exception cref="ArgumentNullException">toogDto</exception>
        public async Task<Response<ToggleDto>> PostToggleAsync(ToggleDto toogDto)
        {
            if (toogDto == null)
            {
                throw new ArgumentNullException(nameof(toogDto));
            }

            if (string.IsNullOrEmpty(toogDto.Name))
            {
                throw new ArgumentNullException(nameof(toogDto.Name));
            }

            if (toogDto.Applications == null && !toogDto.Applications.Any())
            {
                throw new ArgumentNullException(nameof(toogDto.Applications));
            }

            //get all repositories
            var toggleRepository = GetToggleRepository();
            var toggleConfigRepository = _repositoryFactory.CreateRepository<ToggleConfig>(_unitOfWork);
            var applicationRepository = _repositoryFactory.CreateRepository<Application>(_unitOfWork) as ApplicationRepository;


            // define toggle
            var toggle = new ToggleBuilder().WithId(toogDto.Id ?? Guid.NewGuid().ToString())
                                            .WithName(toogDto.Name)
                                            .Build();

            // get apps from database to set the new toggle
            var apps = applicationRepository.GetApplications(toogDto.Applications.Select(c => c.Id).ToList());

            foreach (var app in apps)
            {
                app.IsToggleServiceAllowed = true;
                var toggleConfig = new ToggleConfig
                {
                    Id = Guid.NewGuid().ToString(),
                    Version = toogDto.Version,
                    Value = true,
                    ToggleId = toggle.Id,
                    Toggle = toggle,
                    ApplicationId = app.Id,
                    Application = app,
                    CreatedAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };
                if (toggle.Configs == null)
                {
                    toggle.Configs = new List<ToggleConfig>();
                }
                if (app.Configs == null)
                {
                    app.Configs = new List<ToggleConfig>();
                }

                // set toggle configs
                app.Configs.Add(toggleConfig);
                toggle.Configs.Add(toggleConfig);
                applicationRepository.Update(app);
                toggleConfigRepository.Insert(toggleConfig);
            }

            toggleRepository.Insert(toggle);
            await _unitOfWork.SaveAsync();

            return await Task.FromResult(new Response<ToggleDto> { IsValid = true, Result = ToToggleDto(toggle) });
        }

        /// <summary>
        /// Puts a toggle asynchronous.
        /// </summary>
        /// <param name="toogDto">The toog dto.</param>
        /// <returns>The toggle,</returns>
        /// <exception cref="ArgumentNullException">toogDto</exception>
        public async Task<Response<ToggleDto>> PutToggleAsync(ToggleDto toogDto)
        {
            if (toogDto == null)
            {
                throw new ArgumentNullException(nameof(toogDto));
            }
            if (string.IsNullOrEmpty(toogDto.Id))
            {
                throw new ArgumentNullException(nameof(toogDto.Id));
            }
            var toggleRepository = GetToggleRepository();
            var toogle = toggleRepository.FindIdAsync(toogDto.Id);
            if (toogle == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            // todo update data

            return await Task.FromResult(new Response<ToggleDto> { IsValid = false, ErrorCode = "301", Message = "Method not compled." });
        }

        /// <summary>
        /// Deletes the toggle.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="NotImplementedException">It was not possible to access the data.</exception>
        public void DeleteToggle(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }
            var toggleRepository = GetToggleRepository();
            if (toggleRepository.GetById(id) == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            toggleRepository.Delete(id);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Gets the toggle repository.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">It was not possible to access the data.</exception>
        private IRepository<Toggle> GetToggleRepository()
        {
            var toggleRepository = _repositoryFactory.CreateRepository<Toggle>(_unitOfWork);
            return toggleRepository;
        }
        
        /// <summary>
        /// To the toggle dto.
        /// </summary>
        /// <param name="toggle">The toggle.</param>
        /// <returns></returns>
        private static ToggleDto ToToggleDto(Toggle toggle)
        {
            var apps = new List<ApplicationDto>();
            if (toggle.Configs != null)
            {
                apps.AddRange(toggle.Configs.Select(config => new ApplicationDto
                {
                    Id = config.ApplicationId,
                    Name = config.Application?.Name,
                    Version = config.Version
                }));
            }

            var dto = new ToggleDto
            {
                Id = toggle.Id,
                Name = toggle.Name,
                Applications = apps
            };

            return dto;
        }
    }
}