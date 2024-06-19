using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using ITCompany.Service;
using ITCompany.View;
using ITCompany.ViewModel;
using ITCompany.Model;

namespace ITCompany.Service
{
    public class WindowService : IWindowService // сервис для открытие окон (мне он самому не нравится, понимаю что нарушения паттерна но не могу найти информацию)
    {
		public void CloseWindow(Window window) // метод для закрытия окон
		{
			if (window == null)
				throw new ArgumentNullException(nameof(window));

			window.Close();
		}

		public void ShowAddEmployeeWindow()
		{
			var addEmployeeWindow = new AddEmployee();
			addEmployeeWindow.ShowDialog();
		}

		public void ShowAddPositionWindow()
		{
			var addPositionWindow = new AddPosition();
			addPositionWindow.ShowDialog();
		}

		public void ShowDeletePositionWindow()
		{
			var deletePositionWindow = new DeletePosition();
			deletePositionWindow.ShowDialog();
		}

		public void ShowEditEmployeeWindow(Employees employee)
		{
			var editEmployeeWindow = new EditEmployee();
			var editEmployeeViewModel = new EditEmployeeViewModel(new WindowService(), employee); // передаем выбраного книгу и новый объект сервиса
			editEmployeeWindow.DataContext = editEmployeeViewModel;
			editEmployeeWindow.ShowDialog();
		}

		public void ShowEditPositionWindow()
		{
			var editPositionWindow = new EditPosition();
			editPositionWindow.ShowDialog();
		}

		public void ShowMessage(string message) // вызов MessageBox
		{
			var messageBoxViewModel = new MessageViewModel(new WindowService())
			{
				Message = message
			};

			var messageBoxWindow = new View.Message
			{
				DataContext = messageBoxViewModel
			};

			messageBoxWindow.ShowDialog();
		}

		public void ShowMessageBool(string message, Action<bool> callback) // вызов MessageBox булевский
		{
			var messageBoxViewModel = new MessageBoolViewModel(new WindowService()) // создаем и передаем сообщение которое передали
			{
				Message = message
			};

			messageBoxViewModel.MessageBoxResult += callback; // передаем callback который будет обрабатывать результат

			var messageBoxWindow = new MessageBool
			{
				DataContext = messageBoxViewModel
			};

			messageBoxWindow.ShowDialog();
		}
	}
}
