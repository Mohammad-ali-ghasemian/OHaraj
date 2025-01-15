using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Authentication;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Core.Interfaces.Services;
using Project.Application.Exceptions;
using Project.Application.Responses;
using System.Security.Cryptography;

namespace OHaraj.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthenticationService(IMapper mapper, IHttpContextAccessor httpContextAccessor, IAuthenticationRepository authenticationRepository) {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _authenticationRepository = authenticationRepository;
        }

    }
}
