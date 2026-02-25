using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Data;
using TaskFlow.Infrastructure.Security;

namespace TaskFlow.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        public readonly AppDbContext _db;
        public readonly PasswordHasher _hasher;
        public readonly JwtProvider _jwt;

        public AuthService(AppDbContext db, PasswordHasher hasher, JwtProvider jwt)
        {
            _db = db;
            _hasher = hasher;   
            _jwt = jwt; 
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            if (await _db.Users.AnyAsync(x => x.Email == request.Email, cancellationToken))
                throw new Exception("This email is taken");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = _hasher.Hash(request.Password),
                CreatedAt = DateTime.UtcNow
            };

            _db.Users.Add(user);

            await _db.SaveChangesAsync(cancellationToken);
            return new AuthResponse(user.Id, user.Email, _jwt.Generate(user.Id, user.Email));
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken) {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken)
           ?? throw new Exception("Invalid credentials");

            if (!_hasher.Verify(request.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            return new AuthResponse(user.Id,user.Email, _jwt.Generate(user.Id, user.Email));
        }
    }
}
