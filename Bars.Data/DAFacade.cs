using Bars.Data.Interfaces;
using Bars.Infrasctucture.Components;

namespace Bars.Data
{
    public class DAFacade : ComponentsContainer<IDAComponent>, IDAComponent
    {
        private static DAFacade _instanceValue;
        public static DAFacade Instance => _instanceValue ??= new DAFacade();

        public TDaComponent GetDaComponent<TDaComponent>() where TDaComponent : IDAComponent
        {
            return Get<TDaComponent>();
        }
    }
}