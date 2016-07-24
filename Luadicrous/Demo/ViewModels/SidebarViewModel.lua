import 'Luadicrous.Framework.dll'
import 'Luadicrous.Framework'

vm = {}

vm.SetReverseMode = (function ()
    vm.Mode:Set("Reverse")
end)

vm.SetDoubleMode = (function ()
    vm.Mode:Set("Double")
end)

vm.Mode = BindableProperty("Reverse")

vm.Mode.OnSet = (function (arg)
    Events.GetChannel("SidebarMode"):Publish(arg)
end)

return vm