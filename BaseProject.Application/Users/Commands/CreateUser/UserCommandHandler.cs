using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BaseProject.Application.Interfaces;
using BaseProject.Application.Managers;
using BaseProject.Application.Users.CreateUser;
using BaseProject.Domain;
using BaseProject.Domain.Constants;
using BaseProject.Persistence;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Whoever.Common.Exceptions;
using Whoever.Common.Extensions;

namespace BaseProject.Application.Auth.Commands.Login
{
    public class UserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly BaseProjectDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager _userManager;
        private readonly ITokenFactory _tokenFactory;
        private readonly IJwtFactory _jwtFactory;

        public UserCommandHandler(BaseProjectDbContext context, IMapper mapper, UserManager userManager, ITokenFactory tokenFactory, IJwtFactory jwtFactory)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _tokenFactory = tokenFactory;
            _jwtFactory = jwtFactory;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {                                    
           
            
            using (var ts = _context.BeginTransaction())
            {
                try
                {
                   
                    
                    var user = _mapper.Map<User>(request);
                    var result = await _userManager.CreateAsync(user, request.Password);
                    if (!result.Succeeded)
                    {
                        ValidationFailure[] errors = new ValidationFailure[] { new ValidationFailure("error", result.Errors.First().Description) };
                        throw new ValidationException(errors);
                    }                    
                                                       
                    result = await _userManager.AddToRolesAsync(user, request.Roles);
                    if (!result.Succeeded)
                    {
                        ValidationFailure[] errors = new ValidationFailure[] { new ValidationFailure("error", result.Errors.First().Description) };
                        throw new ValidationException(errors);
                    }              

                    ts.Complete();
                    return user.Id;
                }
                catch (Exception ex)
                {
                    ex.ReThrow();
                    return 0;
                }
            }
        }
    }
}
