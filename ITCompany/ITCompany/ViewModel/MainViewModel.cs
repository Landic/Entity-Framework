using ITCompany.Model;
using ITCompany.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Reflection.Metadata.BlobBuilder;

namespace ITCompany.ViewModel
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private Employees selectedEmployees;
		public event PropertyChangedEventHandler? PropertyChanged;


		private readonly IWindowService windowService;


		private ICommand addEmployeeCommand;
		private ICommand deleteEmployeeCommand;
		private ICommand editEmployeeCommand;


		private ICommand addPositionCommand;
		private ICommand deletePositionCommand;
		private ICommand editPositionCommand;


		private string searchText;


		private ICommand searchCommand;


		private ObservableCollection<Employees> employees;
		public ObservableCollection<Employees> Employee
		{
			get => employees;
			set
			{
				employees = value;
				OnPropertyChanged(nameof(Employee));
			}
		}

		private void LoadData()
		{
			using(var context = new DBContext())
			{
				var employee = context.Employees.Include(i => i.Position).ToList();
				Employee = new ObservableCollection<Employees>(employee);
			}
		}

		public MainViewModel()
		{
			LoadData();
			windowService = new WindowService();
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public Employees SelectedEmployees
		{
			get => selectedEmployees;
			set
			{
				selectedEmployees = value;
				OnPropertyChanged(nameof(SelectedEmployees));
			}
		}


		public string SearchText
		{
			get => searchText;
			set
			{
				searchText = value;
				OnPropertyChanged(nameof(SearchText));
			}
		}


		private void Search()
		{
			using(var context = new DBContext())
			{
				Employee.Clear();
				var results = context.Employees.Where(i => i.Name.Contains(SearchText) || i.Surname.Contains(SearchText) || i.Email.Contains(SearchText) || i.Position.Name == SearchText).Include(i=> i.Position);
				foreach (var employee in results)
				{
					Employee.Add(employee);
				}
			}
		}

		public ICommand SearchCommand => searchCommand ??= new RelayCommand(Search);


		/////// CRUD операции для работника

		private void AddEmployee()
		{
			windowService.ShowAddEmployeeWindow();
			LoadData();
		}


		private void DeleteEmployee()
		{
			windowService.ShowMessageBool($"Вы уверены что хотите удалить работника {SelectedEmployees.Name} {SelectedEmployees.Surname}", result =>
			{
				if (result)
				{
					using (var context = new DBContext())
					{
						try
						{
							var employee = context.Employees.FirstOrDefault(i => i.Id == SelectedEmployees.Id);
							if (employee != null)
							{
								context.Employees.Remove(employee);
								context.SaveChanges();
								LoadData();
							}
						}
						catch (Exception ex)
						{
							windowService.ShowMessage(ex.Message);
						}
					}
				}
			});
		}

		private bool CanDeleteEmployee()
		{
			return SelectedEmployees != null; 
		}


		private void EditEmployee()
		{
			windowService.ShowEditEmployeeWindow(SelectedEmployees);
			LoadData();
		}

		private bool CanEditEmployee()
		{
			return SelectedEmployees != null;
		}

		public ICommand DeleteEmployeeCommand => deleteEmployeeCommand ??= new RelayCommand(DeleteEmployee, CanDeleteEmployee);
		public ICommand AddEmployeeCommand => addEmployeeCommand ??= new RelayCommand(AddEmployee);
		public ICommand EditEmployeeCommand => editEmployeeCommand ??= new RelayCommand(EditEmployee,CanEditEmployee);



		//////// CRUD операции для должности

		private void AddPosition()
		{
			windowService.ShowAddPositionWindow();
		}

		private void DeletePosition()
		{
			windowService.ShowDeletePositionWindow();
		}

		private void EditPosition()
		{
			windowService.ShowEditPositionWindow();
			LoadData();
		}

		public ICommand AddPositionCommand => addPositionCommand ??= new RelayCommand(AddPosition);
		public ICommand DeletePositionCommand => deletePositionCommand ??= new RelayCommand(DeletePosition);
		public ICommand EditPositionCommand => editPositionCommand ??= new RelayCommand(EditPosition);

	}
}
