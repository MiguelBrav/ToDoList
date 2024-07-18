using DocumentFormat.OpenXml.Spreadsheet;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NSubstitute;
using System.Text.Json;
using ToDoList.API.Commands;
using ToDoList.API.Commands.AdminCommands;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.Translated;
using Xunit;

namespace ToDoList.XUnit;

public class GenderTests
{
  
    [Theory]
    [InlineData("es-mx")]
    [InlineData("en-us")]
    public async Task GetGenders_ReturnsStatusCode404_WhenResultsIsNullOrEmpty(string languageId)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        var gendersCommand = new GendersCommand
        {
           LanguageId = languageId
        };

        var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = "Error while retrieving the information." };

        mediatorSub.Send(gendersCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(gendersCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("es-mx")]
    [InlineData("en-us")]
    public async Task GetGenders_ReturnsStatusCode200_WhenResultsAreOk(string languageId)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        var gendersCommand = new GendersCommand
        {
            LanguageId = languageId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonConvert.SerializeObject(new List<GendersTranslated> ()) };

        mediatorSub.Send(gendersCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(gendersCommand);
        var deserializedGenders = JsonConvert.DeserializeObject<List<GendersTranslated>>(response.ResponseMessage);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
        Assert.NotNull(deserializedGenders);
        Assert.IsType<List<GendersTranslated>>(deserializedGenders);

    }

}
