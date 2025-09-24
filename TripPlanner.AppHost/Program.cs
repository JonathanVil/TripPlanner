var builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresServerResource> postgres;
if (builder.ExecutionContext.IsPublishMode)
{
    postgres = builder.AddPostgres("tripplanner-postgres");
}
else
{
    postgres = builder.AddPostgres("tripplanner-postgres")
        .WithPgAdmin(configureContainer: resourceBuilder => resourceBuilder.WithHostPort(18123))
        .WithDataVolume();
}

var database = postgres.AddDatabase("tripplanner-db");

builder.AddProject<Projects.TripPlanner_Web>("tripplanner-web")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();