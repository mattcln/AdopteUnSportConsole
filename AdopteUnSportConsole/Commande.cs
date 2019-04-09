using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AdopteUnSportConsole
{
    class Commande
    {
        string IDCommande;
        string numClient;
        int quantite;

        public Commande(string IDCommande, string numClient, int quantite)
        {
            this.IDCommande = IDCommande;
            this.numClient = numClient;
            this.quantite = quantite;
        }
    }
}
