using System;

namespace Runerne.Instantiation.UnitTest
{
    internal class Person
    {
        public string Name { get; }
        public int Age { get; private set; }
        public Gender Gender { get; }

        public DateTime Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                _birthday = value;
                var now = DateTime.Now;
                var nowDate = now.ToString("MMdd");
                var birthDate = _birthday.ToString("MMdd");
                Age = now.Year - _birthday.Year;
                if (string.CompareOrdinal(nowDate, birthDate) < 0)
                    Age -= 1;
            }
        }
        private DateTime _birthday;

        public Person(string name, int age, Gender gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }
    }
}
