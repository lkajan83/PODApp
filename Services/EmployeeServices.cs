using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PODApp.Models;

namespace PODApp.Services
{
    [Activity(Label = "EmployeeServices")]
    public class EmployeeServices : Activity
    {
        public List<Employee> GetEmployees()
        {
            var list = new List<Employee>()
            {
                new Employee
                {
                    Name = "Kajan",
                    Department = "Marketing"
                },
                new Employee
                {
                    Name = "Rupan",
                    Department = "Accountant"
                },
                new Employee
                {
                    Name = "Kumaran",
                    Department = "Progaramers"
                },
                new Employee
                {
                    Name = "Mathan",
                    Department = "Sales"
                },
                new Employee
                {
                    Name = "Natan",
                    Department = "Management"
                },

            };

            return list;
        }
    }
}