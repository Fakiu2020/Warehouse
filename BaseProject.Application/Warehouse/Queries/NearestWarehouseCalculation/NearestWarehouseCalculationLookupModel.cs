using System;
using System.Collections.Generic;
using BaseProject.Application.Common;
using BaseProject.Application.Roles;
using BaseProject.Domain;
using Whoever.Common.Mapping;

namespace BaseProject.Application.Product.Queries.NearestWarehouseCalculation
{
    public class NearestWarehouseCalculationLookupModel
    {
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
    }
}
