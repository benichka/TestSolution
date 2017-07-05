namespace InheritanceSimple
{
    class Program
    {
        static void Main(string[] args)
        {
            //var toto = new SubTest("toto");
            //Console.WriteLine(toto.MyProperty);

            var mySubAbstract = new MySubAbstract();

            // Méthode implémentée dans MySubAbstract
            mySubAbstract.MyAbstractMethod("toto");
            // Méthode implémentée dans MyAbstract et exploitable depuis l'instance de MySubAbstract
            mySubAbstract.MyNonAbsMethod("toto");
            // Méthode implémentée dans MySubAbstract
            mySubAbstract.MyVirtualMethod("toto");
            // Méthode implémentée dans MySubAbstract et qui appelle MyAbstract.MyNonAbsMethod
            mySubAbstract.TestUseNonAbsMethod("toto");
        }
    }
}
