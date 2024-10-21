namespace ConsulTI_back_end.Entities
{
    public class Empresa
    {
        public int id {  get; set; }
        public string razao_social { get; set; }
        public string ?nome_fantasia { get; set; }
        public string cnpj {  get; set; }

        public ICollection<Setor> setores { get; set; }
    }
}
