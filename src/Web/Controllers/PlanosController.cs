using Business.Interface;
using Business.Model;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class PlanosController : Controller
    {
        protected readonly IPessoaRepository pessoaRepository;
        protected readonly IPlanoRepository planoRepository;
        protected readonly IPessoaPlanoRepository pessoaPlanoRepository;
        public PlanosController(IPessoaRepository pessoaRep, IPlanoRepository planoRep, IPessoaPlanoRepository pessoaPlanoRep)
        {
            pessoaRepository = pessoaRep;
            planoRepository = planoRep;
            pessoaPlanoRepository = pessoaPlanoRep;
        }
        public async Task<IActionResult> Index(Guid? id)
        {
            if(id != null)
            {
                var pesssoaModel = await pessoaRepository.ObterPessoaPlanosPorId((Guid)id);
                var planos = await planoRepository.ObterTodos();
                var planosCandidatos = new List<Plano>();
                foreach (var plano in planos)
                {
                    if (!pesssoaModel.Planos.Any(x => x.Plano == plano))
                    {
                        planosCandidatos.Add(plano);
                    }
                }
                var pessoaViewModel = new PessoaViewModel()
                {
                    Id = pesssoaModel.Id,
                    Nome = pesssoaModel.Nome,
                    CPF = pesssoaModel.CPF,
                    DataNascimento = pesssoaModel.DataNascimento,
                    DataCriacao = pesssoaModel.DataCriacao,
                    Planos = new List<PessoaPlanoViewModel>(),
                };
                foreach (var plano in pesssoaModel.Planos)
                {
                    var PessoaPlanoViewModel = new PessoaPlanoViewModel()
                    {
                        PessoaId = plano.PessoaId,
                        PlanoId = plano.PlanoId,
                        Pessoa = plano.Pessoa,
                        Plano = plano.Plano,
                    };

                    pessoaViewModel.Planos.Add(PessoaPlanoViewModel);
                }

                ViewBag.planos = planosCandidatos;
                if (TempData["Result"] != null)
                {
                    ViewBag.result = TempData["Result"] as string;
                }
                return View(pessoaViewModel);
            }
            return RedirectToAction("ObterPessoas", "Pessoas");
        }

        [HttpPost]
        public async Task<IActionResult> Vincular(PessoaViewModel model)
        {
            try
            {
                if (model.PlanoID != null && model.PessoaID != null)
                {
                    var pessoaPlano = new PessoaPlano()
                    {
                        Id = new Guid(),
                        PessoaId = (Guid)model.PessoaID,
                        PlanoId = (Guid)model.PlanoID,
                        DataCriacao = DateTime.Now,
                        DataAtualizacao = DateTime.Now,
                        Deletado = false,
                    };
                    await pessoaPlanoRepository.Adicionar(pessoaPlano);
                    TempData["Result"] = "Vinculado com sucesso!";
                }
                //var pessoa = await pessoaRepository.ObterPessoaPlanosPorId((Guid)model.PessoaID);
                //var planos = await planoRepository.ObterTodos();
                return RedirectToAction("Index", new { id = (Guid)model.PessoaID });
            }
            catch(Exception ex)
            {
                TempData["Result"] = "Não foi possivel vincular, tente novamente.";
            }

            return RedirectToAction("Index", new { id = (Guid)model.PessoaID });
        }

        public async Task<IActionResult> DeletarPlanoVinculado(Guid id)
        {
            try
            {
                var pessoaPlano = await pessoaPlanoRepository.ObterPorId(id);
                if (pessoaPlano != null)
                {
                    await pessoaPlanoRepository.CancelarPlano(pessoaPlano);
                    TempData["Result"] = "Seu plano foi cancelado com sucesso!";
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                var test = ex;
                TempData["Result"] = "Não foi possível cancelar seu plano.";
            }
            return RedirectToAction("ObterPessoas", "Pessoas");
        }
    }
}
