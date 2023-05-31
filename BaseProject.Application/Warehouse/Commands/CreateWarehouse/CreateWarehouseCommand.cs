using System;
using System.Collections.Generic;
using AutoMapper;
using BaseProject.Application.Roles;
using BaseProject.Domain;
using MediatR;
using Whoever.Common.Mapping;

namespace BaseProject.Application.Warehouse.Commands.CreateWarehouse
{
    public class CreateWarehouseCommand : IRequest<bool>, IHaveCustomMapping
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Code{ get; set; }
        public string State{ get; set; }
        public string County{ get; set; }
        public string Zip{ get; set; }

        public double latitude { get; set; }
        public double longtude { get; set; }
        public string FileData { get; set; }
        public string MymeType { get; set; }
        public string FileName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateWarehouseCommand, Domain.Warehouse>()
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.IsDeleted, opt => opt.Ignore());


        }

    }
}
