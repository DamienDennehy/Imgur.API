﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Imgur.API
{
    internal class ProgressStreamContent : HttpContent
    {
        internal bool _disposed;
        internal const int DefaultBufferSize = 4096;
        internal Stream _readStream;
        internal readonly IProgress<int> _progress;
        internal readonly CancellationToken _cancellationToken;
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
                _disposed = true;
            }

            _readStream = null;

            base.Dispose(disposing);
        }
    }
}
