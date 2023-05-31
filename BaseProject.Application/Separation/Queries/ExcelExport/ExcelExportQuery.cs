using System;
using System.Collections;
using System.Collections.Generic;
using BaseProject.Application.Roles;
using BaseProject.Domain;
using MediatR;
using Whoever.Common.Mapping;

namespace BaseProject.Application.Separation.Queries.ExcelExport
{
    public class ExcelExportQuery: IRequest<byte[]>
    {
        public string Description { get; set; }

        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}
