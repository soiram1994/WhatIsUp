var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.WhatsUp_Aggregator>("Aggregator");
builder.Build().Run();