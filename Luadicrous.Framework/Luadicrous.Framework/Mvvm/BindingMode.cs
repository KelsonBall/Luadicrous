using System;

namespace Luadicrous.Framework
{
    [Flags]
    public enum BindingMode
	{
        FromView = 1,
        FromViewModel = 2,
        TwoWay = 3,
    }
}

