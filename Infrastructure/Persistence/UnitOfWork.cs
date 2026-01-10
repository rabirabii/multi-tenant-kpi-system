using Core.Entities.Global;
using Core.Interface;
using Core.Interface.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        private IGenericRepository<MUser> _users;
        private IGenericRepository<MTenant> _tenants;
        private IGenericRepository<MRole> _roles;
        private IGenericRepository<MGlobalMenu> _menu;
        private IGenericRepository<TrTenantMenu> _tenantMenus;
        private IGenericRepository<TrRoleAccess> _roleAccesses;
        private IGenericRepository<TrUserTenant> _userTenants;
        public IGenericRepository<MUser> Users =>
              _users ??= new GenericRepository<MUser>(_context);

        public IGenericRepository<MTenant> Tenants =>
            _tenants ??= new GenericRepository<MTenant>(_context);

        public IGenericRepository<MRole> Roles => 
            _roles ??= new GenericRepository<MRole>(_context);

        public IGenericRepository<MGlobalMenu> GlobalMenus => 
            _menu ??= new GenericRepository<MGlobalMenu>(_context);

        public IGenericRepository<TrTenantMenu> TenantMenus => 
            _tenantMenus ??= new GenericRepository<TrTenantMenu>(_context);

        public IGenericRepository<TrRoleAccess> RoleAccesses => 
            _roleAccesses ??= new GenericRepository<TrRoleAccess>(_context);

        public IGenericRepository<TrUserTenant> UserTenants =>
            _userTenants ??= new GenericRepository<TrUserTenant>(_context);

        public async Task<IDatabaseTransaction> BeginTransactionAsync()
        {
            var efTransaction = await _context.Database.BeginTransactionAsync();

            return new EfTransaction(efTransaction);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

 
    }
}
