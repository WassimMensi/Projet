namespace ConsoleNoImplicitUsings;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        //  String? s = null;
        // Console.WriteLine(s);

        // var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 ,16, 17, 18, 19, 20 };
        // // Range example
        // var maRange = array[4..^2];

        // Console.WriteLine($"{string.Join(", ", maRange)}");

        var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

        var maRange = array[4..^2];

        Console.WriteLine(string.Join(", ", maRange));
    }
}




// public class Eleve()
// {
//     // En c# on utilise les propriétés (property) et les champs (field)
//     // les propriétés donnent acces a des méthodes qui permettent de manipuler les champs
//     // les champs sont des variables qui stockent des valeurs
//     // this is a property (propriété)
//     public string Nom { get; set; }
//     public string Prenom { get; set; }
//     public int Age { get; set; }

// // this is a field (champ)
//     private string _nom;
// }

// public class Professeur()
// {
//     public string Nom { get; set; }
//     public string Prenom { get; set; }
//     public int Age { get; set; }
// }

// public interface ISeflLunch
// {
//     public void SelfLunch();
//     public void SelfLunch(string plat);

//     public void SelfLunch(string plat, string boisson);

//     public void SelfLunch(string plat, string boisson, string dessert);
// }