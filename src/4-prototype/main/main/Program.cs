using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var john = new Person(new []{"john", "smith"}, new Address("London Road", 123));
            var jane = john.DeepCopyXml();

            jane.Names[0] = "Jane";
            jane.Address.HouseNumber = 111;
            Console.WriteLine(john);
            Console.WriteLine(jane);


            Console.Read();
        }
    }

    /// <summary>
    /// 1. define the deepcopy extension method, with both XML or Binary.
    /// </summary>
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T)copy;
        }

        public static T DeepCopyXml<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                XmlSerializer s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T)s.Deserialize(ms);
            }
        }
    }


    [Serializable] //3. creates this for Binary serialiser
    public class Person 
    {
        public string[] Names;
        public Address Address;

        /// <summary>
        /// 2. creates the empty constructor for XML, otherwise it will fail. 
        /// </summary>
        public Person()
        {
            
        }
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
    }

    [Serializable]
    public class Address
    {
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public Address()
        {
            
        }
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


