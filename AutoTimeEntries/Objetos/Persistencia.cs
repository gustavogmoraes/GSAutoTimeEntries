using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using GSAutoTimeEntriesWebApi.Objetos;
using LiteDB;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace GSAutoTimeEntries.Objetos
{
    public static class Persistencia
    {
        #region Comments

        //public static List<Lancamento> ObtenhaLancamentosRealizados()
        //{
        //    using (var db = new LiteDatabase(@"Dados.db"))
        //    {
        //        var lancamentos = db.GetCollection<Lancamento>("LancamentosRealizados");
        //        return lancamentos.FindAll().ToList();
        //    }
        //}

        //public static void AltereLancamentosRealizados(List<Lancamento> lancamentos)
        //{
        //    using (var db =  new LiteDatabase(@"Dados.db"))
        //    {
        //        var lancamentosDb = db.GetCollection<Lancamento>("LancamentosRealizados");
        //        lancamentosDb.Upsert(lancamentos);
        //    }
        //}

        #endregion

        public const string NomeCollectionLancamentosRealizados = "LancamentosRealizados";
        public const string NomeCollectionLancamentosDispensados = "LancamentosDispensados";

        public static LiteDatabase AbraConexao()
        {
            return new LiteDatabase("@Dados.db");
        }

        public static bool VerifiqueSeExistemLancamentosDeUmaData(DateTime data)
        {
            using (var db = AbraConexao())
            {
                var result = db.GetCollection<Lancamento>(NomeCollectionLancamentosRealizados).FindAll().Where(x => x.Data.Date == data).ToList();
                result.AddRange(db.GetCollection<Lancamento>(NomeCollectionLancamentosDispensados).FindAll().Where(x => x.Data.Date == data));

                return result.Count > 0;
            }
        }

        public static ILiteCollection<Lancamento> ObtenhaCollectionLancamentosRealizados(this LiteDatabase dbConnection)
        {
            return dbConnection.GetCollection<Lancamento>(NomeCollectionLancamentosRealizados);
        }
    }
}
