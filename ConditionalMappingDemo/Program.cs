using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConditionalMappingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var mapper = InitializeAutomapper();
            Product product = new Product()
            {
                ProductID = 101,
                Name = "Led TV",
                OptionalName = "Product name not start with A",
                Quantity = -5,
                Amount = 1000
            };
            var productDTO = mapper.Map<Product, ProductDTO>(product);

            Console.WriteLine("After Mapping : Product");
            Console.WriteLine("ProductID : " + product.ProductID);
            Console.WriteLine("Name : " + product.Name);
            Console.WriteLine("OptionalName : " + product.OptionalName);
            Console.WriteLine("Quantity : " + product.Quantity);
            Console.WriteLine("Amount : " + product.Amount);
            Console.WriteLine();
            Console.WriteLine("After Mapping : ProductDTO");
            Console.WriteLine("ProductID : " + productDTO.ProductID);
            Console.WriteLine("ItemName : " + productDTO.ItemName);
            Console.WriteLine("ItemQuantity : " + productDTO.ItemQuantity);
            Console.WriteLine("Amount : " + productDTO.Amount);
            Console.ReadLine();
        }

        static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>()
                    //If the Name Start with A then Map the Name Value else Map the OptionalName value
                    .ForMember(dest => dest.ItemName, act => act.MapFrom(src =>
                        (src.Name.StartsWith("A") ? src.Name : src.OptionalName)))
                    //Take the quantity value if its greater than 0
                    .ForMember(dest => dest.ItemQuantity, act => act.Condition(src => (src.Quantity > 0)))
                    //Take the amount value if its greater than 100
                    .ForMember(dest => dest.Amount, act => act.Condition(src => (src.Amount > 100)));
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string OptionalName { get; set; }
        public int Quantity { get; set; }
        public int Amount { get; set; }
    }
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string ItemName { get; set; }
        public int ItemQuantity { get; set; }
        public int Amount { get; set; }
    }
}
