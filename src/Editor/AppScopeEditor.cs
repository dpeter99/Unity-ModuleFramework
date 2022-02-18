using com.dpeter99.framework.src;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace src.Editor
{
    [CustomEditor(typeof(Scope))]
    [CanEditMultipleObjects]
    public class AppScopeEditor : UnityEditor.Editor
    {
        public VisualTreeAsset m_InspectorXML;
        private ListView listView;

        /*
        public override VisualElement CreateInspectorGUI()
        {
            // Create a new VisualElement to be the root of our inspector UI
            VisualElement myInspector = new VisualElement();

            // Add a simple label
            //myInspector.Add(new Label("This is a custom inspector"));
            
            // Load and clone a visual tree from UXML
            VisualTreeAsset visualTree = m_InspectorXML;
            visualTree.CloneTree(myInspector);

            var a = myInspector.Q<Foldout>("Modules");
            listView = new ListView();
            listView.makeItem = () => new Label();
            listView.bindItem = (item, index) => { (item as Label).text = "asd"; };
            listView.itemsSource = new[] { "asd","asd2" };
            //listView.binding = ;
            
            
            myInspector.Add(listView);
            
            
            // Return the finished inspector UI
            return myInspector;
        }
*/

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            Scope t = target as Scope;
            
            //EditorGUI.Foldout();

            if (EditorGUILayout.Foldout(true, "Modules"))
            {
                foreach (var module in t._modules)
                {
                    GUILayout.Label(module.GetType().FullName);
                    //EditorGUILayout.PropertyField();
                }
                
            }

        }
    }
    /*
    [CustomPropertyDrawer(typeof(IModule))]
    public class Tire_PropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            // Create a new VisualElement to be the root the property UI
            var container = new VisualElement();

            // Create drawer UI using C#
            // ...
            container.Add(new Label("This is a custom inspector"));
            container.Add(new PropertyField(property));

            // Return the finished UI
            return container;
        }
    }
    */
}