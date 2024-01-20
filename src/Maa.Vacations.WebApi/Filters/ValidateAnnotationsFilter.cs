using MiniValidation;

namespace Maa.Vacations.WebApi.Filters;

public class ValidateAnnotationsFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (context.HttpContext.Request.Method == "POST")
        {
            CreateVacationDto createVacationDto = context.GetArgument<CreateVacationDto>(1);

            if (!MiniValidator.TryValidate(createVacationDto, out IDictionary<string, string[]> validationErrors))
            {
                return TypedResults.ValidationProblem(validationErrors);
            }
        }
        else if (context.HttpContext.Request.Method == "PUT")
        {
            UpdateVacationDto updateVacationDto = context.GetArgument<UpdateVacationDto>(2);

            if (!MiniValidator.TryValidate(updateVacationDto, out IDictionary<string, string[]> validationErrors))
            {
                return TypedResults.ValidationProblem(validationErrors);
            }
        }

        return await next(context);
    }
}