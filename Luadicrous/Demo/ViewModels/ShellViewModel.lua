import 'Luadicrous.Framework.dll'
import 'Luadicrous.Framework'
import 'System'

vm = {}

vm.Text = BindableProperty()

vm.AcceptText = (function(args)
	Console.WriteLine(vm.Text:Get())	
end)

vm.ClearText = (function(args)
	vm.Text:Set("")
end)

return vm