using ITCompany.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ITCompany.ViewModel
{
    public class MessageBoolViewModel : INotifyPropertyChanged
	{
		private string message;
		private ICommand yesCommand;
		private ICommand noCommand;
		private readonly IWindowService windowService;

		public event Action<bool> MessageBoxResult;


		public string Message
		{
			get => message;
			set
			{
				message = value;
				OnPropertyChanged(nameof(Message));
			}
		}

		public ICommand YesCommand => yesCommand ??= new RelayCommand(OnYes);
		public ICommand NoCommand => noCommand ??= new RelayCommand(OnNo);

		public MessageBoolViewModel(IWindowService windowService)
		{
			this.windowService = windowService;
		}

		private void OnYes()
		{
			MessageBoxResult?.Invoke(true);
			CloseWindow();
		}

		private void OnNo()
		{
			MessageBoxResult?.Invoke(false);
			CloseWindow();
		}

		private void CloseWindow()
		{
			foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
			{
				if (window.DataContext == this)
				{
					window.Close();
					break;
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
