using Business.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mappings
{
    public class PessoaPlanoMapping : IEntityTypeConfiguration<PessoaPlano>
    {
        public void Configure(EntityTypeBuilder<PessoaPlano> builder)
        {
            builder.ToTable("PessoaPlano");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Pessoa).WithMany(x => x.Planos).HasForeignKey(x => x.PessoaId);
            builder.HasOne(x => x.Plano).WithMany(x => x.Pessoas).HasForeignKey(x => x.PlanoId);
        }
    }
}
