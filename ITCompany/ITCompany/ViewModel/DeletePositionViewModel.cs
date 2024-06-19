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
	public class DeletePositionViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;


		private readonly IWindowService windowService;

		private ICommand deleteCommand;
		private ICommand cancelCommand;

		private Position selectedPosition;
		private ObservableCollection<Position> position;

		public Position SelectedPosition
		{
			get => selectedPosition;
			set
			{
				selectedPosition = value;
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

		public DeletePositionViewModel(IWindowService windowService)
		{
			this.windowService = windowService;
			using (var context = new DBContext())
			{
				var position = context.Positions.ToList();
				Positions = new ObservableCollection<Position>(position);
			}
		}

		public ICommand DeletePositionCommand => deleteCommand ??= new RelayCommand(DeletePosition, CanDeletePosition);
		public ICommand CancelCommand => cancelCommand ??= new RelayCommand(Cancel);

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void Cancel()
		{
			windowService.CloseWindow(System.Windows.Application.Current.Windows[1]);
		}

		private void DeletePosition()
		{
			windowService.ShowMessageBool("Вы уверены что хотите удалить эту должность?", result =>
			{
				if (result)
				{
					using (var context = new DBContext())
					{
						try
						{
							var employee = context.Employees;
							var position = context.Positions.FirstOrDefault(i => i.Id == SelectedPosition.Id);
							if (position != null)
							{
								if(employee.FirstOrDefault(i=> i.Position == position) != null)
								{
									throw new Exception("Вы не можете удалить эту должность так как к ней присвоен работник");
								}
								context.Positions.Remove(position);
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

		private bool CanDeletePosition()
		{
			return SelectedPosition != null;
		}
	}
}
