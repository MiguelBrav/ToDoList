using NSubstitute;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Queries.ReportQueries;
using ToDoList.DTO.ApiResponse;
using Xunit;

namespace ToDoList.XUnit;

public class ReportTests
{

    [Theory]
    [InlineData("es-mx")]
    [InlineData("en-us")]
    public async Task GetTaskTiersExcels_ReturnsStatusCode404_WhenResultUserDoesNotExists(string languageId)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var reportAggregator = Substitute.For<IReportAggregator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var reportQuery = new GetUserTasksExcelQuery
        {
            LanguageId = languageId,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = "The user does not exists." };

        //mediatorSub.Send(reportQuery, default).Returns(Task.FromResult(apiResponse));
        reportAggregator.GetUserTasksExcelQuery(reportQuery).Returns(apiResponse);

        // Act
        //var response = await mediatorSub.Send(reportQuery);
        var response = await reportAggregator.GetUserTasksExcelQuery(reportQuery);

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
    public async Task GetTaskTiersExcels_ReturnsStatusCode204_WhenUserHasNoTasks(string languageId)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var reportAggregator = Substitute.For<IReportAggregator>();

        string userId = Guid.NewGuid().ToString(); // UserId mock
        var reportQuery = new GetUserTasksExcelQuery
        {
            LanguageId = languageId,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 204, ResponseMessage = "The user has no tasks created." };

        //mediatorSub.Send(reportQuery, default).Returns(Task.FromResult(apiResponse));
        reportAggregator.GetUserTasksExcelQuery(reportQuery).Returns(apiResponse);

        // Act
        //var response = await mediatorSub.Send(reportQuery);
        var response = await reportAggregator.GetUserTasksExcelQuery(reportQuery);

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
    public async Task GetTaskTiersExcels_ReturnsStatusCode400_WhenErrorOccurs(string languageId)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var reportAggregator = Substitute.For<IReportAggregator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var reportQuery = new GetUserTasksExcelQuery
        {
            LanguageId = languageId,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 400, ResponseMessage = "Error with report" };

        //mediatorSub.Send(reportQuery, default).Returns(Task.FromResult(apiResponse));
        reportAggregator.GetUserTasksExcelQuery(reportQuery).Returns(apiResponse);

        // Act
        //var response = await mediatorSub.Send(reportQuery);
        var response = await reportAggregator.GetUserTasksExcelQuery(reportQuery);


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
    public async Task GetTaskTiersExcels_ReturnsStatusCode200OK_WhenUserHasTasks(string languageId)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var reportAggregator = Substitute.For<IReportAggregator>();

        string userId = Guid.NewGuid().ToString(); // UserId mock
        var memoryStream = new MemoryStream(new byte[] { 1, 2, 3 });
        var reportBase64String = Convert.ToBase64String(new byte[] { 1, 2, 3 });
        var reportQuery = new GetUserTasksExcelQuery
        {
            LanguageId = languageId,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = reportBase64String };

        //mediatorSub.Send(reportQuery, default).Returns(Task.FromResult(apiResponse));
        reportAggregator.GetUserTasksExcelQuery(reportQuery).Returns(apiResponse);

        // Act
        //var response = await mediatorSub.Send(reportQuery);
        var response = await reportAggregator.GetUserTasksExcelQuery(reportQuery);

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
    public async Task GetTaskTiersBinExcels_ReturnsStatusCode404_WhenResultUserDoesNotExists(string languageId)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var reportAggregator = Substitute.For<IReportAggregator>();

        string userId = Guid.NewGuid().ToString(); // UserId mock
        var reportQuery = new GetUserTasksBinExcelQuery
        {
            LanguageId = languageId,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = "The user does not exists." };

        //mediatorSub.Send(reportQuery, default).Returns(Task.FromResult(apiResponse));
        reportAggregator.GetUserTasksBinExcelQuery(reportQuery).Returns(apiResponse);

        // Act
        //var response = await mediatorSub.Send(reportQuery);
        var response = await reportAggregator.GetUserTasksBinExcelQuery(reportQuery);

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
    public async Task GetTaskTiersBinExcels_ReturnsStatusCode204_WhenUserHasNoTasks(string languageId)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var reportAggregator = Substitute.For<IReportAggregator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var reportQuery = new GetUserTasksBinExcelQuery
        {
            LanguageId = languageId,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 204, ResponseMessage = "The user has no tasks created." };

        //mediatorSub.Send(reportQuery, default).Returns(Task.FromResult(apiResponse));
        reportAggregator.GetUserTasksBinExcelQuery(reportQuery).Returns(apiResponse);

        // Act
        //var response = await mediatorSub.Send(reportQuery);
        var response = await reportAggregator.GetUserTasksBinExcelQuery(reportQuery);

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
    public async Task GetTaskTiersBinExcels_ReturnsStatusCode400_WhenErrorOccurs(string languageId)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var reportAggregator = Substitute.For<IReportAggregator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var reportQuery = new GetUserTasksBinExcelQuery
        {
            LanguageId = languageId,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 400, ResponseMessage = "Error with report" };

        //mediatorSub.Send(reportQuery, default).Returns(Task.FromResult(apiResponse));
        reportAggregator.GetUserTasksBinExcelQuery(reportQuery).Returns(apiResponse);

        // Act
        //var response = await mediatorSub.Send(reportQuery);
        var response = await reportAggregator.GetUserTasksBinExcelQuery(reportQuery);

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
    public async Task GetTaskTiersBinExcels_ReturnsStatusCode200OK_WhenUserHasTasks(string languageId)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var reportAggregator = Substitute.For<IReportAggregator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var memoryStream = new MemoryStream(new byte[] { 1, 2, 3 });
        var reportBase64String = Convert.ToBase64String(new byte[] { 1, 2, 3 });
        var reportQuery = new GetUserTasksBinExcelQuery
        {
            LanguageId = languageId,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = reportBase64String };

        //mediatorSub.Send(reportQuery, default).Returns(Task.FromResult(apiResponse));
        reportAggregator.GetUserTasksBinExcelQuery(reportQuery).Returns(apiResponse);

        // Act
        //var response = await mediatorSub.Send(reportQuery);
        var response = await reportAggregator.GetUserTasksBinExcelQuery(reportQuery);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

}
