using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoRacaoAPI.InputModel
{
    public class RacaoInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da Marca deve conter entre 3 e 100 caracteres")]
        public string Marca { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "O preço deve ser de no mínimo 1 real e no máximo 1000 reais")]
        public double Preco { get; set; }
    }
}
