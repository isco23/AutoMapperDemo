using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseMappingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Step1: Initialize the Mapper
            var mapper = InitializeAutomapper();
            //Step2: Create the Order Request
            var OrderRequest = CreateOrderRequest();
            //Step3: Map the OrderRequest object to Order DTO
            var orderDTOData = mapper.Map<Order, OrderDTO>(OrderRequest);
            //Step4: Print the OrderDTO Data
            Console.WriteLine("After Mapping - OrderDTO Data");
            Console.WriteLine("OrderId : " + orderDTOData.OrderId);
            Console.WriteLine("NumberOfItems : " + orderDTOData.NumberOfItems);
            Console.WriteLine("TotalAmount : " + orderDTOData.TotalAmount);
            Console.WriteLine("CustomerId : " + orderDTOData.CustomerId);
            Console.WriteLine("Name : " + orderDTOData.Name);
            Console.WriteLine("Postcode : " + orderDTOData.Postcode);
            Console.WriteLine("MobileNo : " + orderDTOData.MobileNo);
            Console.WriteLine();
            //Step5: modify the OrderDTO data
            orderDTOData.OrderId = 10;
            orderDTOData.NumberOfItems = 20;
            orderDTOData.TotalAmount = 2000;
            orderDTOData.CustomerId = 5;
            orderDTOData.Name = "Smith";
            orderDTOData.Postcode = "12345";
            //Step6: Reverse Map
            mapper.Map(orderDTOData, OrderRequest);
            //Step7: Print the Order Data
            Console.WriteLine("After Reverse Mapping - Order Data");
            Console.WriteLine("OrderNo : " + OrderRequest.OrderNo);
            Console.WriteLine("NumberOfItems : " + OrderRequest.NumberOfItems);
            Console.WriteLine("TotalAmount : " + OrderRequest.TotalAmount);
            Console.WriteLine("CustomerId : " + OrderRequest.Customer.CustomerID);
            Console.WriteLine("FullName : " + OrderRequest.Customer.FullName);
            Console.WriteLine("Postcode : " + OrderRequest.Customer.Postcode);
            Console.WriteLine("ContactNo : " + OrderRequest.Customer.ContactNo);
            Console.ReadLine();
        }
        private static Order CreateOrderRequest()
        {
            return new Order
            {
                OrderNo = 101,
                NumberOfItems = 3,
                TotalAmount = 1000,
                Customer = new Customer()
                {
                    CustomerID = 777,
                    FullName = "James Smith",
                    Postcode = "755019",
                    ContactNo = "1234567890"
                },
            };
        }
        static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Order, OrderDTO>()
                    //OrderId is different so map them using For Member
                    .ForMember(dest => dest.OrderId, act => act.MapFrom(src => src.OrderNo))
                    //Customer is a Complex type, so Map Customer to Simple type using For Member
                    .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Customer.FullName))
                    .ForMember(dest => dest.Postcode, act => act.MapFrom(src => src.Customer.Postcode))
                    .ForMember(dest => dest.MobileNo, act => act.MapFrom(src => src.Customer.ContactNo))
                    .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.Customer.CustomerID))
                    .ReverseMap();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}

