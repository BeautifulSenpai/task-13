using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp7.MVVM.Core;

namespace WpfApp7.MVVM.Model
{
    public class Position
    {
        public int ID { get; set; }
        public string PositionName { get; set; }
        public decimal Salary { get; set; }
        public int MaxCountOfEmployees { get; set; }
        public List<Employee> Employees { get; set; }
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        [NotMapped]
        public Department PositionDepartment
        {
            get => DataWorker.GetDepartmentByID(DepartmentID);
        }

        [NotMapped]
        public List<Employee> PositionEmployees
        {
            get => DataWorker.GetAllEmployeesByPositionID(ID);
        }
    }
}
