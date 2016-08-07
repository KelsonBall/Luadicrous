import 'Luadicrous.Framework.dll'
import 'Luadicrous.Framework'

function ViewModel()
	local vm = {}

	function setCoordinate()
		coord = tostring( vm.VerticalPosition:Get() ) .. ", " .. tostring( vm.HorizontalPosition:Get() )
		vm.Coordinate:Set( coord )
	end

	vm.Coordinate = BindableProperty()

	vm.Items = BindableCollection()	
	
	vm.AddToList = function()		
		data = { Name = vm.Coordinate:Get() }
		vm.Items:Add(data)
	end

	Events.GetChannel("ShellContentViewDeletionChannel"):Subscribe((function (arg) 
		vm.Items:Remove(arg)
	end))

	vm.YValue = BindableProperty()

	vm.VerticalPosition = BindableProperty()

	vm.YValue.OnSet  = function ()
		value = vm.YValue:Get()
		value = (50 * value)
		vm.VerticalPosition:Set(value)
		setCoordinate()
	end

	vm.XValue = BindableProperty()

	vm.HorizontalPosition = BindableProperty()

	vm.XValue.OnSet = function()
		value = vm.XValue:Get()
		value = (100 * value)
		vm.HorizontalPosition:Set(value)
		setCoordinate()
	end		

	return vm
end