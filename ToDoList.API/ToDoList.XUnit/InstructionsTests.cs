using MediatR;
using Newtonsoft.Json;
using NSubstitute;
using ToDoList.API.Commands;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.Translated;
using Xunit;

namespace ToDoList.XUnit;

public class InstructionsTests
{
  
    [Theory]
    [InlineData("es-mx")]
    [InlineData("en-us")]
    public async Task GetInstructions_ReturnsStatusCode404_WhenResultsIsNullOrEmpty(string languageId)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        var instructionsCommand = new InstructionCommand
        {
           LanguageId = languageId
        };

        var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = "Error while retrieving the information." };

        mediatorSub.Send(instructionsCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(instructionsCommand);

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
    public async Task GetInstructions_ReturnsStatusCode200_WhenResultsAreOk(string languageId)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        var instructionsCommand = new InstructionCommand
        {
            LanguageId = languageId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonConvert.SerializeObject(new List<InstructionsTranslated> ()) };

        mediatorSub.Send(instructionsCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(instructionsCommand);
        var deserializedGenders = JsonConvert.DeserializeObject<List<InstructionsTranslated>>(response.ResponseMessage);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
        Assert.NotNull(deserializedGenders);
        Assert.IsType<List<InstructionsTranslated>>(deserializedGenders);

    }

    [Theory]
    [InlineData("es-mx", 100)]
    [InlineData("en-us", 100)]
    public async Task GetInstructionById_ReturnsStatusCode404_WhenResultsIsNullOrEmpty(string languageId, int instructionId)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        var instructionsCommand = new InstructionByIdCommand
        {
            LanguageId = languageId,
            InstructionId = instructionId
        };

        var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = "Error while retrieving the information." };

        mediatorSub.Send(instructionsCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(instructionsCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("es-mx",2)]
    [InlineData("en-us",2)]
    public async Task GetInstructionById_ReturnsStatusCode200_WhenResultsAreOk(string languageId, int instructionId)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        var instructionsCommand = new InstructionByIdCommand
        {
            LanguageId = languageId,
            InstructionId = instructionId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonConvert.SerializeObject(new InstructionsTranslated()) };

        mediatorSub.Send(instructionsCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(instructionsCommand);
        var deserializedGenders = JsonConvert.DeserializeObject<InstructionsTranslated>(response.ResponseMessage);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
        Assert.NotNull(deserializedGenders);
        Assert.IsType<InstructionsTranslated>(deserializedGenders);

    }
}
