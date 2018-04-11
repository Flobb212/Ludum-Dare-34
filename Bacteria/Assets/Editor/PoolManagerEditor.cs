using UnityEngine;
using UnityEditorInternal;
using UnityEditor;

[CustomEditor(typeof(PoolManager))]
public class PoolManagerEditor : Editor {

	private ReorderableList list;

	private void OnEnable() {
		list = new ReorderableList (serializedObject, serializedObject.FindProperty ("objectPool"), true, true, true, true);

		list.drawElementCallback =  
		(Rect rect, int index, bool isActive, bool isFocused) => {
			var element = list.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("name"), GUIContent.none);
			EditorGUI.PropertyField(
				new Rect(rect.x + 120, rect.y, rect.width - 120 - 30, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("objectPrefab"), GUIContent.none);
			EditorGUI.PropertyField(
				new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("count"), GUIContent.none);
		};

		list.drawHeaderCallback = (Rect rect) => {  
			EditorGUI.LabelField(rect, "Object Pool");
		};
	}

	public override void OnInspectorGUI () {
		serializedObject.Update ();
		list.DoLayoutList ();
		serializedObject.ApplyModifiedProperties ();
	}
}
