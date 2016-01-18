using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace IISExpressMagic
{
    public class IISExpress : IDisposable
    {
        public static readonly List<string> KnownIISExpressLocations = new List<string>
        {
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)}\IIS Express\iisexpress.exe",
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\IIS Express\iisexpress.exe"
        };

        private readonly Process _process;
        private bool _isDisposed;

        public IISExpress(Arguments arguments)
            : this(arguments, GetIISExpressPath())
        {
        }

        public IISExpress(Arguments arguments, ProcessWindowStyle windowStyle)
            : this(arguments, GetIISExpressPath(), windowStyle)
        {
        }

        public IISExpress(Arguments arguments, string iisExpressPath)
            : this(arguments, iisExpressPath, ProcessWindowStyle.Hidden)
        {
        }

        public IISExpress(Arguments arguments, string iisExpressPath, ProcessWindowStyle windowStyle)
        {
            if (!File.Exists(iisExpressPath))
            {
                throw new ArgumentException("Cannot find IIS Express executable.", nameof(iisExpressPath));
            }

            var info = new ProcessStartInfo
            {
                FileName = iisExpressPath,
                Arguments = arguments.ToString(),
                WindowStyle = windowStyle
            };

            _process = Process.Start(info);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }

        private static string GetIISExpressPath()
        {
            var path = KnownIISExpressLocations.FirstOrDefault(File.Exists);

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new Exception("Cannot find IISExpress.exe.");
            }

            return path;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                _process.Kill();
                _process.Dispose();
            }
            _isDisposed = true;
        }
    }
}