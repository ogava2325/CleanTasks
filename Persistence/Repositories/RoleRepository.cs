using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Base;
using Application.Common.Interfaces.Persistence.Repositories;
using Domain.Entities;

namespace Persistence.Repositories;

public class RoleRepository(IDbConnectionFactory dbConnectionFactory)
    : GenericRepository<Role, Guid>(dbConnectionFactory), IRoleRepository;