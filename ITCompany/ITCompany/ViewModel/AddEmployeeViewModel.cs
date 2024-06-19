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
    public class AddEmployeeViewModel : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler? PropertyChanged;
		private string name;
		private string surname;
		private string email;

		private ICommand addCommand;
		private ICommand cancelCommand;

		private readonly IWindowService windowService;

		private Position selectedPosition;
		private ObservableCollection<Position> position;

		public ICommand AddCommand => addCommand ??= new RelayCommand(AddEmployee, CanAddEmployee);
		public ICommand CancelCommand => cancelCommand ??= new RelayCommand(Cancel);

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

		public AddEmployeeViewModel(IWindowService windowService)
		{
			this.windowService = windowService;
			using(var context = new DBContext())
			{
				var position = context.Positions.ToList();
				Positions = new ObservableCollection<Position>(position);
			}
		}


		private void Cancel()
		{
			windowService.CloseWindow(System.Windows.Application.Current.Windows[1]);
		}

		private void AddEmployee()
		{
			using (var context = new DBContext())
			{
				try
				{
					var position = context.Positions.FirstOrDefault(i => i.Id == SelectedPosition.Id);
					var employee = new Employees();
					employee.Name = Name;
					employee.Surname = Surname;
					employee.Email = Email;
					employee.Position = position;
					context.Add(employee);
					context.SaveChanges();
					windowService.CloseWindow(System.Windows.Application.Current.Windows[1]);
					windowService.ShowMessage($"Добавлен новый работник - {Name} к позиции {SelectedPosition.Name}"); 
				}
				catch (Exception ex)
				{
					windowService.ShowMessage(ex.Message);
					return;
				}
			}

		}

		private bool CanAddEmployee()
		{
			using (var context = new DBContext())
			{
				var employee = context.Employees.Select(i => i.Email).ToList();
				return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname) && !string.IsNullOrEmpty(Email) && !employee.Contains(Email) && SelectedPosition != null;
			}
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
