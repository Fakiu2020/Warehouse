using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using BaseProject.Domain;
using BaseProject.Persistence;
using GeoAPI.Geometries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using Whoever.Common.Exceptions;
using Whoever.Common.Extensions;
using BaseProject.Application.Helper;
using System.Linq;

namespace BaseProject.Application.Warehouse.Commands.CreateWarehouse
{
    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, bool>
    {
        private readonly BaseProjectDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateWarehouseCommandHandler(BaseProjectDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {

            try
            {
                
                var warehouseExist = await _context.Warehouse.FirstOrDefaultAsync(x => x.Code == request.Code);

                if (warehouseExist != null)
                    throw new System.ComponentModel.DataAnnotations.ValidationException("Existing warehouse code");

                var warehouse = _mapper.Map<BaseProject.Domain.Warehouse>(request);
                var result = await _context.Warehouse.AddAsync(warehouse);

                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                ex.ReThrow();
                return false;
            }
        }
    }
}
