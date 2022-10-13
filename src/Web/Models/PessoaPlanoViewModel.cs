using Business.Model;

namespace Web.Models
{
    public class PessoaPlanoViewModel: BaseViewModel
    {
        public Guid PessoaId { get; set; }
        public Guid PlanoId { get; set; }
        public Pessoa Pessoa { get; set; }
        public Plano Plano { get; set; }
    }
}
