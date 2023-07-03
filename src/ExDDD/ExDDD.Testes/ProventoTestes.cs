using ExDDD.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExDDD.Testes
{
    public class ProventoTestes
    {
        [Fact(DisplayName = "Ao validar provento sem preencher os dados deve lançar exception")]
        public void Teste0()
        {
            var provento = new Provento();

            Assert.Throws<ApplicationException>(() => provento.Validar());
        }

        [Fact(DisplayName = "Ao validar provento sem preencher os dados deve lançar exception e exibir mensagem personalizada")]
        public void Teste1()
        {
            var provento = new Provento();

            var result = Assert.Throws<ApplicationException>(() => provento.Validar());

            var mensagens = result.Message.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);


            Assert.Equal("O campo Data é obrigatório.", mensagens[0]);
            Assert.Equal("O campo QtdCotas é obrigatório.", mensagens[1]);
            Assert.Equal("O campo VlProvento é obrigatório.", mensagens[2]);
        }

        [Fact(DisplayName = "Calcular total de provento")]
        public void Teste2()
        {
            var provento = new Provento();
            provento.QtdCotas = 100;
            provento.VlUnitario = 0.80m;

            var totalProvento = provento.CalcularTotalProvento();

            Assert.True(totalProvento.Equals(80.00m));
        }
    }
}
