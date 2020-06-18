using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexMapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Address empAddres = new Address()
            {
                City = "Mumbai",
                Stae = "Maharashtra",
                Country = "India"
            };
            Employee emp = new Employee
            {
                Name = "James",
                Salary = 20000,
                Department = "IT",
                address = empAddres
            };

            var mapper = InitializeAutomapper();
            var empDTO = mapper.Map<EmployeeDTO>(emp);

            Console.WriteLine("Name:" + empDTO.Name + ", Salary:" + empDTO.Salary + ", Department:" + empDTO.Department);
            Console.WriteLine("City:" + empDTO.addressDTO.EmpCity + ", State:" + empDTO.addressDTO.EmpStae + ", Country:" + empDTO.addressDTO.Country);
            Console.ReadLine();
        }

        static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Address, AddressDTO>()
                .ForMember(dest => dest.EmpCity, act => act.MapFrom(src => src.City))
                    .ForMember(dest => dest.EmpStae, act => act.MapFrom(src => src.Stae)); ;
                cfg.CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.addressDTO, act => act.MapFrom(src => src.address));
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
