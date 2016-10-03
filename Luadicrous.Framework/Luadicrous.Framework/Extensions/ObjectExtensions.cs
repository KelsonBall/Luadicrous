using Newtonsoft.Json;

namespace Luadicrous.Framework.Extensions
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
