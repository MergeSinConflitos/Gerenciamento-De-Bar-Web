using GerenciamentoDeBar.Dominio.Modulos.ModuloGarcom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciamentoDeBar.Infra.Modulos.ModuloGarcom;

public sealed class GarcomConfiguration : IEntityTypeConfiguration<Garcom>
{
    public void Configure(EntityTypeBuilder<Garcom> builder)
    {
        builder.ToTable("TBGarcom");

        builder.HasKey(g => g.Id)
            .HasName("PK_TBGarcom");

        builder.Property(g => g.Id)
            .ValueGeneratedNever();

        builder.Property(g => g.Nome)
            .IsRequired();

        builder.Property(g => g.Telefone)
            .IsRequired();

        builder.Property(g => g.Cpf)
            .IsRequired();

        builder.HasIndex(g => g.Telefone)
            .IsUnique()
            .HasDatabaseName("UQ_TBGarcom_Telefone");

        builder.HasIndex(g => g.Cpf)
            .IsUnique()
            .HasDatabaseName("UQ_TBGarcom_Cpf");
    }
}