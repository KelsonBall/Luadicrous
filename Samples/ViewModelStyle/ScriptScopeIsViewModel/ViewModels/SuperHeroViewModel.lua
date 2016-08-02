import 'Luadicrous.Framework.dll'
import 'Luadicrous.Framework'

function IncrementStars()
    Stars:Set(vm.Stars:Get() + 0.1)
end

function DecrementStars()
    vm.Stars:Set(vm.Stars:Get() - 0.1)
end