using System.IO;
using System.Xml;

namespace Luadicrous.Framework.Interfaces
{
    public interface ISourceManager
    {
        void LoadAssets(DirectoryInfo root);
        XmlDocument GetView(FileInfo path);
        SourceAwareLuaScope GetScript(FileInfo path);
    }
}