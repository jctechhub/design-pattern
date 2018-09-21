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
            var jane = john.DeepCopy(); //5. then call the .DeepCopy() method.
            jane.Address.HouseNumber = 111;
            Console.WriteLine(john);
            Console.WriteLine(jane);


            Console.Read();
        }
    }

    /// <summary>
    /// 1. creates new interface for deepcopy. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPrototype<T>
    {
        T DeepCopy();
    }

    public class Person : IPrototype<Person> //2. implements it.
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        /// <summary>
        /// MAKE SURE to have the pass in object to have DEEP COPY, not shalow copy. 
        /// </summary>
        /// <param name="other"></param>
        public Person(Person other)
        {
            Names = other.Names;
            Address = new Address(other.Address); 
        }
        public override string ToString()
        {
            return $"{nameof(Names)} : {string.Join(" ", Names)}, {nameof(Address)}: {Address}";

        }

        /// <summary>
        /// 3. make sure to return a new Person from copying.
        /// </summary>
        /// <returns></returns>
        public Person DeepCopy()
        {
            return new Person(Names, Address.DeepCopy());
        }
    }

    public class Address : IPrototype<Address> //4. do the same thing for address.
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


        public Address DeepCopy()
        {
            return new Address(StreetName, HouseNumber);
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)} : {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }

       
    }
}


