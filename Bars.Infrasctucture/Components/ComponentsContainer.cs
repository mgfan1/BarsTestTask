using Bars.Infrasctucture.Interfaces;

namespace Bars.Infrasctucture.Components
{
    public abstract class ComponentsContainer<TBaseType> where TBaseType : IComponent
    {
        private readonly ComponentsStorage<TBaseType> _storageValue;

        #region Methods

        protected ComponentsContainer()
        {
            _storageValue = new ComponentsStorage<TBaseType>();
        }

        public virtual void Register<T>(TBaseType controller) where T : TBaseType
        {
            _storageValue.Register<T>(controller);
        }

        protected virtual T Get<T>() where T : TBaseType
        {
            return _storageValue.Get<T>();
        }

        public virtual bool Exists<T>() where T : TBaseType
        {
            return _storageValue.Exists<T>();
        }

        #endregion
    }
}