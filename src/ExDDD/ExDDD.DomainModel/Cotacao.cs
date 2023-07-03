using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExDDD.DomainModel
{
    public class Cotacao : EntityBase
    {
        public int AtivoId { get; set; }
        public Decimal Preco { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataInclusao { get; set; }

        public override void Validar()
        {
            CampoDataObrigatorio("Data", Data);
            CampoDataObrigatorio("DataInclusao", DataInclusao);
            CampoNumericoObrigatorio("Preco", Preco);
            base.Validar();
        }
    }
}
