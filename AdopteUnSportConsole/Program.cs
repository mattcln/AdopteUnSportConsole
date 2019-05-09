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
            NouvelleCommande();
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
        static void NouvelleCommande()                                                                                                                              // CA MARCHE
        {
            Console.WriteLine(" Une nouvelle commande vient d'être créer");
            string IDClient = ConnexionClient();
            int NBArticles = 0;
            string IDProduit = AjouterUnArticle();  //Renvoie l'ID d'un produit qui existe
            NBArticles++;
            string IDProduitsCom = IDProduit;   //Enregistrement des ID des produits à ajouter dans la commande
            Console.WriteLine(" Voulez-vous ajouter des articles supplémentaires ?");
            string RéponseArticle = OuiNon();
            while (RéponseArticle == "oui")
            {
                IDProduit = AjouterUnArticle();
                NBArticles++;
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
                EnregistrerCommande(IDClient, NBArticles);
            }            
        }
        static string AjouterUnArticle()                                                                                                                            // CA MARCHE
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
        static void EnregistrerCommande(string IDClient, int NBArticles) //Faut sauvegarder la commande dans le SQL, la méthode reçoit déjà le bon IDClient et le bon NBArticles
        {

        }

        //Client
        static void CréationClient()                                                                                                                                // CA MARCHE
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
        static void EnregistrementClient(string Nom, string Prénom, int AnnéeNaiss, string Adresse, string Ville, string Email)                                     // CA MARCHE
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
        static string CréationIDClient()                                                                                                                            // CA MARCHE
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
        static void InformationsClient()                                                                                                                            // CA MARCHE
        {
            Console.WriteLine("Par quel moyen souhaitez-vous retrouver les informations du client ? (IDClient, Nom, Prénom, AnnéeNaiss, Adresse, Ville, Dépense, Email)");
            string Moyen = Console.ReadLine();
            Moyen = Moyen.ToLower();
            while(Moyen != "idclient" && Moyen != "nom" && Moyen != "prénom" && Moyen != "annéenaiss" && Moyen != "adresse" && Moyen != "ville" && Moyen != "dépense" && Moyen != "email")
            {
                Console.WriteLine(" Veuillez renseigner un moyen valide s'il-vous-plaît : (IDClient, Nom, Prénom, AnnéeNaiss, Adresse, Ville, Dépense, Email)");
                Moyen = Console.ReadLine();
                Moyen = Moyen.ToLower();
            }
            string InfoB = "";
            if (Moyen == "idclient")
            {
                Console.WriteLine("Veuillez renseigner l'ID du client :");
                InfoB = Console.ReadLine();
            }
            if (Moyen == "nom")
            {
                Console.WriteLine("Veuillez renseigner le nom du client :");
                InfoB = Console.ReadLine();
            }
            if (Moyen == "prénom")
            {
                Console.WriteLine("Veuillez renseigner le prénom du client :");
                InfoB = Console.ReadLine();
            }
            if (Moyen == "annéenaiss")
            {
                Console.WriteLine("Veuillez renseigner l'année de naissance du client :");
                InfoB = Console.ReadLine();
            }
            if (Moyen == "adresse")
            {
                Console.WriteLine("Veuillez renseigner l'adresse du client :");
                InfoB = Console.ReadLine();
            }
            if (Moyen == "ville")
            {
                Console.WriteLine("Veuillez renseigner la ville du client :");
                InfoB = Console.ReadLine();
            }
            if (Moyen == "dépense")
            {
                Console.WriteLine("Veuillez renseigner les dépenses du client :");
                InfoB = Console.ReadLine();
            }
            if (Moyen == "email")
            {
                Console.WriteLine("Veuillez renseigner l'email du client :");
                InfoB = Console.ReadLine();
            }
            RetrouverInformationsclient(Moyen, InfoB);
        }
        static void RetrouverInformationsclient(string Moyen, string InfoB)                                                                                               // CA MARCHE
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = MATIbol78;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();
            string IDClient = ""; string Nom = ""; string Prénom = ""; int dateNaiss = 0; string adresse = ""; string ville = ""; int depenses = 0; string email = "";
            MySqlCommand command = maConnexion.CreateCommand();
            MySqlDataReader reader;
            if (Moyen == "idclient")
            {
                command.CommandText = "select nom , prenom, dateNaiss , adresse, ville, depenses, email from Clients where IDClients = '" + InfoB + "'";
                
                reader = command.ExecuteReader();
                string InfoClient = "";
                while (reader.Read())       // parcours ligne par ligne
                {
                    InfoClient = "";
                    for (int i = 0; i < reader.FieldCount; i++)  //parcours cellule par cellule
                    {
                        string valeurattribut = reader.GetValue(i).ToString();
                        InfoClient += valeurattribut + ",";
                    }                    
                }
                string[] TabInfoClient = InfoClient.Split(',');
                IDClient = InfoB;
                Nom = TabInfoClient[0];
                Prénom = TabInfoClient[1];
                dateNaiss = Convert.ToInt32(TabInfoClient[2]);
                adresse = TabInfoClient[3];
                ville = TabInfoClient[4];
                depenses = Convert.ToInt32(TabInfoClient[5]);
                email = TabInfoClient[6];
            }
            if (Moyen == "nom")
            {
                command.CommandText = "select IDClients , prenom, dateNaiss , adresse, ville, depenses, email from Clients where nom = '" + InfoB + "'";

                reader = command.ExecuteReader();
                string InfoClient = "";
                while (reader.Read())       // parcours ligne par ligne
                {
                    InfoClient = "";
                    for (int i = 0; i < reader.FieldCount; i++)  //parcours cellule par cellule
                    {
                        string valeurattribut = reader.GetValue(i).ToString();
                        InfoClient += valeurattribut + ",";
                    }
                }
                string[] TabInfoClient = InfoClient.Split(',');
                IDClient = TabInfoClient[0];
                Nom = InfoB;
                Prénom = TabInfoClient[1];
                dateNaiss = Convert.ToInt32(TabInfoClient[2]);
                adresse = TabInfoClient[3];
                ville = TabInfoClient[4];
                depenses = Convert.ToInt32(TabInfoClient[5]);
                email = TabInfoClient[6];
            }
            if (Moyen == "prenom")
            {
                command.CommandText = "select IDClients , nom, dateNaiss , adresse, ville, depenses, email from Clients where prenom = '" + InfoB + "'";

                reader = command.ExecuteReader();
                string InfoClient = "";
                while (reader.Read())       // parcours ligne par ligne
                {
                    InfoClient = "";
                    for (int i = 0; i < reader.FieldCount; i++)  //parcours cellule par cellule
                    {
                        string valeurattribut = reader.GetValue(i).ToString();
                        InfoClient += valeurattribut + ",";
                    }
                }
                string[] TabInfoClient = InfoClient.Split(',');
                IDClient = TabInfoClient[0];
                Nom = TabInfoClient[1];
                Prénom = InfoB;
                dateNaiss = Convert.ToInt32(TabInfoClient[2]);
                adresse = TabInfoClient[3];
                ville = TabInfoClient[4];
                depenses = Convert.ToInt32(TabInfoClient[5]);
                email = TabInfoClient[6];
            }
            if (Moyen == "annéenaiss")
            {
                command.CommandText = "select IDClients, nom, prenom , adresse, ville, depenses, email from Clients where dateNaiss = " + InfoB;

                reader = command.ExecuteReader();
                string InfoClient = "";
                while (reader.Read())       // parcours ligne par ligne
                {
                    InfoClient = "";
                    for (int i = 0; i < reader.FieldCount; i++)  //parcours cellule par cellule
                    {
                        string valeurattribut = reader.GetValue(i).ToString();
                        InfoClient += valeurattribut + ",";
                    }
                }
                string[] TabInfoClient = InfoClient.Split(',');
                IDClient = TabInfoClient[0];
                Nom = TabInfoClient[1];
                Prénom = TabInfoClient[2];
                dateNaiss = Convert.ToInt32(InfoB);
                adresse = TabInfoClient[3];
                ville = TabInfoClient[4];
                depenses = Convert.ToInt32(TabInfoClient[5]);
                email = TabInfoClient[6];
            }
            if (Moyen == "adresse")
            {
                command.CommandText = "select IDClients, nom, prenom , dateNaiss, ville, depenses, email from Clients where adresse = '" + InfoB + "'";

                reader = command.ExecuteReader();
                string InfoClient = "";
                while (reader.Read())       // parcours ligne par ligne
                {
                    InfoClient = "";
                    for (int i = 0; i < reader.FieldCount; i++)  //parcours cellule par cellule
                    {
                        string valeurattribut = reader.GetValue(i).ToString();
                        InfoClient += valeurattribut + ",";
                    }
                }
                string[] TabInfoClient = InfoClient.Split(',');
                IDClient = TabInfoClient[0];
                Nom = TabInfoClient[1];
                Prénom = TabInfoClient[2];
                dateNaiss = Convert.ToInt32(TabInfoClient[3]);
                adresse = InfoB;
                ville = TabInfoClient[4];
                depenses = Convert.ToInt32(TabInfoClient[5]);
                email = TabInfoClient[6];
            }
            if (Moyen == "ville")
            {
                command.CommandText = "select IDClients, nom, prenom , dateNaiss, adresse, depenses, email from Clients where ville = '" + InfoB + "'";

                reader = command.ExecuteReader();
                string InfoClient = "";
                while (reader.Read())       // parcours ligne par ligne
                {
                    InfoClient = "";
                    for (int i = 0; i < reader.FieldCount; i++)  //parcours cellule par cellule
                    {
                        string valeurattribut = reader.GetValue(i).ToString();
                        InfoClient += valeurattribut + ",";
                    }
                }
                string[] TabInfoClient = InfoClient.Split(',');
                IDClient = TabInfoClient[0];
                Nom = TabInfoClient[1];
                Prénom = TabInfoClient[2];
                dateNaiss = Convert.ToInt32(TabInfoClient[3]);
                adresse = TabInfoClient[4];
                ville = InfoB;
                depenses = Convert.ToInt32(TabInfoClient[5]);
                email = TabInfoClient[6];
            }
            if (Moyen == "dépense")
            {
                command.CommandText = "select IDClients, nom, prenom , dateNaiss, adresse, ville, email from Clients where depenses = " + InfoB;

                reader = command.ExecuteReader();
                string InfoClient = "";
                while (reader.Read())       // parcours ligne par ligne
                {
                    InfoClient = "";
                    for (int i = 0; i < reader.FieldCount; i++)  //parcours cellule par cellule
                    {
                        string valeurattribut = reader.GetValue(i).ToString();
                        InfoClient += valeurattribut + ",";
                    }
                }
                string[] TabInfoClient = InfoClient.Split(',');
                IDClient = TabInfoClient[0];
                Nom = TabInfoClient[1];
                Prénom = TabInfoClient[2];
                dateNaiss = Convert.ToInt32(TabInfoClient[3]);
                adresse = TabInfoClient[4];
                ville = TabInfoClient[5];
                depenses = Convert.ToInt32(InfoB);
                email = TabInfoClient[6];
            }
            if (Moyen == "email")
            {
                command.CommandText = "select IDClients, nom, prenom , dateNaiss, adresse, ville, depenses from Clients where email = '" + InfoB + "'";

                reader = command.ExecuteReader();
                string InfoClient = "";
                while (reader.Read())       // parcours
                {
                    InfoClient = "";
                    for (int i = 0; i < reader.FieldCount; i++)  //parcours cellule par cellule
                    {
                        string valeurattribut = reader.GetValue(i).ToString();
                        InfoClient += valeurattribut + ",";
                    }
                }
                string[] TabInfoClient = InfoClient.Split(',');
                IDClient = TabInfoClient[0];
                Nom = TabInfoClient[1];
                Prénom = TabInfoClient[2];
                dateNaiss = Convert.ToInt32(TabInfoClient[3]);
                adresse = TabInfoClient[4];
                ville = TabInfoClient[5];
                depenses = Convert.ToInt32(TabInfoClient[6]);
                email = InfoB;
            }
            maConnexion.Close();
            AffichageInfoClient(IDClient, Nom, Prénom, dateNaiss, adresse, ville, depenses, email);
        }
        static void AffichageInfoClient(string IDClient, string Nom, string Prénom, int DateNaiss, string Adresse, string Ville, int Dépenses, string Email)        // CA MARCHE
        {
            Console.Clear();
            Console.WriteLine("     Voici les informations du client :");
            Console.WriteLine("");
            Console.WriteLine(" IDClient : " + IDClient);
            Console.WriteLine(" Nom : " + Nom);
            Console.WriteLine(" Prénom : " + Prénom);
            Console.WriteLine(" DateNaiss : " + DateNaiss);
            Console.WriteLine(" Adresse : " + Adresse);
            Console.WriteLine(" Ville : " + Ville);
            Console.WriteLine(" Dépenses : " + Dépenses);
            Console.WriteLine(" Email : " + Email);
        }
        static string ConnexionClient()
        {
            Console.WriteLine(" Est-ce que le client a déjà un compte existant ?");
            string RéponseClient1 = OuiNon();
            string IDClient = "";
            while (IDClient == "")
            {
                if (RéponseClient1 == "oui")
                {
                    Console.WriteLine(" Avez-vous l'ID du client ?");
                    string RéponseClient2 = OuiNon();
                    if (RéponseClient2 == "oui")
                    {
                        Console.WriteLine(" Veuillez renseigner l'ID du client");
                        IDClient = Console.ReadLine();
                    }
                    else
                    {
                        InformationsClient();
                    }
                }
                else
                {
                    CréationClient();
                    RéponseClient1 = "oui";
                }
            }
            return IDClient;
        }


        //Livraison
        static void SelectionFournisseur (string IDProduit) //Donner le fournisseur responsable d'un article quelconque (y'a un pb d'IDFournisseur pour le moment)
        {

        }
        
        //Produit
        static void AjouterStock() //Ajouter du stock dans la base de donnée pour un produit
        {

        }
        static void InformationProduit() //Avoir les informations d'un produit
        {
            Console.WriteLine("Par quel moyen souhaitez-vous retrouver les informations du produit ? (IDproduit, IDfournisseur)");
            string Moyen = Console.ReadLine();
            Moyen = Moyen.ToLower();
            while (Moyen != "idproduit" && Moyen != "idfournisseur")
            {
                Console.WriteLine(" Veuillez renseigner un moyen valide s'il-vous-plaît : (IDproduit, IDfournisseur)");
                Moyen = Console.ReadLine();
                Moyen = Moyen.ToLower();
            }
            string InfoB = "";
            if (Moyen == "idproduit")
            {
                Console.WriteLine("Veuillez renseigner l'ID du produit :");
                InfoB = Console.ReadLine();
            }
            if (Moyen == "idfournisseur")
            {
                Console.WriteLine("Veuillez renseigner l'ID du fournisseur  :");
                InfoB = Console.ReadLine();
            }
            RetrouverInformationsProduit(Moyen, InfoB);
        }
       static void RetrouverInformationsProduit(string Moyen, string InfoB)
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = MATIbol78;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();
            string IDProduit = ""; string IDFournisseur = ""; int prix = 0; int stock = 0; string type = "";
            MySqlCommand command = maConnexion.CreateCommand();
            MySqlDataReader reader;
            if (Moyen == "idproduit")
            {
                command.CommandText = "select IDProduit , IDFournisseur, prix , stock, type from Produit where IDProduit = '" + InfoB + "'";

                reader = command.ExecuteReader();
                string InfoProduit = "";
                while (reader.Read())       // parcours ligne par ligne
                {
                    InfoProduit = "";
                    for (int i = 0; i < reader.FieldCount; i++)  //parcours cellule par cellule
                    {
                        string valeurattribut = reader.GetValue(i).ToString();
                        InfoProduit += valeurattribut + ",";
                    }
                }
                string[] TabInfoProduit = InfoProduit.Split(',');
                InfoProduit = InfoB;
                IDFournisseur = TabInfoProduit[1];
                prix = Convert.ToInt32(TabInfoProduit[2]);
                stock = Convert.ToInt32(TabInfoProduit[3]);
                type = TabInfoProduit[4];
               
            }
            if (Moyen == "idfournisseur")
            {
                command.CommandText = "select IDProduit , IDFournisseur, prix , stock, type from Produit where IDProduit = '" + InfoB + "'";

                reader = command.ExecuteReader();
                string InfoProduit = "";
                while (reader.Read())       // parcours ligne par ligne
                {
                    InfoProduit = "";
                    for (int i = 0; i < reader.FieldCount; i++)  //parcours cellule par cellule
                    {
                        string valeurattribut = reader.GetValue(i).ToString();
                        InfoProduit += valeurattribut + ",";
                    }
                }
                string[] TabInfoProduit = InfoProduit.Split(',');
                InfoProduit = InfoB;
                IDFournisseur = TabInfoProduit[1];
                prix = Convert.ToInt32(TabInfoProduit[2]);
                stock = Convert.ToInt32(TabInfoProduit[3]);
                type = TabInfoProduit[4];
            }
            AffichageInfoProduit(IDProduit, IDFournisseur, prix, stock, type);
        }
        static void AffichageInfoProduit(string IDProduit, string IDFournisseur, int prix, int stock, string type)
        {
            Console.Clear();
            Console.WriteLine("     Voici les informations du produit :");
            Console.WriteLine("");
            Console.WriteLine(" IDProduit : " + IDProduit);
            Console.WriteLine(" IDFournisseur : " + IDFournisseur);
            Console.WriteLine(" prix : " + prix);
            Console.WriteLine(" stock : " + stock);
            Console.WriteLine(" type : " + type);

        }
        //Autre
        static void MeilleurClient() //Faire des stats de meilleur vente etc. ?
        {
        
        }
        static void VérifCodePromo(string Code) //Faire une gestion des codes promo, on disait qu'on faisait une réduc de 100€ ou jsp quoi
        {
        
        }

        //Fonctions outils
        static bool ExistenceProduit(string IDProduit)                                                                                                              // CA MARCHE
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
        static string OuiNon()                                                                                                                                      // CA MARCHE
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
        static void SoustraireArticle(string IDProduit)                                                                                                             // CA MARCHE
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
