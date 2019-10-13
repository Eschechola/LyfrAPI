using LyfrAPI.Context;
using LyfrAPI.Files;
using LyfrAPI.Models;
using LyfrAPI.Models.ModelRoute;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyfrAPI.Aplicacoes.Aplicacoes
{
    public class LivroAplicacao
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
        public LivroAplicacao(LyfrDBContext context)
        {
            _context = context;
        }

        //construtor usado para quando FORMOS UTILIZAR ARQUIVOS, COMO NO CASO DO EMAIL
        public LivroAplicacao(LyfrDBContext context, PhysicalFileProvider provedorDiretoriosArquivos)
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

        public List<Livros> GetAllLivros(int numeroDeLivros = 0)
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
                        //lista auxiliar caso tenha sido passado uma limitação, por exemplo retornar os 5 ou os 6 ultimos livros
                        var listaDeLivrosComNumeroDeLivros = new List<Livros>();

                        //contador ja começa com o número do ultimo cliente da lista
                        int indiceUltimoCliente = listaDeLivros.Count - 1;

                        //contador para se comparar com o número passado
                        int i = 0;
                        while (i < numeroDeLivros)
                        {
                            listaDeLivrosComNumeroDeLivros.Add(listaDeLivros[indiceUltimoCliente]);
                            indiceUltimoCliente--;
                            i++;
                        }

                        return listaDeLivrosComNumeroDeLivros;
                    }
                    else
                    {
                        return listaDeLivros;
                    }
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
    }
}
