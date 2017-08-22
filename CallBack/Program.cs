using System;
using System.Collections.Generic;

namespace CallBack
{
    class Program
    {
        static void Main(string[] args)
        {
            var bookUser = new BookUser("John");
            bookUser.Add("Getting to yes");
            bookUser.Add("Influence");
        }
    }

    /// <summary>Callback</summary>
    public delegate void AddBookCallback(int i);

    /// <summary>
    /// Represents a book
    /// </summary>
    public class Book
    {
        /// <summary>Book name</summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name to give to the book</param>
        public Book(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Add a book to a collection and return the number of book present in the collection
    /// </summary>
    public class BookTracker
    {
        /// <summary>Book list</summary>
        List<Book> books = new List<Book>();

        /// <summary>
        /// Add a book to the book list and return the number in it in the callback
        /// </summary>
        /// <param name="name">Name of book to add</param>
        /// <param name="callback">Callback</param>
        public void AddBook(string name, AddBookCallback callback)
        {
            books.Add(new Book(name));
            callback(books.Count);
        }
    }

    /// <summary>
    /// Describe a book user
    /// </summary>
    public class BookUser
    {
        /// <summary>User name</summary>
        public string UserName { get; set; }

        /// <summary>Book tracker of the user</summary>
        public BookTracker Tracker { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userName">User name</param>
        public BookUser(string userName)
        {
            UserName = userName;
            Tracker = new BookTracker();
        }

        /// <summary>
        /// Add a book to the user
        /// </summary>
        /// <param name="name">Name of the book to add</param>
        public void Add(string name)
        {
            Tracker.AddBook(name, delegate (int i) { Console.WriteLine($"User {UserName} has now {i} books"); });
        }
    }
}
