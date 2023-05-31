using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BaseProject.Application.Interfaces;
using BaseProject.Application.Managers;
using BaseProject.Domain;
using BaseProject.Persistence;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Application.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginModel>
    {
        private readonly BaseProjectDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager _userManager;
        private readonly ITokenFactory _tokenFactory;
        private readonly IJwtFactory _jwtFactory;

        public LoginCommandHandler(BaseProjectDbContext context, IMapper mapper, UserManager userManager, ITokenFactory tokenFactory, IJwtFactory jwtFactory)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _tokenFactory = tokenFactory;
            _jwtFactory = jwtFactory;
        }

        public async Task<LoginModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            
                var user = await _context.Users.FirstOrDefaultAsync(x=>x.Email == request.Email);
                if (user == null)
                    throw new ValidationException("Invalid User");

                var validPassword = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!validPassword)
                    throw new ValidationException("Invalid Password");

                var token = _tokenFactory.GenerateToken();
                
                var roles = await _userManager.GetRolesAsync(user);
                
                var accessToken = await _jwtFactory.GenerateEncodedToken(user.Id.ToString(), user.UserName, roles );
                return new LoginModel(accessToken, token);
           
           
        }
    }
}
