using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        public DbSet<RegisterModel> UsersData { get; set; }

        public DbSet<PropertyModel> Property { get; set; }

        public DbSet<FormModel> Forms { get; set; }
    }
}
