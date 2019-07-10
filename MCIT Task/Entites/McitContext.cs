using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace MCIT_Task.Entites
{
    public class McitContext: DbContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<topic>()
                .HasOptional(s => s.TopicDetails)  
                .WithRequired(ad => ad.Topic);  
        }
        public DbSet<topic> Topics { get; set; }
        public DbSet<TopicDetails> TopicsDetails { get; set; }

    }
}