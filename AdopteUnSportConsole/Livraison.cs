using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdopteUnSportConsole
{
    class Livraison
    {
        int numCommande;
        string idClient;
        string nomC;
        string prenomC;
        string addresseC;
        string villeC;

        public Livraison(int numCommande, string idClient, string nomC, string prenomC, string addresseC, string villeC)
        {
            this.numCommande = numCommande;
            this.idClient = idClient;
            this.nomC = nomC;
            this.prenomC = prenomC;
            this.addresseC = addresseC;
            this.villeC = villeC;
        }
    }
}
