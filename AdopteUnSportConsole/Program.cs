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
        static void ExoType()
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Prekodragan3;";
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

        static void Main(string[] args)
        {
            ExoType();
            Console.ReadKey();
        }
    }
}
