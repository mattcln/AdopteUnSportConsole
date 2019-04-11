using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace AdopteUnSportConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ExoType();
            Console.ReadKey();
        }

        static void ExoType()
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = MATIbol78;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT nom, prenom FROM Clients;"; // exemple de requête

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            // exemple de mannipulation du résultat
            while (reader.Read())       // parcours ligne par ligne
            {
                string tupleCourrant = "";
                for (int i = 0; i < reader.FieldCount; i++)  //parcours cellule par cellule
                {
                    string valeurattribut = reader.GetValue(i).ToString(); //récupération de la valeur de chaque cellule sous forme d'une string
                    tupleCourrant += valeurattribut + ",";
                }
                Console.WriteLine(tupleCourrant); //affichage de la ligne 
            }
            maConnexion.Close();
        }

        static void NouvelleCommande()                                      // EN COURS
        {
            Console.WriteLine("Une nouvelle commande vient d'être créer");
            string IDProduit = AjouterUnArticle();  //Renvoie l'ID d'un produit qui existe
            //Proposer d'ajouter plus d'articles etc.

        }
        static string AjouterUnArticle()                                    // EN COURS
        {
            Console.WriteLine("Veuillez renseigner l'ID du produit que vous voulez ajouter :");
            string IDProduit = Console.ReadLine();
            bool FindProduit = ExistenceProduit(IDProduit); //Renvoie "true" si le produit existe & "false" si le produit existe pas
            while (FindProduit == false)
            {
                Console.WriteLine("L'ID précisé n'existe pas, veuillez renseigner un nouvel ID :");
                IDProduit = Console.ReadLine();
                FindProduit = ExistenceProduit(IDProduit);
                //Faire une fonction pour sortir du programme sinon si on a pas d'ID valide, la boucle est infinie
            }
            return IDProduit;
        }
        static bool ExistenceProduit(string IDProduit)                      //  CA MARCHE
        {
            bool Existence = false;
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = MATIbol78;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT IDProduit from Produit"; // exemple de requête

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string ligne = reader.GetValue(i).ToString();
                    Console.WriteLine(ligne);
                    if (ligne == IDProduit)
                    {
                        Existence = true;
                    }
                }
            }
            maConnexion.Close();
            return Existence;
        }
    }
}
