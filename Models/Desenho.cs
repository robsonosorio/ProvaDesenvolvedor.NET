using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ProvaVetta.Models
{
    public class Desenho
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "O desenho deve ter no mínimo peso igual a 0!")]
        public int Peso { get; set; }
        public Desenho[] Filhos { get; set; }

        public Desenho(int id, string nome, int peso, Desenho[] filhos)
        {
            Id = id;
            Nome = nome;
            Peso = peso;
            Filhos = filhos;
        }

        public virtual int PesoTotal()
        {
            int pesoTotal = Peso;
            foreach (var filho1 in Filhos)
            {
                pesoTotal += filho1.Peso;
                foreach (var filho2 in filho1.Filhos)
                {
                    pesoTotal += filho2.Peso;
                }
            }
            return pesoTotal;
        }

        public string MaisPesado()
        {
            string maisPesado = "";
            int peso = 0;
            foreach (var filho1 in Filhos)
            {
                if (filho1.Peso > peso)
                {
                    maisPesado = filho1.Nome;
                }
                foreach (var filho2 in filho1.Filhos)
                {
                    if (filho2.Peso > filho1.Peso)
                    {
                        maisPesado = filho2.Nome;
                    }
                }
            }
            return maisPesado;
        }
        public List<Desenho> ListaFilhosOrdemCrescente()
        {
            List<Desenho> listaFilhos = new List<Desenho>();
            int pesoTotal = Peso;
            foreach (var filho1 in Filhos)
            {
                foreach (var filho2 in filho1.Filhos)
                {
                    pesoTotal += filho2.Peso;
                }
                filho1.Peso = pesoTotal;
                listaFilhos.Add(filho1);
            }
            return (List<Desenho>)listaFilhos.OrderBy(p => p.Peso);
        }

        public  DataTable BuscaComProcedure(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["banco_teste"];
            using  (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter parameter = new SqlParameter();

                SqlCommand command = new SqlCommand("PR_LISTAR_DESENHOS", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@id", SqlDbType.Int).Value = Id;
                command.Connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public override string ToString()
        {
            var retorna = "";
            retorna += ("O desenho {0} tem peso total de {1}.", this.Nome, this.Peso);
            return retorna;
        }
    }
}