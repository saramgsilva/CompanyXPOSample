using System;
using System.Collections.Generic;
using CompanyXPTO.ToggleService.DataAccess.Data;
using CompanyXPTO.ToggleService.Dtos;
using CompanyXPTO.ToggleService.Model;

namespace CompanyXPTO.ToggleService.Platform.Tests.Data
{
    public static class ToggleFakeData
    {
        public static Toggle GetToggle1()
        {
            return new ToggleBuilder().WithId("625c3b92-c94e-435a-84e5-66e02c346fb4")
                .WithName("isButtonGreen")
                .AllowedForAllApplications(ApplicationFakeData.GetApplications()).Build();
        }

        public static Toggle GetToggle2()
        {
            return new ToggleBuilder().WithId("66e02c346fb4-c94e-435a-84e5--625c3b92").WithName("isButtonRed-new")
                .AllowedForAllApplications(ApplicationFakeData.GetApplications()).Build();
        }

        public static IEnumerable<Toggle> GetToggles()
        {
            return new List<Toggle>()
            {
                GetToggle1(),
                GetToggle2()
            };
        }

        public static ToggleDto GetToggleDto1()
        {
            return new ToggleDto
            {
                Id = "625c3b92-c94e-435a-84e5-66e02c346fb4",
                Name = "isButtonGreen",
                Version = "v1",
                Applications = new List<ApplicationDto>()
                {
                    ApplicationFakeData.GetApplicationDtoXPTO()
                }
            };
        }

        public static ToggleDto GetToggleDto2()
        {
            return new ToggleDto
            {
                Id = "66e02c346fb4-c94e-435a-84e5--625c3b92",
                Name = "isButtonRed",
                Version = "v1",
                Applications = new List<ApplicationDto>()
                {
                    ApplicationFakeData.GetApplicationDtoABC()
                }
            };
        }

        public static Response<ToggleDto> GetResponseWithToggleDto1()
        {
            return new Response<ToggleDto>
            {
                IsValid = true,
                Result = GetToggleDto1(),
            };
        }

        public static Response<IEnumerable<ToggleDto>> GetResponseWith2ToggleDtos()
        {
            var toggles = new List<ToggleDto>
            {
                GetToggleDto1(),
                GetToggleDto2()
            };
            return new Response<IEnumerable<ToggleDto>>
            {
                IsValid = true,
                Result = toggles
            };
        }

        public static Response<IEnumerable<ToggleDto>> GetResponseWithoutToggleDtos()
        {
            var toggles = new List<ToggleDto> { };
            return new Response<IEnumerable<ToggleDto>>
            {
                IsValid = true,
                Result = toggles
            };
        }

        public static Response<IEnumerable<ToggleDto>> GetResponseWithErrorCodeAndMessage()
        {
            return new Response<IEnumerable<ToggleDto>>
            {
                IsValid = false,
                Message = "It was not possible to get the data",
                ErrorCode = "101"
            };
        }
    }
}
