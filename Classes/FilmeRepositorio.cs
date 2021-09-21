using System;
using System.Collections.Generic;
using DIO.Entreterimento.Interfaces;

namespace DIO.Entreterimento
{
	public class FilmeRepositorio : IRepositorio<Entreterimento>
	{
        private List<Entreterimento> listaFilme = new List<Entreterimento>();
		public void Atualiza(int id, Entreterimento objeto)
		{
			listaFilme[id] = objeto;
		}

		public void Exclui(int id)
		{
			listaFilme[id].Excluir();
		}

		public void Insere(Entreterimento objeto)
		{
			listaFilme.Add(objeto);
		}

		public List<Entreterimento> Lista()
		{
			return listaFilme;
		}

		public int ProximoId()
		{
			return listaFilme.Count;
		}

		public Entreterimento RetornaPorId(int id)
		{
			return listaFilme[id];
		}
	}
}