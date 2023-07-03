using ExDDD.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExDDD.Testes
{
    public class VendaTestes
    {
        [Fact(DisplayName = "Ao validar venda sem preencher os dados deve lançar exception")]
        public void Teste0()
        {
            var venda = new Venda();

            Assert.Throws<ApplicationException>(() => venda.Validar());
        }

        [Fact(DisplayName = "Ao validar venda sem preencher os dados deve lançar exception e exibir mensagem personalizada")]
        public void Teste1()
        {
            var venda = new Venda();

            var result = Assert.Throws<ApplicationException>(() => venda.Validar());

            var mensagens = result.Message.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);


            Assert.Equal("O campo QtdCotas é obrigatório.", mensagens[0]);
            Assert.Equal("O campo DtVenda é obrigatório.", mensagens[1]);
            Assert.Equal("O campo VlUnitario é obrigatório.", mensagens[2]);
        }

        [Fact(DisplayName = "Calcular total da venda")]
        public void Teste2()
        {
            var venda = new Venda();
            venda.QtdCotas = 10;
            venda.VlUnitario = 80.00m;

            var totalDaVenda = venda.CalcularTotalVenda();

            Assert.True(totalDaVenda.Equals(800.00m));
        }

        [Fact(DisplayName = "Se venda não pode ser realizada deve lançar exception")]
        public void Teste3()
        {
            var venda = new Venda();
            venda.QtdCotas = 80;

            Assert.Throws<ApplicationException>(() => venda.ValidaSePodeSerRealizada(70));
        }

        [Fact(DisplayName = "Se venda não pode ser realizada deve lançar exception e exibir mensagem personalizada")]
        public void Teste4()
        {
            var venda = new Venda();
            venda.QtdCotas = 80;

            var result = Assert.Throws<ApplicationException>(() => venda.ValidaSePodeSerRealizada(70));

            Assert.Equal("Quantidade de venda maior que o total de cotas do ativo.", result.Message);
        }
    }
}
