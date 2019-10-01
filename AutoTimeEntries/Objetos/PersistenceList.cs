using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSAutoTimeEntriesWebApi.Objetos
{
    public class PersistenceList<T> : List<T>
    {
        public new void Add(T item)
        {
            base.Add(item);

            Persistencia.SalveDados();
        }

        public new void AddRange(IEnumerable<T> itens)
        {
            base.AddRange(itens);

            Persistencia.SalveDados();
        }

        public new void RemoveAt(int indice)
        {
            base.RemoveAt(indice);

            Persistencia.SalveDados();
        }
    }
}
