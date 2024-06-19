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
    public class EditEmployeeViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		private string name;
		private string surname;
		private string email;


		private ICommand editCommand;
		private ICommand cancelCommand;

		private readonly IWindowService windowService;

		private readonly Employees originalEmployee;
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

		public ICommand EditEmployeeommand => editCommand ??= new RelayCommand(EditEmployee, CanEditEmployee);
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

		public string Surname
		{
			get => surname;
			set
			{
				surname = value;
				OnPropertyChanged(nameof(Surname));
			}
		}

		public string Email
		{
			get => email;
			set
			{
				email = value;
				OnPropertyChanged(nameof(Email));
			}
		}

		public EditEmployeeViewModel(IWindowService windowService, Employees employee)
		{
			this.windowService = windowService;
			originalEmployee = employee;
			Name = originalEmployee.Name;
			Surname = originalEmployee.Surname;
			Email = originalEmployee.Email;
			using (var context = new DBContext())
			{
				var position = context.Positions.ToList();
				Positions = new ObservableCollection<Position>(position);
			}
			SelectedPosition = Positions.FirstOrDefault(a => a.Id == originalEmployee.Position.Id);
		}


		private void Cancel() 
		{
			windowService.CloseWindow(System.Windows.Application.Current.Windows[1]);
		}

		private void EditEmployee()
		{
			windowService.ShowMessageBool("Вы уверены что хотите изменить работника?", result =>
			{
				if (result)
				{
					using (var context = new DBContext())
					{
						try
						{
							var positions = context.Positions.FirstOrDefault(i => i.Id == SelectedPosition.Id);
							if (positions == null)
							{
								windowService.ShowMessage("Позиция не найдена!");
							}
							var employee = context.Employees.FirstOrDefault(b => b.Id == originalEmployee.Id);
							if (employee != null)
							{
								employee.Name = Name;
								employee.Surname = Surname;
								employee.Email = Email;
								employee.Position = positions;
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

		private bool CanEditEmployee()
		{
			using (var context = new DBContext())
			{
				return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname) && !string.IsNullOrEmpty(Email) && SelectedPosition != null;
			}
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
