import 'Luadicrous.Framework.dll'
import 'Luadicrous.Framework'

function ViewModel()
	local vm = {}

	vm.Items = BindableCollection()	

	vm.Items:Add({ Name = "Hello" })

	vm.ItemText = BindableProperty()

	vm.Add = (function ()
		vm.Items:Add({ Name = vm.ItemText:Get() })
	end)

	Events.GetChannel("ShellContentViewDeletionChannel"):Subscribe((function (arg) 
		vm.Items:Remove(arg)
	end))

	return vm
end