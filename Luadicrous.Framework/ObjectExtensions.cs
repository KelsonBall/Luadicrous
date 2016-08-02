using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Luadicrous.Framework
{
    public static class ObjectExtensions
    {
        public static object Copy(this object content)
        {
            string json = JsonConvert.SerializeObject(content);
            return JsonConvert.DeserializeObject(json);
        }
    }
}
