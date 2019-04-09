using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdopteUnSportConsole
{
    class Produit
    {
        string IDProduit;
        int IDFournisseur;
        int prix;
        int stock;
        string type;

        public Produit(string IDProduit, int IDFournisseur, int prix, int stock, string type)
        {
            this.IDProduit = IDProduit;
            this.IDFournisseur = IDFournisseur;
            this.prix = prix;
            this.stock = stock;
            this.type = type;
        }
    }
}
