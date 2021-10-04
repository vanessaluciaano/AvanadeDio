using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoRacaoAPI.Entidades
{
    public class Racao
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public double Preco { get; set; }

        internal void Add(Guid id, Racao racao)
        {
            throw new NotImplementedException();
        }

        internal static void Add(Racao racao)
        {
            throw new NotImplementedException();
        }
    }
}
