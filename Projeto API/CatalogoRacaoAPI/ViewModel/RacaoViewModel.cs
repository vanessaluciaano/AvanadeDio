using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoRacaoAPI.ViewModel
{
    public class RacaoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public double Preco { get; set; }
    }
 
}

