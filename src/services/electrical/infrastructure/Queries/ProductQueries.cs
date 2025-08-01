using TriPower.Electrical.Application.Projects;
using TriPower.Electrical.Application.Shared;
using TriPower.Electrical.Application.Shared.Projects.Get;
using TriPower.Electrical.Application.Shared.Projects.List;
using TriPower.Electrical.Infrastructure.Contexts;

namespace TriPower.Electrical.Infrastructure.Queries;

internal class ProductQueries(TriElectricalDbContext db) : IProductQueries
{
    public async Task<ListProjectsResponse> ListAsync(ListProjectsRequest request, Guid userId, CancellationToken cancellationToken = default)
    {
        var query = db
            .Projects
            .AsNoTracking()
            .Where(project => project.UserId == userId)
            .AsQueryable();

        if (request.ShouldBeApplySearchString)
        {
            query = query
                .Where(project => 
                    project.Name.Contains(request.QuerySearchString) ||
                    project.Description.Contains(request.QuerySearchString)
                );
        }

        if (request.ShouldBeApplySort)
        {
            query = request.QuerySortLabel switch
            {
                nameof(ListProjectsResponseItem.Name) => request.QuerySortDirection == TriSortDirection.Ascending
                    ? query.OrderBy(project => project.Name)
                    : query.OrderByDescending(project => project.Name),
                nameof(ListProjectsResponseItem.Description) => request.QuerySortDirection == TriSortDirection.Ascending
                    ? query.OrderBy(project => project.Description)
                    : query.OrderByDescending(project => project.Description),
                nameof(ListProjectsResponseItem.Voltage) => request.QuerySortDirection == TriSortDirection.Ascending
                    ? query.OrderBy(project => project.Voltage)
                    : query.OrderByDescending(project => project.Voltage),
                nameof(ListProjectsResponseItem.Phases) => request.QuerySortDirection == TriSortDirection.Ascending
                    ? query.OrderBy(project => project.Phases)
                    : query.OrderByDescending(project => project.Phases),
                _ => query
            };
        }
        
        var totalItems = await query.CountAsync(cancellationToken);
        
        query = query
            .Skip(request.QueryPage * request.QueryPageSize)
            .Take(request.QueryPageSize);
        
        var projects = await query
            .Select(project => new ListProjectsResponseItem
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Voltage = project.Voltage,
                Phases = project.Phases
            })
            .ToArrayAsync(cancellationToken);

        return new ListProjectsResponse
        {
            TotalItems = totalItems,
            Items = projects
        };
    }

    public async Task<GetProjectResponse> GetAsync(GetProjectRequest request, Guid userId, CancellationToken cancellationToken = default)
    {
        return await db
            .Projects
            .AsNoTracking()
            .Where(project => project.UserId == userId && project.Id == request.Id)
            .Select(project => new GetProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Voltage = project.Voltage,
                Phases = project.Phases,
                Rooms = project.Rooms.Select(room => new RoomDto
                    {
                        Id = room.Id,
                        Name = room.Name,
                        Perimeter = room.Perimeter,
                        Area = room.Area,
                        Type = room.Type,
                        Classification = room.Classification,
                        LightingMinimumLoad = room.Lighting.MinimumLoad,
                        RequiredGeneralSocketsCount = room.GeneralSockets.RequiredCount,
                        RequiredGeneralSocketsLoad = room.GeneralSockets.RequiredLoad,
                        GeneralSocketsModifier = room.GeneralSockets.Modifier,
                        CorrectedGeneralSocketsCount = room.GeneralSockets.CorrectedCount,
                        CorrectedGeneralSocketsLoad = room.GeneralSockets.CorrectedLoad
                    }
                    ).ToArray()
            })
            .FirstOrDefaultAsync(cancellationToken) 
            ?? throw new KeyNotFoundException($"Project with ID {request.Id} not found.");
    }
}