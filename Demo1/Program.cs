using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<Employee, EmployeeDTO>()
                    .ForMember(dest => dest.FullName, act => act.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Dept, act => act.MapFrom(src => src.Department))
                );
            Employee emp = new Employee
            {
                Name = "James",
                Salary = 20000,
                Address = "London",
                Department = "IT"
            };
            var mapper = new Mapper(config);
            var empDTO = mapper.Map<EmployeeDTO>(emp);
            Console.WriteLine("Name:" + empDTO.FullName + ", Salary:" + empDTO.Salary + ", Address:" + empDTO.Address + ", Department:" + empDTO.Dept);
            Console.ReadLine();
        }
    }
}
