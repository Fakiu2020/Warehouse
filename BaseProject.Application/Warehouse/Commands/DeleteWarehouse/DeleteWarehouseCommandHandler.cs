using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BaseProject.Application.Infrastructure.Request.Commands.Update;
using BaseProject.Application.Managers;
using BaseProject.Application.Users.Commands.UpdateUser;
using BaseProject.Application.Warehouse.Commands.DeleteWarehouse;
using BaseProject.Domain;
using BaseProject.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Whoever.Common.Exceptions;
using Whoever.Common.Extensions;

namespace BaseProject.Application.Warehouse.Commands.DeleteWarehouse
{
    public class DeleteWarehouseCommandHandler :  IRequestHandler<DeleteWarehouseCommand>
    {
        private readonly BaseProjectDbContext _context;

        public DeleteWarehouseCommandHandler( BaseProjectDbContext db)
        {
            _context = db;
        }

        public async Task<Unit> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await _context.Warehouse.FirstOrDefaultAsync(x => x.WarehouseId == request.WarehouseId);

            if (warehouse == null)         
                throw new NotFoundException(nameof(Warehouse), request.WarehouseId);

            warehouse.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

       
    }
}
