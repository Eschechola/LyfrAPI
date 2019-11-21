using LyfrAPI.Context;
using LyfrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LyfrAPI.Aplicacoes
{
    public class EditoraAplicacao
    {
        private LyfrDBContext _context;

        public EditoraAplicacao(LyfrDBContext context)
        {
            _context = context;
        }

        public string Insert(Editora editora)
        {
            try
            {
                if (editora != null)
                {
                    if (GetEditoraByNome(editora.Nome) != null)
                    {
                        return "Editora já cadastrada na base de dados!";
                    }
                    else
                    {
                        _context.Add(editora);
                        _context.SaveChanges();

                        return "Editora cadastrada com sucesso!";
                    }
                }
                else
                {
                    return "Editora é nula! Por - favor preencha todos os campos e tente novamente!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public Editora GetEditoraByNome(string nome)
        {
            Editora primeiraEditora = new Editora();

            try
            {
                if (nome == string.Empty || nome == null || nome == "" || string.IsNullOrWhiteSpace(nome))
                {
                    return null;
                }

                var editora = _context.Editora.Where(x => x.Nome == nome).ToList();
                primeiraEditora = editora.FirstOrDefault();


                if (primeiraEditora != null)
                {
                    return primeiraEditora;
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

        public List<Editora> GetAllEditoras(int numeroDeEditoras=0)
        {
            List<Editora> listaDeEditoras = new List<Editora>();
            try
            {
                listaDeEditoras = _context.Editora.Select(x => x).ToList();

                if (listaDeEditoras != null)
                {
                    //caso o numero passado for igual a 0 ele vai retornar todos
                    if (numeroDeEditoras != 0)
                    {
                        //lista auxiliar caso tenha sido passado uma limitação, por exemplo retornar as 5 ou as 6 ultimas editoras
                        var listaDeAutoresComNumeroDeAutores = new List<Editora>();

                        //contador ja começa com o número do ultimo cliente da lista
                        int indiceUltimaEditora = listaDeEditoras.Count - 1;


                        //contador para se comparar com o número passado
                        int i = 0;
                        while (i < numeroDeEditoras)
                        {
                            listaDeAutoresComNumeroDeAutores.Add(listaDeEditoras[indiceUltimaEditora]);
                            indiceUltimaEditora--;
                            i++;
                        }

                        return listaDeAutoresComNumeroDeAutores;
                    }
                    else
                    {
                        return listaDeEditoras;
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

        public Editora GetEditoraById(int id)
        {
            try
            {
                var editora = _context.Editora.Where(x => x.IdEditora.Equals(id));

                return editora.ToList().FirstOrDefault();
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
                    return "Editora inválido! Por favor tente novamente.";
                }
                else
                {
                    var editora = GetEditoraByNome(nome);

                    if (editora != null)
                    {
                        _context.Editora.Remove(editora);
                        _context.SaveChanges();

                        return "Editora " + editora.Nome + " deletado com sucesso!";
                    }
                    else
                    {
                        return "Editora não cadastrado!";
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
