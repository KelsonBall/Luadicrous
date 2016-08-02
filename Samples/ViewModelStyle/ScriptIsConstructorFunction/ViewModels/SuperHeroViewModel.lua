import 'Luadicrous.Framework.dll'
import 'Luadicrous.Framework'

function ViewModel(source)

    function IncrementStarsFunction()
        source.Stars:Set(vm.Stars:Get() + 0.1)
    end

    source.IncrementStars = IncrementStarsFunction

    function DecrementStarsFunction()
        source.Stars:Set(vm.Stars:Get() - 0.1)
    end

    source.DecrementStarsFunction = DecrementStarsFunction

    return source
end
