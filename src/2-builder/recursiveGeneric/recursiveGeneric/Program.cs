using System;

namespace recursiveGeneric
{





    public class Person
    {
        public string Name;

        public string Position;

        //     class Builder : PersonInfoBuilder<Builder> { /* degenerate */ }
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public class PersonInfoBuilder
    {
        protected Person person = new Person();

        public PersonInfoBuilder Called(string name)
        {
            person.Name = name;
            return this;
        }
    }


    public class PersonJobBuilder : PersonInfoBuilder
    {
        public PersonJobBuilder WorkAsA(string position)
        {
            person.Position = position;
            return this;
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            var builder = new PersonJobBuilder();
            builder.Called("Tom")
                .WorkAsA  //can't do this, compile error. 
        }
    }
}