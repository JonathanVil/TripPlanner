var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("docker-compose")
    .WithDashboard(db => db.WithHostPort(8085));

var postgres = builder.AddPostgres("tripplanner-postgres")
    .WithDataVolume();

var database = postgres.AddDatabase("tripplanner-db");

builder.AddProject<Projects.TripPlanner_Web>("tripplanner-web")
    .WithReference(database)
    .WaitFor(database)
    .WithExternalHttpEndpoints();

builder.Build().Run();