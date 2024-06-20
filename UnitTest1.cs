using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Interfaces.Streaming;
using Microsoft.Analytics.Types.Sql;
using Microsoft.Analytics.UnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ClassLibrary10;
using ClassLibraryForHashTable;
using laba14;
using System.Net.NetworkInformation;



namespace TestsFor14
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        
        [TestMethod]
        public void ChooseHashCollIDExp()
        {
            MyCollection<BankCard> tableCollent = new MyCollection<BankCard>();
            tableCollent = Program.MakeHashCollection(tableCollent);
            var res = Program.ChooseDataExp(tableCollent);
            int count = 0;
            foreach (var item in tableCollent)
            {
                if (item.Number <= 30)
                { count++; }
            }
            Assert.AreEqual(count, res.Count());
        }
        [TestMethod]
        public void FindNewCard_Linq_ReturnsCorrectResult()
        {
            // Arrange
            var collection = new List<Dictionary<string, List<BankCard>>>()
        {
            new Dictionary<string, List<BankCard>>()
            {
                { "Key1", new List<BankCard> { new BankCard { Number = 395000000 }, new BankCard { Number = 394100000 } } }
            }
        };

            // Act
            var result = Program.FindNewCard(collection, isLinq: true);

            // Assert
            Assert.AreEqual(395000000, result.First().Number);
        }

        [TestMethod]
        public void FindOldestCard_NotLinq_ReturnsCorrectResult()
        {
            // Arrange
            var collection = new List<Dictionary<string, List<BankCard>>>()
        {
            new Dictionary<string, List<BankCard>>()
            {
                { "Key1", new List<BankCard> { new BankCard { Number = 395000000 }, new BankCard { Number = 394100000 } } }
            }
        };

            // Act
            var result = Program.FindOldestCard(collection, isLinq: false);

            // Assert
            Assert.AreEqual(394100000, result);
        }
        [TestMethod]
        public void ChooseHashCollIDExpEmpy()
        {
            try
            {
                MyCollection<BankCard> tableColleñt = new MyCollection<BankCard>();
                var res = Program.ChooseDataExp(tableColleñt);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Êîëëåêöèÿ ïóñòà", ex.Message);
            }
        }

        [TestMethod]
        public void ChooseHashCollIDExpNoResult()
        {
            try
            {
                MyCollection<BankCard> tableColleñt = new MyCollection<BankCard>();
                BankCard m1 = new BankCard();
                YoungCard g1 = new YoungCard();
                CreditCard e1 = new CreditCard();
                DebitCard p1 = new DebitCard();
                tableColleñt = new MyCollection<BankCard>(m1, g1, e1, p1);
                var res = Program.ChooseDataExp(tableColleñt);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Èñêîìûõ ýëåìåíòîâ íåò", ex.Message);
            }
        }

        [TestMethod]
        public void ChooseHashCollIDLINQ()
        {
            MyCollection<BankCard> tableColleñt = new MyCollection<BankCard>();
            tableColleñt = Program.MakeHashCollection(tableColleñt);
            var res = Program.ChooseDataLINQ(tableColleñt);
            int count = 0;
            foreach (var item in tableColleñt)
            {
                if (item.Number <= 306374839)
                { count++; }
            }
            Assert.AreEqual(count, res.Count());
        }

        [TestMethod]
        public void ChooseHashCollIDLINQEmpy()
        {
            try
            {
                MyCollection<BankCard> tableColleñt = new MyCollection<BankCard>();
                var res = Program.ChooseDataLINQ(tableColleñt);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Êîëëåêöèÿ ïóñòà", ex.Message);
            }
        }

       

        [TestMethod]
        public void SumBalancerExp()
        {
            MyCollection<BankCard> tableColleñt = new MyCollection<BankCard>();
            tableColleñt = Program.MakeHashCollection(tableColleñt);

            int sum = 0;

            foreach (var item in tableColleñt)
            {
                if (item is DebitCard)
                    sum += ((DebitCard)item).Number;
            }

            int res = Program.SumBalanceExp(tableColleñt);
            Assert.AreEqual(sum, res);
        }

       
        [TestMethod]
        public void SumbalanceExpNoResult()
        {
            MyCollection<BankCard> tableColleñt = new MyCollection<BankCard>();
            BankCard m1 = new BankCard();
            BankCard m2 = new BankCard();
           DebitCard p1 = new DebitCard();
            tableColleñt = new MyCollection<BankCard>(m1, m2, p1);

            try
            {
                int res = Program.SumBalanceExp(tableColleñt);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Òàêèõ ýëåìåíòîâ íåò", ex.Message);
            }
        }

        [TestMethod]
        public void SumbaalnceLINQ()
        {
            MyCollection<BankCard> tableColleñt = new MyCollection<BankCard>();
            tableColleñt = Program.MakeHashCollection(tableColleñt);

            int sum = 0;

            foreach (var item in tableColleñt)
            {
                if (item is DebitCard)
                    sum += ((DebitCard)item).Number;
            }

            int res = Program.SumBalanceLINQ(tableColleñt);
            Assert.AreEqual(sum, res);
        }

        [TestMethod]
        public void SumBalanceLINQEmptyCollection()
        {
            MyCollection<BankCard> tableColleñt = new MyCollection<BankCard>();
            try
            {
                int res = Program.SumBalanceLINQ(tableColleñt);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Êîëëåêöèÿ ïóñòàÿ", ex.Message);
            }
        }

        [TestMethod]
        public void SumbalanceLINQNoResult()
        {
            MyCollection<BankCard> tableColleñt = new MyCollection<BankCard>();
            BankCard m1 = new BankCard();
            BankCard m2 = new BankCard();
            DebitCard p1 = new DebitCard();
            tableColleñt = new MyCollection<BankCard>(m1, m2, p1);

            try
            {
                int res = Program.SumBalanceLINQ(tableColleñt);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Òàêèõ ýëåìåíòîâ íåò", ex.Message);
            }
        }

        [TestMethod]
        public void GroupnaMESLINQ()
        {
            MyCollection<BankCard> tableColleñt = new MyCollection<BankCard>();
            tableColleñt = Program.MakeHashCollection(tableColleñt);
            var res = Program.GroupNamesLINQ(tableColleñt);
            int countitem = 0;
            foreach (var name in res)
            {
                foreach (var item in name)
                {
                    countitem++;
                }
            }

            Assert.AreEqual(countitem, tableColleñt.Count);
        }

        [TestMethod]
        public void GroupNAMESsExp()
        {
            MyCollection<BankCard> tableColleñt = new MyCollection<BankCard>();
            tableColleñt = Program.MakeHashCollection(tableColleñt);
            var res = Program.GroupNamesExp(tableColleñt);
            int countitem = 0;
            foreach (var name in res)
            {
                foreach (var item in name)
                {
                    countitem++;
                }
            }

            Assert.AreEqual(countitem, tableColleñt.Count);
        }

        [TestMethod]
        public void GrouNamesExpEmpty()
        {
            MyCollection<BankCard> tableColleñt = new MyCollection<BankCard>();
            try
            {
                Program.GroupNamesExp(tableColleñt);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Êîëëåêöèÿ ïóñòàÿ", ex.Message);
            }
        }

        [TestMethod]
        public void GroupNamesLINQEmpty()
        {
            MyCollection<BankCard> tableColleñt = new MyCollection<BankCard>();
            try
            {
                Program.GroupNamesLINQ(tableColleñt);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Êîëëåêöèÿ ïóñòàÿ", ex.Message);
            }

        }
        [TestMethod]
        public void CountAgeOfCard_Linq_ReturnsCorrectResult()
        {
            // Arrange
            List<Dictionary<string, List<BankCard>>> collection = new List<Dictionary<string, List<BankCard>>>
        {
            new Dictionary<string, List<BankCard>>
            {
                { "key1", new List<BankCard>{ new DebitCard () } }
            }
        };

            bool isLinq = true;

            // Act
            var result = Program.CountAgeOfCard(collection, isLinq);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            // Add more specific assertions based on the expected outcome
        }

        [TestMethod]
        public void CardUnion_Linq_ReturnsCorrectResult()
        {
            // Arrange
            List<Dictionary<string, List<BankCard>>> collection = new List<Dictionary<string, List<BankCard>>>
            {
            new Dictionary<string, List<BankCard>>
            {
                { "key1", new List<BankCard>{ new DebitCard () } }
            }
        };

            bool isLinq = true;

            // Act
            var result = Program.CardUnion(collection, isLinq);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            // Add more specific assertions based on the expected outcome
        }
    }
}
