using System.Collections.Generic;
using BaseProject.Application.Common;
using BaseProject.Application.Warehouse.Queries.GetAllWarehouse;
using BaseProject.Domain;
using Whoever.Common.Mapping;

namespace BaseProject.Application.GetAllWarehouse.Queries.GetAllProduct
{
    public class WarehouseListViewModel : FilterBase
    {
        public List<WarehouseLookupModel> Products { get; set; }
    }
}
