using System;
using System.IO;
using NUnit.Framework;
using Ocaramba.Exceptions;
using Ocaramba.Helpers;

namespace Ocaramba.UnitTests.Tests
{
    [TestFixture()]
    [TestFixture, Parallelizable(ParallelScope.Self)]
    public class FilesHelperTests
    {
        [Test()]
        public void GetFilesOfGivenTypeFromAllSubFolderTest()
        {
            var files = FilesHelper.GetFilesOfGivenTypeFromAllSubFolders(Directory.GetCurrentDirectory(),
                FileType.Xls);
            Assert.IsTrue(files.Count > 0);
        }

        [Test()]
        public void GetFilesOfGivenTypeFromAllSubFoldersTest()
        {
            var files = FilesHelper.GetFilesOfGivenTypeFromAllSubFolders(Directory.GetCurrentDirectory(),
                FileType.Xml, "Driven");
            Assert.IsTrue(files.Count > 0);
        }

        [Test()]
        public void GetAllFilesFromAllSubFoldersTest()
        {
            var files = FilesHelper.GetAllFilesFromAllSubFolders(Directory.GetCurrentDirectory());
            Assert.IsTrue(files.Count > 0);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        [Test()]
        public void GetAllFilesFromAllSubFoldersPrefixTest()
        {
            var files = FilesHelper.GetAllFilesFromAllSubFolders(Directory.GetCurrentDirectory(),
                "*.dll");
            Assert.IsTrue(files.Count > 0);
            File.Create(Directory.GetCurrentDirectory() + FilesHelper.Separator + "testfile.txt");

        }

        [Test()]
        public void RenameDeleteFileTest()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "testfile1.txt");
            File.Create(path).Close();
            path = Path.Combine(Directory.GetCurrentDirectory(), "testfile2.txt");
            File.Create(path).Close();
            FilesHelper.RenameFile(BaseConfiguration.ShortTimeout, "testfile1.txt", "testfile2.txt",
                Directory.GetCurrentDirectory());
        }

        [Test()]
        public void CopyDeleteFileTest()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "testfile3.txt");
            File.Create(path).Close();
            path = Path.Combine(Directory.GetCurrentDirectory(), "testfile4.txt");
            File.Create(path).Close();
            FilesHelper.CopyFile(BaseConfiguration.ShortTimeout, "testfile3.txt", "testfile4.txt",
                Directory.GetCurrentDirectory());
        }

        [Test()]
        public void WaitForFileOfGivenNameExceptionTest()
        {
            var start = DateTime.Now;
            Assert.Throws<WaitTimeoutException>(() => FilesHelper.WaitForFileOfGivenName("nofile.txt", Directory.GetCurrentDirectory()));
            var stop = DateTime.Now;
            Assert.True(stop - start <= TimeSpan.FromSeconds(BaseConfiguration.LongTimeout + 2));
        }
    }
}