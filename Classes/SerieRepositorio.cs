using System;
using System.Collections.Generic;
using DIO.Entreterimento.Interfaces;

namespace DIO.Entreterimento
{
	public class SerieRepositorio : IRepositorio<Entreterimento>
	{
        private List<Entreterimento> listaSerie = new List<Entreterimento>();
		public void Atualiza(int id, Entreterimento objeto)
		{
			listaSerie[id] = objeto;
		}

		public void Exclui(int id)
		{
			listaSerie[id].Excluir();
		}

		public void Insere(Entreterimento objeto)
		{
			listaSerie.Add(objeto);
		}

		public List<Entreterimento> Lista()
		{
			return listaSerie;
		}

		public int ProximoId()
		{
			return listaSerie.Count;
		}

		public Entreterimento RetornaPorId(int id)
		{
			return listaSerie[id];
		}
	}
}