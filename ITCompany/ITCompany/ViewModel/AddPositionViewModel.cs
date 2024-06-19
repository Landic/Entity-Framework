using ITCompany.Model;
using ITCompany.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ITCompany.ViewModel
{
	internal class AddPositionViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		private string name;

		private ICommand addCommand;
		private ICommand cancelCommand;

		private readonly IWindowService windowService;


		public ICommand AddCommand => addCommand ??= new RelayCommand(AddPosition, CanAddPosition);
		public ICommand CancelCommand => cancelCommand ??= new RelayCommand(Cancel);


		public string Name
		{
			get => name;
			set
			{
				name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		

		public AddPositionViewModel(IWindowService windowService)
		{
			this.windowService = windowService;
		}


		private void Cancel()
		{
			windowService.CloseWindow(System.Windows.Application.Current.Windows[1]);
		}

		private void AddPosition()
		{
			using (var context = new DBContext())
			{
				try
				{
					var position = new Position();
					position.Name = Name;
					context.Add(position);
					context.SaveChanges();
					windowService.CloseWindow(System.Windows.Application.Current.Windows[1]);
					windowService.ShowMessage($"Добавлена новая должность - {Name}");
				}
				catch (Exception ex)
				{
					windowService.ShowMessage(ex.Message);
					return;
				}
			}

		}

		private bool CanAddPosition()
		{
			using (var context = new DBContext())
			{
				var position = context.Positions.Select(i => i.Name).ToList();
				return !position.Contains(Name) && !string.IsNullOrEmpty(Name);
			}
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
