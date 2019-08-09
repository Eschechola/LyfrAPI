using LyfrAPI.Interfaces;
using LyfrAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LyfrAPI.Aplicacoes
{
    public class ClienteAplicacao : IDataLyfr<Cliente>
    {
        private readonly DBLyfrContext _context;

        public ClienteAplicacao(DBLyfrContext context)
        {
            _context = context;
        }

        public string Insert(Cliente cliente)
        {
            try
            {
                if (cliente != null)
                {
                    if (GetClienteByEmail(cliente.Email)!=null)
                    {
                        return "Email já cadastrado na base de dados!";
                    }
                    else if (GetClienteByCPF(cliente.Cpf) != null)
                    {
                        return "CPF já cadastrado na base de dados!";
                    }
                    else
                    {
                        _context.Add(cliente);
                        _context.SaveChanges();

                        return "Cliente cadastrado com sucesso!";
                    }
                }
                else
                {
                    return "Cliente é nulo! Por - favor preencha todos os campos e tente novamente!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public string DeleteByEmail(string email)
        {
            try
            {
                if (email == string.Empty || email == null || email == "" || string.IsNullOrWhiteSpace(email))
                {
                    return "Email inválido! Por favor tente novamente.";
                }
                else
                {
                    var cliente = GetClienteByEmail(email);

                    if (cliente != null)
                    {
                        _context.Cliente.Remove(cliente);
                        _context.SaveChanges();

                        return "Cliente " + cliente.Nome +" deletado com sucesso!";
                    }
                    else
                    {
                        return "Cliente não cadastrado!";
                    }
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public string DeleteByCPF(string CPF)
        {
            try
            {
                if (CPF == string.Empty || CPF == null || CPF == "" || string.IsNullOrWhiteSpace(CPF))
                {
                    return "Email inválido! Por favor tente novamente.";
                }
                else
                {
                    var cliente = GetClienteByCPF(CPF);

                    if (cliente != null)
                    {
                        _context.Cliente.Remove(cliente);
                        _context.SaveChanges();

                        return "Cliente " + cliente.Nome + " deletado com sucesso!";
                    }
                    else
                    {
                        return "Cliente não cadastrado!";
                    }
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public string Alter(Cliente cliente)
        {
            try
            {
                if (cliente == null)
                {
                    return "Dados inválidos! Por favor tente novamente.";
                }
                else
                {
                    if (cliente != null)
                    {
                        _context.Cliente.Update(cliente);
                        _context.SaveChanges();

                        return "Cliente " + cliente.Nome + " alterado com sucesso!";
                    }
                    else
                    {
                        return "Cliente não cadastrado!";
                    }
                }
            }
            catch (Exception)
            {
                return "Já existe um usuário cadastrado com seu Email e/ou CPF.";
            }
        }

        public Cliente GetClienteByEmail(string email)
        {
            Cliente primeiroCliente = new Cliente();

            try
            {
                if (email == string.Empty || email == null || email == "" || string.IsNullOrWhiteSpace(email))
                {
                    return null;
                }

                var cliente = _context.Cliente.Where(x => x.Email == email).ToList();
                primeiroCliente = cliente.FirstOrDefault();
                

                if (primeiroCliente != null)
                {
                    return primeiroCliente;
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

        public Cliente GetClienteByCPF(string cpf)
        {
            Cliente primeiroCliente = new Cliente();

            try
            {
                if (cpf == string.Empty || cpf == null || cpf == "" || string.IsNullOrWhiteSpace(cpf))
                {
                    return null;
                }

                var cliente = _context.Cliente.Where(x => x.Cpf == cpf).ToList();
                primeiroCliente = cliente.FirstOrDefault();

                if (primeiroCliente != null)
                {
                    return primeiroCliente;
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

        public List<Cliente> GetAllClientes()
        {
            List<Cliente> listaDeClientes = new List<Cliente>();
            try
            {
                listaDeClientes = _context.Cliente.Select(x => x).ToList();

                if (listaDeClientes!=null)
                {
                    return listaDeClientes;
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

        public string Delete(Cliente item)
        {
            return "";
        }
    }
}
