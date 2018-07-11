using NUnit.Framework;
using Objectivity.Test.Automation.Common.Helpers;

namespace Objectivity.Test.Automation.UnitTests.Tests
{
    [TestFixture()]
    [TestFixture, Parallelizable(ParallelScope.Self)]
    public class NameHelperTests
    {
        [Test()]
        public void ShortenFileNameTest()
        {
            var name = "verylongfilename 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0.txt";
            var text = NameHelper.ShortenFileName(TestContext.CurrentContext.TestDirectory, name, "_", 255);
            Assert.IsTrue((TestContext.CurrentContext.TestDirectory + name).Length > 255);
            text = NameHelper.ShortenFileName(TestContext.CurrentContext.TestDirectory, name, " ", 255);
            Assert.AreEqual(255, (TestContext.CurrentContext.TestDirectory + text).Length);
        }

        [Test()]
        public void RemoveSpecialCharactersTest()
        {
            var name = "name$%//4324 name ^//";
            name = NameHelper.RemoveSpecialCharacters(name);
            Assert.AreEqual("name4324name", name);
        }

    }
}