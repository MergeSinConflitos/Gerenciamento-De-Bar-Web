using GerenciamentoDeBar.Dominio.Modulos.ModuloMesa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciamentoDeBar.Infraestrutura.Modulos.ModuloMesa;

public sealed class MesaConfiguration : IEntityTypeConfiguration<Mesa>
{
    public void Configure(EntityTypeBuilder<Mesa> builder)
    {
        builder.ToTable("TBMesa");

        builder.HasKey(m => m.Id)
            .HasName("PK_TBMesa");

        builder.Property(m => m.Id)
            .ValueGeneratedNever();

        builder.Property(m => m.NumeroDaMesa)
            .IsRequired();

        builder.Property(m => m.QuantidadeDeLugares)
            .IsRequired();

        builder.Property(m => m.StatusMesa)
            .IsRequired();

        builder.HasIndex(m => m.NumeroDaMesa)
            .IsUnique()
            .HasDatabaseName("UQ_TBMesa_NumeroDaMesa");
    }
}
