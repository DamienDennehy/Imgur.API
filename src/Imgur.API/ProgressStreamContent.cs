namespace Imgur.API
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    internal class ProgressStreamContent : HttpContent
    {
        private const int _defaultBufferSize = 4096;
        private readonly int _bufferSize = _defaultBufferSize;
        private readonly Stream _content;
        private readonly IProgress<int> _progress;

        public ProgressStreamContent(Stream content, IProgress<int> progress): this(content, progress, _defaultBufferSize)
        {
        }

        public ProgressStreamContent(Stream content, IProgress<int> progress, int bufferSize)
        {
            _bufferSize = bufferSize > 0 ? bufferSize : _defaultBufferSize;
            _content = content ?? throw new ArgumentNullException(nameof(content));
            _progress = progress ?? throw new ArgumentNullException(nameof(progress));
        }

        protected async override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            var buffer = new byte[_bufferSize];

            using (_content)
            {
                _progress.Report(0);

                while (true)
                {
                    var length = await _content.ReadAsync(buffer, 0, buffer.Length);
                    if (length <= 0)
                    {
                        break;
                    }

                    await stream.WriteAsync(buffer, 0, length);

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
