using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexToPrimitiveDemo
{
    class PrimitiveToComplex
    {
        static void Main1(string[] args)
        {
            Address1 empAddres = new Address1()
            {
                City = "Mumbai",
                State = "Maharashtra",
                Country = "India"
            };
            Employee1 emp = new Employee1();
            emp.Name = "James";
            emp.Salary = 20000;
            emp.Department = "IT";
            emp.City = "Mumbai";
            emp.State = "Maharashtra";
            emp.Country = "India";
            var mapper = InitializeAutomapper();
            var empDTO = mapper.Map<EmployeeDTO1>(emp);
            Console.WriteLine("Name:" + empDTO.Name + ", Salary:" + empDTO.Salary + ", Department:" + empDTO.Department);
            Console.WriteLine("City:" + empDTO.address.City + ", State:" + empDTO.address.State + ", Country:" + empDTO.address.Country);
            Console.ReadLine();
        }

        static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Employee1, EmployeeDTO1>()
                .ForMember(dest => dest.address, act => act.MapFrom(src => new Address1()
                {
                    City = src.City,
                    State = src.State,
                    Country = src.Country
                }));
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
    public class Employee1
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Department { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
    public class EmployeeDTO1
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Department { get; set; }
        public Address address { get; set; }
    }
    public class Address1
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}

