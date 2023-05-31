
using FluentValidation;

namespace BaseProject.Application.Warehouse.Commands.CreateWarehouse
{
    public abstract class CreateWarehouseCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : CreateWarehouseCommand
    {
        public CreateWarehouseCommandValidator()
        {
            RuleFor(v => v.Name).NotEmpty();
            RuleFor(v => v.Address).NotEmpty();
            RuleFor(v => v.Code).NotEmpty();
            RuleFor(v => v.State).NotEmpty();
        }
    }
}
