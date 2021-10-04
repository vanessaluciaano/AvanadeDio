using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CatalogoRacaoAPI.Entidades;

namespace CatalogoRacaoAPI.Repositorio
{
    public class RacaoSQLServerRepositorio : IRacaoRepositorio
    {
        private readonly SqlConnection sqlConnection;

        public RacaoSQLServerRepositorio(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Racao>> Obter(int pagina, int quantidade)
        {
            var racao = new List<Racao>();

            var comando = $"select * from racao order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                racao.Add(new Racao
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Marca = (string)sqlDataReader["Marca"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return racao;
        }

        public async Task<Racao> Obter(Guid id)
        {
            Racao racao = null;

            var comando = $"select * from Racao where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                racao = new Racao
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Marca = (string)sqlDataReader["Marca"],
                    Preco = (double)sqlDataReader["Preco"]
                };
            }

            await sqlConnection.CloseAsync();

            return racao;
        }

        public async Task<List<Racao>> Obter(string nome, string marca)
        {
            var racoes = new List<Racao>();

            var comando = $"select * from Racoes where Nome = '{nome}' and Marca = '{marca}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                racoes.Add(new Racao
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Marca = (string)sqlDataReader["Marca"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return racoes;
        }

        public async Task Inserir(Racao racao)
        {
            var comando = $"insert Racoes (Id, Nome, Marca, Preco) values ('{racao.Id}', '{racao.Nome}', '{racao.Marca}', {racao.Preco.ToString().Replace(",", ".")})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Racao racao)
        {
            var comando = $"update Racoes set Nome = '{racao.Nome}', Marca = '{racao.Marca}', Preco = {racao.Preco.ToString().Replace(",", ".")} where Id = '{racao.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Racoes where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}