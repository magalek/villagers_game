using System;

namespace Managers
{
    public class Manager<TManager> : IManager where TManager : IManager
    {
        public static TManager Current
        {
            get
            {
                var t = typeof(TManager);
                var result = ManagerLoader.Get(typeof(TManager));
                
                return (TManager)Convert.ChangeType(result, result.GetType().BaseType);
            }
        }
    }
}