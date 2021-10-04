using CatalogoRacaoAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoRacaoAPI.Repositorio
{
    public class RacaoRepositorio : IRacaoRepositorio
    {
        private static Dictionary<Guid, Racao> racoes = new Dictionary<Guid, Racao>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Racao{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "Tratto Premium", Marca = "Integral Mix", Preco = 200} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Racao{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "Tratto Cat", Marca = "Integral Mix", Preco = 190} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Racao{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "Pedigree", Marca = "Mars", Preco = 180} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Racao{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome = "Whiskas", Marca = "Mars", Preco = 170} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Racao{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Nome = "Allexis", Marca = "Presence", Preco = 80} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Racao{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Nome = "Blisk", Marca = "Presence", Preco = 190} }
        };

        public Task<List<Racao>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(racoes.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Racao> Obter(Guid id)
        {
            if (!racoes.ContainsKey(id))
                return Task.FromResult<Racao>(null);

            return Task.FromResult(racoes[id]);
        }

        public Task<List<Racao>> Obter(string nome, string marca)
        {
            return Task.FromResult(racoes.Values.Where(racao => racao.Nome.Equals(nome) && racao.Marca.Equals(marca)).ToList());
        }

        public Task<List<Racao>> ObterSemLambda(string nome, string marca)
        {
            var retorno = new List<Racao>();

            foreach (var racao in racoes.Values)
            {
                if (racao.Nome.Equals(nome) && racao.Marca.Equals(marca))
                    retorno.Add(racao);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Racao racao)
        {
            racoes.Add(racao.Id, racao);
            return Task.CompletedTask;
        }

        public Task Atualizar(Racao racao)
        {
            racoes[racao.Id] = racao;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            racoes.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexão com o banco
        }
    }
}