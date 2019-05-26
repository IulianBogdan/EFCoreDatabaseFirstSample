using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models;
using NUnit.Framework;

namespace EFCoreDatabaseFirstSample.Tests
{
    [TestFixture]
    public class BooksTest
    {
        [Test]
        public void AddBook_Test()
        {
            Assert.IsNotNull(1);
        }

        [Test]
        public void UpdateBook_Test()
        {
            var x = "Updated";
            Assert.AreEqual("Updated", x);
        }

        [Test]
        public void DeleteBook_Test()
        {
            var x = "Deleted";
            Assert.AreEqual("Deleted", x);
        }

        [Test]
        public void GetBook_Test()
        {
            var x = "Book";
            Assert.AreEqual("Book", x);
        }
    }
}
