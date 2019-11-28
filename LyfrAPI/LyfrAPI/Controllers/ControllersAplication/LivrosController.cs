using LyfrAPI.Aplicacoes.Aplicacoes;
using LyfrAPI.Context;
using LyfrAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System;
using System.IO;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private LyfrDBContext _context;

        //variavel para poder acessar a pasta wwwroot nas funçoes que necessitam de email
        //dependencia será injetada nas classes necessarias
        private PhysicalFileProvider _provedorDiretoriosArquivos = new PhysicalFileProvider(Directory.GetCurrentDirectory());

        public LivrosController(LyfrDBContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public IActionResult Insert([FromBody]Livros livroEnviado)
        {
            try
            {
                if (!ModelState.IsValid || livroEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new LivrosAplicacao(_context, _provedorDiretoriosArquivos).Insert(livroEnviado);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpPut]
        [Route("Update")]
        [Authorize]
        public IActionResult Update([FromBody]Livros livroEnviado)
        {
            try
            {
                if (!ModelState.IsValid || livroEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new LivrosAplicacao(_context, _provedorDiretoriosArquivos).Update(livroEnviado);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpDelete]
        [Route("DeleteByTitulo")]
        [Authorize]
        public IActionResult DeleteByTitulo([FromBody]string titulo)
        {
            try
            {
                if (!ModelState.IsValid || titulo == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new LivrosAplicacao(_context, _provedorDiretoriosArquivos).DeleteByTitulo(titulo);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpGet]
        [Route("GetAllLivros/{numeroDeLivros}")]
        [Authorize]
        public IActionResult GetAllLivros(int numeroDeLivros=0)
        {
            try
            {

                var listaDeLivros = new LivrosAplicacao(_context).GetAllLivros(numeroDeLivros);

                if (listaDeLivros != null)
                {
                    var resposta = JsonConvert.SerializeObject(listaDeLivros, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }
                    );

                    return Ok(resposta);
                }
                else
                {
                    return BadRequest("Nenhum livro cadastrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }


        [HttpPost]
        [Route("GetLivroByTitulo")]
        [Authorize]
        public IActionResult GetLivroByTitulo([FromBody]string titulo)
        {
            try
            {
                var resposta = new LivrosAplicacao(_context, _provedorDiretoriosArquivos).GetLivroByTitulo(titulo);

                if (resposta != null)
                {
                    var livroResposta = JsonConvert.SerializeObject(resposta);
                    return Ok(livroResposta);
                }
                else
                {
                    return BadRequest("Livro não encontrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }


        [HttpPost]
        [Route("GetLivroByTituloWithAutorEditora")]
        [Authorize]
        public IActionResult GetLivroByTituloWithAutorEditora([FromBody]string titulo)
        {
            try
            {
                var resposta = new LivrosAplicacao(_context).GetLivroByTituloWithAutorEditora(titulo);

                if (resposta != null)
                {
                    var livroResposta = JsonConvert.SerializeObject(resposta, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }
                    );

                    return Ok(livroResposta);
                }
                else
                {
                    return BadRequest("Livro não encontrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpGet]
        [Route("Search/{palavraPesquisar}")]
        [Authorize]
        public IActionResult SearchLivros(string palavraPesquisar)
        {
            try
            {
                var resposta = new LivrosAplicacao(_context).SearchLivros(palavraPesquisar);

                if (resposta != null)
                {
                    var livroResposta = JsonConvert.SerializeObject(resposta);
                    return Ok(livroResposta);
                }
                else
                {
                    return BadRequest("Nenhum livro encontrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpPost]
        [Route("GetLivroByTituloWithoutFile")]
        [Authorize]
        public IActionResult GetLivroByTituloWithoutFile([FromBody]string titulo)
        {
            try
            {
                var resposta = new LivrosAplicacao(_context).GetLivroByTituloWithoutFile(titulo);

                if (resposta != null)
                {
                    var livroResposta = JsonConvert.SerializeObject(resposta, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }
                    );

                    return Ok(livroResposta);
                }
                else
                {
                    return BadRequest("Livro não encontrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpPost]
        [Route("GetLivrosByGenero")]
        [Authorize]
        public IActionResult GetLivrosByGenero([FromBody]string genero)
        {
            try
            {
                var resposta = new LivrosAplicacao(_context).GetLivrosByGenero(genero);

                if (resposta != null)
                {
                    var livrosResposta = JsonConvert.SerializeObject(resposta);
                    return Ok(livrosResposta);
                }
                else
                {
                    return BadRequest("Gênero não encontrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpPost]
        [Route("GetLivrosByEditora")]
        [Authorize]
        public IActionResult GetLivrosByEditora([FromBody]int idEditora)
        {
            try
            {
                var resposta = new LivrosAplicacao(_context).GetLivrosByEditora(idEditora);

                if (resposta != null)
                {
                    var livrosResposta = JsonConvert.SerializeObject(resposta);
                    return Ok(livrosResposta);
                }
                else
                {
                    return BadRequest("Editora não encontrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }


        [HttpPost]
        [Route("GetLivrosByAutor")]
        [Authorize]
        public IActionResult GetLivrosByAutor([FromBody]int idAutor)
        {
            try
            {
                var resposta = new LivrosAplicacao(_context).GetLivrosByAutor(idAutor);

                if (resposta != null)
                {
                    var livrosResposta = JsonConvert.SerializeObject(resposta);
                    return Ok(livrosResposta);
                }
                else
                {
                    return BadRequest("Autor não encontrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }
    }
}