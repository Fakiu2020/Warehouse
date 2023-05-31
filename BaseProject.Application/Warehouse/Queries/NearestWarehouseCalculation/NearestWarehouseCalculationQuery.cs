using System;
using System.Collections.Generic;
using BaseProject.Application.Warehouse.Queries.GetAllWarehouse;
using MediatR;

namespace BaseProject.Application.Warehouse.Queries.NearestWarehouseCalculation
{
    public class NearestWarehouseCalculationQuery :  IRequest<List<WarehouseLookupModel>>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
