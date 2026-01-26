using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.UsersApp;
using UseCaseCore.UseCases;

namespace ToDoList.API.Queries
{
    public class GetUserAppInfoQueryHandler : UseCaseBase<GetUserAppInfoQuery, ApiResponse>
    {

        private readonly IUsersAppService _usersAppService;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly IUsersProfileService _usersProfileService;

        public GetUserAppInfoQueryHandler(IUsersAppService usersAppService, UserManager<IdentityUser> userManager, IUsersProfileService usersProfileService)
        {
            _usersAppService = usersAppService;
            _userManager = userManager;
            _usersProfileService = usersProfileService;
        }

        public override async Task<ApiResponse> Execute(GetUserAppInfoQuery request)
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

