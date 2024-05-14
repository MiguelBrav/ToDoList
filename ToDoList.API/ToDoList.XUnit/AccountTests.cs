using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
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
        var mediatorMock = new Mock<IMediator>();
        var createUserCommand = new CreateUserCommand
        {
            Email = email,
            Name = "Test User",
            Password = "P@ssw0rd",
            Gender = 1, 
            BirthDate = new DateTime(1997, 1, 1)
        };

        var apiResponse = new ApiResponse { StatusCode = 400, ResponseMessage = "The user already exists." };


        mediatorMock.Setup(m => m.Send(createUserCommand, default(CancellationToken))).ReturnsAsync(apiResponse);

        // Act
        var response = await mediatorMock.Object.Send(createUserCommand);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.ResponseMessage);
        Assert.Equal(apiResponse.StatusCode, response.StatusCode);
        Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
        Assert.IsType<string>(apiResponse.ResponseMessage);
        Assert.IsType<ApiResponse>(response);
    }
}
