using System.Reflection;
using GerenciamentoDeBar.Dominio.Compartilhado;
using GerenciamentoDeBar.Dominio.Compartilhado.Identity;
using GerenciamentoDeBar.Dominio.Modulos.ModuloProprietario.cs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeBar.Infra.Compartilhado.Orm
{
    public sealed class GerenciamentoDeBarDbContext(
      DbContextOptions<GerenciamentoDeBarDbContext> options,
        IUserProvider? userProvider = null
    ) : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<Proprietario> proprietarios => Set<Proprietario>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Assembly assembly = typeof(GerenciamentoDeBarDbContext).Assembly;


            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            // O EF faz cachê do OnModelCreating e variáveis locais não são atualizadas
            if (userProvider != null)
            {
                //Use essa configuração  como exemplo a medida que for criando os modulos
                /*
                modelBuilder.Entity<Categoria>()
                    .HasQueryFilter(c => c.UserId == userProvider.Id);
                */
            }
        }

        public override int SaveChanges()
        {
            Guid? userId = userProvider?.Id;

            if (!userId.HasValue)
            {
                throw new UnauthorizedAccessException(
                    "Não é possível salvar entidades do proprietario sem estar autenticado."
                );
            }

            foreach (var entry in ChangeTracker.Entries<IEntidadeUsuario>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity.UserId == Guid.Empty)
                        {
                            entry.Property(nameof(IEntidadeUsuario.UserId)).CurrentValue = userId.Value;
                        }
                        else if (entry.Entity.UserId != userId.Value)
                        {
                            throw new UnauthorizedAccessException(
                                "Tentativa de criar entidade para outro proprietario."
                            );
                        }

                        break;

                    case EntityState.Modified:
                        Guid idOriginalProprietario = entry
                            .Property(nameof(IEntidadeUsuario.UserId))
                            .OriginalValue is Guid idOriginal
                            ? idOriginal
                            : Guid.Empty;

                        Guid idAtualProprietario = entry
                            .Property(nameof(IEntidadeUsuario.UserId))
                            .OriginalValue is Guid idAtual
                            ? idAtual
                            : Guid.Empty;

                        if (idOriginalProprietario != idAtualProprietario)
                        {
                            throw new UnauthorizedAccessException(
                                  "Não é permitido alterar o proprietario de uma entidade."
                              );
                        }

                        if (idAtualProprietario != userId.Value)
                        {
                            throw new UnauthorizedAccessException(
                                "Tentativa de modificar entidade de outra proprietario."
                            );
                        }

                        break;

                    case EntityState.Deleted:
                        Guid proprietarioOriginal = entry
                            .Property(nameof(IEntidadeUsuario.UserId))
                            .OriginalValue is Guid original
                            ? original
                            : Guid.Empty;

                        if (proprietarioOriginal != userId.Value)
                        {
                            throw new UnauthorizedAccessException(
                                "Tentativa de excluir entidade de outro proprietario."
                            );
                        }

                        break;

                }
            }

            return base.SaveChanges();
        }
    }
}