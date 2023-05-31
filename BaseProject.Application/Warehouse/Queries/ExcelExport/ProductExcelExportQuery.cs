using System;
using System.Collections;
using System.Collections.Generic;
using BaseProject.Application.Roles;
using BaseProject.Domain;
using MediatR;
using Whoever.Common.Mapping;

namespace BaseProject.Application.Product.Queries.ExcelExport
{
    public class ProductExcelExportQuery : IRequest<byte[]>
    {
        public  Guid WarehouseId{ get; set; }

    }
}
