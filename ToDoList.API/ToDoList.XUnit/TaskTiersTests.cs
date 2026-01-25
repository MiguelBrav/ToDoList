using MediatR;
using Newtonsoft.Json;
using NSubstitute;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.Translated;
using Xunit;

namespace ToDoList.XUnit;

public class TaskTiersTests
{
    [Theory]
    [InlineData("es-mx")]
    [InlineData("en-us")]
    public async Task GetTaskTiers_ReturnsStatusCode404_WhenResultsIsNullOrEmpty(string language)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var taskTierAggregator = Substitute.For<ITaskTierAggregator>();
        var taskQuery = new TaskTierQuery
        {
            LanguageId = language
        };

        var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = "Error while retrieving the information." };

        //mediatorSub.Send(taskQuery, default).Returns(Task.FromResult(apiResponse));
        taskTierAggregator.TaskTierQuery(taskQuery).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(taskQuery);
        var response = await taskTierAggregator.TaskTierQuery(taskQuery);  

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
    public async Task GetTaskTiers_ReturnsStatusCode200_WhenResultsAreOk(string language)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var taskTierAggregator = Substitute.For<ITaskTierAggregator>();
        var taskQuery = new TaskTierQuery
        {
            LanguageId = language
        };

        var apiResponse = new ApiResponse { StatusCode = 400, ResponseMessage = JsonConvert.SerializeObject(new List<TaskTierTranslated>()) };
        
        //mediatorSub.Send(taskQuery, default).Returns(Task.FromResult(apiResponse));
        taskTierAggregator.TaskTierQuery(taskQuery).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(taskQuery);
        var response = await taskTierAggregator.TaskTierQuery(taskQuery);
        var deserializedTaskTiers = JsonConvert.DeserializeObject<List<TaskTierTranslated>>(response.ResponseMessage);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
        Assert.NotNull(deserializedTaskTiers);
        Assert.IsType<List<TaskTierTranslated>>(deserializedTaskTiers);
    }

    [Theory]
    [InlineData("es-mx", 99)]
    [InlineData("en-us", 99)]
    public async Task GetTaskTierById_ReturnsStatusCode404_WhenResultsIsNullOrEmpty(string language, int taskId)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var taskTierAggregator = Substitute.For<ITaskTierAggregator>();
        var taskQuery = new TaskTierByIdQuery
        {
            LanguageId = language,
            TaskTierId = taskId
        };

        var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = "Error while retrieving the information." };

        //mediatorSub.Send(taskQuery, default).Returns(Task.FromResult(apiResponse));
        taskTierAggregator.TaskTierByIdQuery(taskQuery).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(taskQuery);
        var response = await taskTierAggregator.TaskTierByIdQuery(taskQuery);  

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("es-mx", 1)]
    [InlineData("en-us", 1)]
    public async Task GetTaskTierById_ReturnsStatusCode200_WhenResultsAreOk(string language, int taskId)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var taskTierAggregator = Substitute.For<ITaskTierAggregator>();
        var taskQuery = new TaskTierByIdQuery
        {
            LanguageId = language,
            TaskTierId = taskId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonConvert.SerializeObject(new TaskTierTranslated()) };

        //mediatorSub.Send(taskQuery, default).Returns(Task.FromResult(apiResponse));
        taskTierAggregator.TaskTierByIdQuery(taskQuery).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(taskQuery);
        var response = await taskTierAggregator.TaskTierByIdQuery(taskQuery);

        var deserializedTaskTier = JsonConvert.DeserializeObject<TaskTierTranslated>(response.ResponseMessage);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
        Assert.NotNull(deserializedTaskTier);
        Assert.IsType<TaskTierTranslated>(deserializedTaskTier);
    }

}
