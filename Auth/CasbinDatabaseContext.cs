using Casbin.Persist.Adapter.EFCore;
using Microsoft.EntityFrameworkCore;

namespace auth_casbin.Auth;

// Wrapper for Casbin EF adapter
public class CasbinDatabaseContext(
    DbContextOptions<CasbinDatabaseContext> options) : CasbinDbContext<int>(options, "auth")
{
}
