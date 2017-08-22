using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Serialize
{
    /// <summary>
    /// A project to demonstrate how to serialize objects
    /// </summary>
    class Program
    {
        // Further reading
        // https://docs.microsoft.com/en-gb/dotnet/framework/wcf/feature-details/how-to-serialize-and-deserialize-json-data
        // https://docs.microsoft.com/en-gb/dotnet/api/system.nonserializedattribute?view=netframework-4.7
        // https://msdn.microsoft.com/en-gb/library/system.runtime.serialization.ondeserializedattribute(v=vs.110).aspx

        static void Main(string[] args)
        {
            #region JSON serialization
            var nameTest = new Name()
            {
                Values = new int[] { 1, 2, 3 },
                FirstName = "Benoit",
                LastName = "Masson-Bedeau",
                PublicField = "Je suis un champ public"
            };

            // Very simple serialization, but beware: only public fields and properties
            // will be serialized
            var ser = new JavaScriptSerializer();

            var nameTestAsJSON = ser.Serialize(nameTest);

            // The default constructor is used
            var deserializedName = ser.Deserialize<Name>(nameTestAsJSON);

            // A better way to control serialization with JSON
            // A reference to System.Runtime.Serialization needs to be added
            var nameDataContract = new NameDataContract()
            {
                Values = new int[] { 1, 2, 3 },
                FirstName = "Benoit",
                LastName = "Masson-Bedeau",
                PublicField = "Je suis un champ public"
            };

            var dataContractSer = new DataContractJsonSerializer(typeof(NameDataContract));

            // serialize to a file
            using (FileStream buffer = File.Create(@"D:\temp\jsonDataContractName.txt"))
            {
                dataContractSer.WriteObject(buffer, nameDataContract);
            }

            // serialize to memory
            using (var buffer = new MemoryStream())
            {
                dataContractSer.WriteObject(buffer, nameDataContract);

                // Show the JSON output
                buffer.Position = 0;

                using (StreamReader sr = new StreamReader(buffer))
                {
                    Console.Write("JSON form of Person object: ");
                    Console.WriteLine(sr.ReadToEnd());
                }
            }

            // deserialize from a file
            using (FileStream buffer = File.OpenRead(@"D:\temp\jsonDataContractName.txt"))
            {
                // The object is built only with the memory: no constructor is invoked
                var nameDataContractRehydrated = dataContractSer.ReadObject(buffer);
            }
            #endregion JSON serialization

            #region binary serialization
            var nameSerializable1 = new NameSerializable()
            {
                Values = new int[] { 1, 2, 3 },
                FirstName = "Benoit",
                LastName = "Masson-Bedeau",
                UselessInfo = "useless info",
            };

            var nameSerializable2 = new NameSerializable("Benoit", "Masson-Bedeau");
            nameSerializable2.UselessInfo = "useless info";
            nameSerializable2.Values = new int[] { 1, 2, 3 };

            // Instanciate a binary formatter
            IFormatter binFormatter = new BinaryFormatter();

            // Serialization to a file
            using (FileStream buffer = File.Create(@"D:\temp\binaryNameSerializable.txt"))
            {
                binFormatter.Serialize(buffer, nameSerializable1);
            }

            // Deserialize from a file
            using (FileStream buffer = File.OpenRead(@"D:\temp\binaryNameSerializable.txt"))
            {
                var hydratedNameSerializable = binFormatter.Deserialize(buffer) as NameSerializable;
            }

            // TODO: binary serialization to an object (?)
            #endregion binary serialization

            #region XML serialization
            // Instanciate a XML formatter
            // It's this formatter that needs a default constructor for the type it uses
            // XmlSerializer doesn't implement IFormatter: it serializes the whole object with
            // all it's property, without taking care of the GetObjectData
            // Be careful because not all types can be serialized via xml (example: TimeSpan)
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(NameSerializable));

            // Serialize to a file
            using (FileStream buffer = File.Create(@"D:\temp\xmlNameSerializable.txt"))
            {
                xmlFormatter.Serialize(buffer, nameSerializable1);
            }

            // Deserialize from a file
            using (FileStream buffer = File.OpenRead(@"D:\temp\xmlNameSerializable.txt"))
            {
                var hydratedNameSerializable = xmlFormatter.Deserialize(buffer) as NameSerializable;
            }

            // TODO: binary serialization to a string/XML object
            #endregion XML serialization

            #region SOAP serialization
            // Instanciate a SOAP formatter
            // To instanciate SoapFormatter, a reference to System.Runtime.Serialization.Formatters.Soap
            // must be made
            IFormatter soapFormatter = new SoapFormatter();

            // Serialize to a file
            using (FileStream buffer = File.Create(@"D:\temp\soapNameSerializable.txt"))
            {
                soapFormatter.Serialize(buffer, nameSerializable1);
            }

            // Deserialize from a file
            using (FileStream buffer = File.OpenRead(@"D:\temp\soapNameSerializable.txt"))
            {
                var hydratedNameSerializable = soapFormatter.Deserialize(buffer);
            }
            #endregion SOAP serialization

            #region Microsoft example
            //Creates a new TestSimpleObject object.
            TestSimpleObject obj = new TestSimpleObject();

            Console.WriteLine("Before serialization the object contains: ");
            obj.Print();

            //Opens a file and serializes the object into it in SOAP format.
            Stream stream = File.Create(@"D:\temp\MicrosoftExample.xml");
            SoapFormatter formatter = new SoapFormatter();

            formatter.Serialize(stream, obj);
            stream.Close();

            //Empties obj.
            obj = null;

            //Opens file and deserializes the object from it.
            stream = File.OpenRead(@"D:\temp\MicrosoftExample.xml");
            formatter = new SoapFormatter();

            obj = (TestSimpleObject)formatter.Deserialize(stream);
            stream.Close();

            Console.WriteLine("");
            Console.WriteLine("After deserialization the object contains: ");
            obj.Print();
            #endregion Microsoft example
        }
    }

    /// <summary>
    /// Classic representation of a name
    /// </summary>
    public class Name
    {
        public int[] Values { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private string PrivateProperty { get; set; }
        internal string InternalProperty { get; set; }
        protected string ProtectedProperty { get; set; }
        public string PublicField;
        private string privateField;

        public Name()
        {
            PrivateProperty = "I'm a private property";
            InternalProperty = "I'm a internal property";
            ProtectedProperty = "I'm a protected property";
            privateField = "I'm a private field";
        }
    }

    /// <summary>
    /// Name that can be handled with data contract
    /// </summary>
    [DataContract] // Define the data contract for the class
    public class NameDataContract
    {
        [DataMember] // Every member decorated with this attribute will serialized
        public int[] Values { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        private string PrivateProperty { get; set; }
        internal string InternalProperty { get; set; }
        [DataMember]
        protected string ProtectedProperty { get; set; }
        [DataMember]
        public string PublicField;
        private string privateField;

        // => Only Values, FirstName, LastName, ProtectedProperty and PublicField will be serialized

        public NameDataContract()
        {
            PrivateProperty = "I'm a private property";
            InternalProperty = "I'm a internal property";
            ProtectedProperty = "I'm a protected property";
            privateField = "I'm a private field";
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            PrivateProperty = "This value was set after deserialization.";
        }
    }

    /// <summary>
    /// This class is serializable by any of the .NET Framework IFormatter implementations
    /// </summary>
    [Serializable] // Mandatory if we want the class to be serializable
    public class NameSerializable : ISerializable
    {
        public int[] Values { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UselessInfo { get; set; }

        // In order to be serializable by XmlSerializer (only), the class must have a default constructor
        // This constructor can be private or internal.
        /// <summary>
        /// Default constructor
        /// </summary>
        public NameSerializable()
        { }

        public NameSerializable(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// deserialization constructor, which accepts SerializationInfo and StreamingContext objects as parameters.
        /// This constructor enables you to rehydrate your object during the deserialization process.
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="ctxt">Streaming context</param>
        public NameSerializable(SerializationInfo info, StreamingContext ctxt)
        {
            this.Values = (int[])info.GetValue("Values", typeof(int[]));
            this.FirstName = info.GetValue("FirstName", typeof(string)) as string;
            this.LastName = info.GetValue("LastName", typeof(string)) as string;
        }

        /// <summary>
        /// Object serialization
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Streaming context</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Values", this.Values);
            info.AddValue("FirstName", this.FirstName);
            info.AddValue("LastName", this.LastName);

            // UselessInfo is not added to the serialization: it won't be present
            // in the serialized object
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            FirstName = "This value was set after deserialization.";
        }
    }

    #region Microsoft example
    // A test object that needs to be serialized.
    // https://msdn.microsoft.com/fr-fr/library/system.nonserializedattribute(v=vs.110).aspx
    [Serializable()]
    public class TestSimpleObject
    {
        public int member1;
        public string member2;
        public string member3;
        public double member4;

        // A field that is not serialized.
        [NonSerialized]
        public string member5;

        public TestSimpleObject()
        {
            member1 = 11;
            member2 = "hello";
            member3 = "hello";
            member4 = 3.14159265;
            member5 = "hello world!";
        }

        public void Print()
        {
            Console.WriteLine("member1 = '{0}'", member1);
            Console.WriteLine("member2 = '{0}'", member2);
            Console.WriteLine("member3 = '{0}'", member3);
            Console.WriteLine("member4 = '{0}'", member4);
            Console.WriteLine("member5 = '{0}'", member5);
        }
    }
    #endregion Microsoft example
}
