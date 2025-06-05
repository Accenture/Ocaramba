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

        string folder = TestContext.CurrentContext.TestDirectory;


        


        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        [Test()]
        public void ShortenFileNameTest()
        {
            var name = "verylongfilename3 4 5 6 7 8 9 0 1 2 3 4 1 2 31 2 3 4 5 6 7 8 9 0 1 2 3 4 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0.txt";
            Logger.Debug("name:{0}",name);
            Assert.That((folder + name).Length, Is.GreaterThan(255));
            var text = NameHelper.ShortenFileName(folder, name, " ", 255);
            Logger.Debug("text:{0}", text);
            Assert.That((folder + text).Length, Is.EqualTo(255));
        }

        [Test()]
        public void RemoveSpecialCharactersTest()
        {
            var name = "name$%//4324 name ^//";
            name = NameHelper.RemoveSpecialCharacters(name);
            Assert.That(name, Is.EqualTo("name4324name"));
        }

    }
}