using NSubstitute;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Commands;
using ToDoList.DTO.ApiResponse;
using Xunit;

namespace ToDoList.XUnit;

public class UserProfileTests
{
    [Theory]
    [InlineData("urlImage.com")]
    public async Task SaveUserProfile_ReturnsStatusCode404_WhenUserDoesNotExists(string urlImage)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var userProfAggregator = Substitute.For<IUserProfAggregator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        UserProfileCommand userProfileCommand = new UserProfileCommand()
        {
            UrlImage = urlImage,
            UserId = userId
        };
        var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = "The user does not exists." };

        //mediatorSub.Send(userProfileCommand, default).Returns(Task.FromResult(apiResponse));
        userProfAggregator.UserProfileCommand(userProfileCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(userProfileCommand);
        var response = await userProfAggregator.UserProfileCommand(userProfileCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

 
    [Theory]
    [InlineData("urlImage.com")]
    public async Task SaveUserProfile_ReturnsStatusCode200_WhenResultsAreOk(string urlImage)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var userProfAggregator = Substitute.For<IUserProfAggregator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        UserProfileCommand userProfileCommand = new UserProfileCommand()
        {
            UrlImage = urlImage,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = "The image is uploaded successfully." };

        //mediatorSub.Send(userProfileCommand, default).Returns(Task.FromResult(apiResponse));
        userProfAggregator.UserProfileCommand(userProfileCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(userProfileCommand);
        var response = await userProfAggregator.UserProfileCommand(userProfileCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("urlImage.com")]
    public async Task SaveUserProfile_ReturnsStatusCode200_WhenUserhasImage(string urlImage)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var userProfAggregator = Substitute.For<IUserProfAggregator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        UserProfileCommand userProfileCommand = new UserProfileCommand()
        {
            UrlImage = urlImage,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = "The image already is uploaded." };

        //mediatorSub.Send(userProfileCommand, default).Returns(Task.FromResult(apiResponse));
        userProfAggregator.UserProfileCommand(userProfileCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(userProfileCommand);
        var response = await userProfAggregator.UserProfileCommand(userProfileCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("urlImage.com")]
    public async Task UpdateUserProfile_ReturnsStatusCode404_WhenUserDoesNotExists(string urlImage)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var userProfAggregator = Substitute.For<IUserProfAggregator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        UpdateUserProfileCommand userProfileCommand = new UpdateUserProfileCommand()
        {
            UrlImage = urlImage,
            UserId = userId
        };
        var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = "The user does not exists." };

        //mediatorSub.Send(userProfileCommand, default).Returns(Task.FromResult(apiResponse));
        userProfAggregator.UpdateUserProfileCommand(userProfileCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(userProfileCommand);
        var response = await userProfAggregator.UpdateUserProfileCommand(userProfileCommand);
        
        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }


    [Theory]
    [InlineData("urlImage.com")]
    public async Task UpdateUserProfile_ReturnsStatusCode200_WhenResultsAreOk(string urlImage)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var userProfAggregator = Substitute.For<IUserProfAggregator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        UpdateUserProfileCommand userProfileCommand = new UpdateUserProfileCommand()
        {
            UrlImage = urlImage,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = "The image is updated successfully." };

        //mediatorSub.Send(userProfileCommand, default).Returns(Task.FromResult(apiResponse));
        userProfAggregator.UpdateUserProfileCommand(userProfileCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(userProfileCommand);
        var response = await userProfAggregator.UpdateUserProfileCommand(userProfileCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("urlImage.com")]
    public async Task UpdateUserProfile_ReturnsStatusCode204_WhenUserhasNotImage(string urlImage)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var userProfAggregator = Substitute.For<IUserProfAggregator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        UpdateUserProfileCommand userProfileCommand = new UpdateUserProfileCommand()
        {
            UrlImage = urlImage,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 204, ResponseMessage = "The image does not exists." };

        //mediatorSub.Send(userProfileCommand, default).Returns(Task.FromResult(apiResponse));
        userProfAggregator.UpdateUserProfileCommand(userProfileCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(userProfileCommand);
        var response = await userProfAggregator.UpdateUserProfileCommand(userProfileCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Fact]
    public async Task DeleteUserProfile_ReturnsStatusCode404_WhenUserDoesNotExists()
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var userProfAggregator = Substitute.For<IUserProfAggregator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        DeleteUserProfileCommand userProfileCommand = new DeleteUserProfileCommand()
        {
            UserId = userId
        };
        var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = "The user does not exists." };

        //mediatorSub.Send(userProfileCommand, default).Returns(Task.FromResult(apiResponse));
        userProfAggregator.DeleteUserProfileCommand(userProfileCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(userProfileCommand);
        var response = await userProfAggregator.DeleteUserProfileCommand(userProfileCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }


    [Fact]
    public async Task DeleteUserProfile_ReturnsStatusCode200_WhenResultsAreOk()
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var userProfAggregator = Substitute.For<IUserProfAggregator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        DeleteUserProfileCommand userProfileCommand = new DeleteUserProfileCommand()
        {
             UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = "The image is deleted successfully." };

        //mediatorSub.Send(userProfileCommand, default).Returns(Task.FromResult(apiResponse));
        userProfAggregator.DeleteUserProfileCommand(userProfileCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(userProfileCommand);
        var response = await userProfAggregator.DeleteUserProfileCommand(userProfileCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Fact]
    public async Task DeleteUserProfile_ReturnsStatusCode204_WhenUserhasNotImage()
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var userProfAggregator = Substitute.For<IUserProfAggregator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        DeleteUserProfileCommand userProfileCommand = new DeleteUserProfileCommand()
        {
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 204, ResponseMessage = "The previous user image does not exists." };

        //mediatorSub.Send(userProfileCommand, default).Returns(Task.FromResult(apiResponse));
        userProfAggregator.DeleteUserProfileCommand(userProfileCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(userProfileCommand);
        var response = await userProfAggregator.DeleteUserProfileCommand(userProfileCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }


}
