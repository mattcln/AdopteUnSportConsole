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
            CréationClient();
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

        //Commande
        static void NouvelleCommande()                                                                                                          // CA MARCHE
        {
            Console.WriteLine(" Une nouvelle commande vient d'être créer");
            string IDProduit = AjouterUnArticle();  //Renvoie l'ID d'un produit qui existe
            string IDProduitsCom = IDProduit;   //Enregistrement des ID des produits à ajouter dans la commande
            Console.WriteLine(" Voulez-vous ajouter des articles supplémentaires ?");
            string RéponseArticle = OuiNon();
            while (RéponseArticle == "oui")
            {
                IDProduit = AjouterUnArticle();
                IDProduitsCom += "," + IDProduit;
                Console.WriteLine(" Voulez-vous ajouter des articles supplémentaires ?");
                RéponseArticle = OuiNon();
            }
            Console.WriteLine(" Voici la liste des IDs des produits séléctionnés : " + IDProduitsCom);
            Console.WriteLine(" Confirmez-vous la commande ? ('Oui' pour confirmer, 'Non' pour annuler)");
            string RConfirmation = OuiNon();
            if (RConfirmation == "oui")
            {
                string[] IDP = IDProduitsCom.Split(',');
                for (int i = 0; i < IDP.Length; i++)
                {
                    SoustraireArticle(IDP[i]);
                }
            }
        }
        static string AjouterUnArticle()                                                                                                        // CA MARCHE
        {
            Console.WriteLine(" Veuillez renseigner l'ID du produit que vous voulez ajouter :");
            string IDProduit = Console.ReadLine();
            bool FindProduit = ExistenceProduit(IDProduit); //Renvoie "true" si le produit existe & "false" si le produit existe pas
            while (FindProduit == false)
            {
                Console.WriteLine(" L'ID précisé n'existe pas, veuillez renseigner un nouvel ID :");
                IDProduit = Console.ReadLine();
                FindProduit = ExistenceProduit(IDProduit);
                //Faire une fonction pour sortir du programme sinon si on a pas d'ID valide, la boucle est infinie
            }
            return IDProduit;
        }

        //Client
        static void CréationClient()                                                                                                            // CA MARCHE
        {
            Console.WriteLine(" Veuillez rentrer les informations suivantes du client :");
            Console.WriteLine(" Nom :");
            string Nom = Console.ReadLine();
            Console.WriteLine(" Prénom :");
            string Prénom = Console.ReadLine();
            Console.WriteLine(" Année de naissance :");
            int AnnéeNaiss = int.Parse(Console.ReadLine());
            Console.WriteLine(" Adresse :");
            string Adresse = Console.ReadLine();
            Console.WriteLine(" Ville :");
            string Ville = Console.ReadLine();
            Console.WriteLine(" Email :");
            string Email = Console.ReadLine();
            EnregistrementClient(Nom, Prénom, AnnéeNaiss, Adresse, Ville, Email);

        }                                                                                                         
        static void EnregistrementClient(string Nom, string Prénom, int AnnéeNaiss, string Adresse, string Ville, string Email)                 // CA MARCHE
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = MATIbol78;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();

            string IDClient = CréationIDClient();

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "insert into Clients values ('" + IDClient + "','" + Nom + "','" + Prénom + "','" + AnnéeNaiss + "','" + Adresse + "','" + Ville + "','" + 0 + "','" + Email + "')";
            Console.WriteLine(command.CommandText);
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            Console.WriteLine("Le client a bien été enregistré.");
            maConnexion.Close();
        }
        static string CréationIDClient()                                                                                                        // CA MARCHE
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = MATIbol78;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "select count(IDClients)+1 from Clients";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string IDClient = "";
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    IDClient = reader.GetValue(i).ToString();
                }
            }
            Console.WriteLine(IDClient);
            while (IDClient.Length < 4)
            {
                IDClient = "0" + IDClient;
            }
            IDClient = "A" + IDClient;
            Console.WriteLine(IDClient);
            maConnexion.Close();
            return IDClient;
        }                               
        

        //Fonctions outils

        static bool ExistenceProduit(string IDProduit)                                                                                          // CA MARCHE
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
                    if (ligne == IDProduit)
                    {
                        Existence = true;
                        Console.WriteLine(" Le produit a été trouvé.");
                    }
                }
            }
            maConnexion.Close();
            return Existence;
        }
        static string OuiNon()                                                                                                                  // CA MARCHE
        {
            string Réponse = Console.ReadLine();
            Réponse = Réponse.ToLower();
            while (Réponse != "oui" && Réponse != "non")
            {
                Console.WriteLine("\n   Il y a eu une erreur de compréhension, veuillez renseigner de nouveau par 'oui' ou 'non' s'il-vous-plaît: ");
                Réponse = Console.ReadLine();
                Réponse = Réponse.ToLower();
            }
            Console.Clear();
            return Réponse;
        } 
        static void SoustraireArticle(string IDProduit)                                                                                         // CA MARCHE
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = MATIbol78;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "UPDATE Produit SET stock = stock - 1 WHERE IDProduit = '"+IDProduit+"'";    
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            Console.WriteLine("Le stock du produit " + IDProduit + " a été baissé de 1 avec succès.");
            maConnexion.Close();
        }
    }
}
