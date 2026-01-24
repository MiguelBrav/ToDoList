using Newtonsoft.Json;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.Translated;
using UseCaseCore.UseCases;

namespace ToDoList.API.Queries
{
    public class GendersQueryHandler : UseCaseBase<GendersQuery, ApiResponse>
    {
        private readonly IGendersTranslatedService _gendersTranslatedService;

        public GendersQueryHandler(IGendersTranslatedService gendersTranslatedService)
        {
            _gendersTranslatedService = gendersTranslatedService;
        }

        public override async Task<ApiResponse> Execute(GendersQuery request)
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
