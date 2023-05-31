using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseProject.Application.Common;
using BaseProject.Application.Infrastructure.Request.Queries.GetById;
using BaseProject.Application.Managers;
using BaseProject.Application.Roles;
using BaseProject.Application.Roles.GetAllRoles;
using BaseProject.Application.Users.Queries.GetAllUsers;
using BaseProject.Application.Warehouse.Queries.GetAllWarehouse;
using BaseProject.Domain;
using BaseProject.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Application.GetAllWarehouse.Queries.GetAllProduct
{
    public class GetWarehouseListQueryHandler : IRequestHandler<GetWarehouseListQuery, WarehouseListViewModel>
    {
        private readonly BaseProjectDbContext _context;
        private readonly IMapper _mapper;
        public GetWarehouseListQueryHandler(BaseProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WarehouseListViewModel> Handle(GetWarehouseListQuery request, CancellationToken cancellationToken)
        {

            var data = _context.Warehouse
                                      .OrderByDescending(x => x.CreationTime)
                                      .Where(x => !x.IsDeleted &&
                                                         (string.IsNullOrEmpty(request.Name) || (x.Name.Contains(request.Name) || x.Code.Contains(request.Name))) )
                                      .AsQueryable().ProjectTo<WarehouseLookupModel>(_mapper.ConfigurationProvider);

            var pageList = await PagedList<WarehouseLookupModel>.CreateAsync(data, request.PageNumber, request.PageSize);


            return new WarehouseListViewModel {
                PageNumber = pageList.CurrentPage,
                PageSize = pageList.PageSize,
                PageTotal = pageList.TotalPages,
                TotalRecords = pageList.TotalCount,
                Products = pageList.Entities
            };
        }
    }
}