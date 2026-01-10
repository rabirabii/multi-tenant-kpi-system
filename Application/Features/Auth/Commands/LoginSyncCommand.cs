using Application.DTOs;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands
{
    public class LoginSyncCommand : IRequest<ServiceResponse<string>>
    {
        public KeycloakUserDto KeycloakUser { get; set; }

        public LoginSyncCommand(KeycloakUserDto keycloakUser)
        {
            KeycloakUser = keycloakUser;
        }
    }
}
