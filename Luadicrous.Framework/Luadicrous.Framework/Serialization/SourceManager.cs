using Luadicrous.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Luadicrous.Framework.Serialization
{
    public class SourceManager : ISourceManager
    {
        private Dictionary<FileInfo, XmlDocument> views = new Dictionary<FileInfo, XmlDocument>(new StrictFileEqualityComparer());

        private Dictionary<FileInfo, SourceAwareLuaScope> scripts = new Dictionary<FileInfo, SourceAwareLuaScope>(new StrictFileEqualityComparer());

        public XmlDocument GetView(FileInfo path)
        {
            return views[path];
        }

        public SourceAwareLuaScope GetScript(FileInfo path)
        {
            return scripts[path];
        }

        public void LoadAssets(DirectoryInfo root)
        {
            root.EnumerateFiles("*.xml")
                .Select(info =>
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(info.FullName);
                        return new Tuple<FileInfo, XmlDocument>(info, doc);
                    })
                .ToList()
                .ForEach(kvp => views.Add(kvp.Item1, kvp.Item2));

            root.EnumerateFiles("*.lua")
                .Select(info =>
                {
                    string source = File.ReadAllText(info.FullName);
                    return new Tuple<FileInfo, SourceAwareLuaScope>(info, new SourceAwareLuaScope(source));
                })
                .ToList()
                .ForEach(kvp => scripts.Add(kvp.Item1, kvp.Item2));

            root.EnumerateDirectories().ToList().ForEach(dir => LoadAssets(dir));
        }
    }
}
