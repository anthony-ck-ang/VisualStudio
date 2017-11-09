using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections1
{
    public class Program
    {
        static void Main(string[] args)
        {
            GetDictionaryWithCustomClass();
            GetListWithCustomClass();
        }

        private static Guid NewMethod()
        {
            return Guid.NewGuid();
        }

        public static void GetDictionaryWithCustomClass()
        {
            var dictionaryHolder = new DictionaryHolder();
            dictionaryHolder[1001] = new User { UserId = Guid.NewGuid(), Name = "Andora", Age = 32, Email = "andora@hotmail.com", PhoneNo = 123456 };
            dictionaryHolder[1002] = new User { UserId = Guid.NewGuid(), Name = "Bentton", Age = 27, Email = "Bentton@hotmail.com", PhoneNo = 234567 };
            dictionaryHolder[1003] = new User { UserId = Guid.NewGuid(), Name = "Collaras", Age = 35, Email = "Collaras@hotmail.com", PhoneNo = 345678 };

            Console.WriteLine(dictionaryHolder[1001]);
        }

        public static void GetListWithCustomClass()
        {
            //adding user method 1
            List<User> userList = new List<User>()
            {
                new User
                {
                    UserId = Guid.NewGuid(),
                    Name = "Dallas", Age = 32,
                    Email = "Dallas@hotmail.com",
                    PhoneNo = 4567890,
                    Addresses = new List<Address>
                    {
                        new Address{FlatName="FlatName1", Street="ST1", Country="Singapore"},
                        new Address{FlatName="FlatName1", Street="ST1", Country="Singapore"}
                    }
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    Name = "Ezekiel", Age = 32,
                    Email = "Ezekiel@hotmail.com",
                    PhoneNo = 5678901,
                    Addresses = new List<Address>
                    {
                        new Address{FlatName="FlatName1", Street="ST1", Country="US"},
                        new Address{FlatName="FlatName1", Street="ST1", Country="US"}
                    }
                }
               //,
               //new User {
               //     UserId = Guid.NewGuid(),
               //     Name = "Farras", Age = 28,
               //     Email = "Farras@hotmail.com",
               //     PhoneNo = 6789012,
               //     Address = new Address{FlatName="FlatName1", Street="ST1", Country="Singapore"}}
             };

            //adding user method 2
            //userList.Add(new User { UserId = Guid.NewGuid(), Name = "Genisa", Age = 30, Email = "Genisa@hotmail.com", PhoneNo = 7890123, Address = new Address { FlatName = "FlatName2", Street = "St 2", Country = "Singapore" } });

            //foreach (var user in userList)
            //{
            //    Console.WriteLine(String.Format("User: {0} Age: {1} Email: {2} Phone number: {3}", user.Name, user.Age, user.Email, user.PhoneNo));
            //}

            //----------------------------------------------------------------------------------------------------------------------------------------
            //LINQ
            //Query by method
            var queriedUserListByName = userList.Select(x => x.Name); //query and retrieve all user's name
            var queriedUserListByAge = userList.Where(x => x.Age == 32).Select(x => x); //query and retrieve a list of user by age

            //Query by statement
            var queriedUserListByName2 = from u in userList
                                         select u.Name;

            var queriedUserListByAge2 = from u in userList
                                        where u.Age == 32
                                        select u;

            foreach (var user in queriedUserListByAge2)
            {
                Console.WriteLine(user);
            }

            //Projection -> transform an object into new form using only those properties stated (eg: different columns name)
            //using Anonymous type -> new{}
            var queriedUserListByAge3 = from u in userList
                                        where u.Age == 32
                                        select new { FirstName = u.Name, PhoneNumber = u.PhoneNo };

            foreach (var user in queriedUserListByAge3)
            {
                Console.WriteLine(user.FirstName);
                Console.WriteLine(user.PhoneNumber);
            }

            //Iterate through a list of addresses in each user object
            var queriedUserListByAge4 = from u in userList
                                        from a in u.Addresses
                                        where u.Age == 32
                                        select new { FirstName = u.Name, PhoneNumber = u.PhoneNo, Address = a };

            var addressesList = userList.Where(x => x.Age == 32).SelectMany(x => x.Addresses);

            foreach (var user in addressesList)
            {
                Console.WriteLine(user.Country);
            }


        }
    }

    public class DictionaryHolder
    {
        private readonly Dictionary<int, User> _userdictionary; //uses dict for data store internally

        public DictionaryHolder()
        {
            _userdictionary = new Dictionary<int, User>();
        }

        //indexer -> a way to set and get objects to/from dictionary
        public User this[int key]
        {
            get { return _userdictionary[key]; }
            set { _userdictionary[key] = value; }
        }

    }

    //Custom type
    public class User
    {
        //Automatically implemented property
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public Int64 PhoneNo { get; set; }
        public List<Address> Addresses { get; set; }

        public override string ToString()
        {
            return base.ToString() + ": "
                + "[User Id:" + UserId.ToString()
                + "], [Name:" + Name.ToString()
                + "], [Age:" + Age.ToString()
                + "], [Email:" + Email.ToString()
                + "], [PhoneNo:" + PhoneNo.ToString() + "]";

        }

    }

    //store address of each user
    public class Address
    {
        public string FlatName { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
    }
}
