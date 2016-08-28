using System;
using System.Collections.Generic;
using System.IO;

namespace Luadicrous.Framework.Serialization
{
    internal class StrictFileEqualityComparer : IEqualityComparer<FileInfo>
    {
        public bool Equals(FileInfo x, FileInfo y)
        {
            if (x.Exists && y.Exists)
            {
                return Path.GetFullPath(x.FullName).Equals(Path.GetFullPath(y.FullName), StringComparison.OrdinalIgnoreCase);
            }
            throw new FileNotFoundException("Attempted to compare a file that does not exist.");
        }

        public int GetHashCode(FileInfo obj)
        {
            if (obj.Exists)
            {
                return Path.GetFullPath(obj.FullName).ToUpperInvariant().GetHashCode();
            }
            throw new FileNotFoundException("Attempt to compare a file that does not exist");
        }
    }
}
