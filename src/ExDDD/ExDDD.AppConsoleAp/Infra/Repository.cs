using ExDDD.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExDDD.AppConsoleAp.Infra
{
    internal class AtivoRepository
    {
        private readonly MyContext _context;
        public AtivoRepository()
        {
            _context = new MyContext();
        }

        public Ativo ObterPor(int id)
        {
            return _context.Ativos.Include(p => p.Aportes)
                                  .Include(p => p.Vendas)
                                  .Include(p => p.Cotacoes)
                                  .Include(p => p.Proventos).FirstOrDefault(p => p.Id == id);
        }

        public void Salvar(Ativo ativo)
        {
            if (ativo.Id.Equals(0))
            {
                _context.Ativos.Add(ativo);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
