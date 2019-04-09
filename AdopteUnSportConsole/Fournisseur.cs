using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AdopteUnSportConsole
{
    class Fournisseur
    {
        string nom;
        string ville;

        public Fournisseur(string nom, string ville)
        {
            this.nom = nom;
            this.ville = ville;
        }
    }
}
