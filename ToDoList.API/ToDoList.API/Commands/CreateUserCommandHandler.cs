using MediatR;
using Microsoft.AspNetCore.Identity;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.UsersApp;

namespace ToDoList.API.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse>
    {

        private readonly UserManager<IdentityUser> _userManager;

        private readonly IUsersAppService _usersAppService;

        public CreateUserCommandHandler(UserManager<IdentityUser> userManager, IUsersAppService usersAppService)
        {
            _userManager = userManager;
            _usersAppService = usersAppService;
        }

        public async Task<ApiResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            IdentityUser oldUser = await _userManager.FindByEmailAsync(request.Email);

            if(oldUser != null)
            {
                response.Response = false;
                response.ResponseMessage = "The user already exists.";
                response.StatusCode = StatusCodes.Status400BadRequest;

                return response;
            }


            IdentityUser usuario = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,                
            };

            IdentityResult resultado = await _userManager.CreateAsync(usuario, request.Password);

            IdentityUser newUser = await _userManager.FindByEmailAsync(request.Email);

            if(resultado == null || resultado.Succeeded == false)
            {
                response.Response = false;
                response.ResponseMessage = "The user could not register.";
                response.StatusCode = StatusCodes.Status400BadRequest;

                return response;
            }

            UsersApp usersApp = new UsersApp()
            {
                BirthDate = request.BirthDate,
                CreatedDate = DateTime.UtcNow,
                Email = request.Email,
                Gender = request.Gender,
                LastModificationDate = DateTime.UtcNow,
                Name = request.Name,
                UserId = newUser.Id,
            };


            await _usersAppService.SaveUsersApps(usersApp);

            response.Response = true;
            response.ResponseMessage = "The user registered successfully.";
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}
