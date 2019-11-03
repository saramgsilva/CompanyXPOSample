using System.Collections.Generic;
using CompanyXPTO.ToggleService.DataAccess.Data;
using CompanyXPTO.ToggleService.Dtos;
using CompanyXPTO.ToggleService.Model;

namespace CompanyXPTO.ToggleService.Platform.Tests.Data
{
    public static class ApplicationFakeData
    {
        public static IEnumerable<Application> GetApplications()
        {
            return SeedConfig.DefineApplications();
        }

        public static IEnumerable<Application> GetWithoutApplications()
        {
            return new List<Application>();
        }

        public static ApplicationDto GetApplicationDtoXPTO()
        {
            return new ApplicationDto
            {
                Name = "XPTO",
                Id = "ece9a845-1898-485a-9ef2-0bafc7db5570"
            };
        }

        public static ApplicationDto GetApplicationDtoABC()
        {
            return new ApplicationDto
            {
                Name = "ABC",
                Id = "ece9a845-485a-1898-9ef2-0bafc7db5570"
            };
        }

        public static Response<ApplicationDto> GetResponseWithAbcApplicationDto()
        {
            return new Response<ApplicationDto>
            {
                IsValid = true,
                Result = GetApplicationDtoABC(),
            };
        }

        public static Response<IEnumerable<ApplicationDto>> GetResponseWith2ApplicationDtos()
        {
            var apps = new List<ApplicationDto>
            {
                GetApplicationDtoXPTO(),
                GetApplicationDtoABC()
            };
            return new Response<IEnumerable<ApplicationDto>>
            {
                IsValid = true,
                Result = apps
            };
        }

        public static Response<IEnumerable<ApplicationDto>> GetResponseWithoutApplicationDtos()
        {
            var apps = new List<ApplicationDto> {};
            return new Response<IEnumerable<ApplicationDto>>
            {
                IsValid = true,
                Result = apps
            };
        }

        public static Response<IEnumerable<ApplicationDto>> GetResponseWithErrorCodeAndMessage()
        {
            return new Response<IEnumerable<ApplicationDto>>
            {
                IsValid = false,
                Message = "It was not possible to get the data",
                ErrorCode = "101"
            };
        }
    }
}