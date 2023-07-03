using ExDDD.DomainModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExDDD.Testes
{
    public class AtivoTestes
    {
        [Fact(DisplayName = "Ao validar ativo sem preencher os dados deve lançar exception")]
        public void Teste0()
        {
            var ativo = new Ativo();

            Assert.Throws<ApplicationException>(() => ativo.Validar());
        }

        [Fact(DisplayName = "Ao validar ativo sem preencher os dados deve lançar exception e exibir mensagem personalizada")]
        public void Teste1()
        {
            var ativo = new Ativo();

            var result = Assert.Throws<ApplicationException>(() => ativo.Validar());

            var mensagens = result.Message.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal("O campo Ticker é obrigatório.", mensagens[0]);
            Assert.Equal("O campo CNPJ é obrigatório.", mensagens[1]);
        }

        [Fact(DisplayName = "Ao adicionar novo aporte deve calcular precoMedio, totalInvestido, qtdCotas")]
        public void Teste3()
        {
            var ativo = new Ativo();

            api.Setup(p => p.ValidaPosicao(It.IsAny<string>())).Returns(true);

            var novoAporte = new Aporte();
            novoAporte.QtdCotas = 10;
            novoAporte.VlUnitario = 56.60m;

            ativo.AddAporte(novoAporte, api.Object);


            Assert.True(ativo.PrecoMedio.Equals(56.60m));
            Assert.True(ativo.TotalInvestido.Equals(566.00m));
            Assert.True(ativo.QtdCotas.Equals(10));
        }

        [Fact(DisplayName = "Ao adicionar novo provento deve calcular totalProventos")]
        public void Teste4()
        {
            var ativo = new Ativo();

            var novoProvento = new Provento();
            novoProvento.VlUnitario = 0.87m;
            novoProvento.QtdCotas = 100;

            ativo.AddProvento(novoProvento);

            Assert.True(ativo.TotalProventos.Equals(87.00m));
        }

        [Fact(DisplayName = "Ao adicionar uma venda deve lançar exception se venda não pode ser realizada")]
        public void Teste5()
        {
            var ativo = new Ativo();
            ativo.QtdCotas = 30;

            var novaVenda = new Venda();
            novaVenda.VlUnitario = 0.87m;
            novaVenda.QtdCotas = 100;

            var result = Assert.Throws<ApplicationException>(() => ativo.AddVenda(novaVenda));

            Assert.Equal("Quantidade de venda maior que o total de cotas do ativo.", result.Message);

        }

        [Fact(DisplayName = "Ao adicionar uma venda deve calcular qtdCotas, totalResgatado e saldoAtual")]
        public void Teste6()
        {
            var ativo = ObterAtivoComValores();

            var novaVenda = new Venda();
            novaVenda.VlUnitario = 80.00m;
            novaVenda.QtdCotas = 10;

            ativo.AddVenda(novaVenda);


            Assert.True(ativo.QtdCotas.Equals(90));
            Assert.True(ativo.TotalResgatado.Equals(800.00m));
            Assert.True(ativo.SaldoAtual.Equals(9200.00m));

        }

        [Fact(DisplayName = "Ao adicionar nova cotação deve setar cotacaoAtual e calcular ganhoPerda")]
        public void Teste7()
        {
            var ativo = ObterAtivoComValores();

            var novaCotacao = new Cotacao();
            novaCotacao.Preco = 80.00m;

            ativo.AddCotacao(novaCotacao);

            Assert.True(ativo.CotacaoAtual.Equals(80.00m));
            Assert.True(ativo.GanhoPerda.Equals(-2000.00m));
        }

        [Fact(DisplayName = "Ao adicionar novo aporte com Ativo não validado deve lançar exception")]
        public void Teste8()
        {
            var ativo = new Ativo();

            api.Setup(p => p.ValidaPosicao(It.IsAny<string>())).Returns(false);


            var result = Assert.Throws<ApplicationException>(() => ativo.AddAporte(It.IsAny<Aporte>(), api.Object));


            Assert.Equal("O Ativo não é recondado para Aporte.", result.Message);

        }


        private Ativo ObterAtivoComValores()
        {
            return new Ativo()
            {
                TotalInvestido = 10000.00m,
                QtdCotas = 100,
                SaldoAtual = 10000.00m,
                PrecoMedio = 100.00m,
                CotacaoAtual = 80.00m
            };
        }

        private readonly Mock<IApiChecagem> api = new();
    }
}
