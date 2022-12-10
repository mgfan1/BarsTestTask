using System.Data;
using Bars.Data.DataAccess;
using Bars.Data.Interfaces;
using Bars.Infrasctucture.Components;

namespace Bars.Data.DAComponents
{
    public abstract class DAComponentBase : ComponentsContainer<IDAComponent>, IDAComponent
    {
        protected virtual IConnectionContainer ConnectionContainer =>
            DAFacade.Instance.Exists<IConnectionContainer>() ?
                DAFacade.Instance.GetDaComponent<IConnectionContainer>().IsExistsForCurrentThread() ?
                    DAFacade.Instance.GetDaComponent<IConnectionContainer>().ForCurrentThread() : null : null;

        #region Methods

        protected Procedure GetProcedure(string name)
        {
            if (ConnectionContainer?.Connection == null)
            {
                return CreateDefaultProcedure(name);
            }
            if (ConnectionContainer.CurrentTransaction == null)
            {
                return new Procedure(ConnectionContainer.Connection, name);
            }
            return new Procedure(ConnectionContainer.Connection, ConnectionContainer.CurrentTransaction, name);
        }


        protected T ReadStruct<T>(IDataRecord record, string columnName) where T : struct
        {
            return (T)record[columnName];
        }

        #endregion
    }
}
