using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BaseProject.Application.Infrastructure.Request.Commands.Update;
using BaseProject.Application.Managers;
using BaseProject.Application.Users.Commands.UpdateUser;
using BaseProject.Domain;
using BaseProject.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Whoever.Common.Exceptions;
using Whoever.Common.Extensions;

namespace BaseProject.Application.Warehouse.Commands.UpdateWarehouse
{
    public class UpdateWarehouseCommandHandler :  IRequestHandler<UpdateWarehouseCommand>
    {
        private readonly BaseProjectDbContext _context;
        private readonly UserManager _userManager;
        public UpdateWarehouseCommandHandler( BaseProjectDbContext db , UserManager userManager)
        {
            _context = db;
            _userManager  =userManager;
        }

        public async Task<Unit> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {


            var wareHouse = await _context.Warehouse.FirstOrDefaultAsync(x => x.WarehouseId == request.WarehouseId);
            if (wareHouse == null)
                throw new System.ComponentModel.DataAnnotations.ValidationException("Warehouse not found");

            var wareHouseExist = await _context.Warehouse.FirstOrDefaultAsync(x => x.Code==request.Code && x.WarehouseId != request.WarehouseId);
            if (wareHouseExist != null)
                throw new System.ComponentModel.DataAnnotations.ValidationException("Existing warehouse code");

            

            wareHouse.Name = request.Name;
            wareHouse.State = request.State;
            wareHouse.Address = request.Address;
            wareHouse.County= request.County;
            wareHouse.Code = request.Code;
            wareHouse.Zip = request.Zip;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

       
    }
}
