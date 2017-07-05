namespace InheritanceSimple
{
    class SubTest : Test
    {
        public SubTest(string name) : base(ConvToUpper(name))
        {
            this.MyProperty += " hopla";
        }

        private static string ConvToUpper(string param)
        {
            return param.ToUpper();
        }
    }
}
