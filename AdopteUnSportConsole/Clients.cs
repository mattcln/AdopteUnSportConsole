using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdopteUnSportConsole
{
    class Clients
    {
        string IDClients;
        string nom;
        string prénom;
        string dateNaiss;
        string addresse;
        string ville;
        double dépenses;
        string email;

        public Clients(string IDClients, string nom, string prénom, string dateNaiss, string addresse, string ville, double dépenses, string email)
        {
            this.IDClients = IDClients;
            this.nom = nom;
            this.prénom = prénom;
            this.dateNaiss = dateNaiss;
            this.addresse = addresse;
            this.ville = ville;
            this.dépenses = dépenses;
            this.email = email;
        }
    }
}
