using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.DBControllers;

namespace Project.Models
{
    public class Mand
    {
        public int ID { get; set; }
        public String Naam { get; set; }
        public int Aantal { get; set; }
        public double Prijs { get; set; }
    }
    public class Winkelmand
    {
        private List<Mand> winkelMand = new List<Mand>();

        private Boolean CheckBestaandProduct(int id)
        {
            for(int i = 0; i < winkelMand.Count; i++)
            {
                if (winkelMand[i].ID == id)
                {
                    winkelMand[i].Aantal++;
                    return true;
                }
            }
            
            return false;
        }

        public void Addwinkelmand(int id)
        {
            if (!CheckBestaandProduct(id))
            {
                ProductDBController prddbctrol = new ProductDBController();
                Product prd = prddbctrol.GetProduct(id);

                Mand mand = new Mand { ID = id, Aantal = 1, Naam = prd.Naam, Prijs = prd.Prijs };
                winkelMand.Add(mand);   
            }
        }

        

        public List<Mand> GetAllwinkelmand()
        {
            return winkelMand;
        }

        public Mand GetWinkelmandItem(int idx)
        {
            return winkelMand[idx];
        }

        public void WinkelmandItemPlus(int idx)
        {
            winkelMand[idx].Aantal++;
        }

        public void WinkelmandItemMin(int idx)
        {
            if (winkelMand[idx].Aantal > 1)
            {
                winkelMand[idx].Aantal--;
            }
        }

        public void Deletewinkelmand(int idx)
        {
            winkelMand.RemoveAt(idx);
        }

        public void DeleteAllwinkelmand()
        {
            winkelMand.Clear();
        }
    }
}
