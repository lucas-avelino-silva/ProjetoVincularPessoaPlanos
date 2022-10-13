using Business.Model;

namespace Web.Models
{
    public class PlanoViewModel
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public List<PessoaPlanoViewModel> Pessoas { get; set; }
    }
}
