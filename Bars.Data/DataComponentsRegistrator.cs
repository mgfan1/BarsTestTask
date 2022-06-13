using Bars.Data.Builders;
using Bars.Infrasctucture.Components;

namespace Bars.Data
{
    public static class DataComponentsRegistrator
    {
        public static void Register()
        {
            var director = new ModelDirector();
            director.Register(new DAFacadeServicesBuilder());
            director.Construct();
        }
    }
}
