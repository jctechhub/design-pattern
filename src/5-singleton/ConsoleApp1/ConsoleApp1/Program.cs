﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using MoreLinq;
using NUnit.Framework;
using static System.Console;

namespace ConsoleApp1
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private static int instanceCount;
        public static int Count => instanceCount;

        private SingletonDatabase()
        {
            instanceCount++;
            WriteLine("Initializing database");

            capitals = File.ReadAllLines(
                    Path.Combine(
                        new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "capitals.txt")
                )
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1).ToString()));
        }

        public int GetPopulation(string name)
        {
            return capitals[name];
        }
        private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(()=>new SingletonDatabase());
   
        public static SingletonDatabase Instance => instance.Value;
    }

    [TestFixture]
    public class SingletonTests
    {
        [Test]
        public void IsSingltonTest()
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
            var city = "Sydney";
            WriteLine($"{city} has the population of {db.GetPopulation(city)}");
            Console.Read();
        }
    }
}
