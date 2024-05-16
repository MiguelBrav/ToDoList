using MediatR;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Text.Json;
using ToDoList.API.Commands;
using ToDoList.DTO.ApiResponse;
using Xunit;

namespace ToDoList.XUnit;

public class AccountTests
{
    [Theory]
    [InlineData("test@example.com")]  
    public async Task CreateUser_ReturnsStatusCode400_WhenUserAlreadyExists(string email)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        var createUserCommand = new CreateUserCommand
        {
            Email = email,
            Name = "Test User",
            Password = "P@ssw0rd",
            Gender = 1, 
            BirthDate = new DateTime(1997, 1, 1)
        };

        var apiResponse = new ApiResponse { StatusCode = 400, ResponseMessage = "The user already exists." };


        mediatorSub.Send(createUserCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(createUserCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("userfail@example.com")]
    public async Task CreateUser_ReturnsStatusCode400_WhenUserCantCreate(string email)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        var createUserCommand = new CreateUserCommand
        {
            Email = email,
            Name = "Test User",
            Password = "P@ssw0rd",
            Gender = 1,
            BirthDate = new DateTime(1997, 1, 1)
        };

        var apiResponse = new ApiResponse { StatusCode = 400, ResponseMessage = "The user could not register" };

        mediatorSub.Send(createUserCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(createUserCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("usersuccess@example.com")]
    public async Task CreateUser_ReturnsStatusCode200_WhenUserIsCreated(string email)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        var createUserCommand = new CreateUserCommand
        {
            Email = email,
            Name = "New User",
            Password = "P@ssw0rd",
            Gender = 1,
            BirthDate = new DateTime(1997, 1, 1)
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = "The user registered successfully." };

        mediatorSub.Send(createUserCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(createUserCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("userfails@example.com","passw12345")]
    public async Task LoginUser_ReturnsStatusCode400_WhenUserCantLogin(string email, string password)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        var loginUserCommand = new LoginUserCommand
        {
            Email = email,
            Password = password
        };

        var apiResponse = new ApiResponse { StatusCode = 400, ResponseMessage = "The user could not login." };

        mediatorSub.Send(loginUserCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(loginUserCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("usersuccess@example.com", "passw12345exists")]
    public async Task LoginUser_ReturnsStatusCode200_WhenUserLogin(string email, string password)
    {
        // Arrange
        var mediatorSub = Substitute.For<IMediator>();
        var loginUserCommand = new LoginUserCommand
        {
            Email = email,
            Password = password
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new ResponseAuth()) };

        mediatorSub.Send(loginUserCommand, default).Returns(Task.FromResult(apiResponse));

        // Act
        var response = await mediatorSub.Send(loginUserCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
        Assert.IsType<ResponseAuth>(JsonSerializer.Deserialize<ResponseAuth>(response.ResponseMessage));

    }
}
