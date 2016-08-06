using System.Windows.Forms;

namespace Luadicrous.Framework
{
	public class Window : SingleItemContainer
	{
		private Form window;

		internal override object Widget
		{
			get { return window; }
			set { window = (Form)value; }
		}

		public Window(string title = null)
		{
			window = new Form();
            window.Size = new System.Drawing.Size(400, 400);
		}

		public Window Render()
		{
			window.Show();
            Application.Run(window);
			return this;
		}
	}
}

