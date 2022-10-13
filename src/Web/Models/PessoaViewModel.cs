using Business.Model;

namespace Web.Models
{
    public class PessoaViewModel: BaseViewModel
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public List<PessoaPlanoViewModel>? Planos { get; set; }
        public Guid? PessoaID { get; set; }
        public Guid? PlanoID { get; set; }
    }
}
