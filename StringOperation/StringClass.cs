namespace StringOperation
{
    class StringClass
    {
        public string StringInClassWOAccessor;
        public string stringInClass { get; set; }

        public void MethodWithOptionalArgs(string arg1, string arg2 = null)
        {

        }
    }
}
