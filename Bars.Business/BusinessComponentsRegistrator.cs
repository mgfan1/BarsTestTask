using Bars.Business.Builders;
using Bars.Infrasctucture.Components;

namespace Bars.Business
{
    public static class BusinessComponentsRegistrator
    {
        public static void Register()
        {
            var director = new ModelDirector();
            director.Register(new BCFacadeServicesBuilder());
            director.Construct();
        }
    }
}