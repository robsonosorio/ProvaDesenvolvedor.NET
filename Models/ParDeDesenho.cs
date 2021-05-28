using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProvaVetta.Models
{
    public class ParDeDesenho : Desenho
    {
        public ParDeDesenho(int id, string nome, int peso, Desenho[] filhos) : base(id, nome, peso, filhos)
        {
            Id = id;
            Nome = nome;
            Peso = peso;
            Filhos = filhos;
        }

        public override int PesoTotal()
        {
            var resultado = base.PesoTotal();
            resultado *= 2;

            return (resultado);
        }
    }
}