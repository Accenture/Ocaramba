using System.IO;
using NLog;
using NUnit.Framework;
using Ocaramba.Helpers;

namespace Ocaramba.UnitTests.Tests
{
    [TestFixture()]
    [TestFixture, Parallelizable(ParallelScope.Self)]
    public class NameHelperTests
    {
#if netcoreapp3_1
        string folder = Directory.GetCurrentDirectory();
#endif

#if net47
        string folder = TestContext.CurrentContext.TestDirectory;
#endif
#if net47
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
#endif
#if netcoreapp3_1
        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
#endif
        [Test()]
        public void ShortenFileNameTest()
        {
            var name = "verylongfilename3 4 5 6 7 8 9 0 1 2 3 4 1 2 31 2 3 4 5 6 7 8 9 0 1 2 3 4 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0.txt";
            Logger.Debug("name:{0}",name);
            Assert.IsTrue((folder + name).Length > 255);
            var text = NameHelper.ShortenFileName(folder, name, " ", 255);
            Logger.Debug("text:{0}", text);
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