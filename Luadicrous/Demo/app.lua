import 'Luadicrous.Framework.dll'
import 'Luadicrous.Framework'

window = Window("Hello from Lua")

shell = Control.LoadFromSource("Views/ShellView.xml");
window:AddChild(shell);

window:Render()
