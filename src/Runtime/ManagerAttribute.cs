using System;

namespace com.dpeter99.framework.src
{
    [AttributeUsage(AttributeTargets.Class)]
    
    public class ManagerAttribute: Attribute
    {
        public ManagerType managerType { get; private set; }
        
        public ManagerAttribute(ManagerType type = ManagerType.App)
        {
            managerType = type;
        }

        public enum ManagerType
        {
            App,
            Game
        }
    }
}