using ExDDD.AppConsoleAp.Infra;
using ExDDD.DomainModel;
using System;

namespace ExDDD.AppConsoleAp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var repository = new AtivoRepository();

                #region CriarAtivo
                //var ativo = new Ativo()
                //{
                //    Ticker = "FUND11",
                //    CNPJ = "000000000000"
                //};
                //ativo.Validar();

                //repository.Salvar(ativo);
                #endregion

                #region AddAporte
                var aporte = new Aporte()
                {
                    DtCompra = DateTime.Now,
                    QtdCotas = 50,
                    VlUnitario = 58.00m
                };
                aporte.Validar();

                var api = new ApiChecagem();
                var ativo = repository.ObterPor(id: 1);
                ativo.AddAporte(aporte, api);
                #endregion

                #region AddProvento
                //var provento = new Provento()
                //{
                //    Data = DateTime.Now,
                //    QtdCotas = 100,
                //    VlUnitario = 0.65m
                //};
                //provento.Validar();

                //var ativo = repository.ObterPor(id: 1);
                //ativo.AddProvento(provento);

                #endregion

                #region AddVenda
                //var venda = new Venda()
                //{
                //    DtVenda = DateTime.Now,
                //    VlUnitario = 70.00m,
                //    QtdCotas = 100,
                //};
                //venda.Validar();

                //var ativo = repository.ObterPor(id: 1);
                //ativo.AddVenda(venda);

                #endregion

                #region AddCotacao
                //var cotacao = new Cotacao()
                //{
                //    Data = DateTime.Now,
                //    DataInclusao = DateTime.Now,
                //    Preco = 70.00m,

                //};
                //cotacao.Validar();

                //var ativo = repository.ObterPor(id: 1);
                //ativo.AddCotacao(cotacao);

                #endregion

                repository.SaveChanges();
                Console.WriteLine("Sucesso");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
