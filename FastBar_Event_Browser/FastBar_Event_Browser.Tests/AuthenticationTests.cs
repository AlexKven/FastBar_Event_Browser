using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace FastBar_Event_Browser.Tests
{
    //Tests all are dependent on the following known good login information:
    //email:    demo@getfastbar.com
    //password: password
    [TestClass]
    public class AuthenticationTests
    {
        [TestMethod]
        public async Task SuccessfulLogin()
        {
            string token = await APIManager.GetToken("demo@getfastbar.com", "password");
            if (token == null)
                Assert.Fail("Correct login information didn't yield a token.");
        }

        [TestMethod]
        public async Task UnsuccessfulLogin()
        {
            string token = await APIManager.GetToken("demo@getfoobar.com", "password");
            if (token != null)
                Assert.Fail("Incorrect login information yielded a token.");
        }

        [TestMethod]
        public async Task SuccessfulRetrievalOfEvents()
        {
            string token = await APIManager.GetToken("demo@getfastbar.com", "password");
            var events = await APIManager.GetEvents(token);
            if (events == null)
                Assert.Fail("Correct login information didn't yield event entries.");
        }

        [TestMethod]
        public async Task UnsuccessfulRetrievalOfEvents()
        {
            string token = await APIManager.GetToken("demo@getfoobar.com", "password");
            var events = await APIManager.GetEvents(token);
            if (events != null)
                Assert.Fail("Incorrect login information yielded event entries.");
        }

        [TestMethod]
        public void Cipher()
        {
            //Generate a random token of random length
            Random rnd = new Random();
            int length = rnd.Next(200, 500);
            byte[] tokenBytes = new byte[length];
            rnd.NextBytes(tokenBytes);
            string token = new string((from bte in tokenBytes select (char)bte).ToArray());
            //Run the token through the XOR cipher twice, which should yield the same token again
            if (token.CompareTo(APIManager.CipherToken(APIManager.CipherToken(token))) != 0)
                Assert.Fail("Token wasn't deciphered correctly.");
        }

        //[TestMethod]
        //public async Task Debug()
        //{
        //}
    }
}
