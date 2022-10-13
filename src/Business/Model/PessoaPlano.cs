using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Business.Model
{
    public class PessoaPlano: BaseModel
    {
        public Guid PessoaId { get; set; }
        public Guid PlanoId { get; set; }
        public Pessoa Pessoa { get; set; }
        public Plano Plano { get; set; }
    }
}
