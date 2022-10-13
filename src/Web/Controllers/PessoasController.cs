using Business.Interface;
using Business.Model;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
    public class PessoasController : Controller
    {
        private readonly ILogger<PessoasController> _logger;
        private readonly IPessoaRepository pessoaRepository;
        private readonly IPlanoRepository planoRepository;
        private readonly IPessoaPlanoRepository pessoaPlanoRepository;

        public PessoasController(ILogger<PessoasController> logger, IPlanoRepository planoRepository,
            IPessoaRepository pessoaRep, IPessoaPlanoRepository pessoaPlanoRep)
        {
            _logger = logger;
            pessoaRepository = pessoaRep;
            this.planoRepository = planoRepository;
            pessoaPlanoRepository = pessoaPlanoRep;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ObterPessoas()
        {
            var pessoas = await pessoaRepository.ObterTodosPessoasPlanos();
            var planos = await planoRepository.ObterTodos();

            //foreach (var pessoa in pessoas)
            //pessoa.Plano = await planoRepository.ObterPorId(pessoa.PlanoId);
            //pessoa.Pessoa = await pessoaRepository.ObterPorId(pessoa.PessoaId);
            if (TempData["Result"] != null)
            {
                ViewBag.result = TempData["Result"] as string;
            }
            return View(pessoas);
        }

        public async Task<ActionResult> CriarPessoa(Guid? id)
        {
            if (id != null)
            {
                var pessoa = await pessoaRepository.ObterPorId((Guid)id);
                var pessoaVieModel = new PessoaViewModel()
                {
                    Id = pessoa.Id,
                    Nome = pessoa.Nome,
                    CPF = pessoa.CPF,
                    DataNascimento = pessoa.DataNascimento,
                    DataCriacao = pessoa.DataCriacao,
                };
                return View(pessoaVieModel);
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CriarPessoa(PessoaViewModel pessoa)
        {
            if (ModelState.IsValid)
            { 
                if (pessoa.Id != null)
                {
                    try
                    {
                        var pessoaModel = await pessoaRepository.ObterPorId((Guid)pessoa.Id);
                        pessoaModel.Nome = pessoa.Nome;
                        pessoaModel.CPF = pessoa.CPF;
                        pessoaModel.DataNascimento = pessoa.DataNascimento;
                        pessoaModel.DataAtualizacao = DateTime.Now;
                        pessoaModel.Deletado = false;
                        await pessoaRepository.Atualizar(pessoaModel);
                        TempData["Result"] = "Editado sucesso!";
                        return RedirectToAction("ObterPessoas");
                    }
                    catch
                    {
                        TempData["Result"] = "Não foi possivel editar, tente novamente.";
                        return RedirectToAction("ObterPessoas");
                    }
                }
                try
                {
                    var pessoaModel = new Pessoa()
                    {
                        Id = new Guid(),
                        Nome = pessoa.Nome,
                        CPF = pessoa.CPF,
                        DataNascimento = pessoa.DataNascimento,
                        DataCriacao = DateTime.Now,
                        DataAtualizacao = DateTime.Now,
                        Deletado = false,
                        Planos = new List<PessoaPlano>()
                    };

                    await pessoaRepository.Adicionar(pessoaModel);
                    TempData["Result"] = "Cadastrado com sucesso!";
                }
                catch (Exception ex)
                {
                    var exe = ex;
                    TempData["Result"] = "Não foi possivel editar, tente novamente.";
                }
            }

            return RedirectToAction("ObterPessoas");
        }

        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                var pessoaModel = await pessoaRepository.ObterPorId(id);
                if (pessoaModel != null)
                {
                    await pessoaRepository.Remover(pessoaModel);
                    TempData["Result"] = "Deletado com sucesso!";

                }
            }
            catch
            {
                TempData["Result"] = "Não foi possivel editar, tente novamente.";
            }
            return RedirectToAction("ObterPessoas");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}