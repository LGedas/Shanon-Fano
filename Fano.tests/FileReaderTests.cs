using Fano.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fano.tests
{
    public class FileReaderTests : IClassFixture<TestFileUtilities>
    {
        private readonly TestFileUtilities _testFileUtilities;
        public FileReaderTests(TestFileUtilities testFileUtilities)
        {
            _testFileUtilities = testFileUtilities;
        }

        [Fact]
        public void Constructor_FilePath_NotNull()
        {
            using (var file = new FileReader(TestFileUtilities.path))
            {
                Assert.NotNull(file);
            }
        }

        [Fact]
        public void Read_File_16Symbols()
        {
            byte[] expected = Encoding.UTF8.GetBytes("aaaabbbbccccddff");

            TestFileUtilities.MakeFile("aaaabbbbccccddff");

            using (var file = new FileReader(TestFileUtilities.path))
            {
                byte[] buffer = file.Read();

                Assert.True(expected.SequenceEqual(buffer));
            }
        }
    }
}
