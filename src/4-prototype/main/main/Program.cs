using System;
using System.Net.Sockets;

namespace main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var john = new Person(new []{"john", "smith"}, new Address("London Road", 123));
            var jane = new Person(john);
            jane.Address.HouseNumber = 111;
            Console.WriteLine(john);
            Console.WriteLine(jane);


            Console.Read();
        }
    }


    public class Person
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public Person(Person other)
        {
            Names = other.Names;
            Address = new Address(other.Address); 
        }
        public override string ToString()
        {
            return $"{nameof(Names)} : {string.Join(" ", Names)}, {nameof(Address)}: {Address}";

        }
    }

    public class Address
    {
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public Address(Address other)
        {
            this.StreetName = other.StreetName;
            this.HouseNumber = other.HouseNumber;
        }

        public Address(string streetName, int houseNumber)
        {
            this.StreetName = streetName;
            this.HouseNumber = houseNumber;
        }


        public override string ToString()
        {
            return $"{nameof(StreetName)} : {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }
}


