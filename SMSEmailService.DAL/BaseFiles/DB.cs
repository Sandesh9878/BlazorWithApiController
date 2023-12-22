using Microsoft.EntityFrameworkCore;
using SMSEmailService.DAL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMSEmailService.DAL.BaseFiles
{
    public class DB : SMSContext
    {
        private string ConnectionString { get; set; }
        public DB(String connectionString) : base()
        {
            ConnectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer(this.ConnectionString);
            }
        }
        public DB(DbContextOptions<SMSContext> options) : base(options) { }
    }
}
