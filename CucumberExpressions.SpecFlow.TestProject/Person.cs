﻿using System;

namespace CucumberExpressions.SpecFlow.TestProject
{
    public class Person
    {
        public string Name { get; }

        public Person(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"Person: {Name}";
        }
    }

    public class User : Person
    {
        public User(string name) : base(name)
        {
        }

        public override string ToString()
        {
            return $"User: {Name}";
        }
    }
}
