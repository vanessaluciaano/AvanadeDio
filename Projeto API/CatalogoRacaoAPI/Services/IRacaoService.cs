using CatalogoRacaoAPI.InputModel;
using CatalogoRacaoAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoRacaoAPI.Services
{
    public interface IRacaoService : IDisposable
    {
        Task<List<RacaoViewModel>> Obter(int pagina, int quantidade);
        Task<RacaoViewModel> Obter(Guid id);
        Task<RacaoViewModel> Inserir(RacaoInputModel racao);
        Task Atualizar(Guid id, RacaoInputModel racao);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);
    }
}
