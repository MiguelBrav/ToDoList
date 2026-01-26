using Newtonsoft.Json;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.Translated;
using UseCaseCore.UseCases;

namespace ToDoList.API.Queries
{
    public class InstructionQueryHandler : UseCaseBase<InstructionQuery, ApiResponse>
    {
        private readonly IInstructionsTranslatedService _instructionsTranslatedService;

        public InstructionQueryHandler(IInstructionsTranslatedService instructionsTranslatedService)
        {
            _instructionsTranslatedService = instructionsTranslatedService;
        }

        public override async Task<ApiResponse> Execute(InstructionQuery request)
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
