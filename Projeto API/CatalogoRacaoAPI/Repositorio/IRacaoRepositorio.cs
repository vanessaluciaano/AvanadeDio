using CatalogoRacaoAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoRacaoAPI.Repositorio
{
    public interface IRacaoRepositorio : IDisposable
    {
        Task<List<Racao>> Obter(int pagina, int quantidade);
        Task<Racao> Obter(Guid id);
        Task<List<Racao>> Obter(string nome, string marca);
        Task Inserir(Racao racao);
        Task Atualizar(Racao racao);
        Task Remover(Guid id);
    }
}
