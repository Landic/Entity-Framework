using ITCompany.Model;
using ITCompany.Service;
using ITCompany.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ITCompany.ViewModel
{
	public class EditPositionViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private string name;

		private readonly IWindowService windowService;

		private ICommand editCommand;
		private ICommand cancelCommand;

		private Position selectedPosition;
		private ObservableCollection<Position> position;

		public string Name
		{
			get => name;
			set
			{
				name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public Position SelectedPosition
		{
			get => selectedPosition;
			set
			{
				selectedPosition = value;
				Name = selectedPosition.Name;
				OnPropertyChanged(nameof(SelectedPosition));
			}
		}

		public ObservableCollection<Position> Positions
		{
			get => position;
			set
			{
				position = value;
				OnPropertyChanged(nameof(Positions));
			}
		}

		public EditPositionViewModel(IWindowService windowService)
		{
			this.windowService = windowService;
			using (var context = new DBContext())
			{
				var position = context.Positions.ToList();
				Positions = new ObservableCollection<Position>(position);
			}
		}

		public ICommand EditPositionCommand => editCommand ??= new RelayCommand(EditPosition, CanEditPosition);
		public ICommand CancelCommand => cancelCommand ??= new RelayCommand(Cancel);

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void Cancel()
		{
			windowService.CloseWindow(System.Windows.Application.Current.Windows[1]);
		}

		private void EditPosition()
		{
			windowService.ShowMessageBool("Вы уверены что хотите изменить эту должность?", result =>
			{
				if (result)
				{
					using (var context = new DBContext())
					{
						try
						{
							var position = context.Positions.FirstOrDefault(i => i.Id == SelectedPosition.Id);
							if (position != null)
							{
								position.Name = Name;
								context.SaveChanges();
								windowService.CloseWindow(System.Windows.Application.Current.Windows[1]);
							}
						}
						catch (Exception ex)
						{
							windowService.ShowMessage(ex.Message);
							return;
						}
					}
				}
			});
		}

		private bool CanEditPosition()
		{
			using (var context = new DBContext()) {
				var position = context.Positions.Select(i => i.Name).ToList();
				return SelectedPosition != null && !position.Contains(Name);
			}
		}
	}
}
