using MediatR;
using Newtonsoft.Json;
using NSubstitute;
using ToDoList.API.Aggregators;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Commands;
using ToDoList.API.Queries;
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
        //var mediatorSub = Substitute.For<IMediator>();
        var instructionAggregator = Substitute.For<IInstructionAggregator>();

        var instructionQuery = new InstructionQuery
        {
           LanguageId = languageId
        };

        var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = "Error while retrieving the information." };

        //mediatorSub.Send(instructionQuery, default).Returns(Task.FromResult(apiResponse));
        instructionAggregator.InstructionQuery(instructionQuery).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(instructionQuery);
        var response = await instructionAggregator.InstructionQuery(instructionQuery);

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
        //var mediatorSub = Substitute.For<IMediator>();
        var instructionAggregator = Substitute.For<IInstructionAggregator>();
        var instructionQuery = new InstructionQuery
        {
            LanguageId = languageId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonConvert.SerializeObject(new List<InstructionsTranslated> ()) };

        //mediatorSub.Send(instructionQuery, default).Returns(Task.FromResult(apiResponse));
        instructionAggregator.InstructionQuery(instructionQuery).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(instructionQuery);
        var response = await instructionAggregator.InstructionQuery(instructionQuery);
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
        //var mediatorSub = Substitute.For<IMediator>();
        var instructionAggregator = Substitute.For<IInstructionAggregator>();

        var instructionByIdQuery = new InstructionByIdQuery
        {
            LanguageId = languageId,
            InstructionId = instructionId
        };

        var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = "Error while retrieving the information." };

        //mediatorSub.Send(instructionByIdQuery, default).Returns(Task.FromResult(apiResponse));
        instructionAggregator.InstructionByIdQuery(instructionByIdQuery).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(instructionByIdQuery);
        var response = await instructionAggregator.InstructionByIdQuery(instructionByIdQuery);

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
        //var mediatorSub = Substitute.For<IMediator>();
        var instructionAggregator = Substitute.For<IInstructionAggregator>();
        var instructionByIdQuery = new InstructionByIdQuery
        {
            LanguageId = languageId,
            InstructionId = instructionId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonConvert.SerializeObject(new InstructionsTranslated()) };

        //mediatorSub.Send(instructionByIdQuery, default).Returns(Task.FromResult(apiResponse));
        instructionAggregator.InstructionByIdQuery(instructionByIdQuery).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(instructionByIdQuery);
        var response = await instructionAggregator.InstructionByIdQuery(instructionByIdQuery);
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
