using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Imgur.API.Tests
{
    public class ProgressStreamContentTests
    {
        [Fact]
        public void ProgressStreamContent_WithStreamNull_ThrowsArgumentNullException()
        {
            var currentProgress = 0;
            int report(int progress) => currentProgress = progress;
            var byteProgress = new Progress<int>(percent => report(percent));

            var exception =
                Record.Exception(() =>
                new ProgressStreamContent(null,
                                          byteProgress,
                                          4096));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("content", argNullException.ParamName);
        }

        [Fact]
        public void ProgressStreamContent_WithProgressNull_ThrowsArgumentNullException()
        {
            using var ms = new MemoryStream(new byte[9]);

            var exception =
                Record.Exception(() =>
                new ProgressStreamContent(ms,
                                          null,
                                          4096));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("progress", argNullException.ParamName);
        }

        [Fact]
        public void ProgressStreamContent_WithBufferSizeNull_ThrowsArgumentNullException()
        {
            using var ms = new MemoryStream(new byte[9]);
            var currentProgress = 0;
            int report(int progress) => currentProgress = progress;
            var byteProgress = new Progress<int>(percent => report(percent));

            var exception =
                Record.Exception(() =>
                new ProgressStreamContent(ms,
                                          byteProgress,
                                          null));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("bufferSize", argNullException.ParamName);
        }
    }
}
