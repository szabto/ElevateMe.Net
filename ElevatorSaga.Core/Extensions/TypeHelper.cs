using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;

namespace ElevatorSaga.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class DllLoadHelper
    {
        private static readonly object loaderLock = new object();

        private static readonly Dictionary<string, Type> typeCache = new Dictionary<string, Type>();
        private static readonly Dictionary<string, Assembly> asmCache = new Dictionary<string, Assembly>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static Assembly LoadAssembly(this string assemblyName)
        {
            lock (loaderLock)
            {
                //assemblyName = assemblyName.ToLower();
                if (asmCache.ContainsKey(assemblyName)) { return asmCache[assemblyName]; }
                return asmCache[assemblyName] = Assembly.LoadFrom(assemblyName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public static Type LoadType(this string className)
        {
            lock (loaderLock)
            {
                string asmPath = null;
                string[] tmp = className.Split(',').Select(x => x.Trim()).ToArray();

                if (tmp.Length < 2) throw new Exception("Invalid className given.");
                asmPath = tmp[1];
                className = tmp[0];

                string typeDesc = string.Join(",", tmp);
                if (typeCache.ContainsKey(typeDesc)) return typeCache[typeDesc];
                
                return typeCache[typeDesc] = asmPath.LoadAssembly().GetType(className, true, false);
            }
        }
    }
}
