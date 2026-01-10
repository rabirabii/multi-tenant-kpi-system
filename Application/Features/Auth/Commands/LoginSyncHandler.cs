using Application.Common.Extensions;
using Application.Wrappers;
using Core.Entities;
using Core.Entities.Global;
using Core.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands
{
    public class LoginSyncHandler : IRequestHandler<LoginSyncCommand, ServiceResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;

        public LoginSyncHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
        }

        public async Task<ServiceResponse<string>> Handle(LoginSyncCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var keycloakUser = request.KeycloakUser;

                var user = (await _unitOfWork.Users.FindAsync(u => u.KeyCloakId == keycloakUser.KeycloakId)).FirstOrDefault();

                if (user == null)
                {
                    user = new MUser
                    {
                        KeyCloakId = keycloakUser.KeycloakId,
                        Username = keycloakUser.Username,
                        Email = keycloakUser.Email,
                        IsActive = true
                    };
                    await _unitOfWork.Users.AddAsync(user);
                    await _unitOfWork.CompleteAsync();
                }

                var userTenant = (await _unitOfWork.UserTenants.FindAsync(ut => ut.UserId == user.UserId)).FirstOrDefault();

                var sessionModel = new UserSession
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    TenantId = userTenant?.TenantId ?? Guid.Empty,
                    Role = userTenant?.Role?.RoleName ?? "Guest",
                    KeycloakId = user.KeyCloakId.ToString(),
                    Email = user.Email,
                    Permissions = []
                };

                if (_httpContext.HttpContext?.Session != null)
                {
                    _httpContext.HttpContext.Session.SetCurrentUser(sessionModel);
                }

                return ServiceResponse<string>.Ok(user.UserId.ToString(), "Login & Sync Successful");
            }
            catch (Exception ex)
            {
                return ServiceResponse<string>.Fail(ex.Message);
            }
        }
    }
}

