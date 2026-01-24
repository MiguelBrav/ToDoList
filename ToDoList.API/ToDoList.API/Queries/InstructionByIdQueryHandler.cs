using Newtonsoft.Json;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.Translated;
using UseCaseCore.UseCases;

namespace ToDoList.API.Queries
{
    public class InstructionByIdQueryHandler : UseCaseBase<InstructionByIdQuery, ApiResponse>
    {
        private readonly IInstructionsTranslatedService _instructionsTranslatedService;

        public InstructionByIdQueryHandler(IInstructionsTranslatedService instructionsTranslatedService)
        {
            _instructionsTranslatedService = instructionsTranslatedService;
        }

        public override async Task<ApiResponse> Execute(InstructionByIdQuery request)
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
