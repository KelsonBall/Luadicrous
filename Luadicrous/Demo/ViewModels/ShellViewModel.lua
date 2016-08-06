import 'Luadicrous.Framework.dll'
import 'Luadicrous.Framework'
import 'System'

function ViewModel()
	local vm = {}

	vm.SelectedDate = BindableProperty()

	vm.AddDateToList = function()
		date = vm.SelectedDate:Get():ToShortDateString()
		data = { Name = date }
		vm.Items:Add(data)
	end

	vm.GoToToday = function()
		vm.SelectedDate:Set(DateTime.Now)
	end

	vm.Items = BindableCollection()	

	Events.GetChannel("ShellContentViewDeletionChannel"):Subscribe((function (arg) 
		vm.Items:Remove(arg)
	end))

	return vm
end