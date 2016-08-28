using Luadicrous.Framework.Interfaces;
using Luadicrous.Framework.Serialization;
using System;
using System.Linq;
using System.IO;
using System.Reflection;
using NLua.Event;
using System.Diagnostics;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Luadicrous.Framework
{
	public static class LuadicrousApplication
	{
        public static IViewFactory ViewFactory { get; private set; }

        public static ISourceManager SourceManager { get; private set; }

        public static Action<string[], object, DebugHookEventArgs> DebugHook;

		static LuadicrousApplication()
		{
            ApplicationDirectory = new DirectoryInfo(Environment.CurrentDirectory);

            DirectoryInfo bin = new DirectoryInfo(GetDirectory("./bin/"));

            Debug.Assert(bin.Exists, "Could not find /bin/ directory.");

            var controlFactory = new ControlFactory(bin.EnumerateFiles("*.dll").Select(file => Assembly.LoadFrom(file.FullName)));

            ViewFactory = new ViewFactory(controlFactory);

            SourceManager = new SourceManager();
            SourceManager.LoadAssets(ApplicationDirectory);
		}

        public static void Run()
		{
            SourceManager.GetScript(new FileInfo(GetDirectory("app.lua"))).Execute();
            Application.Run();
        }

		public static void Quit()
		{
            // Exit application

            throw new NotImplementedException();
        }

		public static DirectoryInfo ApplicationDirectory { get; set; }
		public static string GetDirectory(string source)
		{
			return Path.Combine(ApplicationDirectory.FullName, source);
		}

        public static FileInfo GetFileInfo(string value)
        {
            return new FileInfo(GetDirectory(value));
        }

        private static int windowCount;
        public static void Window(string source)
        {
            MetroForm form = new MetroForm();
            windowCount++;
            form.FormClosed += (sender, args) =>
            {
                if (windowCount <= 1)
                {
                    Application.Exit();
                }
                else
                {
                    windowCount--;
                }
            };
            View mainView = ViewFactory.CreateView(SourceManager.GetView(new FileInfo(GetDirectory(source))).ChildNodes.Item(1));
            form.Controls.Add(mainView.Control);
            form.Show();
        }
	}
}

