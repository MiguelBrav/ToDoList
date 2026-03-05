using Microsoft.Extensions.Diagnostics.HealthChecks;
using ToDoList.Infraestructure;

namespace ToDoList.API.Configurations;

public class DbContextHealthCheck : IHealthCheck
{
    private readonly AppDBContext _dbContext;

    public DbContextHealthCheck(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _dbContext.Database.CanConnectAsync(cancellationToken)
                ? HealthCheckResult.Healthy("Successfully connected to the database.")
                : HealthCheckResult.Unhealthy("Failed to connect to the database.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message ?? "An unknown error occurred while connecting to the database.", ex);
        }
    }
}
