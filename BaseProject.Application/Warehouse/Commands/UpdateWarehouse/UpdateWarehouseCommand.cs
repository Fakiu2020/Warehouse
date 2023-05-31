using System;
using System.Collections.Generic;
using AutoMapper;
using BaseProject.Application.Infrastructure.Request.Commands.Update;
using BaseProject.Application.Roles;
using BaseProject.Domain;
using MediatR;
using Whoever.Common.Mapping;

namespace BaseProject.Application.Warehouse.Commands.UpdateWarehouse
{
    public class UpdateWarehouseCommand : UpdateCommand, IRequest
    {
        public Guid WarehouseId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Zip { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateWarehouseCommand, Domain.Warehouse>()
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.IsDeleted, opt => opt.Ignore());


        }

    }
}
