using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExDDD.DomainModel
{
   public interface IApiChecagem
    {
        bool ValidaPosicao(string cnpj);
    }
}
