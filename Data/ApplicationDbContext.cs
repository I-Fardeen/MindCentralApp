using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication4.Models;
using JokesWebApp.Models;

namespace WebApplication4.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApplication4.Models.Joke> Joke { get; set; }
        public DbSet<JokesWebApp.Models.Riddles> Riddles { get; set; }
    }
}
