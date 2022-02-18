using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Object = System.Object;

namespace com.dpeter99.framework.src
{
    public interface IScope
    {
        IModule GetModule(Type t, bool onlySelf = false);
        T GetModule<T>(bool onlySelf = false) where T : class;
        T GetModuleSelf<T>() where T : class;
    }

    public class Scope: MonoBehaviour, IScope
    {
        public List<IModule> _modules = new();
        
        public IScope _parent;

        public IModule GetModule(Type t, bool onlySelf = false)
        {
            var module = _modules.Where(t.IsInstanceOfType).FirstOrDefault();
            if (!onlySelf && module == null)
            {
                module = _parent?.GetModule(t);
            }

            return module;
        }
        
        public T GetModule<T>(bool onlySelf = false) where T : class
        {
            return GetModule(typeof(T), onlySelf) as T;
        }

        public T GetModuleSelf<T>() where T : class
        {
            return GetModule<T>(true);
        }

        public IModule AddModule(Type type)
        {
            var customBuilder =
                (from methodInfo in type.GetMethods().AsParallel()
                    let attributes = methodInfo.GetCustomAttributes(typeof(CustomBuilderAttribute), true)
                    where attributes != null && attributes.Length > 0
                    select methodInfo).FirstOrDefault();

            IModule comp = null;
            
            if (customBuilder is not null)
            {
                comp = customBuilder.Invoke(null, new[] { gameObject }) as IModule;
            }
            else
            {
                comp = gameObject.AddComponent(type) as IModule;
            }
            
            _modules.Add(comp);

            if (comp is Scope)
            {
                (comp as Scope)._parent = this;
            }
            
            return comp;
        }
        
        public T AddModule<T>() where T : class, IModule
        {
            return AddModule(typeof(T)) as T;
        }
    }
}