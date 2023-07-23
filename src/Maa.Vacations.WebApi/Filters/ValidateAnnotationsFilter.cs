using MiniValidation;

namespace Maa.Vacations.WebApi.Filters;


public class ValidateAnnotationsFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if(context.HttpContext.Request.Method == "POST")
        {
            var createVacationDto = context.GetArgument<CreateVacationDto>(1);

            if (!MiniValidator.TryValidate(createVacationDto, out var validationErrors))
            {
                return TypedResults.ValidationProblem(validationErrors);
            }
        }
        else if (context.HttpContext.Request.Method == "PUT")
        {
            var updateVacationDto = context.GetArgument<UpdateVacationDto>(2);

            if (!MiniValidator.TryValidate(updateVacationDto, out var validationErrors))
            {
                return TypedResults.ValidationProblem(validationErrors);
            }
        }

        return await next(context);
    }
}