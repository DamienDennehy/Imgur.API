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
        private int _bufferSize = _defaultBufferSize;
        private Stream _content;
        private bool _contentConsumed;
        private IProgress<int> _progress;

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
            
            PrepareContent();
            
            using (_content)
            {
                var buffer = new Byte[_bufferSize];

                while (true)
                {
                    var bytesRead = await _content.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                    if (bytesRead <= 0) return;

                    await stream.WriteAsync(buffer, 0, bytesRead).ConfigureAwait(false);

                    _progress.Report(bytesRead);
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

        private void PrepareContent()
        {
            if (_contentConsumed)
            {
                // If the content needs to be written to a target stream a 2nd time, then the stream must support
                // seeking (e.g. a FileStream), otherwise the stream can't be copied a second time to a target 
                // stream (e.g. a NetworkStream).
                if (_content.CanSeek)
                {
                    _content.Position = 0;
                }
                else
                {
                    throw new InvalidOperationException("Can't set stream position.");
                }
            }

            _contentConsumed = true;
        }
    }
}
