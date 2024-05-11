using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.API.Commands.ReportCommands;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.UsersApp;

namespace ToDoList.API.Commands.ReportCommands
{
    public class GetUserTasksBinExcelCommandHandler : IRequestHandler<GetUserTasksBinExcelCommand, ApiResponse>
    {

        private readonly UserManager<IdentityUser> _userManager;

        private readonly IUsersAppService _usersAppService;

        private readonly ITaskUserService _taskUserService;

        private readonly ITaskTierTranslatedService _taskTierTranslatedService;

        private readonly IConfiguration _configuration;

        private readonly string _defaultLanguage;
        public GetUserTasksBinExcelCommandHandler(UserManager<IdentityUser> userManager, IUsersAppService usersAppService, ITaskUserService taskUserService, ITaskTierTranslatedService taskTierTranslatedService, IConfiguration configuration)
        {
            _userManager = userManager;
            _usersAppService = usersAppService;
            _taskUserService = taskUserService;
            _taskTierTranslatedService = taskTierTranslatedService;
            _configuration = configuration;
            _defaultLanguage = _configuration.GetValue<string>("DefaultLanguage");
        }

        public async Task<ApiResponse> Handle(GetUserTasksBinExcelCommand request, CancellationToken cancellationToken)
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

            List<TaskByUser> tasksByUser = await  _taskUserService.GetAllTasksBinByUserId(request.UserId);

            if (tasksByUser == null || tasksByUser.Count == 0)
            {
                response.Response = false;
                response.ResponseMessage = "The user has no tasks created.";
                response.StatusCode = StatusCodes.Status204NoContent;

                return response;
            }

            try
            {
                string base64String = string.Empty;



                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (var workbook = new XLWorkbook())
                    {
                        string _language = request.LanguageId.ToUpper();

                        string worksheetName = _language == "ES-MX" ? "TareasEliminadas" : "TaskBinByUser";
                        string taskIdHeader = _language == "ES-MX" ? "IdentificadorTarea" : "TaskId";
                        string taskTierHeader = _language == "ES-MX" ? "NivelTarea" : "TaskTier";
                        string taskNameHeader = _language == "ES-MX" ? "NombreTarea" : "TaskName";
                        string taskDescriptionHeader = _language == "ES-MX" ? "DescripcionTarea" : "TaskDescription";
                        string isCompletedHeader = _language == "ES-MX" ? "EstaCompletada" : "IsCompleted";
                        string createdDateHeader = _language == "ES-MX" ? "FechaCreacion" : "CreatedDate";
                        string expectedDateTimeHeader = _language == "ES-MX" ? "FechaHoraEsperada" : "ExpectedDateTime";
                        string finishDateHeader = _language == "ES-MX" ? "FechaFinalizacion" : "FinishDate";

                        var worksheet = workbook.Worksheets.Add(worksheetName);

                        worksheet.Cell(1, 1).Value = taskIdHeader;
                        worksheet.Cell(1, 2).Value = taskTierHeader;
                        worksheet.Cell(1, 3).Value = taskNameHeader;
                        worksheet.Cell(1, 4).Value = taskDescriptionHeader;
                        worksheet.Cell(1, 5).Value = isCompletedHeader;
                        worksheet.Cell(1, 6).Value = createdDateHeader;
                        worksheet.Cell(1, 7).Value = expectedDateTimeHeader;
                        worksheet.Cell(1, 8).Value = finishDateHeader;

                        int count = 2;
                        Array.ForEach(tasksByUser.ToArray(), task =>
                        {
                            worksheet.Cell(count, 1).Value = task.Id;
                            worksheet.Cell(count, 2).Value = task.OriginalTaskTierId;
                            worksheet.Cell(count, 3).Value = task.TaskName;
                            worksheet.Cell(count, 4).Value = task.TaskDescription;
                            worksheet.Cell(count, 5).Value = task.IsCompleted;
                            worksheet.Cell(count, 6).Value = task.CreatedDate;
                            worksheet.Cell(count, 7).Value = task.ExpectedDateTime;
                            worksheet.Cell(count, 8).Value = task.FinishDate;
                            count++;
                        });

                        workbook.SaveAs(memoryStream);
                    }
                    base64String = Convert.ToBase64String(memoryStream.ToArray());
                }

                response.Response = true;
                response.ResponseMessage = !string.IsNullOrEmpty(base64String) ? base64String : "Error generating report";
                response.StatusCode = StatusCodes.Status200OK;
            }
            catch (Exception)
            {

                response.Response = true;
                response.ResponseMessage = "Error with report";
                response.StatusCode = StatusCodes.Status400BadRequest;

            }

            return response;
        }
    }
}

