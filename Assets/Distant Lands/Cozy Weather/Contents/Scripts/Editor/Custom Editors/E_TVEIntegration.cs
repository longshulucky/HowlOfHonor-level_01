﻿using UnityEditor;

namespace DistantLands.Cozy.EditorScripts
{

    [CustomEditor(typeof(CozyTVEModule))]
    [CanEditMultipleObjects]
    public class E_TVEIntegration : Editor
    {
        SerializedProperty updateFrequency;
        CozyTVEModule module;


        void OnEnable()
        {
            updateFrequency = serializedObject.FindProperty("updateFrequency");
            module = (CozyTVEModule)target;



        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(updateFrequency);
            serializedObject.ApplyModifiedProperties();

#if THE_VEGETATION_ENGINE
            if (!module.globalControl || !module.globalMotion)
            {
                EditorGUILayout.Space(20);
                EditorGUILayout.HelpBox("Make sure that you have active TVE Global Motion and TVE Global Control objects in your scene!", MessageType.Warning);

            }
#else
            EditorGUILayout.Space(20);
            EditorGUILayout.HelpBox("The Vegetation Engine is not currently in this project! Please make sure that it has been properly downloaded before using this module.", MessageType.Warning);

#endif
        }
    }
}
