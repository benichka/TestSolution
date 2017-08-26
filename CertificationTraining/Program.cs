using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CertificationTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            #region book: blocking collection
            BlockingCollection<string> col = new BlockingCollection<string>();

            // Always run
            // Listens for new items to be added to col
            Task read = Task.Run(() =>
            {
                while (true)
                {
                    if (!col.IsAddingCompleted)
                    {
                        // Block there until some data are available
                        var s = col.Take();
                        Console.WriteLine(s);

                        // This code is executed only when a data has just been taken
                        // from col
                        Console.WriteLine("an item was taken from col");

                        if (s.Equals("stop"))
                        {
                            // If the user enter "stop", the collection must not
                            // take any other entry
                            col.CompleteAdding();
                        } 
                    }
                    else
                    {
                        break;
                    }
                }
            });

            // Prompt the user to enter something in the line;
            // is the user doesn't provide a text (if he just hits enter),
            // the task ends.
            // Otherwise, the entered text is stored in col
            Task write = Task.Run(() =>
            {
                while (true)
                {
                    string s = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(s)) break;
                    col.Add(s);
                }
            });
            write.Wait();
            #endregion book: blocking collection

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

            #region question 88
            var myArray = new ArrayList();

            int myInt = 2;
            int myInt2;

            myArray.Add(myInt);

            int casted = (int)myArray[0];
            int casted2 = Convert.ToInt32(myArray[0]);

            // Compiler error
            //int wrongAnswer = ((List<int>)myArray)[0];
            #endregion question 88

            #region question 95
            // The right answer is A

            Dictionary<string, int> coinsCollected = new Dictionary<string, int>();
            coinsCollected.Add("Jean", 2);
            coinsCollected.Add("Ben", 0);
            coinsCollected.Add("Nico", 1);
            coinsCollected.Add("Toto", 10);
            coinsCollected.Add("Titi", 1000);

            foreach (var entry in coinsCollected)
            {
                Console.WriteLine(String.Format("Player {0} collected {1} coins", entry.Key, entry.Value.ToString("000")));
                //Console.WriteLine(String.Format("Player {1} collected {2} coins", "as", 2));
            }
            #endregion question 95

            #region question 102
            // The right answer is D

            MemoryStream WriteName(FullName name)
            {
                var ms = new MemoryStream();
                var binary = XmlDictionaryWriter.CreateBinaryWriter(ms);

                // A DataContractSerializer herits from XmlDictionaryWriter
                var ser = new DataContractSerializer(typeof(FullName));

                // Position the object into the stream. The data will be written
                // only after the stream is flushed or closed
                ser.WriteObject(binary, name);

                // Force the writer to write to the stream
                binary.Flush();

                return ms;
            }

            FullName ReadName(MemoryStream ms)
            {
                var binaryReader = XmlDictionaryReader.CreateBinaryReader(ms, XmlDictionaryReaderQuotas.Max);

                var ser = new DataContractSerializer(typeof(FullName));

                // Carefull! We need the reposition the stream at the beginning
                ms.Seek(0, SeekOrigin.Begin);

                var rehydrated = ser.ReadObject(binaryReader) as FullName;

                return rehydrated;
            }

            using (var fullNameStream = WriteName(new FullName() { FirstName = "Benoit", LastName = "Masson-Bedeau" }))
            {
                // The object is the name after deserialization
                var rehydratedFullName = ReadName(fullNameStream);
            }

            // Beyond the question
            void WriteNameInFile(FullName name)
            {
                using (var fs = File.Create(@"D:\temp\SerializeAsXml.xml"))
                {
                    var binary = XmlDictionaryWriter.CreateBinaryWriter(fs);

                    var ser = new DataContractSerializer(typeof(FullName));
                    ser.WriteObject(binary, name);

                    binary.Flush();
                }
            }

            FullName ReadNameInFile(string filePath)
            {
                using (var fs = File.OpenRead(filePath))
                {
                    var reader = XmlDictionaryReader.CreateBinaryReader(fs, XmlDictionaryReaderQuotas.Max);

                    var ser = new DataContractSerializer(typeof(FullName));
                    var fullName = ser.ReadObject(reader) as FullName;

                    return fullName;
                }
            }

            WriteNameInFile(new FullName() { FirstName = "Benoit", LastName = "Masson-Bedeau" });

            var readName = ReadNameInFile(@"D:\temp\SerializeAsXml.xml");
            #endregion question 102

            #region question 104
            /*
            // Correct answer is C, not B

            Console.Write("Enter unit price:");
            var price = Console.ReadLine();

            // B
            if(Regex.IsMatch(price, @"^(-)?\d+(\.\d\d)?$"))
            {
                Console.WriteLine("Valid price (answer B)");
            }
            else
            {
                Console.WriteLine("Invalid price (answer B)");
            }

            // C
            Regex reg1 = new Regex(@"^\d+(\.\d\d)?$");
            if (reg1.IsMatch(price))
            {
                Console.WriteLine("Valid price (answer C)");
            }
            else
            {
                Console.WriteLine("Invalid price (answer C)");
            }

            // D
            Regex reg2 = new Regex(@"^(-)?\d+(\.\d\d)?$");
            if (reg2.IsMatch(price))
            {
                Console.WriteLine("Valid price (answer D)");
            }
            else
            {
                Console.WriteLine("Invalid price (answer D)");
            }
            */
            #endregion question 104

            #region question 110
            // Some code is missing... It can be found here:
            // http://vceguide.com/which-code-segment-should-you-use-57/
            #endregion question 110

            #region question 119
            // ------------ using anonymous type ------------
            var message = new { From = "John", To = "Jack", Content = "Hello world" };

            // This method take a anonymous type as parameter; to do this, the parameter
            // must be of type dynamic
            void SendMessage(dynamic msg)
            {
                Console.WriteLine($"Sending message from anonymous type.");
                Console.WriteLine($"Sending message {msg.Content} to {msg.To} from {msg.From}.");
            }

            SendMessage(message);

            // ------------ using ExpandoObject ------------
            // We instanciate an ExpandoObject but we want to object to act as a dynamic object; that way,
            // we can add field at runtime
            // See https://msdn.microsoft.com/en-us/library/system.dynamic.expandoobject(v=vs.110).aspx
            dynamic message2 = new ExpandoObject();
            message2.From = "John";
            message2.To = "Jack";
            message2.Content = "Hello world";

            void SendMessage2(dynamic msg)
            {
                Console.WriteLine($"Sending message from ExpandoObject type.");
                Console.WriteLine($"Sending message {msg.Content} to {msg.To} from {msg.From}.");
            }

            SendMessage2(message2);
            #endregion question 119

            #region question 124
            // Correct answer is D.
            // Same question as 102.
            #endregion question 124

            #region question 125
            // Correct answer is C

            var myUrl = "http://www.google.com";

            var websites = TestIfWebSite(myUrl);

            List<string> TestIfWebSite(string url)
            {
                const string pattern = @"http://(www.)?([^\.]+)\.com";
                List<string> resultUrl = new List<string>();

                MatchCollection matches = Regex.Matches(url, pattern);

                foreach (Match match in matches)
                {
                    // match.Groups: return an array of each group captured. In our case:
                    // [2]: www.google.com (the whole string)
                    // [0]: www.
                    // [1]: google
                    resultUrl.Add(match.Value);
                }

                return resultUrl;
            }
            #endregion question 125

            #region question 135
            // D is the right answer. You can check the MD5/SHA256 hash of a file
            // here: http://onlinemd5.com/

            byte[] GetHashAnswerD(string fileName, string algorithmType)
            {
                var hasher = HashAlgorithm.Create(algorithmType);
                var fileBytes = File.ReadAllBytes(fileName);

                // Answer D
                // Compute the hash value for the specified byte array
                hasher.ComputeHash(fileBytes);

                // Gets the value of the computed hash code
                return hasher.Hash;
            }

            byte[] GetHashAnswerA(string fileName, string algorithmType)
            {
                var hasher = HashAlgorithm.Create(algorithmType);
                var fileBytes = File.ReadAllBytes(fileName);

                var outputBuffer = new byte[fileBytes.Length];
                hasher.TransformBlock(fileBytes, 0, fileBytes.Length, outputBuffer, 0);
                // Execution error at this line
                hasher.TransformFinalBlock(fileBytes, fileBytes.Length - 1, fileBytes.Length);

                return outputBuffer;
            }

            byte[] GetHashAnswerC(string fileName, string algorithmType)
            {
                var hasher = HashAlgorithm.Create(algorithmType);
                var fileBytes = File.ReadAllBytes(fileName);

                var outputBuffer = new byte[fileBytes.Length];
                hasher.TransformBlock(fileBytes, 0, fileBytes.Length, outputBuffer, 0);

                // Seems to work well even without TransformFinalBlock because the output buffer is
                // the size of the input buffer... But probably not a good practice
                return outputBuffer;
            }

            var hashedFileAnswerD = GetHashAnswerD(@"D:\temp\test.test", "SHA256");
            var AnswerDHashAsString = BitConverter.ToString(hashedFileAnswerD);

            //var hashedFileAnswerA = GetHashAnswerA(@"D:\temp\test.test", "SHA256");
            //var AnswerAHashAsString = BitConverter.ToString(hashedFileAnswerD);

            var hashedFileAnswerC = GetHashAnswerC(@"D:\temp\test.test", "SHA256");
            var AnswerCHashAsString = BitConverter.ToString(hashedFileAnswerD);
            #endregion question 135

            #region question 154
            void ProcessTask()
            {
                Task[] tasks = new Task[3]
                {
                    Task.Factory.StartNew(() => Console.WriteLine("task 1")),
                    Task.Factory.StartNew(() => Console.WriteLine("task 2")),
                    Task.Factory.StartNew(() => Console.WriteLine("task 3"))
                };

                // WaitFor doesn't exist for the static class Task
                //Task.WaitFor(3);

                // Yield doesn't exist. It exists for the static class Task but not for that use case
                //tasks.Yield();

                // WaitForCompletion doesn't exist, even for the static class Task
                //tasks.WaitForCompletion();

                Task.WaitAll(tasks);
            }

            ProcessTask();
            #endregion question 154

            #region question 166
            decimal[] loanAmounts = { 303m, 1000m, 85579m, 501,51m, 1200m, 400m, 22m };

            // Good answer
            IEnumerable<decimal> query = from amount in loanAmounts
                                         where amount % 2 == 0
                                         orderby amount ascending
                                         select amount;

            // Wrong answer
            //IEnumerable<decimal> query = from amount in loanAmounts
            //                             where amount % 2 == 0
            //                             ascending amount orderby
            //                             select amount;
            #endregion question 166

            #region question 184
            // See https://msdn.microsoft.com/en-us/library/w4hkze5k(v=vs.110)

            var myString1 = "toto";
            var myString2 = "toto";

            // Returns true because:
            // - first check – same reference: no;
            // - second check – both object are null: no;
            // - last check – myString1.Equals(myString2) -> yes!
            var areEquals = Object.Equals(myString1, myString2);

            // Special treatment for string... Returns true!
            // see : https://msdn.microsoft.com/en-us/library/system.object.referenceequals(v=vs.110)
            // see : https://msdn.microsoft.com/en-us/library/system.string.isinterned(v=vs.110)
            // tl;dr: string are "interned", so multiple instance with same text point to the same reference
            var areRefEquals1 = Object.ReferenceEquals(myString1, myString2);

            var myString1Plus = "toto" + myString1;
            var myString2Plus = "toto" + myString1;

            // This time, strings are not interned -> returns false
            var areRefEquals2 = Object.ReferenceEquals(myString1Plus, myString2Plus);

            var myString3 = myString1;
            // Returns true
            var areRefEquals3 = Object.ReferenceEquals(myString1, myString3);

            var myString4 = myString1Plus;
            // Also returns true -> string are reference type so the reference is copied
            var areRefEquals4 = Object.ReferenceEquals(myString1Plus, myString4);

            var myString5 = myString1Plus + "";
            // Also returns true...
            var areRefEquals5 = Object.ReferenceEquals(myString1Plus, myString5);

            var myString6 = "toto2";
            myString6 = myString6.Substring(0, 4);
            // returns false! String are immutable so another instance of string is created
            // when myString6 changes value
            var areRefEquals6 = Object.ReferenceEquals(myString1, myString6);

            var myString7 = "toto";
            // At the point, Object.ReferenceEquals(myString1, myString7) returns true
            myString7 = myString7 + "a";
            // Another string is instanciated, so Object.ReferenceEquals(myString1, myString7) is false
            myString7 = myString7.Substring(0, 4);
            // false!
            var areRefEquals7 = Object.ReferenceEquals(myString1, myString6);
            #endregion question 184

            #region question 185
            Group myGroup1 = Group.Supervisors;
            var isInferior = myGroup1 < Group.Administrators;

            Group myGroup2 = Group.Supervisors | Group.Managers;
            #endregion question 185

            #region question 189
            var dateSample = DateTime.Now;
            var tempSample = 25;

            var sampleText = String.Format(new CultureInfo("en-US"), "Temperature at {0:t} on {0:d}: {1:N2} °C", dateSample, tempSample);
            #endregion question 189

            #region question 190
            // cf. https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/linq-to-xml
            XNamespace ew = "ContactList";
            XElement root = new XElement(ew + "Root");
            Console.WriteLine(root);

            using (SchoolDBEntities dbContext = new SchoolDBEntities())
            {
                // It's sometimes better to use AsEnumerable than ToList.
                // For instance (not this case) when the query is done in a add loop
                XElement contacts = new XElement("contacts",
                                    //from s in dbContext.Students.ToList()
                                    from s in dbContext.Students.AsEnumerable()
                                    orderby s.Id
                                    select
                                        new XElement("contact", new XAttribute("contactId", s.Id),
                                            new XElement("firstName", s.FirstName),
                                            new XElement("lastName", s.LastName))
                                    );
                root.Add(contacts);
            }
            #endregion question 190

            #region question 194
            var sourceFilePath = @"D:\temp\only80bytes.txt";
            var headerFilePath = @"D:\temp\header.txt";
            var bodyFilePath = @"D:\temp\body.txt";

            using (FileStream fsource = File.OpenRead(sourceFilePath))
            using (FileStream fheader = File.OpenWrite(headerFilePath))
            using (FileStream fbody = File.OpenWrite(bodyFilePath))
            {
                byte[] header = new byte[20];
                byte[] body = new byte[fsource.Length - 20];

                // Read the source file and put the result in the header byte array
                fsource.Read(header, 0, header.Length);

                // Put the content of the header byte array in the fheader stream (that is, the "header" file)
                fheader.Write(header, 0, header.Length);

                // Read the source file, continuing at last index, and put the result in the body byte array
                fsource.Read(body, 0, body.Length);

                // Put the content of the body byte array in the fsource stream (that is, the "body" file)
                fbody.Write(body, 0, body.Length);
            }

            #endregion question 194
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

    #region question 102
    [DataContract] // The class need that attribute to be handled by a DataContractSerializer
    class FullName
    {
        [DataMember] // Don't forget the DataMember attribute, otherwise the prop won't be serialized
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
    }
    #endregion question 102

    #region question 185
    [Flags]
    public enum Group
    {
        Users = 1,
        Supervisors = 2,
        Managers = 4,
        Administrators = 8
    }
    #endregion question 185
}
