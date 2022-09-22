using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp7.MVVM.Core;
using WpfApp7.MVVM.Model;
using WpfApp7.MVVM.View;
using WpfApp7.MVVM.View.AddWindow;
using WpfApp7.MVVM.View.EditWindow;

namespace WpfApp7.MVVM.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public static string? DepartmentName { get; set; }

        public static string? PositionName { get; set; }
        public static decimal PositionSalary { get; set; }
        public static int PositionMaxCountOfEmp{ get; set; }
        public static Department? PositionDepartment { get; set; }

        public static string? EmployeeName { get; set; }
        public static string? EmployeeSurname { get; set; }
        public static string? EmployeePhone { get; set; }
        public static Position? EmployeePosition { get; set; }

        public TabItem? SelectedTabItem { get; set; }
        public static Employee? SelectedEmployee { get; set; }
        public static Position? SelectedPosition { get; set; }
        public static Department? SelectedDepartment { get; set; }

        private List<Department> _allDepartments = DataWorker.GetAllDepartments();
        public List<Department> AllDepartments
        {
            get { return _allDepartments; }
            set
            {
                _allDepartments = value;
                OnPropertyChanged();
            }
        }

        private List<Position> _allPositions = DataWorker.GetAllPositions();
        public List<Position> AllPositions
        {
            get { return _allPositions; }
            set
            {
                _allPositions = value;
                OnPropertyChanged();
            }
        }

        private List<Employee> _allEmployees = DataWorker.GetAllEmployees();
        public List<Employee> AllEmployees
        {
            get { return _allEmployees; }
            set
            {
                _allEmployees = value;
                OnPropertyChanged();
            }
        }

        public void OpenAddNewDepartmentWindow()
        {
            AddNewDepartmentWindow addNewDepartmentWindow = new AddNewDepartmentWindow();
            SetCenterPositionAndOpenWindow(addNewDepartmentWindow);
        }

        public void OpenAddNewPositionWindow()
        {
            AddNewPositionWindow addNewPositionWindow = new AddNewPositionWindow();
            SetCenterPositionAndOpenWindow(addNewPositionWindow);
        }

        public void OpenAddEmployeeWindow()
        {
            AddNewEmployeeWindow addNewEmployeeWindow = new AddNewEmployeeWindow();
            SetCenterPositionAndOpenWindow(addNewEmployeeWindow);
        }

        public void OpenEditDepartmentWindow(Department department)
        {
            EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow(department);
            SetCenterPositionAndOpenWindow(editDepartmentWindow);
        }

        public void OpenEditPositionWindow(Position position)
        {
            EditPositionWindow editPositionWindow = new EditPositionWindow(position);
            SetCenterPositionAndOpenWindow(editPositionWindow);
        }

        public void OpenEditEmployeeWindow(Employee employee)
        {
            EditEmployeeWindow editEmployeeWindow = new EditEmployeeWindow(employee);
            SetCenterPositionAndOpenWindow(editEmployeeWindow);
        }

        private void SetCenterPositionAndOpenWindow(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        private readonly RelayCommand? _openAddDepartmentWindow;

        public RelayCommand OpenAddDepartmentWindow
        {
            get
            {
                return _openAddDepartmentWindow ?? new RelayCommand(e => { OpenAddNewDepartmentWindow(); });
            }

        }

        private readonly RelayCommand? _openAddPositionWindow;

        public RelayCommand OpenAddPositionWindow
        {
            get
            {
                return _openAddPositionWindow ?? new RelayCommand(e => { OpenAddNewPositionWindow(); });
            }
        }
        private readonly RelayCommand? _openAddEmpWindow;

        public RelayCommand OpenAddEmpWindow
        {
            get
            {
                return _openAddEmpWindow ?? new RelayCommand(e => { OpenAddEmployeeWindow(); });
            }

        }

        private readonly RelayCommand? _openEditItem;

        public RelayCommand OpenEditItem
        {
            get
            {
                return _openEditItem ?? new RelayCommand(e =>
                {
                    if (SelectedTabItem.Name == "EmployeeTab" && SelectedEmployee != null)
                    {
                        OpenEditEmployeeWindow(SelectedEmployee);
                    }

                    if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
                    {
                        OpenEditPositionWindow(SelectedPosition);
                    }

                    if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                    {
                        OpenEditDepartmentWindow(SelectedDepartment);
                    }
                });
            }
        }

        private RelayCommand? _createNewDepartment;

        public RelayCommand CreateNewDepartment
        {
            get
            {
                return _createNewDepartment ?? new RelayCommand(c =>
                {
                    Window? window = c as Window;
                    string resultStr = "";
                    if (DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(window, "TxbDepartmentName");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateDepartment(DepartmentName);
                        UpdateDepartmentView();
                        ShowMessageToUser(resultStr);
                        SetNull();
                        window.Close();
                    }
                });
            }  
        }

        private readonly RelayCommand? _createNewPosition;

        public RelayCommand CreateNewPosition
        {
            get
            {
                return _createNewPosition ?? new RelayCommand(p =>
                {
                    Window? window = p as Window;
                    string resultStr = "";
                    if (PositionName == null || PositionName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(window, "TxbPositionName");
                    }
                    if (PositionDepartment == null)
                    {
                        MessageBox.Show("Отдел не выбран", "Системное сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        resultStr = DataWorker.CreatePosition(PositionName, PositionSalary, PositionMaxCountOfEmp, PositionDepartment);
                        UpdateDepartmentView();
                        ShowMessageToUser(resultStr);
                        SetNull();
                        window.Close();
                    }
                });
            }
        }

        private readonly RelayCommand? _createNewEmployee;

        public RelayCommand CreateNewEmployee
        {
            get
            {
                return _createNewEmployee ?? new RelayCommand(e =>
                {
                    Window? window = e as Window;
                    string resultStr = "";
                    if (EmployeeName == null || EmployeeName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(window, "TxbName");
                    }
                    if (EmployeeName == null || EmployeeSurname.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(window, "TxbSurname");
                    }
                    if (EmployeeName == null || EmployeePhone.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(window, "TxbPhone");
                    }
                    if (PositionDepartment == null)
                    {
                        MessageBox.Show("Должность не выбрана", "Системное сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        resultStr = DataWorker.CreateEmployee(EmployeeName, EmployeeSurname, EmployeePhone, EmployeePosition);
                        UpdateDepartmentView();
                        ShowMessageToUser(resultStr);
                        SetNull();
                        window.Close();
                    }
                });
            }
        }

        private readonly RelayCommand? _deleteItem;

        public RelayCommand DeleteItem
        {
            get
            {
                return _deleteItem ?? new RelayCommand(d =>
                {
                    string resultString = "Данные для удаления нет!";

                    if (SelectedTabItem.Name == "EmployeeTab" && SelectedEmployee != null)
                    {
                        resultString = DataWorker.DeleteEmployee(SelectedEmployee);
                        UpdateAllDataView();
                    }

                    if (SelectedTabItem.Name == "PositionTab" && SelectedPosition != null)
                    {
                        resultString = DataWorker.DeletePosition(SelectedPosition);
                        UpdateAllDataView();
                    }

                    if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                    {
                        resultString = DataWorker.DeleteDepartment(SelectedDepartment);
                        UpdateAllDataView();
                    }

                    SetNull();
                    ShowMessageToUser(resultString);

                });
            }
        }

        private readonly RelayCommand? _editEmployee;

        public RelayCommand EditEmployee
        {
            get
            {
                return _editEmployee ?? new RelayCommand(e =>
                {
                    Window? window = e as Window;

                    string resultString = "Работник не выбран!";
                    string noPositionStr = "Должность не выбрана!";


                    if (SelectedEmployee != null)
                    {
                        if (EmployeePosition != null)
                        {
                            resultString = DataWorker.EditEmloyee(SelectedEmployee,
                                EmployeeName, EmployeeSurname, EmployeePhone, EmployeePosition);
                            UpdateAllDataView();
                            SetNull();
                            ShowMessageToUser(resultString);
                            window.Close();
                        }
                        else
                        {
                            ShowMessageToUser(noPositionStr);
                        }
                    }

                    else
                    {
                        ShowMessageToUser(resultString);
                    }

                });
            }
        }


        private readonly RelayCommand? _editPosition;

        public RelayCommand EditPosition
        {
            get => _editPosition ?? new RelayCommand(p =>
            {
                Window? window = p as Window;

                string resultString = "Должность не выбран!";
                string noDepartmentStr = "Отдел не выбран!";


                if (SelectedPosition != null)
                {
                    if (PositionDepartment != null)
                    {
                        resultString = DataWorker.EditPosition(SelectedPosition, PositionName, PositionSalary, PositionMaxCountOfEmp, PositionDepartment);
                        UpdateAllDataView();
                        SetNull();
                        ShowMessageToUser(resultString);
                        window.Close();
                    }
                    else
                    {
                        ShowMessageToUser(noDepartmentStr);
                    }
                }

                else
                {
                    ShowMessageToUser(resultString);
                }

            });
        }


        private readonly RelayCommand? _editDepartment;

        public RelayCommand EditDepartment
        {
            get
            {
                return _editDepartment ?? new RelayCommand(d =>
                {
                    Window? window = d as Window;

                    string resultString = "Отдел не выбран!";


                    if (SelectedDepartment != null)
                    {

                        resultString = DataWorker.EditDepartment(SelectedDepartment, DepartmentName);
                        UpdateAllDataView();
                        SetNull();
                        ShowMessageToUser(resultString);
                        window.Close();

                    }

                    else
                    {
                        ShowMessageToUser(resultString);
                    }

                });
            }
        }

        private void UpdateDepartmentView()
        {
            AllDepartments = DataWorker.GetAllDepartments();
            MainWindow.UpdateDepartmentView.ItemsSource = null;
            MainWindow.UpdateDepartmentView.Items.Clear();
            MainWindow.UpdateDepartmentView.ItemsSource = AllDepartments;
            MainWindow.UpdateDepartmentView.Items.Refresh();
        }

        private void UpdatePositionView()
        {
            AllPositions = DataWorker.GetAllPositions();
            MainWindow.UpdatePositionView.ItemsSource = null;
            MainWindow.UpdatePositionView.Items.Clear();
            MainWindow.UpdatePositionView.ItemsSource = AllPositions;
            MainWindow.UpdatePositionView.Items.Refresh();
        }

        private void UpdateEmployeeView()
        {
            AllEmployees = DataWorker.GetAllEmployees();
            MainWindow.UpdateEmployeeView.ItemsSource = null;
            MainWindow.UpdateEmployeeView.Items.Clear();
            MainWindow.UpdateEmployeeView.ItemsSource = AllEmployees;
            MainWindow.UpdateEmployeeView.Items.Refresh();
        }

        private void UpdateAllDataView()
        {
            UpdateDepartmentView();
            UpdatePositionView();
            UpdateEmployeeView();
        }

        private void SetRedBlockControl(Window window, string blockName)
        {
            Control? control = window.FindName(blockName) as Control;
            control.BorderBrush = Brushes.DarkRed;
        }

        private void ShowMessageToUser(string message)
        {
            MessageWindow window = new MessageWindow(message);
            SetCenterPositionAndOpenWindow(window);
        }

        private void SetNull()
        {
            DepartmentName = null;

            PositionName = null;
            PositionSalary = 0;
            PositionMaxCountOfEmp = 0;
            PositionDepartment = null;

            EmployeeName = null;
            EmployeeSurname = null;
            EmployeePhone = null;
            EmployeePosition = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
