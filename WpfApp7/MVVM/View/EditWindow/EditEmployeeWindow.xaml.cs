using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp7.MVVM.Model;
using WpfApp7.MVVM.ViewModel;

namespace WpfApp7.MVVM.View.EditWindow
{
    public partial class EditEmployeeWindow : Window
    {
        public EditEmployeeWindow(Employee employee)
        {
            InitializeComponent();
            MainViewModel.SelectedEmployee = employee;
            MainViewModel.EmployeeName = employee.Name;
            MainViewModel.EmployeeSurname = employee.Surname;
            MainViewModel.EmployeePhone = employee.Phone;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
