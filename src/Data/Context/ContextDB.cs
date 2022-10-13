using Business.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class ContextDB: DbContext
    {
        public ContextDB(DbContextOptions options): base(options)
        {

        }

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Plano> Plano { get; set; }
        public DbSet<PessoaPlano> PessoaPlano { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextDB).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
