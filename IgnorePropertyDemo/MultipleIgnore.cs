using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgnorePropertyDemo
{
    class MultipleIgnore
    {
        static void Main2(string[] args)
        {
            var mapper = InitializeAutomapper();
            Employee1 employee = new Employee1()
            {
                ID = 101,
                Name = "James",
                Address = "Mumbai"
            };
            var empDTO = mapper.Map<Employee1, EmployeeDTO1>(employee);
            Console.WriteLine("After Mapping : Employee");
            Console.WriteLine("ID : " + employee.ID + ", Name : " + employee.Name + ", Address : " + employee.Address + ", Email : " + employee.Email);
            Console.WriteLine();
            Console.WriteLine("After Mapping : EmployeeDTO");
            Console.WriteLine("ID : " + empDTO.ID + ", Name : " + empDTO.Name + ", Address : " + empDTO.Address + ", Email : " + empDTO.Email);
            Console.ReadLine();
        }
        static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee1, EmployeeDTO1>()
                .IgnoreNoMap(); ;
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }

    public class NoMapAttribute : System.Attribute
    {
    }
    public static class IgnoreNoMapExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreNoMap<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            foreach (var property in sourceType.GetProperties())
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name];
                NoMapAttribute attribute = (NoMapAttribute)descriptor.Attributes[typeof(NoMapAttribute)];
                if (attribute != null)
                    expression.ForMember(property.Name, opt => opt.Ignore());
            }
            return expression;
        }
    }

    public class Employee1
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [NoMap]
        public string Address { get; set; }
        [NoMap]
        public string Email { get; set; }
    }
    public class EmployeeDTO1
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
