﻿using APILyfr.Context;
using APILyfr.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APILyfr.Aplicacoes
{
    public class EditoraAplicacao
    {
        private readonly LyfrDBContext _context;

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

        public List<Editora> GetAllEditoras()
        {
            List<Editora> listaDeEditoras = new List<Editora>();
            try
            {
                listaDeEditoras = _context.Editora.Select(x => x).ToList();

                if (listaDeEditoras != null)
                {
                    return listaDeEditoras;
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
