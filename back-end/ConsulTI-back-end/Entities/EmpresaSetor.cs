namespace ConsulTI_back_end.Entities
{
    public class EmpresaSetor
    {
        public int empresa_id {  get; set; }

        public int setor_id { get; set; }

        public virtual Empresa empresa { get; set; }

        public virtual Setor setor { get; set; }
    }
}
