using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoRacaoAPI.Exceptions
{
    public class RacaoNaoCadastradaException : Exception
        {
            public RacaoNaoCadastradaException()
                : base("Esta ração não está cadastrada")
            { }
        }
    }
