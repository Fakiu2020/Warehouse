using System.Collections.Generic;
using BaseProject.Application.Common;

using MediatR;

namespace BaseProject.Application.GetAllWarehouse.Queries.GetAllProduct
{
    public class GetWarehouseListQuery :FilterBase ,IRequest<WarehouseListViewModel>
    {
        public string Name { get; set; }

    }
}
