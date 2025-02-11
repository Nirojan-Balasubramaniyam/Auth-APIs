﻿using Auth_APIs.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth_APIs.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
