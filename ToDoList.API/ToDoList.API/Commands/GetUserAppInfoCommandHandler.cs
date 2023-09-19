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
    public class GetUserAppInfoCommandHandler : IRequestHandler<GetUserAppInfoCommand, ApiResponse>
    {

        private readonly IUsersAppService _usersAppService;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly IUsersProfileService _usersProfileService;

        public GetUserAppInfoCommandHandler(IUsersAppService usersAppService, UserManager<IdentityUser> userManager, IUsersProfileService usersProfileService)
        {
            _usersAppService = usersAppService;
            _userManager = userManager;
            _usersProfileService = usersProfileService;
        }

        public async Task<ApiResponse> Handle(GetUserAppInfoCommand request, CancellationToken cancellationToken)
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

            UserProfile userProfile = await _usersProfileService.GetUserProfile(userExists.Id);

            UserInformation userAppInformation = new UserInformation()
            {
                Id = userInformation.Id,
                Name = userInformation.Name,
                BirthDate = userInformation.BirthDate,
                Email = userInformation.Email,
                CreatedDate = userInformation.CreatedDate,
                Gender = userInformation.Gender,
                LastModificationDate = userInformation.LastModificationDate,
                UserId = userInformation.UserId,
                UrlImage = userProfile?.UrlImage != null ? userProfile.UrlImage : null,
            };

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(userAppInformation);
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}

