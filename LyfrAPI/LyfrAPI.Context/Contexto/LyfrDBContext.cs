using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LyfrAPI.Models;

namespace LyfrAPI.Context
{
    public partial class LyfrDBContext : DbContext
    {
        public LyfrDBContext()
        {
        }

        public LyfrDBContext(DbContextOptions<LyfrDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrador> Administrador { get; set; }
        public virtual DbSet<Autores> Autores { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Editora> Editora { get; set; }
        public virtual DbSet<Favoritos> Favoritos { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<Historico> Historico { get; set; }
        public virtual DbSet<Livros> Livros { get; set; }
        public virtual DbSet<Sugestao> Sugestao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.HasKey(e => e.Cpf);

                entity.ToTable("administrador");

                entity.HasKey(e => e.IdAdministrador)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdAdministrador)
                    .HasColumnName("Id_Administrador")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Login).HasColumnType("varchar(40)");

                entity.Property(e => e.Login).HasColumnType("varchar(40)");

                entity.Property(e => e.Cpf).HasColumnType("varchar(20)");

                entity.Property(e => e.Email).HasColumnType("varchar(50)");

                entity.Property(e => e.Senha).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Autores>(entity =>
            {
                entity.HasKey(e => e.IdAutor);

                entity.ToTable("autores");

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

                entity.ToTable("cliente");

                entity.HasIndex(e => e.Cpf)
                    .HasName("Cpf")
                    .IsUnique();

                entity.HasIndex(e => e.Email)
                    .HasName("Email")
                    .IsUnique();

                entity.Property(e => e.IdCliente)
                    .HasColumnName("Id_Cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cpf).HasColumnType("varchar(20)");

                entity.Property(e => e.Email).HasColumnType("varchar(70)");

                entity.Property(e => e.Nome).HasColumnType("varchar(80)");

                entity.Property(e => e.Senha).HasColumnType("varchar(70)");
            });

            modelBuilder.Entity<Editora>(entity =>
            {
                entity.HasKey(e => e.IdEditora);

                entity.ToTable("editora");

                entity.Property(e => e.IdEditora)
                    .HasColumnName("Id_Editora")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nome).HasColumnType("varchar(70)");
            });

            modelBuilder.Entity<Favoritos>(entity =>
            {
                entity.HasKey(e => e.Id_Favoritos);

                entity.ToTable("favoritos");

                entity.HasIndex(e => e.FkIdCliente)
                    .HasName("Fk_Id_Cliente");

                entity.HasIndex(e => e.FkIdLivro)
                    .HasName("Fk_Id_Livro");

                entity.Property(e => e.Id_Favoritos)
                    .HasColumnName("Id_Favoritos")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkIdCliente)
                    .HasColumnName("Fk_Id_Cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkIdLivro)
                    .HasColumnName("Fk_Id_Livro")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkIdClienteNavigation)
                    .WithMany(p => p.Favoritos)
                    .HasForeignKey(d => d.FkIdCliente)
                    .HasConstraintName("Fk_Id_Cliente");

                entity.HasOne(d => d.FkIdLivroNavigation)
                    .WithMany(p => p.Favoritos)
                    .HasForeignKey(d => d.FkIdLivro)
                    .HasConstraintName("Fk_Id_Livro");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.IdGenero);

                entity.ToTable("genero");

                entity.Property(e => e.IdGenero)
                    .HasColumnName("Id_Genero")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nome).HasColumnType("varchar(40)");

                entity.Property(e => e.Foto).HasColumnType("varchar(400)");
            });

            modelBuilder.Entity<Historico>(entity =>
            {
                entity.HasKey(e => e.IdHistorico);

                entity.ToTable("historico");

                entity.HasIndex(e => e.FkIdCliente)
                    .HasName("FkIdCliente");

                entity.HasIndex(e => e.FkIdLivro)
                    .HasName("FkIdLivro");

                entity.Property(e => e.IdHistorico)
                    .HasColumnName("IdHistorico")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DataLeitura).HasColumnType("varchar(30)");

                entity.Property(e => e.FkIdCliente)
                    .HasColumnName("FkIdCliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkIdLivro)
                    .HasColumnName("FkIdLivro")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkIdClienteNavigation)
                    .WithMany(p => p.Historico)
                    .HasForeignKey(d => d.FkIdCliente)
                    .HasConstraintName("FkIdCliente");

                entity.HasOne(d => d.FkIdLivroNavigation)
                    .WithMany(p => p.Historico)
                    .HasForeignKey(d => d.FkIdLivro)
                    .HasConstraintName("FkIdLivro");
            });

            modelBuilder.Entity<Livros>(entity =>
            {
                entity.HasKey(e => e.IdLivro);

                entity.ToTable("livros");

                entity.HasIndex(e => e.FkAutor)
                    .HasName("Fk_Autor");

                entity.HasIndex(e => e.FkEditora)
                    .HasName("Fk_Editora");

                entity.Property(e => e.IdLivro)
                    .HasColumnName("Id_Livro")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Ano_Lanc)
                    .HasColumnName("Ano_Lanc")
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

         
            modelBuilder.Entity<Sugestao>(entity =>
            {
                entity.HasKey(e => e.IdSugestao);

                entity.ToTable("sugestao");

                entity.Property(e => e.IdSugestao)
                    .HasColumnName("idSugestao")
                    .HasColumnType("int(11)");

                entity.HasIndex(e => e.FkIdCliente)
                    .HasName("Fk_Id_Cliente5");

                entity.Property(e => e.IdSugestao)
                    .HasColumnName("idSugestao")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkIdCliente)
                    .HasColumnName("Fk_Id_Cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Mensagem).HasColumnType("text");

                entity.Property(e => e.Atendido).HasColumnType("varchar(1)");

                entity.HasOne(d => d.FkIdClienteNavigation)
                    .WithMany(p => p.Sugestao)
                    .HasForeignKey(d => d.FkIdCliente)
                    .HasConstraintName("Fk_Id_Cliente5");
            });
        }
    }
}
