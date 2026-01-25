using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.UsersApp;
using UseCaseCore.UseCases;

namespace ToDoList.API.Commands
{
    public class SaveUserLanguageCommandHandler : UseCaseBase<SaveUserLanguageCommand, ApiResponse>
    {

        private readonly IUsersAppService _usersAppService;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly IUserLanguageSelectionService _userLanguageService;

        public SaveUserLanguageCommandHandler(IUsersAppService usersAppService, UserManager<IdentityUser> userManager, IUserLanguageSelectionService userLanguageService)
        {
            _usersAppService = usersAppService;
            _userManager = userManager;
            _userLanguageService = userLanguageService;
        }

        public override async Task<ApiResponse> Execute(SaveUserLanguageCommand request)
        {
            ApiResponse response = new ApiResponse();

            IdentityUser userExists = await _userManager.FindByIdAsync(request.UserId);

            if (userExists == null)
            {
                response.Response = false;
                response.ResponseMessage = "The user does not exists.";
                response.StatusCode = StatusCodes.Status404NotFound;

                return response;
            }

            UsersApp userInformation = await _usersAppService.GetUserApp(userExists.Id);

            if (userInformation == null)
            {
                response.Response = false;
                response.ResponseMessage = "The user does not exists.";
                response.StatusCode = StatusCodes.Status404NotFound;

                return response;
            }

            LanguageUserSelection userLanguage = await _userLanguageService.GetLanguageUserSelection(userExists.Id);

            if (userLanguage != null)
            {
                response.Response = false;
                response.ResponseMessage = "The user language is already selected";
                response.StatusCode = StatusCodes.Status200OK;

                return response;
            }

            LanguageUserSelection languageUserSelection = new LanguageUserSelection()
            {
                LanguageId = request.LanguageId,
                UserId = request.UserId,
                IsActive = true
            };

            await _userLanguageService.SaveLanguageUserSelection(languageUserSelection);

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(languageUserSelection);
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}

