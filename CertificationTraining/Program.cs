using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CertificationTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            #region question 20
            /*
            var myRunnerList = new Runners();
            myRunnerList.Add("Toto");
            myRunnerList.Add("Tata");
            myRunnerList.Add("Titi");
            */
            #endregion question 20

            #region question 22
            var inputData = @"http://www.google.fr";

            // VERY simple pattern...
            string regExPattern = @"^(https?://)";

            var evaluator = new Regex(regExPattern, RegexOptions.Compiled);

            var result = evaluator.IsMatch(inputData);
            #endregion question 22

            #region question 31
            var bookUser = new BookUser("John");
            bookUser.Add("Getting to yes");
            bookUser.Add("Influence");
            #endregion question 31
        }
    }

    #region question 20
    // Little bit updated to make more sense

    public delegate void AddUserCallback(int i);

    public class User
    {
        public string Name { get; set; }

        public User(string name)
        {
            Name = name;
        }
    }

    public class UserTracker
    {
        List<User> users = new List<User>();

        public void AddUser(string name, AddUserCallback callback)
        {
            users.Add(new User(name));
            callback(users.Count);
        }
    }

    public class Runner : User
    {
        public Runner(string name) : base(name)
        {

        }
    }

    public class Runners
    {
        private UserTracker tracker = new UserTracker();

        public List<Runner> RunnerList { get; private set; }

        public void Add(string name)
        {
            tracker.AddUser(name, delegate (int i) { Console.WriteLine($"I'm {name} and I'm the {i}th user"); });
        }
    }
    #endregion question 20

    #region question 31
    public delegate void AddBookCallback(int i);

    public class Book
    {
        public string Name { get; set; }

        public Book(string name)
        {
            Name = name;
        }
    }

    public class BookTracker
    {
        List<Book> books = new List<Book>();

        public void AddBook(string name, AddBookCallback callback)
        {
            books.Add(new Book(name));
            callback(books.Count);
        }
    }

    public class BookUser
    {
        public string UserName { get; set; }

        public BookUser(string userName)
        {
            UserName = userName;
        }

        BookTracker tracker = new BookTracker();

        // Add a book to the user
        public void Add(string name)
        {
            tracker.AddBook(name, delegate (int i) { Console.WriteLine($"User {UserName} has now {i} books"); });
        }
    }
    #endregion question 31
}
