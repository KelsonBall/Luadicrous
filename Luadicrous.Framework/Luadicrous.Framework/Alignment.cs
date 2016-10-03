using System;
namespace Luadicrous.Framework
{
    [Flags]
    public enum Alignment
    {
        Center = 0,
        Left = 1 << 0,
        Right = 1 << 1,
        StretchHorizontal = Left | Right,
        Top = 1 << 2,
        Bottom = 1 << 3,
        StretchVertical = Top | Bottom,
        Fill = Left | Right | Top | Bottom
    }
}