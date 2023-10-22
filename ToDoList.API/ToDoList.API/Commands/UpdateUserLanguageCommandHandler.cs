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

namespace ToDoList.API.Commands
{
    public class UpdateUserLanguageCommandHandler : IRequestHandler<UpdateUserLanguageCommand, ApiResponse>
    {

        private readonly IUsersAppService _usersAppService;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly IUserLanguageSelectionService _userLanguageService;

        public UpdateUserLanguageCommandHandler(IUsersAppService usersAppService, UserManager<IdentityUser> userManager, IUserLanguageSelectionService userLanguageService)
        {
            _usersAppService = usersAppService;
            _userManager = userManager;
            _userLanguageService = userLanguageService;
        }

        public async Task<ApiResponse> Handle(UpdateUserLanguageCommand request, CancellationToken cancellationToken)
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

            if (userLanguage == null)
            {
                response.Response = false;
                response.ResponseMessage = "The user language is not selected";
                response.StatusCode = StatusCodes.Status204NoContent;

                return response;
            }

            userLanguage.LanguageId = request.LanguageId;
            
            await _userLanguageService.UpdateLanguageUserSelection(userLanguage);

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(userLanguage);
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}

