import 'Luadicrous.Framework.dll'
import 'Luadicrous.Framework'

window = Window("Hello from Lua")

shell = Component.LoadFromSource("Views/ShellView.xml");
window:AddChild(shell);

window:Render()