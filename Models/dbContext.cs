using Microsoft.EntityFrameworkCore;

namespace BTL.Models;

public partial class dbContext : DbContext
{
    public dbContext()
    {

    }
    public dbContext(DbContextOptions<dbContext> options)
        : base(options)
    {

    }
    //Admin
    public DbSet<Staff> tblStaff { get; set; }
    public DbSet<Motel> tblMotel { get; set; }
    //User
    public DbSet<Users> tblUser { get; set; }
    public DbSet<Favorite> tblFavourite { get; set; }
    public DbSet<Favourite_detail> tblFavourite_detail { get; set; }



}
