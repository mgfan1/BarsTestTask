using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Bars.Business;
using Bars.Business.Interfaces;
using Bars.Entities.Dto;
using Bars.Helpers;
using DevExpress.Mvvm;
using DevExpress.Xpf.CodeView;

namespace Bars.ViewModels
{
    public class ContractsViewModel : ViewModelBase
    {
        private IBCContracts BcContracts => BCFacade.Instance.GetBcComponent<IBCContracts>();

        public virtual ObservableCollection<Contract> Contracts { get; set; }

        public ContractsViewModel()
        {
            LoadData();
        }

        public ICommand Refresh => new RelayCommand(LoadData);

        private void LoadData()
        {
            var result = BcContracts.GetList();

            if (result.IsSuccess)
            {
                if (Contracts == null)
                {
                    Contracts = new ObservableCollection<Contract>(result.Context);
                }
                else
                {
                    Contracts.Clear();
                    Contracts.AddRange(result.Context);
                }
            }
            else
            {
                MessageBox.Show(result.Message, nameof(MessageBoxImage.Error),
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}