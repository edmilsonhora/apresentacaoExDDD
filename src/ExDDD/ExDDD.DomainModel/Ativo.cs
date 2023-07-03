using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExDDD.DomainModel
{
    public class Ativo : EntityBase
    {
        private List<Aporte> _aportes;
        private List<Provento> _proventos;
        private List<Venda> _vendas;
        private List<Cotacao> _cotacoes;

        public Ativo()
        {
            _aportes = new List<Aporte>();
            _proventos = new List<Provento>();
            _vendas = new List<Venda>();
            _cotacoes = new List<Cotacao>();
        }
        public string Ticker { get; set; }
        public string CNPJ { get; set; }
        public int QtdCotas { get;  set; }
        public decimal TotalInvestido { get; set; }
        public decimal PrecoMedio { get; set; }
        public decimal TotalProventos { get; set; }
        public decimal TotalResgatado { get; set; }
        public decimal SaldoAtual { get; set; }
        public decimal CotacaoAtual { get; set; }
        public decimal GanhoPerda { get; set; }
        public IEnumerable<Cotacao> Cotacoes { get { return _cotacoes; } private set { _cotacoes = value.ToList(); } }
        public IEnumerable<Venda> Vendas { get { return _vendas; } private set { _vendas = value.ToList(); } }
        public IEnumerable<Provento> Proventos { get { return _proventos; } private set { _proventos = value.ToList(); } }
        public IEnumerable<Aporte> Aportes { get { return _aportes; } private set { _aportes = value.ToList(); } }

        public override void Validar()
        {
            CampoTextoObrigatorio("Ticker", Ticker);
            CampoTextoObrigatorio("CNPJ", CNPJ);

            base.Validar();
        }

        public void AddAporte(Aporte aporte, IApiChecagem api)
        {
            if (api.ValidaPosicao(CNPJ))
            {
                _aportes.Add(aporte);
                PrecoMedio = CalcularPrecoMedio(aporte);
                TotalInvestido += aporte.CalcularTotalAporte();
                QtdCotas += aporte.QtdCotas;
            }
            else
            {
                throw new ApplicationException("O Ativo não é recondado para Aporte.");
            }
        }
        public void AddProvento(Provento provento)
        {
            _proventos.Add(provento);
            TotalProventos += provento.CalcularTotalProvento();
        }

        public void AddVenda(Venda venda)
        {
            venda.ValidaSePodeSerRealizada(QtdCotas);
            _vendas.Add(venda);
            TotalResgatado += venda.CalcularTotalVenda();
            QtdCotas -= venda.QtdCotas;
            CalculaSaldoAtual();

        }
        public void AddCotacao(Cotacao cotacao)
        {
            _cotacoes.Add(cotacao);
            CotacaoAtual = cotacao.Preco;
            CalculaGanhoPerda();
        }
        private void CalculaGanhoPerda()
        {
            GanhoPerda = decimal.Subtract(decimal.Multiply(CotacaoAtual, QtdCotas), decimal.Multiply(PrecoMedio, QtdCotas));
        }
        private void CalculaSaldoAtual()
        {
            SaldoAtual = decimal.Subtract(TotalInvestido, TotalResgatado);
        }
        private decimal CalcularPrecoMedio(Aporte aporte)
        {
            decimal precoPonderado = TotalInvestido;
            int quantidades = QtdCotas;

            precoPonderado += aporte.CalcularTotalAporte();
            quantidades += aporte.QtdCotas;

            return (precoPonderado / quantidades);

        }

    }


}