var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("tripplanner-postgres")
    .WithPgAdmin();
var database = postgres.AddDatabase("tripplanner-db");

builder.AddProject<Projects.TripPlanner_Web>("tripplanner-web")
    .WithReference(database);

builder.Build().Run();
