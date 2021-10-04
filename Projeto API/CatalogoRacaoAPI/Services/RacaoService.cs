using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoRacaoAPI.Entidades;
using CatalogoRacaoAPI.Exceptions;
using CatalogoRacaoAPI.InputModel;
using CatalogoRacaoAPI.ViewModel;
using CatalogoRacaoAPI.Repositorio;

namespace CatalogoRacaoAPI.Services
{
    public class RacaoService : IRacaoService
    {
        private readonly IRacaoRepositorio _racaoRepositorio;

        public RacaoService(IRacaoRepositorio racaoRepositorio)
        {
            _racaoRepositorio = racaoRepositorio;
        }

        public async Task<List<RacaoViewModel>> Obter(int pagina, int quantidade)
        {
            var racoes = await _racaoRepositorio.Obter(pagina, quantidade);

            return racoes.Select(racao => new RacaoViewModel
            {
                Id = racao.Id,
                Nome = racao.Nome,
                Marca = racao.Marca,
                Preco = racao.Preco
            })
                               .ToList();
        }

        public async Task<RacaoViewModel> Obter(Guid id)
        {
            var racao = await _racaoRepositorio.Obter(id);

            if (racao == null)
                return null;

            return new RacaoViewModel
            {
                Id = racao.Id,
                Nome = racao.Nome,
                Marca = racao.Marca,
                Preco = racao.Preco
            };
        }

        public async Task<RacaoViewModel> Inserir(RacaoInputModel racao)
        {
            var entidadeRacao = await _racaoRepositorio.Obter(racao.Nome, racao.Marca);

            if (entidadeRacao.Count > 0)
                throw new RacaoJaCadastradaException();

            var racaoInsert = new Racao
            {
                Id = Guid.NewGuid(),
                Nome = racao.Nome,
                Marca = racao.Marca,
                Preco = racao.Preco
            };

            await _racaoRepositorio.Inserir(racaoInsert);

            return new RacaoViewModel
            {
                Id = racaoInsert.Id,
                Nome = racao.Nome,
                Marca = racao.Marca,
                Preco = racao.Preco
            };
        }

        public async Task Atualizar(Guid id, RacaoInputModel racao)
        {
            var entidadeRacao = await _racaoRepositorio.Obter(id);

            if (entidadeRacao == null)
                throw new RacaoNaoCadastradaException();

            entidadeRacao.Nome = racao.Nome;
            entidadeRacao.Marca = racao.Marca;
            entidadeRacao.Preco = racao.Preco;

            await _racaoRepositorio.Atualizar(entidadeRacao);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeRacao = await _racaoRepositorio.Obter(id);

            if (entidadeRacao == null)
                throw new RacaoNaoCadastradaException();

            entidadeRacao.Preco = preco;

            await _racaoRepositorio.Atualizar(entidadeRacao);
        }

        public async Task Remover(Guid id)
        {
            var racao = await _racaoRepositorio.Obter(id);

            if (racao == null)
                throw new RacaoNaoCadastradaException();

            await _racaoRepositorio.Remover(id);
        }

        public void Dispose()
        {
            _racaoRepositorio?.Dispose();
        }
    }
}