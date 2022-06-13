using System;
using System.Collections.Generic;
using System.Data.Common;
using Bars.Data.Interfaces;
using Bars.Entities.Dto;

namespace Bars.Data.DAComponents
{
    internal class DAContracts : DAComponentBase, IDAContracts
    {
        #region Methods

        public List<Contract> GetList()
        {
            using var proc = GetProcedure("GetListContracts");
            return proc.ExecuteReader(r =>
            {
                var list = new List<Contract>();
                while (r.Read())
                {
                    list.Add(Map(r));
                }
                return list;
            });
        }

        private Contract Map(DbDataReader reader)
        {
            var c = new Contract();
            c.Number = ReadStruct<int>(reader, "Number");
            c.Date = ReadStruct<DateTime>(reader, "Date");
            c.LastModifiedDate = ReadStruct<DateTime>(reader, "LastModifiedDate");
            c.IsActual = ReadStruct<bool>(reader, "IsActual");
            return c;
        }

        #endregion
    }
}
