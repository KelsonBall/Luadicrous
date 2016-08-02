import 'Luadicrous.Framework.dll'
import 'Luadicrous.Framework'

vm = source or {}

function IncrementStarsFunction()
    vm.Stars:Set(vm.Stars:Get() + 0.1)
end

vm.IncrementStars = IncrementStarsFunction

function DecrementStarsFunction()
    vm.Stars:Set(vm.Stars:Get() - 0.1)
end

vm.DecrementStarsFunction = DecrementStarsFunction

return vm