using System;
using BaseProject.Application.Warehouse.Queries.GetAllWarehouse;
using MediatR;

namespace BaseProject.Application.Warehouse.Queries.GetWarehouseDetail
{
    public class GetWarehouseDetailQuery :  IRequest<WarehouseLookupModel>
    {
        public Guid WarehouseId { get; set; }
    }
}
