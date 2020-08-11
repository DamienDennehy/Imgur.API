using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Imgur.API
{
    internal class ProgressStreamContent : HttpContent
    {
        private const int DefaultBufferSize = 4096;
        private readonly Stream _readStream;
        private readonly IProgress<int> _progress;
        private readonly CancellationToken _cancellationToken;
        internal readonly int _bufferSize = DefaultBufferSize;

        internal ProgressStreamContent(Stream content,
                                       IProgress<int> progress,
                                       CancellationToken cancellationToken = default) :
            this(content,
                 progress,
                 DefaultBufferSize,
                 cancellationToken)
        {
        }

        internal ProgressStreamContent(Stream content,
                                       IProgress<int> progress,
                                       int? bufferSize,
                                       CancellationToken cancellationToken = default)
        {
            _readStream = content ?? throw new ArgumentNullException(nameof(content));
            _progress = progress ?? throw new ArgumentNullException(nameof(progress));
            _bufferSize = bufferSize ?? throw new ArgumentNullException(nameof(bufferSize));
            _cancellationToken = cancellationToken;
        }

        protected override async Task SerializeToStreamAsync(Stream stream,
                                                             TransportContext context)
        {
            var buffer = new byte[_bufferSize];

            while (true)
            {
                var length = await _readStream.ReadAsync(buffer,
                                                         0,
                                                         buffer.Length,
                                                         _cancellationToken)
                                              .ConfigureAwait(false);

                if (length <= 0)
                {
                    break;
                }

                await stream.WriteAsync(buffer,
                                        0,
                                        length,
                                        _cancellationToken)
                            .ConfigureAwait(false);

                _progress.Report(length);
            }
        }

        protected override bool TryComputeLength(out long length)
        {
            length = _readStream.Length;
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _readStream.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
