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
#if net6_0
        string folder = Directory.GetCurrentDirectory();
#endif

#if net47
        string folder = TestContext.CurrentContext.TestDirectory;
#endif
        [Test()]
        public void GetFilesOfGivenTypeFromAllSubFolderTest()
        {
            var files = FilesHelper.GetFilesOfGivenTypeFromAllSubFolders(folder,
                FileType.Xlsx);
            Assert.IsTrue(files.Count > 0);
        }

        [Test()]
        public void GetFilesOfGivenTypeFromAllSubFoldersTest()
        {
            var files = FilesHelper.GetFilesOfGivenTypeFromAllSubFolders(folder,
                FileType.Xml, "Driven");
            Assert.IsTrue(files.Count > 0);
        }

        [Test()]
        public void GetAllFilesFromAllSubFoldersTest()
        {
            var files = FilesHelper.GetAllFilesFromAllSubFolders(folder);
            Assert.IsTrue(files.Count > 0);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        [Test()]
        public void GetAllFilesFromAllSubFoldersPrefixTest()
        {
            var files = FilesHelper.GetAllFilesFromAllSubFolders(folder,
                "*.dll");
            Assert.IsTrue(files.Count > 0);
            File.Create(folder + FilesHelper.Separator + "testfile.txt");

        }

        [Test()]
        public void RenameDeleteFileTest()
        {
            string path = Path.Combine(folder, "testfile1.txt");
            File.Create(path).Close();
            path = Path.Combine(folder, "testfile2.txt");
            File.Create(path).Close();
            FilesHelper.RenameFile(BaseConfiguration.ShortTimeout, "testfile1.txt", "testfile2.txt",
                folder);
        }

        [Test()]
        public void CopyDeleteFileTest()
        {
            string path = Path.Combine(folder, "testfile3.txt");
            File.Create(path).Close();
            path = Path.Combine(folder, "testfile4.txt");
            File.Create(path).Close();
            FilesHelper.CopyFile(BaseConfiguration.ShortTimeout, "testfile3.txt", "testfile4.txt",
                folder);
        }

        [Test()]
        public void WaitForFileOfGivenNameExceptionTest()
        {
            var start = DateTime.Now;
            Assert.Throws<WaitTimeoutException>(() => FilesHelper.WaitForFileOfGivenName("nofile.txt", folder));
            var stop = DateTime.Now;
            Assert.True(stop - start <= TimeSpan.FromSeconds(BaseConfiguration.LongTimeout + 2));
        }
    }
}