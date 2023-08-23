using Maa.Vacations.WebApi.Filters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Maa.Vacations.WebApi.HttpRouting;

public static class ConfigureRoutingExtension
{
    public static void ConfigureApi(this WebApplication app)
    {
        ConfigureRoutes(app);
    }

    private static void ConfigureRoutes(WebApplication app)
    {
        var root = app.MapGroup("/");


        root.MapGet("", async Task<Ok<IEnumerable<CurrentVacationDto>>> (IVacationService routingService) =>
        {
            var routes = await routingService.GetAllVacationsAsync();
            return TypedResults.Ok(routes);
        });

        root.MapGet("/{id:int}", async Task<Results<NotFound, Ok<CurrentVacationDto>>> (IVacationService routingService, int id) =>
        {
            var route = await routingService.GetVacationByIdAsync(id);
            return route is null ? TypedResults.NotFound() : TypedResults.Ok(route);
        })
        .WithName("GetVacation");

        root.MapPost("/", async Task<CreatedAtRoute<VacationCreatedDto>> (IVacationService routingService, CreateVacationDto route) =>
        {
            var createdRoute = await routingService.AddVacationAsync(route);
            return TypedResults.CreatedAtRoute(createdRoute, "GetVacation", new { id = createdRoute.Id });
        })
        .AddEndpointFilter<ValidateAnnotationsFilter>();

        root.MapPut("/{id:int}", async Task<Results<NotFound, Ok>> (IVacationService routingService, int id, UpdateVacationDto route) =>
        {
            var routeUpdatedDto = await routingService.UpdateVacationAsync(id, route);
            return routeUpdatedDto is null ? TypedResults.NotFound() : TypedResults.Ok();
        })
        .AddEndpointFilter<ValidateAnnotationsFilter>();

        root.MapDelete("/{id:int}", async Task<Results<NotFound, NoContent>> (IVacationService routingService, int id) =>
        {
            var deletedRouteDto = await routingService.DeleteVacationAsync(id);
            return deletedRouteDto is null ? TypedResults.NotFound() : TypedResults.NoContent();
        });
    }
}