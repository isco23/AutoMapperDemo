using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseValueAndResolveUsingDemo
{
    class NullSubstitutionDemo
    {
        static void Main(string[] args)
        {
            InitializeAutomapper();
            A1 aObj = new A1()
            {
                Name = "Pranaya",
                AAddress = null
            };
            var bObj = Mapper.Map<A1, B1>(aObj);
            Console.WriteLine("After Mapping : ");
            //Here FixedValue and DOJ will be empty for aObj
            Console.WriteLine("aObj.Member : " + aObj.Name + ", aObj.FixedValue : " + aObj.FixedValue + ", aObj.AAddress : " + aObj.AAddress);
            Console.WriteLine("bObj.Member : " + bObj.Name + ", bObj.FixedValue : " + bObj.FixedValue + ", bObj.BAddress : " + bObj.BAddress);
            Console.ReadLine();
        }
        static void InitializeAutomapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<A1, B1>()
                    .ForMember(dest => dest.BAddress, act => act.MapFrom(src => src.AAddress))
                    //You need to use NullSubstitute method to substitute null value
                    .ForMember(dest => dest.FixedValue, act => act.NullSubstitute("Hello"))
                    .ForMember(dest => dest.BAddress, act => act.NullSubstitute("N/A"));
            });
        }
    }
    public class A1
    {
        public string Name { get; set; }
        public string AAddress { get; set; }
        public string FixedValue { get; set; }
    }
    public class B1
    {
        public string Name { get; set; }
        public string BAddress { get; set; }
        public string FixedValue { get; set; }
    }
}
