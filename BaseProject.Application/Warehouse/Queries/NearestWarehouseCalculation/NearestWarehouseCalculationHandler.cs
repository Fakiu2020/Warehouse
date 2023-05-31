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
using BaseProject.Application.Managers;
using Microsoft.Extensions.Configuration;
using NetTopologySuite;
using GeoAPI.Geometries;
using GeoCoordinatePortable;
using System.Collections.Generic;
using BaseProject.Application.Warehouse.Queries.GetAllWarehouse;

namespace BaseProject.Application.Warehouse.Queries.NearestWarehouseCalculation
{
    public class NearestWarehouseCalculationHandler : IRequestHandler<NearestWarehouseCalculationQuery, List<WarehouseLookupModel>>
    {
        private readonly BaseProjectDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager _userMananger;
        private readonly IConfiguration _configuration;


        public NearestWarehouseCalculationHandler(BaseProjectDbContext db, IMapper mapper, UserManager user, IConfiguration configuration)
        {
            _context = db;
            _mapper=mapper;
            _userMananger = user;
            _configuration = configuration;

        }

        public async Task<List<WarehouseLookupModel>> Handle(NearestWarehouseCalculationQuery request, CancellationToken cancellationToken)
        {
            try
            {



                GeoCoordinate coordinateFrom = new GeoCoordinate(request.Latitude, request.Longitude);
                
                var result = await _context.Warehouse
                                            .OrderBy(x => coordinateFrom.GetDistanceTo(new GeoCoordinate(x.Latitude, x.Longitude)))
                                            .Select(x=> new WarehouseLookupModel() { Latitude =x.Latitude,Longitude=x.Longitude, Name= x.Name, WarehouseId=x.WarehouseId,Address=x.Address,State=x.State,County=x.County,
                                                                                    Distance=(coordinateFrom.GetDistanceTo(new GeoCoordinate(x.Latitude, x.Longitude)))/1000 })
                                            .Take(3).ToListAsync();
                
                return result;

            }
            catch (System.Exception ex)
            {

                throw;
            }
           
        }
    }
}
