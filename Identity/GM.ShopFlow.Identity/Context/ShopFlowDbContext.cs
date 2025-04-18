using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GM.ShopFlow.Identity.Models;

namespace GM.ShopFlow.Identity.Context;

public class ShopFlowDbContext(DbContextOptions<ShopFlowDbContext> options)
    : IdentityDbContext<User>(options)
{

}
