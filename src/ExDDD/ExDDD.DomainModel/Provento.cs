using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExDDD.DomainModel
{
    public class Provento : EntityBase
    {
        public int AtivoId { get; set; }
        public DateTime Data { get; set; }
        public int QtdCotas { get; set; }
        public decimal VlUnitario { get; set; }

        public override void Validar()
        {

            CampoDataObrigatorio("Data", Data);
            CampoNumericoObrigatorio("QtdCotas", QtdCotas);
            CampoNumericoObrigatorio("VlProvento", VlUnitario);

            base.Validar();
        }
        public decimal CalcularTotalProvento()
        {
            return decimal.Multiply(VlUnitario, QtdCotas);
        }

    }
}
