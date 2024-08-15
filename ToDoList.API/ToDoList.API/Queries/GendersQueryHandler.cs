using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.Translated;
using ToDoList.DTO.UsersApp;

namespace ToDoList.API.Queries
{
    public class GendersQueryHandler : IRequestHandler<GendersQuery, ApiResponse>
    {
        private readonly IGendersTranslatedService _gendersTranslatedService;

        public GendersQueryHandler(IGendersTranslatedService gendersTranslatedService)
        {
            _gendersTranslatedService = gendersTranslatedService;
        }

        public async Task<ApiResponse> Handle(GendersQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            List<GendersTranslated> gendersTranslateds = await _gendersTranslatedService.GetGendersTranslateds(request.LanguageId);

            if (gendersTranslateds == null || gendersTranslateds.Count == 0)
            {
                response.Response = false;
                response.ResponseMessage = "Error while retrieving the information.";
                response.StatusCode = StatusCodes.Status404NotFound;

                return response;
            }

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(gendersTranslateds);
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}
