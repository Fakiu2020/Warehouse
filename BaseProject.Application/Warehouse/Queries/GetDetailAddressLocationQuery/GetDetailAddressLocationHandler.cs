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
using BaseProject.Application.Helper;
using Microsoft.Extensions.Configuration;
using NetTopologySuite;
using GeoAPI.Geometries;
using GeoCoordinatePortable;
using System.Collections.Generic;

namespace BaseProject.Application.Warehouse.Queries.GetDetailAddressLocation
{
    public class GetDetailAddressLocationHandler : IRequestHandler<GetDetailAddressLocationQuery, GetDetailAddressLocationLookupModel>
    {
        private readonly BaseProjectDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager _userMananger;
        private readonly IConfiguration _configuration;


        public GetDetailAddressLocationHandler(BaseProjectDbContext db, IMapper mapper, UserManager user, IConfiguration configuration)
        {
            _context = db;
            _mapper=mapper;
            _userMananger = user;
            _configuration = configuration;

        }

        public async Task<GetDetailAddressLocationLookupModel> Handle(GetDetailAddressLocationQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var key = _configuration.GetConnectionString("VirtualearthApiKey");
                var pointFromResult= await VirtualearthApiHelper.GetLatLongFromAddress(request.Address, key);

                if(pointFromResult.StatusCode!= "200")
                    throw new System.ComponentModel.DataAnnotations.ValidationException("Distance Calculation Error");

                if (!pointFromResult.ResourceSets.Any())
                    throw new System.ComponentModel.DataAnnotations.ValidationException("Address not found");

                if (!pointFromResult.ResourceSets.First().Resources.Any())
                    throw new System.ComponentModel.DataAnnotations.ValidationException("Address not found");

                return new GetDetailAddressLocationLookupModel() { Result= pointFromResult.ResourceSets.First().Resources.First() };

            }
            catch (System.Exception ex)
            {

                throw;
            }
           
        }
    }
}
