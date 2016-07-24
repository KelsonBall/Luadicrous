import 'Luadicrous.Framework.dll'
import 'Luadicrous.Framework'

vm = {}

vm.Text = BindableProperty()

function reverse(arg)
	vm.ReversedText:Set(string.reverse(arg))
end

function double(arg)
	vm.ReversedText:Set(arg .. arg)
end

vm.Text.OnSet = reverse

Events.GetChannel("SidebarMode"):Subscribe((function (arg) 
	if arg == "Reverse" then
		vm.Text.OnSet = reverse
	else
		vm.Text.OnSet = double
	end
	vm.Text.OnSet(vm.Text:Get() or "")
end))

vm.ReversedText = BindableProperty()

vm.Clicked = (function()
	vm.Text:Set(vm.ReversedText:Get())
end)

return vm