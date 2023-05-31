﻿using System;
using System.Collections.Generic;
using AutoMapper;
using BaseProject.Application.Infrastructure.Request.Commands.Update;
using BaseProject.Application.Roles;
using BaseProject.Domain;
using MediatR;
using Whoever.Common.Mapping;

namespace BaseProject.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : UpdateCommand, IRequest, IHaveCustomMapping
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid PlantId{ get; set; }
        public IList<string> Roles{ get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateCommand, User>()
               .ForMember(x => x.Roles, opt => opt.Ignore());
        }
    }
}
