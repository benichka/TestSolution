namespace Singleton
{
    class Singleton
    {
        // TODO: no, this isn't the good implementation
        // see : http://csharpindepth.com/Articles/General/Singleton.aspx
        // see : https://dzone.com/articles/understanding-and-implementing-singleton-pattern-i
        private static readonly Singleton _Instance;
        public Singleton Instance
        {
            get
            {
                return _Instance;
            }
        }
        public string ID { get; set; }
    }
}
