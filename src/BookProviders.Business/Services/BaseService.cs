using BookProviders.Business.Interfaces;
using BookProviders.Business.Models;
using BookProviders.Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace BookProviders.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        public BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }
        protected void Notify(ValidationResult validation)
        {
            foreach (var error in validation.Errors)
                Notify(error.ErrorMessage);
        }

        protected void Notify(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected bool ExecuteValidation<TValidation, TEntity>(TValidation validation, TEntity entity)
            where TValidation : AbstractValidator<TEntity> where TEntity : Entity 
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid)
                return true;

            Notify(validator);

            return false;
        }
    }
}
