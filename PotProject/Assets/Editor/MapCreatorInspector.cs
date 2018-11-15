using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(MapCreator))]
public class MapCreatorInspector : Editor
{

    ReorderableList reorderableList;

    private void OnEnable()
    {
        var prop = serializedObject.FindProperty("tiles");

        reorderableList = new ReorderableList(serializedObject, prop);
        reorderableList.elementHeight = 68;
        reorderableList.drawElementCallback =
  (rect, index, isActive, isFocused) => {
      var element = prop.GetArrayElementAtIndex(index);
      rect.height -= 4;
      rect.y += 2;
      EditorGUI.PropertyField(rect, element);
  };

        var defaultColor = GUI.backgroundColor;

        reorderableList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, prop.displayName);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }


}
