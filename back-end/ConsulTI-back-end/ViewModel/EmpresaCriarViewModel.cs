using System.ComponentModel.DataAnnotations;

namespace ConsulTI_back_end.ViewModel
{
    public class EmpresaCriarViewModel
    {


        [Required]
        public string razao_social { get; set; }
        public string nome_fantasia { get; set; }
        public string cnpj {  get; set; }

        public EmpresaCriarViewModel() { }
        
    }
}
