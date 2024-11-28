using MediatR;
using Newtonsoft.Json;
using NSubstitute;
using ToDoList.API.Commands;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.Translated;
using ToDoList.DTO.UsersApp;
using Xunit;

namespace ToDoList.XUnit;

public class UserLanguageTests
{
    [Fact]
    public async Task GetUserLanguage_ReturnsStatusCode404_WhenUserDoesNotExists()
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var userLanguageQuery = new GetUserLanguageQuery
        {
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = "The user does not exists." };

        mediatorSub.Send(userLanguageQuery, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(userLanguageQuery);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Fact]
    public async Task GetUserLanguage_ReturnsStatusCode204_WhenUserSelectionDoesNotExists()
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var userLanguageQuery = new GetUserLanguageQuery
        {
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 204, ResponseMessage = "The user does not have language selection." };

        mediatorSub.Send(userLanguageQuery, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(userLanguageQuery);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Fact]
    public async Task GetUserLanguage_ReturnsStatusCode200_WhenHasLanguage()
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var userLanguageQuery = new GetUserLanguageQuery
        {
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonConvert.SerializeObject(new LanguageUserSelection()) };

        mediatorSub.Send(userLanguageQuery, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(userLanguageQuery);
        var languageUser = JsonConvert.DeserializeObject<LanguageUserSelection>(response.ResponseMessage);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
        Assert.NotNull(languageUser);
        Assert.IsType<LanguageUserSelection>(languageUser);
    }


    [Theory]
    [InlineData("es-mx")]
    [InlineData("en-us")]
    public async Task SaveUserLanguage_ReturnsStatusCode404_WhenUserDoesNotExists(string language)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var saveUserLanguageCommand = new SaveUserLanguageCommand
        {
            LanguageId = language,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = "The user does not exists." };

        mediatorSub.Send(saveUserLanguageCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(saveUserLanguageCommand);

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
    public async Task SaveUserLanguage_ReturnsStatusCode200_WhenResultsAreOk(string language)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var saveUserLanguageCommand = new SaveUserLanguageCommand
        {
            LanguageId = language,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonConvert.SerializeObject(new LanguageUserSelection()) };

        mediatorSub.Send(saveUserLanguageCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(saveUserLanguageCommand);
        var languageUser = JsonConvert.DeserializeObject<LanguageUserSelection>(response.ResponseMessage);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
        Assert.NotNull(languageUser);
        Assert.IsType<LanguageUserSelection>(languageUser);
    }

    [Theory]
    [InlineData("es-mx")]
    [InlineData("en-us")]
    public async Task SaveUserLanguage_ReturnsStatusCode200_WhenSelectionAlreadyExists(string language)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var saveUserLanguageCommand = new SaveUserLanguageCommand
        {
            LanguageId = language,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = "The user language is already selected" };

        mediatorSub.Send(saveUserLanguageCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(saveUserLanguageCommand);

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
    public async Task UpdateUserLanguage_ReturnsStatusCode200_WhenResultsAreOk(string language)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var userLanguageCommand = new UpdateUserLanguageCommand
        {
            LanguageId = language,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonConvert.SerializeObject(new LanguageUserSelection()) };

        mediatorSub.Send(userLanguageCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(userLanguageCommand);
        var languageUser = JsonConvert.DeserializeObject<LanguageUserSelection>(response.ResponseMessage);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
        Assert.NotNull(languageUser);
        Assert.IsType<LanguageUserSelection>(languageUser);
    }

    [Theory]
    [InlineData("es-mx")]
    [InlineData("en-us")]
    public async Task UpdateUserLanguage_ReturnsStatusCode204_WhenUserSelectionDoesNotExists(string language)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var userLanguageCommand = new UpdateUserLanguageCommand
        {
            LanguageId = language,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 204, ResponseMessage = "The user language is not selected" };

        mediatorSub.Send(userLanguageCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(userLanguageCommand);

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
    public async Task UpdateUserLanguage_ReturnsStatusCode404_WhenUserDoesNotExists(string language)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var userLanguageCommand = new UpdateUserLanguageCommand
        {
            LanguageId = language,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = "The user does not exists." };

        mediatorSub.Send(userLanguageCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(userLanguageCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }
}
