import 'Luadicrous.Framework.dll'
import 'Luadicrous.Framework'

vm = {}

vm.SuperHeros = BindableProperty()

function Load()
	-- In real life, load from a file instead of creating a table
	csvData = { 
		{ Name = BindableProperty("Batman", Stars = BindableProperty(4.7) },
		{ Name = BindableProperty("Superman", Stars = BindableProperty(3.5) },
		{ Name = BindableProperty("Ant Man"), Stars = BindableProperty(9001) }
	}

	vm.SuperHeros:Set(csvData)
end

vm.LoadFromCsv = Load

function Save()
	csvData = SuperHeros:Get()

	-- In real life, save data to csv file
end

vm.SaveToCsv = Save

return vm