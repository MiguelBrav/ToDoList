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
    public class InstructionQueryHandler : IRequestHandler<InstructionQuery, ApiResponse>
    {
        private readonly IInstructionsTranslatedService _instructionsTranslatedService;

        public InstructionQueryHandler(IInstructionsTranslatedService instructionsTranslatedService)
        {
            _instructionsTranslatedService = instructionsTranslatedService;
        }

        public async Task<ApiResponse> Handle(InstructionQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            List<InstructionsTranslated> instructionsTranslateds = await _instructionsTranslatedService.GetInstructionsTranslateds(request.LanguageId);

            if(instructionsTranslateds == null || instructionsTranslateds.Count == 0)
            {
                response.Response = false;
                response.ResponseMessage = "Error while retrieving the information.";
                response.StatusCode = StatusCodes.Status404NotFound;

                return response;
            }

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(instructionsTranslateds);
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}
