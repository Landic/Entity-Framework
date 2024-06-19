
using ITCompany.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ITCompany.Service
{
    public interface IWindowService
    {
		void CloseWindow(Window window);

        void ShowMessage(string message);
        void ShowMessageBool(string message, Action<bool> callback);

        void ShowAddEmployeeWindow();

        void ShowEditEmployeeWindow(Employees employee);

        void ShowAddPositionWindow();

        void ShowDeletePositionWindow();

        void ShowEditPositionWindow();
	}
}
