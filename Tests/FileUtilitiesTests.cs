using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFS;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class FileUtilitiesTests
    {
        [Test]
        public void GetType_CssFilePathGiven_cssReturned()
        {
            var extension = FileUtilities.GetType("/some/path/file.css");
            Assert.AreEqual("css", extension);
        }

        [Test]
        public void GetType_pagePathGiven_nullReturned()
        {
            var extension = FileUtilities.GetType("/some/path/page");
            Assert.AreEqual(null, extension);
        }
    }
}
