using eAgenda.Dominio.Modulos.Contatos;
using Infraestrutura.Compartilhado.Identity;
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


        builder.Property(c => c.Telefone)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(c => c.Cargo)
            .HasColumnType("varchar(100)")
            .IsRequired(false);

        builder.Property(c => c.Empresa)
            .HasColumnType("varchar(100)")
            .IsRequired(false);


        builder.HasIndex(c => new { c.UsuarioId, c.Email })
            .IsUnique();

        builder.HasIndex(c => new { c.UsuarioId, c.Telefone })
           .IsUnique();

        builder.HasOne<UsuarioIdentity>()
            .WithMany()
            .HasForeignKey(c => c.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
