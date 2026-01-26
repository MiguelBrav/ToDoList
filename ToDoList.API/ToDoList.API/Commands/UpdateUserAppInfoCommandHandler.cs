using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.UsersApp;
using UseCaseCore.UseCases;

namespace ToDoList.API.Commands
{
    public class UpdateUserAppInfoCommandHandler : UseCaseBase<UpdateUserAppInfoCommand, ApiResponse>
    {

        private readonly IUsersAppService _usersAppService;

        private readonly UserManager<IdentityUser> _userManager;

        public UpdateUserAppInfoCommandHandler(IUsersAppService usersAppService, UserManager<IdentityUser> userManager)
        {
            _usersAppService = usersAppService;
            _userManager = userManager;
           
        }

        public override async Task<ApiResponse> Execute(UpdateUserAppInfoCommand request)
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

            userInformation.BirthDate = userInformation.BirthDate != request.BirthDate ? request.BirthDate : userInformation.BirthDate;
            userInformation.Name = userInformation.Name != request.Name ? request.Name : userInformation.Name;
            userInformation.Gender = userInformation.Gender != request.Gender ? request.Gender : userInformation.Gender;
            userInformation.LastModificationDate = DateTime.UtcNow;

            await _usersAppService.UpdateUserApp(userInformation);

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(userInformation);
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}

