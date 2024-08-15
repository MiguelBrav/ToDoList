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
    public class InstructionByIdQueryHandler : IRequestHandler<InstructionByIdQuery, ApiResponse>
    {
        private readonly IInstructionsTranslatedService _instructionsTranslatedService;

        public InstructionByIdQueryHandler(IInstructionsTranslatedService instructionsTranslatedService)
        {
            _instructionsTranslatedService = instructionsTranslatedService;
        }

        public async Task<ApiResponse> Handle(InstructionByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            InstructionsTranslated instructionsTranslated = await _instructionsTranslatedService.GetInstructionTranslatedById(request.InstructionId, request.LanguageId);

            if (instructionsTranslated == null)
            {
                response.Response = false;
                response.ResponseMessage = "Error while retrieving the information.";
                response.StatusCode = StatusCodes.Status404NotFound;

                return response;
            }

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(instructionsTranslated);
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}
