using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using ToDoList.API.Commands;
using ToDoList.API.Commands.TaskByUserBinCommands;
using ToDoList.API.Commands.TaskByUserCommands;
using ToDoList.Domain.Interfaces;
using ToDoList.Infraestructure;
using ToDoList.Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddControllers();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoListApi", Version = "v1" });
    // Include the XML comments in the Swagger documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }

                        },
                            new string[]{}
                        }
                    });

});

builder.Services.AddDbContext<AppDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
sqlServerOptions => {
    sqlServerOptions.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(60),
        errorNumbersToAdd: null
        );
    sqlServerOptions.CommandTimeout(60);
}), ServiceLifetime.Transient);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["key_jwt"])),
        ClockSkew = TimeSpan.Zero
    });


var container = new ContainerBuilder();
container.Populate(builder.Services);

container.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

//Configuracion de comandos
container.RegisterAssemblyTypes(typeof(CreateUserCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(LoginUserCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(ValidateTokenCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(TaskTierCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(GendersCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(UserProfileCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(UpdateUserProfileCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(DeleteUserProfileCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(GetUserAppInfoCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(UpdateUserAppInfoCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(GetUserLanguageCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(SaveUserLanguageCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(UpdateUserLanguageCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(InstructionCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(CreateTaskCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(TaskTierByIdCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(GetUserTaskCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(CancelTasksCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(CancelAllTasksCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(GetUserTaskBinCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(UpdateTaskCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(RestoreTasksCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));
container.RegisterAssemblyTypes(typeof(CleanTasksCommandHandler).GetTypeInfo().Assembly)
.AsClosedTypesOf(typeof(IRequestHandler<,>));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
   .AddEntityFrameworkStores<AppDBContext>()
   .AddDefaultTokenProviders();

builder.Services.AddTransient<IUsersAppService, UsersAppService>();
builder.Services.AddTransient<ITaskTierTranslatedService, TaskTierTranslatedService>();
builder.Services.AddTransient<IGendersTranslatedService, GendersTranslatedService>();
builder.Services.AddTransient<IUsersProfileService, UsersProfileService>();
builder.Services.AddTransient<IUserLanguageSelectionService, LanguageUserSelectionService>();
builder.Services.AddTransient<IInstructionsTranslatedService, InstructionsTranslatedService>();
builder.Services.AddTransient<ITaskUserService, TaskUserService>();
builder.Services.AddTransient<ITaskUserHistoricalService, TaskUserHistoricalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
