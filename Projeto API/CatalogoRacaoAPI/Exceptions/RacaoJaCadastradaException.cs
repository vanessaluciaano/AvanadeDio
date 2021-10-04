using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoRacaoAPI.Exceptions
{
    public class RacaoJaCadastradaException : Exception
    {
        public RacaoJaCadastradaException()
            : base("Esta ração já está cadastrado")
        { }
    }
}