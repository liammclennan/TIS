using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodayIShall.Test
{
    public class Builder
    {
        private Dictionary<Type, Func<object>> defaults;

        public void Configure(Dictionary<Type, Func<object>> defaults)
        {
            this.defaults = defaults;
        }

        public T A<T>()
        {
            if (!defaults.ContainsKey(typeof(T))) throw new ArgumentException("No object of type " + typeof(T).Name + " has been configured with the builder.");
            T o = (T)defaults[typeof(T)]();
            return o;
        }

        public T A<T>(Action<T> customisation)
        {
            T o = A<T>();
            customisation(o);
            return o;
        }

        public T An<T>()
        {
            return A<T>();
        }

        public T An<T>(Action<T> customisation)
        {
            return An<T>(customisation);
        }
    }
}
