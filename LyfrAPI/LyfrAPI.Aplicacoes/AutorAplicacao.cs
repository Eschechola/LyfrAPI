﻿using LyfrAPI.Context;
using LyfrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyfrAPI.Aplicacoes
{
    public class AutorAplicacao
    {
        private LyfrDBContext _context;

        public AutorAplicacao(LyfrDBContext context)
        {
            _context = context;
        }

        public string Insert(Autores autor)
        {
            try
            {
                if (autor != null)
                {
                    if (GetAutorByNome(autor.Nome) != null)
                    {
                        return "Autor já cadastrado na base de dados!";
                    }
                    else
                    {
                        _context.Add(autor);
                        _context.SaveChanges();

                        return "Autor cadastrado com sucesso!";
                    }
                }
                else
                {
                    return "Autor é nulo! Por - favor preencha todos os campos e tente novamente!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public Autores GetAutorByNome(string nome)
        {
            Autores primeiroAutor = new Autores();

            try
            {
                if (nome == string.Empty || nome == null || nome == "" || string.IsNullOrWhiteSpace(nome))
                {
                    return null;
                }

                var autor = _context.Autores.Where(x => x.Nome == nome).ToList();
                primeiroAutor = autor.FirstOrDefault();


                if (primeiroAutor != null)
                {
                    return primeiroAutor;
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

        public List<Autores> GetAllAutores(int numeroDeAutores=0)
        {
            List<Autores> listaDeAutores = new List<Autores>();
            try
            {

                listaDeAutores = _context.Autores.Select(x => x).ToList();

                if (listaDeAutores != null)
                {
                    //caso o numero passado for igual a 0 ele vai retornar todos
                    if (numeroDeAutores != 0)
                    {
                        //lista auxiliar caso tenha sido passado uma limitação, por exemplo retornar os 5 ou os 6 ultimos autores
                        var listaDeAutoresComNumeroDeAutores = new List<Autores>();

                        //verifica se existe a quantidade de clientes que foi pedida (evitar exceção)
                        if (numeroDeAutores > listaDeAutores.Count)
                        {
                            numeroDeAutores = listaDeAutores.Count;
                        }

                        //contador ja começa com o número do ultimo cliente da lista
                        int indiceUltimoAutor = listaDeAutores.Count - 1;
                        //contador para se comparar com o número passado
                        int i = 0;
                        while (i < numeroDeAutores)
                        {
                            listaDeAutoresComNumeroDeAutores.Add(listaDeAutores[indiceUltimoAutor]);
                            indiceUltimoAutor--;
                            i++;
                        }

                        return listaDeAutoresComNumeroDeAutores;
                    }
                    else
                    {
                        return listaDeAutores;
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

        public string DeleteByNome(string nome)
        {
            try
            {
                if (nome == string.Empty || nome == null || nome == "" || string.IsNullOrWhiteSpace(nome))
                {
                    return "Nome inválido! Por favor tente novamente.";
                }
                else
                {
                    var autor = GetAutorByNome(nome);

                    if (autor != null)
                    {
                        _context.Autores.Remove(autor);
                        _context.SaveChanges();

                        return "Autor " + autor.Nome + " deletado com sucesso!";
                    }
                    else
                    {
                        return "Autor não cadastrado!";
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
