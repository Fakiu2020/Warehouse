using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseProject.Application.Common;
using BaseProject.Application.Infrastructure.Request.Queries.GetById;
using BaseProject.Application.Managers;
using BaseProject.Application.Users.Queries.GetAllUsers;
using BaseProject.Domain;
using BaseProject.Domain.Constants;
using BaseProject.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Application.Users.Administrators.Queries.GetAdministratorDetailQuery
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, UserListViewModel>
    {
        private readonly BaseProjectDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly UserManager _userManager;

        public GetUserListQueryHandler(BaseProjectDbContext context, IMapper mapper, UserManager userManager,  IHttpContextAccessor httpContextAccesor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccesor = httpContextAccesor;
            _userManager = userManager;
        }

        public async Task<UserListViewModel> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var userId = Convert.ToInt16(_httpContextAccesor.HttpContext.User.FindFirst("id")?.Value);
            var user = await _context.Users.Where(x => x.Id == userId).FirstAsync();
            var superAdmin = await _userManager.IsInRoleAsync(user, "Super Admin");
            var data = _context.Users
                                      .OrderByDescending(x => x.CreationTime)
                                      .Where(x =>  (x.Id != userId)  &&
                                      (string.IsNullOrEmpty(request.Email) || x.Email.Contains(request.Email)) )
                                      .AsQueryable().ProjectTo<UserLookupModel>(_mapper.ConfigurationProvider);
           var pageList = await PagedList<UserLookupModel>.CreateAsync(data, request.PageNumber, request.PageSize);           
            return new UserListViewModel {
                PageNumber=pageList.CurrentPage,
                PageSize=pageList.PageSize,
                PageTotal=pageList.TotalPages,
                TotalRecords=pageList.TotalCount,
                Users = pageList.Entities
            };

        }
    }
}