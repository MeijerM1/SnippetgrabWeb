using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnippetgrabClasslibrary.Models;

namespace UserUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConstructor()
        {
            string name = "Max";
            string email = "test@test.nl";
            DateTime joinDate = DateTime.Today;
            int reputation = 10;
            bool isAdmin = false;
            List<int> tags = new List<int>();


            User testUser = new User(name, joinDate, reputation, email, isAdmin,tags);

            Assert.AreEqual(name, testUser.Name);
            Assert.AreEqual(email, testUser.Email);
            Assert.AreEqual(joinDate, testUser.JoinDate);
            Assert.AreEqual(reputation, testUser.Reputation);
            Assert.AreEqual(isAdmin, testUser.IsAdmin);
            Assert.AreEqual(tags, testUser.Tags);

        }
    }
}
