using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Luadicrous.Framework
{    
    public class BindableCollection
    {
        internal IDictionary<string, object> Contents = new ExpandoObject();        
        
        public Action<string, dynamic> OnAdded;
        public Action<string, dynamic> OnRemoved;

        private HashSet<int> usedIntegerKeys = new HashSet<int>();

        public dynamic this[string key]
        {
            get
            {                                
                return Contents[key];                                    
            }            
        }        

        public BindableCollection Add(object obj)
        {
            int numericKey = Enumerable.Range(1, int.MaxValue)
                                .Except(usedIntegerKeys)
                                .First();
            usedIntegerKeys.Add(numericKey);
            string key = numericKey.ToString();
            Contents.Add(key, obj);
            OnAdded?.Invoke(key, obj);
            return this;
        }

        public BindableCollection Remove(string key)
        {
            int numericKey;
            bool isNumeric = int.TryParse(key, out numericKey);
            if (isNumeric)
            {
                usedIntegerKeys.Remove(numericKey);
            }
            object item = Contents[key];
            Contents.Remove(key);
            OnRemoved?.Invoke(key, item);
            return this;
        }
        
    }
}
