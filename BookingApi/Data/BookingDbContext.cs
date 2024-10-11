using BookingApi.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<News> New { get; set; }
    }
}
