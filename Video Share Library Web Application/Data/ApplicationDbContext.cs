using Microsoft.EntityFrameworkCore;
using Video_Share_Library_Web_Application.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Video_Share_Library_Web_Application.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<VideoInfo> VideoInfo { get; set; }
        public DbSet<LikeDislike> LikeDislike { get; set; }
    }
}
