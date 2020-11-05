using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFrameworks_CA1.Question3
{
    class hseEmployee
    {
        public string empName { get; set; }
        public string empType { get; set; }
        public int empYrsService { get; set; }
        public double empSalary { get; set; }
        public int empNumber { get; set; }
        public static int generated = 0;

        public hseEmployee()
        {
            empName = "John Doe";
            empType = "Employee";
            empYrsService = 20;
            empSalary = 30000.00;
            empNumber = AssignEmpNumber();
        }

        public hseEmployee(string empName, string empType, int empYrsService, double empSalary)
        {
            this.empName = empName;
            this.empType = empType;
            this.empYrsService = empYrsService;
            this.empSalary = empSalary;
            empNumber = AssignEmpNumber();
        }

        protected static int AssignEmpNumber()
        {
            return generated += 100;
        }

        public static string PrintDetails(hseEmployee employee)
        {
            if(employee is Doctor)
            {
                return employee.ToString() + "\nI can PRESCRIBE for patients!!!";
            }
            else if (employee is Porter)
            {
                return employee.ToString() + "\nI am a Porter!!!";
            }
            else return employee.ToString();

        }

        override
        public string ToString()
        {
            return 
                "Emp Name: " + empName + "\nEmp Number: " + empNumber + 
                "\nEmp Type: " + empType + "\nEmp Yrs Service: " + empYrsService +
                "\nEmp Salary: " + "€" + empSalary + "";
        }

        public static hseEmployee createEmployee(string type, string name, string eType, int yrsService, double salary)
        {
            if(type == "Porter")
            {
                hseEmployee p = new Porter(name,type,yrsService,salary);
                return p;
            }
            if(type == "Doctor")
            {
                hseEmployee d = new Doctor(name, type, yrsService, salary);
                return d;
            }
            if(type == "Employee")
            {
                hseEmployee e = new hseEmployee(name, type, yrsService, salary);
                return e;
            }
            return null;
        }
    }
    class Doctor : hseEmployee
    {
        public Doctor()
        {
            empName = "Dr. A. N. Other";
            AssignEmpNumber();
            empType = "Doctor";
            empYrsService = 0;
            empSalary = 110000.00;
        }
        public Doctor(string empName, string empType, int empYrsService, double empSalary)
        {
            this.empName = empName;
            this.empType = empType;
            this.empYrsService = empYrsService;
            this.empSalary = empSalary;
            AssignEmpNumber();
        }
    }

    class Porter : hseEmployee
    {
        public Porter()
        {
            empName = "Porter Guy";
            AssignEmpNumber();
            empType = "Porter";
            empYrsService = 2;
            empSalary = 30000.00;
        }

        public Porter(string empName, string empType, int empYrsService, double empSalary)
        {
            this.empName = empName;
            this.empType = empType;
            this.empYrsService = empYrsService;
            this.empSalary = empSalary;
            AssignEmpNumber();
        }
    }
}
