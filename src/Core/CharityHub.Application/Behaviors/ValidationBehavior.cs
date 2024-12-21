using FluentValidation;
using CharityHub.Application.Base;
using MediatR;

namespace CharityHub.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {

        #region Fileds
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        #endregion

        #region Constructor(s)

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        #endregion

        #region Functions

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    var message = failures.Select(x =>x.ErrorMessage).FirstOrDefault();
                    throw new ValidationException(message);
                }
            }

            return await next();
        }
        #endregion
    }
}
