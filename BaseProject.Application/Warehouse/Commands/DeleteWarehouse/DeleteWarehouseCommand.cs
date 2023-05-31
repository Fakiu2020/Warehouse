using System;
using BaseProject.Application.Infrastructure.Request.Commands.Update;
using BaseProject.Domain;
using MediatR;
using Whoever.Common.Mapping;

namespace BaseProject.Application.Warehouse.Commands.DeleteWarehouse
{
    public class DeleteWarehouseCommand : IRequest
    {
        public Guid WarehouseId{ get; set; }
    }
}
