using LyfrAPI.Context;
using LyfrAPI.Files;
using LyfrAPI.Models;
using LyfrAPI.Models.ModelRoute;
using LyfrAPI.Models.ModelsDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfrAPI.Aplicacoes.Aplicacoes
{
    public class LivrosAplicacao
    {
        //variavel de contexto
        private LyfrDBContext _context;

        //variavel para poder acessar a pasta wwwroot nas funçoes que necessitam de imagem
        //dependencia será injetada na classe ClienteMessages
        private PhysicalFileProvider _provedorDiretoriosArquivos;

        //varivel onde vai guardar a extensão do diretório dos livros
        private string diretorioLivros = "wwwroot/Livros/Epubs";

        //varivel onde vai guardar a extensão do diretório das capas
        private string diretorioCapas = "wwwroot/Livros/Capas";

        //construtor usado para quando NÂO FOR UTILIZAR ARQUIVOS
        public LivrosAplicacao(LyfrDBContext context)
        {
            _context = context;
        }

        //construtor usado para quando FORMOS UTILIZAR ARQUIVOS, COMO NO CASO DO EMAIL
        public LivrosAplicacao(LyfrDBContext context, PhysicalFileProvider provedorDiretoriosArquivos)
        {
            _context = context;
            _provedorDiretoriosArquivos = provedorDiretoriosArquivos;
        }

        public string Insert(Livros livro)
        {
            try
            {
                if (livro != null)
                {
                    if (GetLivroByTitulo(livro.Titulo) != null)
                    {
                        return "Autor já cadastrado na base de dados!";
                    }
                    else
                    {
                        //chama o método que formata o novo nome do livro
                        var nomeLivro = new GetNameFiles().GetNovoNome("lyfr_book", ".epub");


                        //chama o método que formata o novo nome da capa
                        var nomeCapa = new GetNameFiles().GetNovoNome("lyfr_cover", ".jpg");

                        //chama o método para salvar a capa
                        var salvarCapa = new FilesManipulation().ConverterDeBase64EmArquivo(_provedorDiretoriosArquivos.GetFileInfo(diretorioCapas).PhysicalPath, nomeCapa, livro.Capa);

                        //chama o método para salvar o livro
                        var salvarLivro = new FilesManipulation().ConverterDeBase64EmArquivo(_provedorDiretoriosArquivos.GetFileInfo(diretorioLivros).PhysicalPath, nomeLivro, livro.Arquivo);

                    
                        //caso tenha conseguido salvar a foto, atribui o link a ela
                        //senão utiliza uma foto not found
                        if (salvarCapa && salvarLivro)
                        {
                            livro.Arquivo = "wwwroot/Livros/Epubs/" + nomeLivro;
                            livro.Capa = DefaultRoute.RotaPadrao + "/Livros/Capas/" + nomeCapa;

                        }
                        else if (salvarCapa && !salvarLivro)
                        {
                            livro.Arquivo = "wwwroot/Livros/Epubs/None/NotFound.html";
                            livro.Capa = DefaultRoute.RotaPadrao + "/Livros/Capas/" + nomeCapa;
                        }
                        else if (!salvarCapa && salvarLivro)
                        {
                            livro.Arquivo = "wwwroot/Livros/Epubs/" + nomeLivro;
                            livro.Capa = DefaultRoute.RotaPadrao + "/Livros/Capas/None/NotFound.jpg";
                        }
                        else
                        {
                            livro.Arquivo = "wwwroot/Livros/Epubs/None/NotFound.html";
                            livro.Capa = DefaultRoute.RotaPadrao + "/Livros/Capas/None/NotFound.jpg";
                        }

                        //define o total de acessos do livro como 0
                        livro.TotalAcessos = 0;

                        _context.Add(livro);
                        _context.SaveChanges();

                        return "Livro cadastrado com sucesso!";
                    }
                }
                else
                {
                    return "Livro é nulo! Por - favor preencha todos os campos e tente novamente!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public Livros GetById(int idLivro)
        {
            try
            {
                var livro = _context.Livros.Where(x => x.IdLivro.Equals(idLivro));
                return livro.ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Livros GetLivroByTitulo(string titulo)
        {
            Livros primeiroLivro = new Livros();

            try
            {
                if (titulo == string.Empty || titulo == null || titulo == "" || string.IsNullOrWhiteSpace(titulo))
                {
                    return null;
                }

                var livro = _context.Livros.Where(x => x.Titulo == titulo).ToList();
                primeiroLivro = livro.FirstOrDefault();


                if (primeiroLivro != null)
                {
                    //adiciona 1 ao acesso do livro
                    primeiroLivro.TotalAcessos += 1;
                    _context.Livros.Update(primeiroLivro);

                    //retorna o arquivo .epub em base64
                    primeiroLivro.Arquivo = new FilesManipulation().ConverterDeArquivoEmBase64(_provedorDiretoriosArquivos.GetFileInfo(primeiroLivro.Arquivo).PhysicalPath);

                    return primeiroLivro;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public LivrosData GetLivroByTituloWithAutorEditora(string titulo)
        {
            LivrosData primeiroLivro = new LivrosData();

            try
            {
                if (titulo == string.Empty || titulo == null || titulo == "" || string.IsNullOrWhiteSpace(titulo))
                {
                    return null;
                }

                var queryNoBanco = from l in _context.Livros
                             join a in _context.Autores on l.FkAutor equals a.IdAutor
                             join e in _context.Editora on l.FkEditora equals e.IdEditora
                             where l.Titulo.Equals(titulo)
                             select new LivrosData
                             {
                                 IdLivro = l.IdLivro,
                                 Titulo = l.Titulo,
                                 Autor = new AutorAplicacao(_context).GetAutorByNome(a.Nome),
                                 Editora = new EditoraAplicacao(_context).GetEditoraByNome(e.Nome),
                                 Ano_Lanc = l.Ano_Lanc,
                                 Genero = l.Genero,
                                 Sinopse = l.Sinopse,
                                 Capa = l.Capa,
                                 Arquivo = l.Arquivo,
                                 Isbn = l.Isbn,
                                 Idioma = l.Idioma,
                                 TotalAcessos = l.TotalAcessos
                             };

                var livroRetornar = queryNoBanco.Select(x => x).ToList();

                primeiroLivro = livroRetornar.FirstOrDefault();


                if (primeiroLivro != null)
                {
                    //retorna o arquivo .epub em base64
                    primeiroLivro.Arquivo = new FilesManipulation().ConverterDeArquivoEmBase64(_provedorDiretoriosArquivos.GetFileInfo(primeiroLivro.Arquivo).PhysicalPath);
                    return primeiroLivro;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public LivrosData GetLivroByTituloWithoutFile(string titulo)
        {
            Livros primeiroLivro = new Livros();

            try
            {
                if (titulo == string.Empty || titulo == null || titulo == "" || string.IsNullOrWhiteSpace(titulo))
                {
                    return null;
                }

                var livro = _context.Livros.Where(x => x.Titulo == titulo).ToList();
                primeiroLivro = livro.FirstOrDefault();
                primeiroLivro.Arquivo = "http://www.lyfrapi.com.br/" + primeiroLivro.Arquivo;
                primeiroLivro.Arquivo = primeiroLivro.Arquivo.Replace("/wwwroot", "");



                if (primeiroLivro != null)
                {
                    var livroComAutorEditora = new LivrosData
                    {
                        IdLivro = primeiroLivro.IdLivro,
                        Titulo = primeiroLivro.Titulo,
                        Arquivo = primeiroLivro.Arquivo,
                        Ano_Lanc = primeiroLivro.Ano_Lanc,
                        Capa = primeiroLivro.Capa,
                        Genero = primeiroLivro.Genero,
                        Idioma = primeiroLivro.Idioma,
                        Isbn = primeiroLivro.Isbn,
                        Sinopse = primeiroLivro.Sinopse,
                        TotalAcessos = primeiroLivro.TotalAcessos,
                        Editora = new EditoraAplicacao(_context).GetEditoraById(primeiroLivro.FkEditora),
                        Autor = new AutorAplicacao(_context).GetAutorById(primeiroLivro.FkAutor)
                    };


                    return livroComAutorEditora;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public List<Livros> SearchLivros(string palavraPesquisar)
        {
            try
            {
                if (palavraPesquisar == string.Empty || palavraPesquisar == null || palavraPesquisar == "" || string.IsNullOrWhiteSpace(palavraPesquisar))
                {
                    return null;
                }

                var livrosEncontrados = _context.Livros.Where(x => x.Titulo.ToLower().Contains(palavraPesquisar.ToLower())).ToList();


                if (livrosEncontrados != null)
                {
                    return livrosEncontrados;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }



        public string DeleteByTitulo(string titulo)
        {
            try
            {
                if (titulo == string.Empty || titulo == null || titulo == "" || string.IsNullOrWhiteSpace(titulo))
                {
                    return "Título é nulo! Tente novamente.";
                }

                var livroParaExcluir = GetLivroByTitulo(titulo);

                if (livroParaExcluir != null)
                {
                    //função que vai deletar o arquivo .epub do livro
                    var deleteEpub = new FilesManipulation().DeletarArquivo(_provedorDiretoriosArquivos.GetFileInfo(livroParaExcluir.Arquivo).PhysicalPath);

                    //pega o diretorio da capa atraves do link
                    var diretorioCapa = new FilesManipulation().PegarDiretorioLink(livroParaExcluir.Capa);
                    
                    //deleta a capa pelo diretorio passado
                    var deleteCover = new FilesManipulation().DeletarArquivo(_provedorDiretoriosArquivos.GetFileInfo(diretorioCapa).PhysicalPath);


                    _context.Livros.Remove(livroParaExcluir);
                    _context.SaveChanges();

                    return "Livro " + livroParaExcluir.Titulo + " deletado com sucesso";
                }
                else
                {
                    return "Livro não encontrador";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public List<LivrosData> GetAllLivros(int numeroDeLivros = 0)
        {
            var listaDeLivros = new List<Livros>();
            try
            {
                listaDeLivros = _context.Livros.Select(x => x).ToList();

                if (listaDeLivros != null)
                {
                    //caso o numero passado for igual a 0 ele vai retornar todos
                    if (numeroDeLivros != 0 && numeroDeLivros > 0)
                    {
                        listaDeLivros = listaDeLivros.TakeLast(numeroDeLivros).ToList();
                    }

                    var listaAutorEditora = new List<LivrosData>();

                    //pega o objeto de autor e o objeto de editora
                    for(int i = 0; i< listaDeLivros.Count;i++)
                    {
                        //atribui uma lista nula de livros pra não ocorrer repetição 
                        var autor = new AutorAplicacao(_context).GetAutorById(listaDeLivros[i].FkAutor);
                        autor.Livros = new List<Livros>();

                        //atribui uma lista nula de livros pra não ocorrer repetição
                        var editora = new EditoraAplicacao(_context).GetEditoraById(listaDeLivros[i].FkEditora);
                        editora.Livros = new List<Livros>();

                        listaAutorEditora.Add(
                            new LivrosData
                            {
                                IdLivro = listaDeLivros[i].IdLivro,
                                Titulo = listaDeLivros[i].Titulo,
                                Capa = listaDeLivros[i].Capa,
                                Ano_Lanc = listaDeLivros[i].Ano_Lanc,
                                Arquivo = listaDeLivros[i].Arquivo,
                                Sinopse = listaDeLivros[i].Sinopse,
                                Genero = listaDeLivros[i].Genero,
                                Idioma = listaDeLivros[i].Idioma,
                                Isbn = listaDeLivros[i].Isbn,
                                Autor = autor,
                                Editora = editora,
                                TotalAcessos = listaDeLivros[i].TotalAcessos,
                            }
                        );
                    }

                    return listaAutorEditora;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return null;
            }
        }

        public List<Livros> GetLivrosByGenero(string genero)
        {
            try
            {
                if (genero == string.Empty || genero == null || genero == "" || string.IsNullOrWhiteSpace(genero))
                {
                    return null;
                }

                var listaDeLivros = _context.Livros.Where(x => x.Genero == genero).ToList();


                if (listaDeLivros != null)
                {
                    return listaDeLivros;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Livros> GetLivrosByEditora(int idEditora)
        {
            try
            {
                if (idEditora < 0)
                {
                    return null;
                }

                var listaDeLivros = _context.Livros.Where(x => x.FkEditora == idEditora).ToList();


                if (listaDeLivros != null)
                {
                    return listaDeLivros;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Livros> GetLivrosByAutor(int idAutor)
        {
            try
            {
                if (idAutor < 0)
                {
                    return null;
                }

                var listaDeLivros = _context.Livros.Where(x => x.FkAutor == idAutor).ToList();


                if (listaDeLivros != null)
                {
                    return listaDeLivros;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Livros GetLivroById(int idLivro)
        {
            try
            {
                var livro = _context.Livros.Where(x => x.IdLivro.Equals(idLivro));

                return livro.ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }


        public string Update(Livros livroRecebido)
        {
            try
            {
                var livroAlterar = GetLivroById(livroRecebido.IdLivro);

                if (livroRecebido != null)
                {
                    //caso a capa enviada tenha sido diferente de nulo
                    if (livroRecebido.Capa != null)
                    {
                        //pega o nome do arquivo atraves do link
                        var nomeCapa = new GetNameFiles().GetCoverNameFileByLink(livroAlterar.Capa);
                        var diretorioCapa = diretorioCapas + "/" + nomeCapa;

                        //deleta o arquivo antigo
                        var deletarArquivo = new FilesManipulation().DeletarArquivo(_provedorDiretoriosArquivos.GetFileInfo(diretorioCapa).PhysicalPath);

                        //escreve a capa nova
                        var nomeNovaCapa = new GetNameFiles().GetNovoNome("lyfr_cover", ".jpg");
                        var escreverNovaCapa = new FilesManipulation().ConverterDeBase64EmArquivo(_provedorDiretoriosArquivos.GetFileInfo(diretorioCapas).PhysicalPath, nomeNovaCapa, livroRecebido.Capa);

                        //atribui a nova capa ao livro que será alterado
                        livroAlterar.Capa = DefaultRoute.RotaPadrao + "/Livros/Capas/" + nomeNovaCapa;

                    }

                    if (livroRecebido.Arquivo!=null)
                    {
                        //pega o nome do arquivo através dos dados no banco
                        var nomeArquivo = new GetNameFiles().GetFileNameFileByLink(livroAlterar.Arquivo);
                        var diretorioArquivo = diretorioLivros + "/" + nomeArquivo;

                        //deleta o arquivo antigo
                        var deletarArquivo = new FilesManipulation().DeletarArquivo(_provedorDiretoriosArquivos.GetFileInfo(diretorioArquivo).PhysicalPath);

                        //escreve o novo arquivo
                        var nomeNovoArquivo = new GetNameFiles().GetNovoNome("lyfr_book", ".epub");
                        var escreverNovoLivro = new FilesManipulation().ConverterDeBase64EmArquivo(_provedorDiretoriosArquivos.GetFileInfo(diretorioLivros).PhysicalPath, nomeNovoArquivo, livroRecebido.Arquivo);

                        livroAlterar.Arquivo = diretorioLivros+"/"+nomeNovoArquivo;
                    }

                    //atribui os dados enviados ao livro que será alterado
                    var livro = new Livros
                    {
                        IdLivro = livroAlterar.IdLivro,
                        Titulo = livroAlterar.Titulo,
                        Ano_Lanc = livroAlterar.Ano_Lanc,
                        Arquivo = livroAlterar.Arquivo,
                        Capa = livroAlterar.Capa,
                        FkAutor = livroAlterar.FkAutor,
                        FkEditora = livroAlterar.FkEditora,
                        Genero = livroAlterar.Genero,
                        Idioma = livroAlterar.Idioma,
                        Isbn = livroAlterar.Isbn,
                        Sinopse = livroAlterar.Sinopse,
                        TotalAcessos = livroAlterar.TotalAcessos
                    };

                    //atualiza o livro no banco de dados
                    _context.Livros.Update(livro);
                    _context.SaveChanges();

                    return "Livro atualizado com sucesso";

                }
                else
                {
                    return "Dados enviados estão inválidos. Tente novamente";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

    }
}
