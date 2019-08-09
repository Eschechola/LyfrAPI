using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LyfrAPI.Models
{
    public partial class DBLyfrContext : DbContext
    {
        public DBLyfrContext()
        {
        }

        public DBLyfrContext(DbContextOptions<DBLyfrContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autores> Autores { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Editora> Editora { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<Livros> Livros { get; set; }
        public virtual DbSet<LivrosClientes> LivrosClientes { get; set; }

        // Unable to generate entity type for table 'Administrador'. Please see the warning messages.
        // Unable to generate entity type for table 'Favoritos'. Please see the warning messages.
        // Unable to generate entity type for table 'Historico'. Please see the warning messages.
        // Unable to generate entity type for table 'HistoricoPagamento'. Please see the warning messages.
        // Unable to generate entity type for table 'PaginasMarcadas'. Please see the warning messages.
        // Unable to generate entity type for table 'Sugestao'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=db4free.net;Database=lyfr_db;Uid=lyfr_user;Pwd=1234abcd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autores>(entity =>
            {
                entity.HasKey(e => e.IdAutor);

                entity.Property(e => e.IdAutor)
                    .HasColumnName("Id_Autor")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AnoNasc)
                    .HasColumnName("Ano_Nasc")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Bio).HasColumnType("text");

                entity.Property(e => e.Foto).HasColumnType("varchar(200)");

                entity.Property(e => e.Nome).HasColumnType("varchar(70)");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.HasIndex(e => e.Cpf)
                    .HasName("Cpf")
                    .IsUnique();

                entity.HasIndex(e => e.Email)
                    .HasName("Email")
                    .IsUnique();

                entity.Property(e => e.IdCliente)
                    .HasColumnName("Id_Cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cep)
                    .HasColumnName("CEP")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Cidade).HasColumnType("varchar(40)");

                entity.Property(e => e.Cpf).HasColumnType("varchar(20)");

                entity.Property(e => e.DataNasc)
                    .HasColumnName("Data_Nasc")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Email).HasColumnType("varchar(70)");

                entity.Property(e => e.Estado).HasColumnType("char(2)");

                entity.Property(e => e.Nome).HasColumnType("varchar(80)");

                entity.Property(e => e.Numero).HasColumnType("varchar(30)");

                entity.Property(e => e.Rua).HasColumnType("varchar(100)");

                entity.Property(e => e.Senha).HasColumnType("varchar(70)");

                entity.Property(e => e.Telefone).HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<Editora>(entity =>
            {
                entity.HasKey(e => e.IdEditora);

                entity.Property(e => e.IdEditora)
                    .HasColumnName("Id_Editora")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nome).HasColumnType("varchar(70)");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.IdGenero);

                entity.Property(e => e.IdGenero)
                    .HasColumnName("Id_Genero")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nome).HasColumnType("varchar(40)");
            });

            modelBuilder.Entity<Livros>(entity =>
            {
                entity.HasKey(e => e.IdLivro);

                entity.HasIndex(e => e.FkAutor)
                    .HasName("Fk_Autor");

                entity.HasIndex(e => e.FkEditora)
                    .HasName("Fk_Editora");

                entity.Property(e => e.IdLivro)
                    .HasColumnName("Id_Livro")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AnoNasc)
                    .HasColumnName("Ano_Nasc")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Arquivo).HasColumnType("varchar(200)");

                entity.Property(e => e.Capa).HasColumnType("varchar(200)");

                entity.Property(e => e.FkAutor)
                    .HasColumnName("Fk_Autor")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkEditora)
                    .HasColumnName("Fk_Editora")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Genero).HasColumnType("varchar(30)");

                entity.Property(e => e.IdMediaNota)
                    .HasColumnName("Id_MediaNota")
                    .HasColumnType("float(5,3)");

                entity.Property(e => e.Idioma).HasColumnType("varchar(40)");

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Sinopse).HasColumnType("text");

                entity.Property(e => e.Titulo).HasColumnType("varchar(40)");

                entity.Property(e => e.TotalAcessos)
                    .HasColumnName("Total_Acessos")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkAutorNavigation)
                    .WithMany(p => p.Livros)
                    .HasForeignKey(d => d.FkAutor)
                    .HasConstraintName("Fk_Autor");

                entity.HasOne(d => d.FkEditoraNavigation)
                    .WithMany(p => p.Livros)
                    .HasForeignKey(d => d.FkEditora)
                    .HasConstraintName("Fk_Editora");
            });

            modelBuilder.Entity<LivrosClientes>(entity =>
            {
                entity.HasKey(e => e.IdConexao);

                entity.HasIndex(e => e.FkIdCliente)
                    .HasName("Fk_Id_Cliente2");

                entity.HasIndex(e => e.FkIdLivro)
                    .HasName("Fk_Id_Livro2");

                entity.Property(e => e.IdConexao)
                    .HasColumnName("Id_Conexao")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkIdCliente)
                    .HasColumnName("Fk_Id_Cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkIdLivro)
                    .HasColumnName("Fk_Id_Livro")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nota).HasColumnType("int(11)");

                entity.HasOne(d => d.FkIdClienteNavigation)
                    .WithMany(p => p.LivrosClientes)
                    .HasForeignKey(d => d.FkIdCliente)
                    .HasConstraintName("Fk_Id_Cliente2");

                entity.HasOne(d => d.FkIdLivroNavigation)
                    .WithMany(p => p.LivrosClientes)
                    .HasForeignKey(d => d.FkIdLivro)
                    .HasConstraintName("Fk_Id_Livro2");
            });
        }
    }
}
