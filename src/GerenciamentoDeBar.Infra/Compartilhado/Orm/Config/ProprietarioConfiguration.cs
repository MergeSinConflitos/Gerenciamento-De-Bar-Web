using GerenciamentoDeBar.Dominio.Modulos.ModuloProprietario;
using GerenciamentoDeBar.Dominio.Modulos.ModuloProprietario.cs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciamentoDeBar.Infra.Compartilhado.Orm.Configurations;

public sealed class ProprietarioConfiguration
    : IEntityTypeConfiguration<Proprietario>
{
    public void Configure(EntityTypeBuilder<Proprietario> builder)
    {
        builder.ToTable("TBProprietario");

        builder.HasKey(p => p.UserId)
            .HasName("PK_TBProprietario");

        builder.Property(p => p.UserId)
            .ValueGeneratedNever();

        builder.Property(p => p.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne<IdentityUser<Guid>>()
            .WithOne()
            .HasForeignKey<Proprietario>(p => p.UserId)
            .HasConstraintName("FK_TBProprietario_AspNetUsers")
            .OnDelete(DeleteBehavior.Restrict);
    }
}