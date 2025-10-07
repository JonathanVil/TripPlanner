var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("docker-env");

var postgres = builder.AddPostgres("tripplanner-postgres", port: 56861)
    .WithDataVolume();

var database = postgres.AddDatabase("tripplanner-db");

builder.AddProject<Projects.TripPlanner_Web>("tripplanner-web")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();