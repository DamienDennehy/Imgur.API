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
        private readonly int _bufferSize = DefaultBufferSize;
        private readonly Stream _content;
        private readonly IProgress<int> _progress;
        private readonly CancellationToken _cancellationToken;

        internal ProgressStreamContent(Stream content,
                                       IProgress<int> progress) : this(content,
                                                                       progress,
                                                                       DefaultBufferSize)
        {
        }

        internal ProgressStreamContent(Stream content,
                                       IProgress<int> progress,
                                       int? bufferSize,
                                       CancellationToken cancellationToken = default)
        {
            _content = content ?? throw new ArgumentNullException(nameof(content));
            _progress = progress ?? throw new ArgumentNullException(nameof(progress));
            _bufferSize = bufferSize ?? throw new ArgumentNullException(nameof(bufferSize));
            _cancellationToken = cancellationToken;
        }

        internal Task SerializeToProgressStreamAsync(Stream stream,
                                                     TransportContext context)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return SerializeToStreamAsync(stream, context);
        }

        protected override async Task SerializeToStreamAsync(Stream stream,
                                                             TransportContext context)
        {
            var buffer = new byte[_bufferSize];

            using (_content)
            {
                _progress.Report(0);

                while (true)
                {
                    var length = await _content.ReadAsync(buffer,
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
        }

        protected override bool TryComputeLength(out long length)
        {
            length = _content.Length;
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _content.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
