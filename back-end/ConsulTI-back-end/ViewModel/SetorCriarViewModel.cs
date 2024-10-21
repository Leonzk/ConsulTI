using System.ComponentModel.DataAnnotations;

namespace ConsulTI_back_end.ViewModel
{
    public class SetorCriarViewModel
    {
        [Required]
        public string descricao { get; set; }

        public SetorCriarViewModel() { }
    }
}
