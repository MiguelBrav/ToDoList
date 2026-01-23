using DocumentFormat.OpenXml.Spreadsheet;
using MediatR;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Text.Json;
using ToDoList.API.Aggregators;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Commands;
using ToDoList.API.Commands.AdminCommands;
using ToDoList.API.Queries;
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
        //var mediatorSub = Substitute.For<IMediator>();
        var accountAggregator = Substitute.For<IAccountAggregator>();
        var createUserCommand = new CreateUserCommand
        {
            Email = email,
            Name = "Test User",
            Password = "P@ssw0rd",
            Gender = 1,
            BirthDate = new DateTime(1997, 1, 1)
        };

        var apiResponse = new ApiResponse { StatusCode = 400, ResponseMessage = "The user already exists." };

        //mediatorSub.Send(createUserCommand, default).Returns(Task.FromResult(apiResponse));
        accountAggregator.CreateUserCommand(createUserCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(createUserCommand);
        var response = await accountAggregator.CreateUserCommand(createUserCommand);

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
        //var mediatorSub = Substitute.For<IMediator>();
        var accountAggregator = Substitute.For<IAccountAggregator>();
        var createUserCommand = new CreateUserCommand
        {
            Email = email,
            Name = "Test User",
            Password = "P@ssw0rd",
            Gender = 1,
            BirthDate = new DateTime(1997, 1, 1)
        };

        var apiResponse = new ApiResponse { StatusCode = 400, ResponseMessage = "The user could not register" };

        //mediatorSub.Send(createUserCommand, default).Returns(Task.FromResult(apiResponse));
        accountAggregator.CreateUserCommand(createUserCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(createUserCommand);
        var response = await accountAggregator.CreateUserCommand(createUserCommand);

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
        //var mediatorSub = Substitute.For<IMediator>();
        var accountAggregator = Substitute.For<IAccountAggregator>();
        var createUserCommand = new CreateUserCommand
        {
            Email = email,
            Name = "New User",
            Password = "P@ssw0rd",
            Gender = 1,
            BirthDate = new DateTime(1997, 1, 1)
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = "The user registered successfully." };

        //mediatorSub.Send(createUserCommand, default).Returns(Task.FromResult(apiResponse));
        accountAggregator.CreateUserCommand(createUserCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(createUserCommand);
        var response = await accountAggregator.CreateUserCommand(createUserCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("userfails@example.com", "passw12345")]
    public async Task LoginUser_ReturnsStatusCode400_WhenUserCantLogin(string email, string password)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var accountAggregator = Substitute.For<IAccountAggregator>();
        var loginUserCommand = new LoginUserCommand
        {
            Email = email,
            Password = password
        };

        var apiResponse = new ApiResponse { StatusCode = 400, ResponseMessage = "The user could not login." };

        //mediatorSub.Send(loginUserCommand, default).Returns(Task.FromResult(apiResponse));
        accountAggregator.LoginUserCommand(loginUserCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(loginUserCommand);
        var response = await accountAggregator.LoginUserCommand(loginUserCommand);

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
        //var mediatorSub = Substitute.For<IMediator>();
        var accountAggregator = Substitute.For<IAccountAggregator>();
        var loginUserCommand = new LoginUserCommand
        {
            Email = email,
            Password = password
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new ResponseAuth()) };

        //mediatorSub.Send(loginUserCommand, default).Returns(Task.FromResult(apiResponse));
        accountAggregator.LoginUserCommand(loginUserCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(loginUserCommand);
        var response = await accountAggregator.LoginUserCommand(loginUserCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
        Assert.IsType<ResponseAuth>(JsonSerializer.Deserialize<ResponseAuth>(response.ResponseMessage));

    }

    [Theory]
    [InlineData("usertoAdmin@example.com", "6dffa9ce-d97f-48ae-ad1e-86d81da4a745")]
    public async Task MakeUserAdmin_ReturnsStatusCode400_WhenUserDoesNotExists(string email, string keyAdmin)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var accountAggregator = Substitute.For<IAccountAggregator>();
        var loginUserCommand = new UserAdminCommand
        {
            Email = email,
            KeyAdmin = keyAdmin
        };

        var apiResponse = new ApiResponse { StatusCode = 400, ResponseMessage = "The user does not exists." };

        //mediatorSub.Send(loginUserCommand, default).Returns(Task.FromResult(apiResponse));
        accountAggregator.UserAdminCommand(loginUserCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(loginUserCommand);
        var response = await accountAggregator.UserAdminCommand(loginUserCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("usertoAdmin@example.com", "wrongkey-d97f-48ae-ad1e-86d81da4a745")]
    public async Task MakeUserAdmin_ReturnsStatusCode400_WhenUserKeyIsInvalid(string email, string keyAdmin)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var accountAggregator = Substitute.For<IAccountAggregator>();
        var loginUserCommand = new UserAdminCommand
        {
            Email = email,
            KeyAdmin = keyAdmin
        };

        var apiResponse = new ApiResponse { StatusCode = 400, ResponseMessage = "The key admin is invalid." };

        //mediatorSub.Send(loginUserCommand, default).Returns(Task.FromResult(apiResponse));
        accountAggregator.UserAdminCommand(loginUserCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(loginUserCommand);
        var response = await accountAggregator.UserAdminCommand(loginUserCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }


    [Theory]
    [InlineData("usertoAdmin@example.com", "6dffa9ce-d97f-48ae-ad1e-86d81da4a745")]
    public async Task MakeUserAdmin_ReturnsStatusCode200_WhenUserIsAssingAdmin(string email, string keyAdmin)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var accountAggregator = Substitute.For<IAccountAggregator>();
        var loginUserCommand = new UserAdminCommand
        {
            Email = email,
            KeyAdmin = keyAdmin
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = "The admin role is added for the user" };

        //mediatorSub.Send(loginUserCommand, default).Returns(Task.FromResult(apiResponse));
        accountAggregator.UserAdminCommand(loginUserCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(loginUserCommand);
        var response = await accountAggregator.UserAdminCommand(loginUserCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("usertoAdmin@example.com", "6dffa9ce-d97f-48ae-ad1e-86d81da4a745")]
    public async Task MakeUserAdmin_ReturnsStatusCode200_WhenUserIsAlreadyAdmin(string email, string keyAdmin)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var accountAggregator = Substitute.For<IAccountAggregator>();
        var loginUserCommand = new UserAdminCommand
        {
            Email = email,
            KeyAdmin = keyAdmin
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = "The user is already admin." };

        //mediatorSub.Send(loginUserCommand, default).Returns(Task.FromResult(apiResponse));
        accountAggregator.UserAdminCommand(loginUserCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(loginUserCommand);
        var response = await accountAggregator.UserAdminCommand(loginUserCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }


    [Theory]
    [InlineData("usertoRemoveAdmin@example.com", "6dffa9ce-d97f-48ae-ad1e-86d81da4a745")]
    public async Task RemoveUserAdmin_ReturnsStatusCode400_WhenUserDoesNotExists(string email, string keyAdmin)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var accountAggregator = Substitute.For<IAccountAggregator>();
        var loginUserCommand = new RemoveUserAdminCommand
        {
            Email = email,
            KeyAdmin = keyAdmin
        };

        var apiResponse = new ApiResponse { StatusCode = 400, ResponseMessage = "The user does not exists." };

        //mediatorSub.Send(loginUserCommand, default).Returns(Task.FromResult(apiResponse));
        accountAggregator.RemoveUserAdminCommand(loginUserCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(loginUserCommand);
        var response = await accountAggregator.RemoveUserAdminCommand(loginUserCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }


    [Theory]
    [InlineData("usertoRemoveAdmin", "wrongkey-d97f-48ae-ad1e-86d81da4a745")]
    public async Task RemoveUserAdmin_ReturnsStatusCode400_WhenUserKeyIsInvalid(string email, string keyAdmin)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var accountAggregator = Substitute.For<IAccountAggregator>();
        var loginUserCommand = new RemoveUserAdminCommand
        {
            Email = email,
            KeyAdmin = keyAdmin
        };

        var apiResponse = new ApiResponse { StatusCode = 400, ResponseMessage = "The key admin is invalid." };

        //mediatorSub.Send(loginUserCommand, default).Returns(Task.FromResult(apiResponse));
        accountAggregator.RemoveUserAdminCommand(loginUserCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(loginUserCommand);
        var response = await accountAggregator.RemoveUserAdminCommand(loginUserCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("usertoRemoveAdmin", "6dffa9ce-d97f-48ae-ad1e-86d81da4a745")]
    public async Task RemoveUserAdmin_ReturnsStatusCode200_WhenUserIsRemovedFromAdminRole(string email, string keyAdmin)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var accountAggregator = Substitute.For<IAccountAggregator>();
        var loginUserCommand = new RemoveUserAdminCommand
        {
            Email = email,
            KeyAdmin = keyAdmin
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = "The admin role is deleted for the user" };

        //mediatorSub.Send(loginUserCommand, default).Returns(Task.FromResult(apiResponse));
        accountAggregator.RemoveUserAdminCommand(loginUserCommand).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(loginUserCommand);
        var response = await accountAggregator.RemoveUserAdminCommand(loginUserCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("email@hotmail.com")]
    public async Task RefreshToken_ReturnsStatusCode400_WhenUserDoesNotExists(string email)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var accountAggregator = Substitute.For<IAccountAggregator>();
        var refreshQuery = new RefreshTokenQuery
        {
            Email = email
        };

        var apiResponse = new ApiResponse { StatusCode = 400, ResponseMessage = "The user does not exists." };

        //mediatorSub.Send(refreshQuery, default).Returns(Task.FromResult(apiResponse));
        accountAggregator.RefreshTokenQuery(refreshQuery).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(refreshQuery);
        var response = await accountAggregator.RefreshTokenQuery(refreshQuery);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }

    [Theory]
    [InlineData("emailValid@hotmail.com")]
    public async Task RefreshToken_ReturnsStatusCode200_WhenEmailIsValid(string email)
    {
        // Arrange
        //var mediatorSub = Substitute.For<IMediator>();
        var accountAggregator = Substitute.For<IAccountAggregator>();
        var refreshQuery = new RefreshTokenQuery
        {
            Email = email
        };

        var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new ResponseAuth()) };

        //mediatorSub.Send(refreshQuery, default).Returns(Task.FromResult(apiResponse));
        accountAggregator.RefreshTokenQuery(refreshQuery).Returns(Task.FromResult(apiResponse));

        // Act
        //var response = await mediatorSub.Send(refreshQuery);
        var response = await accountAggregator.RefreshTokenQuery(refreshQuery);

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
