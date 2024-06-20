using ClassLibrary10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary10;

namespace laba14
{
    public class PopularBanks : IInit
    {
        Random rnd = new Random();
        static protected string[] namesofpeople = new string[] { "Иван", "Ольга", "Мария", "Михаил", "Антон", "Елизавета", "Ольга", "Вера", "Артем", "Андрей" };
        static protected string[] namesofbanks = new string[] { "Сбербанк", "Тинькофф", "Альфабанк", "Газпромбанк", "Райффайзен", "ВТБ", "Открытие" };
        public string Names { get; set; }
        public string NameOfBank { get; set; }
        public PopularBanks(string names, string nameOfFactory)
        {
            Names = names;
            NameOfBank = nameOfFactory;
        }

        public PopularBanks()
        {
            Names = "Иван";
            NameOfBank = "Сбербанк";
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void RandomInit()
        {
            Names = namesofpeople[rnd.Next(namesofpeople.Length)];
            NameOfBank = namesofbanks[rnd.Next(namesofbanks.Length)];
        }

        public string Show()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"Имя {Names} находится в банке {NameOfBank}.";
        }
    }
}
