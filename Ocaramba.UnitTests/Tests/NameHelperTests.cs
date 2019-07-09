using System.IO;
using NUnit.Framework;
using Ocaramba.Helpers;

namespace Ocaramba.UnitTests.Tests
{
    [TestFixture()]
    [TestFixture, Parallelizable(ParallelScope.Self)]
    public class NameHelperTests
    {
#if netcoreapp2_2
        string folder = Directory.GetCurrentDirectory();
#endif

#if net45
        string folder = TestContext.CurrentContext.TestDirectory;
#endif
        [Test()]
        public void ShortenFileNameTest()
        {
            var name = "verylongfilename3 4 5 6 7 8 9 0 1 2 3 4 1 2 31 2 3 4 5 6 7 8 9 0 1 2 3 4 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0.txt";
            var text = NameHelper.ShortenFileName(folder, name, "_", 255);
            Assert.IsTrue((folder + name).Length > 255);
            text = NameHelper.ShortenFileName(folder, name, " ", 255);
            Assert.AreEqual(255, (folder + text).Length);
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