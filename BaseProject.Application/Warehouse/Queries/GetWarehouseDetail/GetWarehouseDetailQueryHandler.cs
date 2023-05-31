using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BaseProject.Application.Infrastructure.Request.Queries.GetById;
using BaseProject.Domain;
using BaseProject.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Whoever.Common.Exceptions;
using System.Linq;
using AutoMapper.QueryableExtensions;
using BaseProject.Application.Roles;
using BaseProject.Application.Managers;
using BaseProject.Application.Users.Queries.GetAllUsers;
using BaseProject.Application.Warehouse.Queries.GetAllWarehouse;

namespace BaseProject.Application.Warehouse.Queries.GetWarehouseDetail
{
    public class GetWarehouseDetailQueryHandler : IRequestHandler<GetWarehouseDetailQuery, WarehouseLookupModel>
    {
        private readonly BaseProjectDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager _userMananger;

        public GetWarehouseDetailQueryHandler(BaseProjectDbContext db, IMapper mapper, UserManager user)
        {
            _context = db;
            _mapper=mapper;
            _userMananger = user;
        }

        public async Task<WarehouseLookupModel> Handle(GetWarehouseDetailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var warehouse = await _context.Warehouse.FirstOrDefaultAsync(x => x.WarehouseId == request.WarehouseId);

                if (warehouse == null)
                    throw new NotFoundException(nameof(BaseProject.Domain.Warehouse), request.WarehouseId);

                return _mapper.Map<WarehouseLookupModel>(warehouse);

            }
            catch (System.Exception ex)
            {

                throw;
            }
           
        }
    }
}
