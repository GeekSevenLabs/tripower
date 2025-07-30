// See https://aka.ms/new-console-template for more information

using TriPower;

Console.WriteLine("Hello, World!");


IRouterBuilder<SampleRequest> builder = new RouteBuilder<SampleRequest>()
    .AddSegments("api", "v1")
    .AddSegment("samples")
    .AddParameter(request => request.Id)
    .AddSegment("edit")
    .AddParameter(request => request.CreatedAt)
    .AddSegment("big")
    .AddQueryParameter(request => request.Name)
    .AddQueryParameter(request => request.Description)
    .AddQueryParameter(request => request.UpdatesAt)
    .AddQueryParameter(request => request.CreatedBy)
    .AddQueryParameter(request => request.Ages);

Console.WriteLine(builder.BuildRoutePattern());
Console.WriteLine(builder.BuildRouteGenerator()(new SampleRequest
{
    Id = Guid.NewGuid(),
    Name = "Sample",
    Description = "This is a sample request",
    CreatedAt = DateTime.UtcNow,
    UpdatesAt = [
        DateTimeOffset.UtcNow,
        DateTimeOffset.UtcNow.AddDays(1),
        DateTimeOffset.UtcNow.AddDays(2)
    ],
    CreatedBy = "User123",
    Ages = [25, 30, 35]
}));


