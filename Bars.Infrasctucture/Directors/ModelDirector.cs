using Bars.Infrasctucture.Directors;
using Bars.Infrasctucture.Interfaces;

namespace Bars.Infrasctucture.Components
{
    public class ModelDirector : BaseDirector
    {
        public void Construct()
        {
            InvokeBuild(new ComponentsStorage<IComponent>());
        }
    }
}