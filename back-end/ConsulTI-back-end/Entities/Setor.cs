namespace ConsulTI_back_end.Entities
{
    public class Setor
    {
        public int id { get; set; }
        public string descricao { get; set; }

        public ICollection<Empresa> empresas { get; set; }
    }
}
