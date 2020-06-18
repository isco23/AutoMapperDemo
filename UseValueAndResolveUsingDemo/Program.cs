using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseValueAndResolveUsingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeAutomapper();
            A aObj = new A()
            {
                Name = "Pranaya",
                AAddress = "Mumbai"
            };
            var bObj = Mapper.Map<A, B>(aObj);
            Console.WriteLine("After Mapping : ");
            //Here FixedValue and DOJ will be empty for aObj
            Console.WriteLine("aObj.Member : " + aObj.Name + ", aObj.FixedValue : " + aObj.FixedValue + ", aObj.DOJ : " + aObj.DOJ + ", aObj.AAddress : " + aObj.AAddress);
            Console.WriteLine("bObj.Member : " + bObj.Name + ", bObj.FixedValue : " + bObj.FixedValue + ", bObj.DOJ : " + bObj.DOJ + ", bObj.BAddress : " + bObj.BAddress);

            bObj.Name = "Rout";
            bObj.BAddress = "Delhi";
            Mapper.Map(bObj, aObj);
            Console.WriteLine("After ReverseMap : ");
            Console.WriteLine("aObj.Member : " + aObj.Name + ", aObj.FixedValue : " + aObj.FixedValue + ", aObj.DOJ : " + aObj.DOJ + ", aObj.AAddress : " + aObj.AAddress);
            Console.WriteLine("bObj.Member : " + bObj.Name + ", bObj.FixedValue : " + bObj.FixedValue + ", bObj.DOJ : " + bObj.DOJ + ", bObj.BAddress : " + bObj.BAddress);
            Console.ReadLine();
        }
        static void InitializeAutomapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<A, B>()
                    .ForMember(dest => dest.BAddress, act => act.MapFrom(src => src.AAddress))
                    //To Store Static Value use the UseValue() method
                    .ForMember(dest => dest.FixedValue, act => act.UseValue("Hello"))
                    //To Store DateTime value use ResolveUsing() method
                    .ForMember(dest => dest.DOJ, act => act.ResolveUsing(src =>
                    {
                        return DateTime.Now;
                    }))
                    .ReverseMap();
            });
        }
    }
}
