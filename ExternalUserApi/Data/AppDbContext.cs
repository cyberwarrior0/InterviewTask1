﻿using ExternalUserApi.Model;
using Microsoft.EntityFrameworkCore;

namespace ExternalUserApi.Data
{
    public class AppDbContext : DbContext   
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
       
    }

}
