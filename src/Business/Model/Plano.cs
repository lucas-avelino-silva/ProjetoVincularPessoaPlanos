using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Model
{
    public class Plano: BaseModel
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public List<PessoaPlano>? Pessoas { get; set; }
    }
}
