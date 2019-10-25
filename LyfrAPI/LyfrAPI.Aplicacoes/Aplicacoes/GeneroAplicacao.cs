using LyfrAPI.Context;
using LyfrAPI.Files;
using LyfrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LyfrAPI.Models.ModelRoute;
using Microsoft.Extensions.FileProviders;

namespace LyfrAPI.Aplicacoes
{
    public class GeneroAplicacao
    {
        private LyfrDBContext _context;

        //variavel para poder acessar a pasta wwwroot nas funçoes que necessitam de imagem
        //dependencia será injetada na classe ClienteMessages
        private PhysicalFileProvider _provedorDiretoriosArquivos;

        //varivel onde vai guardar a extensão do diretório das fotos
        private string diretorioFotos = "wwwroot/Generos/Fotos";

        //construtor usado para quando NÂO FOR UTILIZAR ARQUIVOS
        public GeneroAplicacao(LyfrDBContext context)
        {
            _context = context;
        }

        //construtor usado para quando FORMOS UTILIZAR ARQUIVOS, COMO NO CASO DAS FOTOS
        public GeneroAplicacao(LyfrDBContext context, PhysicalFileProvider provedorDiretoriosArquivos)
        {
            _context = context;
            _provedorDiretoriosArquivos = provedorDiretoriosArquivos;
        }

        public string Insert(Genero genero)
        {
            try
            {
                if (genero != null)
                {
                    if (GetGeneroByNome(genero.Nome) != null)
                    {
                        return "Genero já cadastrado na base de dados!";
                    }
                    else
                    {
                        //chama o método que formata o novo nome do autor
                        var nomeFoto = new GetNameFiles().GetNovoNome("lyfr_gender_", ".jpg");
                        //chama o método para salvar a foto
                        var salvarFoto = new FilesManipulation().ConverterDeBase64EmArquivo(_provedorDiretoriosArquivos.GetFileInfo(diretorioFotos).PhysicalPath, nomeFoto, genero.Foto);

                        //caso tenha conseguido salvar a foto, atribui o link a ela
                        //senão utiliza uma foto not found
                        if (salvarFoto)
                        {
                            genero.Foto = DefaultRoute.RotaPadrao + "/Generos/Fotos/" + nomeFoto;

                        }
                        else
                        {
                            genero.Foto = DefaultRoute.RotaPadrao + "/Generos/None/NotFound.jpg";
                        }

                        _context.Add(genero);
                        _context.SaveChanges();

                        return "Genero cadastrado com sucesso!";
                    }
                }
                else
                {
                    return "Genero é nulo! Por - favor preencha todos os campos e tente novamente!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public Genero GetGeneroByNome(string nome)
        {
            Genero primeiroGenero = new Genero();

            try
            {
                if (nome == string.Empty || nome == null || nome == "" || string.IsNullOrWhiteSpace(nome))
                {
                    return null;
                }

                var genero = _context.Genero.Where(x => x.Nome == nome).ToList();
                primeiroGenero = genero.FirstOrDefault();


                if (primeiroGenero != null)
                {
                    return primeiroGenero;
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

        public List<Genero> GetAllGeneros()
        {
            List<Genero> listaDeGeneros = new List<Genero>();
            try
            {
                listaDeGeneros = _context.Genero.Select(x => x).ToList();

                if (listaDeGeneros != null)
                {
                    return listaDeGeneros;
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

        public string DeleteByNome(string nome)
        {
            try
            {
                if (nome == string.Empty || nome == null || nome == "" || string.IsNullOrWhiteSpace(nome))
                {
                    return "Genero inválido! Por favor tente novamente.";
                }
                else
                {
                    var genero = GetGeneroByNome(nome);

                    if (genero != null)
                    {
                        //pega o diretorio da capa atraves do link
                        var diretorioFoto = new FilesManipulation().PegarDiretorioLink(genero.Foto);

                        //deleta a capa pelo diretorio passado
                        var deletePicture = new FilesManipulation().DeletarArquivo(_provedorDiretoriosArquivos.GetFileInfo(diretorioFoto).PhysicalPath);

                        _context.Genero.Remove(genero);
                        _context.SaveChanges();

                        return "Genero " + genero.Nome + " deletado com sucesso!";
                    }
                    else
                    {
                        return "Genero não cadastrado!";
                    }
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }
    }
}
