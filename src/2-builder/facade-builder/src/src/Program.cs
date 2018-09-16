using System;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            Person person = pb
                .Works
                    .At("Sydney")
                    .AsA("PM")
                    .Earning(150000)
                .Lives
                    .At("23 king street")
                    .In("Sydney")
                    .WithPostcode("2000"); //the power of builder, can chain many calls together. 

            Console.WriteLine(person);

            Console.Read();
        }


        public class Person
        {
            public string StreetAddress, Postcode, City;
            public string CompanyName, Position;
            public int AnnualIncome;

            public override string ToString()
            {
                return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
            }
        }

        public class PersonBuilder //facade builder
        {
            //1. must be protected
            protected Person person = new Person(); //this is a reference

            //3. use the builders
            public PersonJobBuilder Works => new PersonJobBuilder(person);
            public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

            //2. make sure to return the Person. 
            public static implicit operator Person(PersonBuilder pb) => pb.person;
        }

        public class PersonJobBuilder : PersonBuilder
        {
            public PersonJobBuilder(Person person)
            {
                //1. pass to the base class
                this.person = person;
            }

            //2. constructor with companyName
            public PersonJobBuilder At(string companyName)
            {
                person.CompanyName = companyName;
                return this;
            }
            public PersonJobBuilder AsA(string position)
            {
                person.Position = position;
                return this;
            }
            public PersonJobBuilder Earning(int income)
            {
                person.AnnualIncome = income;
                return this;
            }
        }

        public class PersonAddressBuilder : PersonBuilder
        {
            // might not work with a value type!
            public PersonAddressBuilder(Person person)
            {
                this.person = person;
            }

            public PersonAddressBuilder At(string streetAddress)
            {
                person.StreetAddress = streetAddress;
                return this;
            }

            public PersonAddressBuilder WithPostcode(string postcode)
            {
                person.Postcode = postcode;
                return this;
            }

            public PersonAddressBuilder In(string city)
            {
                person.City = city;
                return this;
            }

        }


    }
}
