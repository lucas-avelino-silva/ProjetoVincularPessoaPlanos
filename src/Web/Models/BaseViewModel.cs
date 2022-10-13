namespace Web.Models
{
    public class BaseViewModel
    {
        public Guid? Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public bool Deletado { get; set; }
    }
}
