using eAgenda.Dominio.Modulos.Compromissos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.Infraestrutura.Modulos.Compromissos;

public sealed class CompromissoConfiguration : IEntityTypeConfiguration<Compromisso>
{
    public void Configure(EntityTypeBuilder<Compromisso> builder)
    {
        builder.ToTable("TBCompromissos");

        builder.HasKey(compromisso => compromisso.Id);

        builder.Property(compromisso => compromisso.Assunto)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(compromisso => compromisso.DataOcorrencia)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(compromisso => compromisso.HoraInicio)
            .HasColumnType("time")
            .IsRequired();

        builder.Property(compromisso => compromisso.HoraTermino)
            .HasColumnType("time")
            .IsRequired();

        builder.Property(compromisso => compromisso.Tipo)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(compromisso => compromisso.LinkOuLocal)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasIndex(compromisso => new
        {
            compromisso.DataOcorrencia,
            compromisso.HoraInicio
        });

        builder.HasOne(compromisso => compromisso.Contato)
            .WithMany()
            .HasForeignKey(compromisso => compromisso.ContatoId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
