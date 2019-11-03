using System.Collections.Generic;
using CompanyXPTO.ToggleService.Dtos;

namespace CompanyXPTO.ToggleService.Platform.Tests.Data
{
    public static class ToggleServiceConfigFakeData
    {
        public static ToggleServiceConfigDto GetToggleServiceConfigDtoDto1()
        {
            return new ToggleServiceConfigDto
            {
                Id = ToggleFakeData.GetToggleDto1().Id,
                Version = ToggleFakeData.GetToggleDto1().Version
            };
        }

        public static ToggleServiceConfigDto GetToggleServiceConfigDtoDto2()
        {
            return new ToggleServiceConfigDto
            {
                Id = ToggleFakeData.GetToggleDto2().Id,
                Version = ToggleFakeData.GetToggleDto2().Version
            };
        }

        public static Response<ToggleServiceConfigDto> GetResponseWithToggleServiceConfigDto1()
        {
            return new Response<ToggleServiceConfigDto>
            {
                IsValid = true,
                Result = GetToggleServiceConfigDtoDto1()
            };
        }

        public static Response<IEnumerable<ToggleServiceConfigDto>> GetResponseWith2ToggleServiceConfigDto()
        {
            var toggles = new List<ToggleServiceConfigDto>
            {
                GetToggleServiceConfigDtoDto1(),
                GetToggleServiceConfigDtoDto2()
            };
            return new Response<IEnumerable<ToggleServiceConfigDto>>
            {
                IsValid = true,
                Result = toggles
            };
        }

        public static Response<IEnumerable<ToggleServiceConfigDto>> GetResponseWithoutToggleServiceConfigDto()
        {
            var apps = new List<ToggleServiceConfigDto> { };
            return new Response<IEnumerable<ToggleServiceConfigDto>>
            {
                IsValid = true,
                Result = apps
            };
        }

        public static Response<IEnumerable<ToggleServiceConfigDto>> GetResponseWithErrorCodeAndMessage()
        {
            return new Response<IEnumerable<ToggleServiceConfigDto>>
            {
                IsValid = false,
                Message = "It was not possible to get the data",
                ErrorCode = "101"
            };
        }
    }
}