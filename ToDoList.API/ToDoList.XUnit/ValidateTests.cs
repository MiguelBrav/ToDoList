using MediatR;
using NSubstitute;
using ToDoList.API.Commands;
using ToDoList.DTO.ApiResponse;
using Xunit;

namespace ToDoList.XUnit;

public class ValidateTests
{
    [Theory]
    [InlineData("miguelfail@example.com")]
    [InlineData("fail@example.com")]
    public async Task ValidateToken_ReturnsStatusCode404_WhenUserDoesNotExists(string email)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var tokenCommand = new ValidateTokenCommand
        {
            Email = email,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = "The user does not exists." };

        mediatorSub.Send(tokenCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(tokenCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("miguelfail@example.com")]
    [InlineData("fail@example.com")]
    public async Task ValidateToken_ReturnsStatusCode400_WhenTokenIsInvalid(string email)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var tokenCommand = new ValidateTokenCommand
        {
            Email = email,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 400, ResponseMessage = "The token is not valid." };

        mediatorSub.Send(tokenCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(tokenCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }


    [Theory]
    [InlineData("miguel@example.com")]
    [InlineData("success@example.com")]
    public async Task ValidateToken_ReturnsStatusCode200OK_WhenUserAndTokenIsValid(string email)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        string userId = Guid.NewGuid().ToString(); // UserId mock
        var tokenCommand = new ValidateTokenCommand
        {
            Email = email,
            UserId = userId
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = "The token is valid." };

        mediatorSub.Send(tokenCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(tokenCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }
}
