using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexToPrimitiveDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Address empAddres = new Address()
            {
                City = "Mumbai",
                State = "Maharashtra",
                Country = "India"
            };
            Employee emp = new Employee();
            emp.Name = "James";
            emp.Salary = 20000;
            emp.Department = "IT";
            emp.address = empAddres;
            var mapper = InitializeAutomapper();
            var empDTO = mapper.Map<EmployeeDTO>(emp);
            Console.WriteLine("Name:" + empDTO.Name + ", Salary:" + empDTO.Salary + ", Department:" + empDTO.Department);
            Console.WriteLine("City:" + empDTO.City + ", State:" + empDTO.State + ", Country:" + empDTO.Country);
            Console.ReadLine();
        }

        static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Employee, EmployeeDTO>()
               .ForMember(dest => dest.City, act => act.MapFrom(src => src.address.City))
               .ForMember(dest => dest.State, act => act.MapFrom(src => src.address.State))
               .ForMember(dest => dest.Country, act => act.MapFrom(src => src.address.Country));
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
