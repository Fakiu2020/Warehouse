using System;
using System.Collections.Generic;
using MediatR;

namespace BaseProject.Application.Warehouse.Queries.GetDetailAddressLocation
{
    public class GetDetailAddressLocationQuery :  IRequest<GetDetailAddressLocationLookupModel>
    {
        public string Address { get; set; }
    }
}
