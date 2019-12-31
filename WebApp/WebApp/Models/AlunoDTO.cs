using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class AlunoDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage = "O nome de preenchimento é obrigatorio!")]
        [StringLength(50, ErrorMessage = "O nome deve ter no minimo 2 letras e no maximo 50!", MinimumLength = 2)]
        public string nome { get; set; }

        [Required(ErrorMessage = "O sobrenome é de preenchimento obrigatorio!")]
        [StringLength(50, ErrorMessage = "O sobrenome deve ter no minimo 2 letras e no maximo 50!", MinimumLength = 2)]
        public string sobrenome { get; set; }

        [RegularExpression(@"[0-9]{4}\-[0-9]{2})", ErrorMessage = "Data fora do formato YYYY-MM")]
        public string data { get; set; }

        [Required(ErrorMessage = "Preencha o telefone!")]
        public string telefone { get; set; }

        [Required(ErrorMessage = "O RA é de preenchimento obrigatorio!")]
        [Range(1, 9999, ErrorMessage = "O número de RA deve ser de 1 até 9999!")]
        public int ra { get; set; }
    }
}