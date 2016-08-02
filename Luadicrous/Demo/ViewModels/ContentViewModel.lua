import 'Luadicrous.Framework.dll'
import 'Luadicrous.Framework'

function ViewModel(key, model)    

    local vm = {}

    vm.Content = BindableProperty()

    vm.Content:Set(model.Name)

    vm.Delete = (function()
        Events.GetChannel("ShellContentViewDeletionChannel"):Publish(key)
    end)

    return vm
end