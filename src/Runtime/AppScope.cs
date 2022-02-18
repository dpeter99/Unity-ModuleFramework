using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using com.dpeter99.framework.src;
using UnityEngine;

namespace com.dpeter99.framework.src
{
    public class AppScope
    {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Bootstrap()
        {
            var go = new GameObject("App");

            var inst = go.AddComponent<Scope>();

            var querry =
                from a in AppDomain.CurrentDomain.GetAssemblies().AsParallel()
                from t in a.GetTypes()
                let attributes = t.GetCustomAttributes(typeof(ManagerAttribute), true)
                where attributes != null && attributes.Length > 0 && typeof(IModule).IsAssignableFrom(t)
                select new { Type = t, Attributes = attributes.Cast<ManagerAttribute>().First() };
            

            var managers = querry.Where(i => i.Attributes.managerType == ManagerAttribute.ManagerType.App);

            foreach (var man in managers)
            {
                inst.AddModule(man.Type);
            }

            GameObject.DontDestroyOnLoad(go);
        }
        
        
    }
}