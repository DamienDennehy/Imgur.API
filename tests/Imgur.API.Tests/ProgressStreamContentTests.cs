using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            var progress = new Progress<int>(percent => report(percent));

            var exception =
                Record.Exception(() =>
                new ProgressStreamContent(null,
                                          progress,
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
            var progress = new Progress<int>(percent => report(percent));

            var exception =
                Record.Exception(() =>
                new ProgressStreamContent(ms,
                                          progress,
                                          null));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("bufferSize", argNullException.ParamName);
        }

        [Fact]
        public void ProgressStreamContent_ConstructorWithDefaultBufferSize_ShouldMatch()
        {
            var inputBytes = new byte[5242880];

            var random = new Random();
            random.NextBytes(inputBytes);

            var currentProgress = 0;
            int report(int progress) => currentProgress += progress;
            var progress = new Progress<int>(percent => report(percent));
            var cancellationToken = new CancellationTokenSource().Token;

            using var inputMs = new MemoryStream(inputBytes);
            using var progressStreamContent = new ProgressStreamContent(inputMs, progress, cancellationToken);

            Assert.Same(progressStreamContent._readStream, inputMs);
            Assert.Same(progressStreamContent._progress, progress);
            Assert.Equal(progressStreamContent._bufferSize, ProgressStreamContent.DefaultBufferSize);
            Assert.Equal(progressStreamContent._cancellationToken, cancellationToken);
        }

        [Fact]
        public void ProgressStreamContent_ConstructorWithCustomBufferSize_ShouldMatch()
        {
            var inputBytes = new byte[5242880];

            var random = new Random();
            random.NextBytes(inputBytes);

            var currentProgress = 0;
            int report(int progress) => currentProgress += progress;
            var progress = new Progress<int>(percent => report(percent));
            var cancellationToken = new CancellationTokenSource().Token;

            using var inputMs = new MemoryStream(inputBytes);
            using var progressStreamContent = new ProgressStreamContent(inputMs, progress, 9999, cancellationToken);

            Assert.Same(inputMs, progressStreamContent._readStream);
            Assert.Same(progress, progressStreamContent._progress);
            Assert.Equal(9999, progressStreamContent._bufferSize);
            Assert.Equal(cancellationToken, progressStreamContent._cancellationToken);
        }

        [Fact]
        public async Task ProgressStreamContent_5MB_File_Matches()
        {
            var inputBytes = new byte[5242880];
            var outputBytes = new byte[5242880];

            var random = new Random();
            random.NextBytes(inputBytes);

            var currentProgress = 0;
            int report(int progress) => currentProgress += progress;
            var progress = new Progress<int>(percent => report(percent));

            using var inputMs = new MemoryStream(inputBytes);
            using var progressStreamContent = new ProgressStreamContent(inputMs, progress, 4096);
            using var outputMs = new MemoryStream(outputBytes);
            await progressStreamContent.CopyToAsync(outputMs);

            inputMs.Position = 0;
            outputMs.Position = 0;

            var inputArray = inputMs.ToArray();
            var outputArray = outputMs.ToArray();

            Assert.True(inputArray.SequenceEqual(outputArray));
        }
    }
}
