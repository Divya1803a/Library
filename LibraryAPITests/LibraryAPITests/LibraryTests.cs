using System;
using NUnit.Framework;
using Newtonsoft.Json;
using LibraryAPITests.Models;
using System.Globalization;
using LibraryAPI.Services;


namespace LibraryAPITests
{
    [TestFixture]
    public class LibraryTests
    {
         private LibraryAPIHelper apiHelper;
        [SetUp]
        public void Setup()
        {
            apiHelper = new LibraryAPIHelper();
        }

        [Test]
        [TestCase("admin", "admin123")]
        [TestCase("user", "user123")]

        public void LoginTest(string userName, string password)
        {
            var svc = new UserService();
            var res = svc.Login(userName, password, out string token, out string errMsg);
            Assert.IsTrue(res);
            Assert.IsNotNull(token);
            Assert.AreEqual(errMsg, "");
        }

        [Test]
        [TestCase("admin", "a123")]
        [TestCase("user", "u123")]

        public void LoginFailTest(string userName, string password)
        {
            var svc = new UserService();
            var res = svc.Login(userName, password, out string token, out string errMsg);
            Assert.IsFalse(res);
            Assert.AreEqual(token, "");
            Assert.IsNotNull(errMsg);
        }



        [Test]
        public void GetLibrarysTest()
        {
            var library = apiHelper.GetBooksAsync().Result;
            Console.WriteLine(JsonConvert.SerializeObject(library));
            Assert.IsNotNull(library);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetLibraryTest(int BookID)
        {
            var Books = apiHelper.GetBooksAsync(BookID).Result;
            Assert.IsNotNull(BookID);
            Assert.IsNotNull(Books.BookID);
        }

        [Test]
        public void InsertLibraryTest()
        {
            var newBooks = new Books();
            //newBooks.BookID = 5;
            newBooks.BookName = "My Project";
            newBooks.BookPrice = 700;
            newBooks.CategoryID = 141;
            newBooks.AuthorsID = 1004;
           
            var insBooks = apiHelper.CreateBooksAsync(newBooks).Result;
            Assert.IsNotNull(insBooks);
            Assert.Greater(insBooks.BookID, 0);

            //Get Student and Validate
            //GetStudentTest(insStudent.StudentId);
            var books = apiHelper.GetBooksAsync(insBooks.BookID).Result;
            Assert.IsNotNull(books);
            Assert.IsNotNull(books.BookName);

            Assert.AreEqual(newBooks.BookName, books.BookName);

            //Updated Student
            var updBooks = new Books();
           // updBooks.BookID =7;
            updBooks.BookName = "Divya";
            updBooks.BookPrice = 900;
            updBooks.CategoryID = 141;
            updBooks.AuthorsID = 1004;
           
            updBooks.BookID = insBooks.BookID;

            var updatedLibrary = apiHelper.UpdateBooksAsync(updBooks).Result;
            Assert.IsNotNull(updatedLibrary);
            Assert.AreEqual(updatedLibrary.BookName, updBooks.BookName);

            books = apiHelper.GetBooksAsync(insBooks.BookID).Result;
            Assert.IsNotNull(books);

            //Delete Student
            var delBookID = apiHelper.DeleteBooksAsync(insBooks.BookID).Result;
            Assert.AreEqual(insBooks.BookID,delBookID);

            books = apiHelper.GetBooksAsync(insBooks.BookID).Result;
            Assert.IsNull(books);
        }
    }
}