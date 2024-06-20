using ClassLibrary10;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary10;
using ClassLibraryForHashTable;
using System.Threading;

namespace laba14
{
    internal class Program
    {
        static void PrintTaskList(List<Dictionary<string, List<BankCard>>> list)
        {
            foreach (var item in list)
            {
                foreach (var item2 in item)
                {
                    foreach (var item3 in item2.Value)
                    {
                        Console.WriteLine($"{item2.Key} - {item3.ToString()}");
                    }
                    Console.WriteLine();
                }
            }
        }

        static void Printtable(MyCollection<BankCard> table) 
        {
            table.PrintTable();
        }


        static public void AddObject<T, P>( P newItem, List<P> list) where T : P, IInit, new() where P : IInit, ICloneable, new()
        {
            newItem = new T();
            ((T)newItem).RandomInit();
            var milliseconds = 300;
            Thread.Sleep(milliseconds);
            list.Add(newItem);
        }

        static public void AddObject<T, P>( P newItem, MyCollection<P> table) where T : P, IInit, new() where P : IInit, ICloneable, IComparable, new()
        {
            newItem = new T();
            ((T)newItem).RandomInit();
            var milliseconds = 300;
            Thread.Sleep(milliseconds);
            table.Add(newItem);
        }

        static int ChooseCard()
        {
            int classChoice = 0;
            while (classChoice > 4 || classChoice < 1)
            {
                Console.WriteLine($"Выберите какой тип объекта вы хотите добавить в список:\n" +
                "1) Банковские карты\n" +
                "2) Дебетовые карты\n" +
                "3) Молодежные карты\n" +
                "4) Кредитные карты\n");
                Console.WriteLine("Введите номер объекта: ");
                classChoice = int.Parse(Console.ReadLine());

                if (classChoice <= 4 || classChoice >= 1);
                {
                    Console.WriteLine("Данный объект добавлен в список.");
                }

            }
            return classChoice;
        }
        //для нахождения номера больше 394059485
        static public IEnumerable<BankCard> FindNewCard(List<Dictionary<string, List<BankCard>>> collection, bool isLinq)
        {
            if (isLinq)
            {
                var res = from dict in collection
                          from item in dict
                          from card in item.Value
                          where card.Number > 394059485
                          select card;

                return res;
            }
            else
            {
                var pair = collection.SelectMany(x => x);
                var res = pair.SelectMany(x => x.Value);
                res = res.Where(x => x.Number > 394059485);

                return res;
            }
        }

        //static public IEnumerable<BankCard> FindNewCard(MyCollection<BankCard> collection, bool isLinq)
        //{
        //    if (isLinq)
        //    {
        //        var res = from card in collection
        //                  where card.Number < 839403847
        //                  select card;


        //        return res;
        //    }
        //    else
        //    {
        //        var res = collection.Where(x => x.Number < 839403847).Select(x => x);

        //        return res;
        //    }
        //}

        static public int FindOldestCard(List<Dictionary<string, List<BankCard>>> collection, bool isLinq)
        {
            if (isLinq)
            {
                var res = (from dict in collection
                           from item in dict
                           from card in item.Value
                           select card.Number).Min();

                return res;
            }
            else
            {
                var pair = collection.SelectMany(x => x); //Проецирует каждый элемент последовательности в объект IEnumerable<T> и объединяет результирующие последовательности в одну последовательность.
                var res = pair.SelectMany(x => x.Value);
                int min = res.Select(x => x.Number).Min();

                return min;
            }
        }

        static public IEnumerable<IGrouping<string, BankCard>> GroupCardByName(List<Dictionary<string, List<BankCard>>> collection, bool isLinq)
        {
            if (isLinq)
            {
                var res = from dict in collection
                          from item in dict
                          from card in item.Value
                          group card by card.Name;

                return res;
            }
            else
            {
                var pair = collection.SelectMany(x => x);
                var res = pair.SelectMany(x => x.Value);
                var groupedRes = res.GroupBy(x => x.Name);

                return groupedRes;
            }
        }

        //static public IEnumerable<IGrouping<string, BankCard>> GroupCardByName(MyCollection<BankCard> collection, bool isLinq)
        //{
        //    if (isLinq)
        //    {
        //        var res = from card in collection
        //                  group card by card.Name;

        //        return res;
        //    }
        //    else
        //    {
        //        var groupedCards = collection.Select(x => x).GroupBy(x => x.Name);

        //        return groupedCards;
        //    }
        //}

        static public IEnumerable<dynamic> CountAgeOfCard(List<Dictionary<string, List<BankCard>>> collection, bool isLinq)
        {
            if (isLinq)
            {
                var res = from dict in collection
                          from item in dict
                          from card in item.Value
                          where card is DebitCard
                          let age = 2024 - card.Number
                          select new { Id = card.id, Name = card.Name, Year = card.Term, Age = DateTime.Now };

                return res;
            }
            else
            {
                var pair = collection.SelectMany(x => x);
                var res = pair.SelectMany(x => x.Value);
                var age = res.Where(x => x is DebitCard)
                    .Select(x => new { Id = x.id, Name = x.Name, Year = x.Term, Age = DateTime.Now - x.Term });

                return age;
            }
        }
        //Находит объединение множеств, представленных двумя последовательностями.
        static public IEnumerable<BankCard> CardUnion(List<Dictionary<string, List<BankCard>>> collection, bool isLinq)
        {

            if (isLinq)
            {
                var debitCard = from dict in collection
                                 from item in dict
                                 from card in item.Value
                                 where card is DebitCard
                                 select card;
                var creditCard = from dict in collection
                                      from item in dict
                                      from card in item.Value
                                      where card is CreditCard
                                      select card;
                var cardUnion = debitCard.Union(creditCard);

                return cardUnion;
            }
            else
            {
                var pair = collection.SelectMany(x => x);
                var res = pair.SelectMany(x => x.Value);
                var debitcard = res.Where(x => x is DebitCard).Select(x => x);
                var creditcard = res.Where(x => x is CreditCard).Select(x => x);
                var cardUnion = debitcard.Union(creditcard);

                return cardUnion;
            }
        }

        static public IEnumerable<dynamic> CardJoinWorkshop(List<Dictionary<string, List<BankCard>>> collection, List<PopularBanks> listOfWorkshop, bool isLinq)
        {

            if (isLinq)
            {
                var joinedCard = from dict in collection
                                  from item in dict
                                  from card in item.Value
                                  join popular in listOfWorkshop on card.Name equals popular.Names
                                  select new
                                  {
                                      Id = card.id,
                                      NameOfCard = card.Name,
                                      Year = card.Term,
                                      Workshop = popular.NameOfBank
                                  };

                return joinedCard;
            }
            else
            {
                var pair = collection.SelectMany(x => x);
                var res = pair.SelectMany(x => x.Value);
                var joinedCard = res.Join(listOfWorkshop, card => card.Name, popular => popular.Names,
                    (card, popular) => new {
                        Id = card.id,
                        NameOfCard = card.Name,
                        Year = card.Term,
                        PopularBanks = popular.NameOfBank
                    });

                return joinedCard;
            }
        }

        //static public int CountCardWithBalance(MyCollection<BankCard> collection, bool isLinq)
        //{
        //    if (isLinq)
        //    {
        //        int res = (from card in collection
        //                   where card is DebitCard && ((DebitCard)card).Balance < 2000  
        //                   select card).Count();


        //        return res;
        //    }
        //    else
        //    {
        //        int res = collection.Where(x => x is DebitCard && ((DebitCard)x)
        //        .Balance < 2000)
        //        .Select(x => x).Count();

        //        return res;
        //    }
        //}

        //static public string[] MakeStatistics(MyCollection<BankCard> collection, bool isLinq)
        //{
        //    if (isLinq)
        //    {
        //        int min = (from card in collection
        //                   select card.Number).Min();
        //        int max = (from card in collection
        //                   select card.Number).Max();
        //        double avg = (from card in collection
        //                      select card.Number).Average();

        //        return new string[] { min.ToString(), max.ToString(), avg.ToString() };
        //    }
        //    else
        //    {
        //        int min = collection.Select(x => x.Number).Min();
        //        int max = collection.Select(x => x.Number).Max();
        //        double avg = collection.Select(x => x.Number).Average();

        //        return new string[] { min.ToString(), max.ToString(), avg.ToString() };
        //    }
        //}

        static void PrintRes(IEnumerable<dynamic> res, bool isLinq)
        {
            string message = isLinq ? "LINQ: " : "Методы расширения: ";
            Console.WriteLine($"{message}");
            foreach (var item in res)
            {
                Console.WriteLine($"{item.ToString()}");
            }

            if (res.Count() == 0)
            {
                Console.WriteLine("Невозможно выполнить запрос: запрашиваемые объекты не найдены.");
            }
            else
            {
                Console.WriteLine();
            }
        }

        static void PrintRes(IEnumerable<IGrouping<string, BankCard>> res, bool isLinq)
        {
            string message = isLinq ? "LINQ: " : "Методы расширения: ";
            Console.WriteLine($"{message}");
            foreach (var group in res)
            {
                Console.WriteLine($"Карта {group.Key}:");
                foreach (var item in group)
                {
                    Console.WriteLine($"{item.ToString()}");
                }
                Console.WriteLine($"Количество карт в группе = {group.Count()}");
            }

            if (res.Count() == 0)
            {
                Console.WriteLine("Невозможно выполнить запрос: запрашиваемые объекты не найдены.");
            }
            else
            {
                Console.WriteLine();
            }

        }
        
        public static MyCollection<BankCard> MakeHashCollection(MyCollection<BankCard> coll)
        {
            BankCard m1 = new BankCard();
            m1.RandomInit();
            var milliseconds = 300;
            Thread.Sleep(milliseconds);
            YoungCard m2 = new YoungCard();
            m2.RandomInit();
            var milliseconds1 = 300;
            Thread.Sleep(milliseconds1);
            DebitCard g1 = new DebitCard();
            g1.RandomInit();
            var milliseconds2 = 300;
            Thread.Sleep(milliseconds2);
            DebitCard g2 = new DebitCard();
            g2.RandomInit();
            var milliseconds3 = 300;
            Thread.Sleep(milliseconds3);
            DebitCard g3 = new DebitCard();
            g3.RandomInit();
            var milliseconds4 = 300;
            Thread.Sleep(milliseconds4);
            CreditCard e1 = new CreditCard();
            e1.RandomInit();
            var milliseconds5 = 300;
            Thread.Sleep(milliseconds5);

            coll = new MyCollection<BankCard>(m1,m2,g1,g2,g3,e1);

           
            return coll;
        }

        /// <summary>
        /// Выборка элементов с определенным номером с помощью методов расширений
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<BankCard> ChooseDataExp(MyCollection<BankCard> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пуста");

            var res = coll.Where(item => item.Number <= 688976547);

            if (res.Count() == 0)
                throw new Exception("Искомых элементов нет");

            return res;
        }

        /// <summary>
        /// Выборка элементов с определенным номером с помощью LINQ запросов
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<BankCard> ChooseDataLINQ(MyCollection<BankCard> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пуста");

            var res = from item in coll
                      where item.Number <= 688976547
                      select item;

            if (res.Count() == 0)
                throw new Exception("Таких элементов нет");

            return res;
        }

        /// <summary>
        /// Получение суммы балансов с помощью методов расширения
        /// </summary>
        /// <param name="coll"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int SumBalanceExp(MyCollection<BankCard> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пустая");

            var res = coll.Where(item => item is DebitCard).Sum(item => ((DebitCard)item).Balance);

            if (res == 0)
                throw new Exception("Таких элементов нет");

            return (int)res;
        }

        /// <summary>
        /// Получение суммы балансов с помощью LINQ запросов
        /// </summary>
        /// <param name="coll"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int SumBalanceLINQ(MyCollection<BankCard> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пустая");

            var res = (from item in coll
                       where item is DebitCard
                       select ((DebitCard)item).Balance).Sum();

            if (res == 0)
                throw new Exception("Таких элементов нет");

            return (int)res;
        }

        /// <summary>
        /// Группировака элементов коллекции по имени с помощью LINQ запросов
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<IGrouping<string, BankCard>> GroupNamesLINQ(MyCollection<BankCard> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пустая");

            var res = from item in coll
                      group item by item.Name;

            return res;
        }

        /// <summary>
        /// Группировака элементов коллекции по имени с помощью методов расширения
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<IGrouping<string, BankCard>> GroupNamesExp(MyCollection<BankCard> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пустая");

            var res = coll.GroupBy(item => item.Name);

            return res;
        }

        /// <summary>
        /// Получение количества элементов в каждой группе с помощью методов расширения
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
        public static void GetCountInGroupCardExp(MyCollection<BankCard> coll)
        {
            var res = coll.GroupBy(item => item.Name)
              .Select(grouped => new { Name = grouped.Key, Count = grouped.Count() });

            if (res.Count() == 0)
                throw new Exception("Таких элементов нет");

            foreach (var item in res)
                Console.WriteLine($"{item.Name} - {item.Count}");
        }

        /// <summary>
        /// Получение количества элементов в каждой группе с помощью LINQ запросов
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
        public static void GetCountInGroupCardLINQ(MyCollection<BankCard> coll)
        {
            var res = from item in coll
                      group item by item.Name
                      into grouped
                      select new { Name = grouped.Key, Count = grouped.Count() };

            if (res.Count() == 0)
                throw new Exception("Таких элементов нет");

            foreach (var item in res)
                Console.WriteLine($"{item.Name} - {item.Count}");
        }

        public static int ChooseOption(string msg)
        {
            int number;
            bool isConvert;
            do
            {
                Console.Write(msg);
                isConvert = int.TryParse(Console.ReadLine(), out number);
                if (!isConvert || number <= 0) Console.WriteLine("Неверный ввод. Попробуйте еще раз");
            } while (!isConvert || number <= 0);

            return number;
        }
        static void Main(string[] args)
        {
            int collectionChoice = -1;
            while (collectionChoice != 0)
            {
                Console.WriteLine("Выберите коллекцию для запросов:\n" +
                    "1) Коллекция из задания\n" +
                    "2) Коллекция из лабораторной работы 12 (АВЛ-дерево)\n" +
                    "0) Выйти\n");
                Console.WriteLine(("Введите номер действия:", 1));
       
                collectionChoice = int.Parse(Console.ReadLine());
                switch (collectionChoice)
                {
                    case 0:
                        Console.WriteLine("До свидания!");
                        break;
                    case 1:
                        List<Dictionary<string, List<BankCard>>> storeNet = new List<Dictionary<string, List<BankCard>>>();
                        List<PopularBanks> popbanks = new List<PopularBanks>();
                        Console.WriteLine("Введите количество отделений по клиентам (Всероссийский банк): ", "Данное значение может быть только целым неотрицательным числом.", 1);
                        int listSize = int.Parse(Console.ReadLine()); 
                        for (int i = 0; i < listSize; i++)
                        {
                            Console.WriteLine($"{i + 1} отедление: ");
                            Console.WriteLine("Введите количество карт в данном отделении: ", "Данное значение может быть только целым неотрицательным числом.", 1);
                            int cardCount = int.Parse(Console.ReadLine());
                            List<BankCard> listToAdd = new List<BankCard>();
                            for (int j = 0; j < cardCount; j++)
                            {
                                PopularBanks workshopToAdd = new PopularBanks();
                                workshopToAdd.RandomInit();
                                popbanks.Add(workshopToAdd);

                                Console.WriteLine($"{j + 1} карта: ");
                                int classChoice = ChooseCard();
                                BankCard newCard = null;
                                switch (classChoice)
                                {
                                    case 1:
                                        AddObject<BankCard, BankCard>( newCard, listToAdd);
                                        break;
                                    case 2:
                                        AddObject<DebitCard, BankCard>( newCard, listToAdd);
                                        break;
                                    case 3:
                                        AddObject<YoungCard, BankCard>(newCard, listToAdd);
                                        break;
                                    case 4:
                                        AddObject<CreditCard, BankCard>(newCard, listToAdd);
                                        break;
                                    default:
                                        Console.WriteLine("Данного номера объекта нет в меню.");
                                        break;
                                }
                            }
                            storeNet.Add(new Dictionary<string, List<BankCard>>() { { $"ВсеросБанк-{i + 1}", listToAdd } });
                        }

                        PrintTaskList(storeNet);

                        int queryChoice = -1;
                        while (queryChoice != 0)
                        {
                            Console.WriteLine("Выберите запрос:\n" +
                                              "1) Выбрать все карты, с номером > тут номер\n" +
                                              "2) Найти карты с минимальным годом выпуска\n" +
                                              "3) Группировка по имени\n" +
                                              "4) Определить возраст всех аналоговых картх\n" +
                                              "5) Объединение дебет и кредит\n" +
                                              "6) Вывести некоторые отделения, где карты по имени \n" +
                                              "0) Выйти\n");
                            Console.WriteLine("Введитеномер запроса", 1);
                          
                            queryChoice = int.Parse(Console.ReadLine());

                            switch (queryChoice)
                            {
                                case 0:
                                    break;
                                case 1:
                                    var resWithLinq = FindNewCard(storeNet, true);
                                    var resNoLinq = FindNewCard(storeNet, false);
                                    PrintRes(resWithLinq, true);
                                    PrintRes(resNoLinq, false);
                                    break;
                                case 2:
                                    if (listSize != 0)
                                    {
                                        int resWithLinq2 = FindOldestCard(storeNet, true);
                                        int resNoLinq2 = FindOldestCard(storeNet, false);
                                        Console.WriteLine("Наименьший год выпуска: ");
                                        Console.WriteLine($"LINQ: {resWithLinq2}\n");
                                        Console.WriteLine($"Методы расширения: {resNoLinq2}\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Невозможно выполнить запрос: запрашиваемые объекты не найдены.");
                                    }
                                    break;
                                case 3:
                                    var resWithLinq3 = GroupCardByName(storeNet, true);
                                    var resNoLinq3 = GroupCardByName(storeNet, false);
                                    PrintRes(resWithLinq3, true);
                                    PrintRes(resNoLinq3, false);
                                    break;
                                case 4:
                                    var resWithLinq4 = CountAgeOfCard(storeNet, true);
                                    var resNoLinq4 = CountAgeOfCard(storeNet, false);
                                    PrintRes(resWithLinq4, true);
                                    PrintRes(resNoLinq4, false);
                                    break;
                                case 5:
                                    var resWithLinq5 = CardUnion(storeNet, true);
                                    var resNoLinq5 = CardUnion(storeNet, false);
                                    PrintRes(resWithLinq5, true);
                                    PrintRes(resNoLinq5, false);
                                    break;
                                case 6:
                                    Console.WriteLine("Отделения: ");
                                    foreach (var item in popbanks)
                                    {
                                        Console.WriteLine($"{item.ToString()}");
                                    }
                                    var resWithLinq6 = CardJoinWorkshop(storeNet, popbanks, true);
                                    var resNoLinq6 = CardJoinWorkshop(storeNet, popbanks, false);
                                    PrintRes(resWithLinq6, true);
                                    PrintRes(resNoLinq6, false);
                                    break;
                                default:
                                    Console.WriteLine("Такого номера запроса нет в меню.");
                                    break;
                            }
                        }
                        break;
                    case 2:
                        {
                            MyCollection<BankCard> tableColleсt = new MyCollection<BankCard>();

                            int ans;
                            do
                            {
                                Console.WriteLine();
                                Console.WriteLine("1. Создание коллекции");
                                Console.WriteLine("2. Печать коллекции");
                                Console.WriteLine("3. Выборка данных: Выбрать элементы, у которых номер меньше или равно 688976547"); 
                                Console.WriteLine("4. Агрегирование:  Сумма балансов "); 
                                Console.WriteLine("5. Группироавка по имени и счетчик: "); 
                                Console.WriteLine("6. Назад");
                                Console.WriteLine();

                                ans = ChooseOption("Выберете пункт меню: ");
                                switch (ans)
                                {
                                    case 1: //Создание коллекции
                                        {
                                            tableColleсt = MakeHashCollection(tableColleсt);
                                            Console.WriteLine("Коллекция сформирована");
                                            break;
                                        }
                                    case 2: //Печать коллекции
                                        {
                                            Console.WriteLine(" === КОЛЛЕКЦИЯ === ");
                                            try
                                            {
                                                tableColleсt.PrintTable();
                                            }
                                            catch (Exception ex) { Console.WriteLine(ex.Message); }

                                            break;
                                        }
                                    case 3: // Выборка
                                        {
                                            Console.WriteLine(" === Элементы, у которых номер меньше или равно 688976547 ===");
                                            try
                                            {
                                                Console.WriteLine(" МЕТОДЫ РАСШИРЕНИЯ: ");
                                                var res = ChooseDataExp(tableColleсt);
                                                foreach (var item in res)
                                                {
                                                    Console.WriteLine(item);
                                                }

                                                Console.WriteLine("\n LINQ ЗАПРОСЫ : ");
                                                var res2 = ChooseDataLINQ(tableColleсt);
                                                foreach (var item in res2)
                                                {
                                                    Console.WriteLine(item);
                                                }
                                            }
                                            catch (Exception ex) { Console.WriteLine("Исключение: " + ex.Message); }

                                            break;
                                        }
                                    case 4: //Нахождение суммы 
                                        {
                                            try
                                            {
                                                Console.WriteLine(" МЕТОДЫ РАСШИРЕНИЯ: ");
                                                Console.WriteLine($"=== Сумма балансов: {SumBalanceExp(tableColleсt)} ===");
                                                Console.WriteLine("\n LINQ ЗАПРОСЫ : ");
                                                Console.WriteLine($"=== Сумма балансов: {SumBalanceLINQ(tableColleсt)} ===");
                                            }
                                            catch (Exception ex) { Console.WriteLine("Исключение: " + ex.Message); }
                                            break;
                                        }
                                    case 5: //Группировака и счетчик
                                        {
                                            Console.WriteLine("Группирвоака по имени");
                                            try
                                            {
                                                Console.WriteLine(" МЕТОДЫ РАСШИРЕНИЯ: ");
                                                var res = GroupNamesExp(tableColleсt);
                                                foreach (var name in res)
                                                {
                                                    Console.WriteLine(name.Key);
                                                    foreach (var item in name)
                                                    {
                                                        Console.WriteLine("  " + item);
                                                    }
                                                }

                                                Console.WriteLine("\n LINQ ЗАПРОСЫ : ");
                                                var res2 = GroupNamesLINQ(tableColleсt);
                                                foreach (var name in res2)
                                                {
                                                    Console.WriteLine(name.Key);
                                                    foreach (var item in name)
                                                    {
                                                        Console.WriteLine("  " + item);
                                                    }
                                                }

                                                Console.WriteLine($"\nКоличество элементов: ");

                                                Console.WriteLine(" МЕТОДЫ РАСШИРЕНИЯ: ");
                                                GetCountInGroupCardExp(tableColleсt);
                                                Console.WriteLine("\n LINQ ЗАПРОСЫ : ");
                                                GetCountInGroupCardLINQ(tableColleсt);

                                            }
                                            catch (Exception ex) { Console.WriteLine("Исключение: " + ex.Message); }

                                            break;
                                        }
                                }

                            } while (ans != 6);
                            break;
                        }
                        

                     
                    default:
                        Console.WriteLine("Данного номера коллекции нет в меню.");
                        break;
                }
            }
        }
    }
}
