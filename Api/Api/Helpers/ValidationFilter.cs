using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Helpers
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                foreach (var arg in context.ActionArguments)
                {
                    if (arg.Value == null) continue;

                    var validatorType = typeof(IValidator<>).MakeGenericType(arg.Value!.GetType());
                    var validator = scope.ServiceProvider.GetService(validatorType) as IValidator;
                    if (validator != null)
                    {
                        var result = await validator.ValidateAsync(new FluentValidation.ValidationContext<object>(arg.Value));
                        if (!result.IsValid)
                        {
                            context.Result = new BadRequestObjectResult(result.Errors.Select(e => e.ErrorMessage));
                            return;
                        }
                    }
                }
            }

            await next();
        }

    }
}
