using Core.Entities.Global;
using Core.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<MUser> Users { get; }
        IGenericRepository<MTenant> Tenants { get; }
        IGenericRepository<MRole> Roles { get; }

        IGenericRepository<MGlobalMenu> GlobalMenus { get; }

        IGenericRepository<TrTenantMenu> TenantMenus { get; }

        IGenericRepository<TrRoleAccess> RoleAccesses { get; }

        IGenericRepository<TrUserTenant> UserTenants { get; }

        Task<int> CompleteAsync();
        Task<IDatabaseTransaction> BeginTransactionAsync();
    }
}
