﻿/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using InfinityCode.UltimateEditorEnhancer.InspectorTools;
using UnityEditor;

namespace InfinityCode.UltimateEditorEnhancer
{
    public static partial class Prefs
    {
        public static bool emptyInspector = true;

        public class EmptyInspectorManager : StandalonePrefManager<EmptyInspectorManager>
        {
            private SerializedObject serializedObject;
            private SerializedProperty elementsProperty;

            public override IEnumerable<string> keywords
            {
                get
                {
                    return new[]
                    {
                        "Empty Inspector",
                    };
                }
            }

            public override void Draw()
            {
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                if (serializedObject == null)
                {
                    serializedObject = new SerializedObject(ReferenceManager.instance);
                    if (serializedObject != null)
                    {
                        elementsProperty = serializedObject.FindProperty("_emptyInspectorItems");
                        if (elementsProperty != null) elementsProperty.isExpanded = true; 
                    }
                }

                emptyInspector = EditorGUILayout.ToggleLeft("Empty Inspector", emptyInspector);

                if (elementsProperty != null)
                {
                    serializedObject.Update();

                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(elementsProperty);
                    bool isDirty = EditorGUI.EndChangeCheck();
                    
                    serializedObject.ApplyModifiedProperties();

                    if (isDirty) EmptyInspector.ResetCachedItems();
                }


                EditorGUILayout.EndScrollView();
            }
        }
    }
}