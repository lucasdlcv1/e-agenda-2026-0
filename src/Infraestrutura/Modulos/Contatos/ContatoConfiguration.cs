using eAgenda.Dominio.Modulos.Contatos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.Infra.Orm.Modulos.Contatos;

public class ContatoConfiguration : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("TBContato");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(c => c.Email)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.HasIndex(c => c.Email)
            .IsUnique();

        builder.Property(c => c.Telefone)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.HasIndex(c => c.Telefone)
            .IsUnique();

        builder.Property(c => c.Cargo)
            .HasColumnType("varchar(100)")
            .IsRequired(false);

        builder.Property(c => c.Empresa)
            .HasColumnType("varchar(100)")
            .IsRequired(false);

        // builder.HasMany(c => c.Compromissos)
        //.WithOne(cmp => cmp.Contato)
        //.HasForeignKey(cmp => cmp.ContatoId)
        //.OnDelete(DeleteBehavior.Restrict);
    }
}
