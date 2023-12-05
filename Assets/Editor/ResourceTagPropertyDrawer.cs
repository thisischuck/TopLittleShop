using Unity;
using UnityEditor;
using UnityEngine;
using Quests;
using Inventory;
using System;

[CustomPropertyDrawer(typeof(ResourceTag))]
public class ResourceTagPropertyDrawer : PropertyDrawer
{
	private static readonly string[] EnumNames = GetEnumNames();

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		string myTypeString = EnumNames[property.enumValueIndex];
		int selectedIndex = GetSelectedIndex(myTypeString);
		property.enumValueIndex = EditorGUI.Popup(
			position, label.text,
			selectedIndex,
			EnumNames);
	}

	private static SerializedProperty FindSoundTypeProperty(SerializedProperty property)
	{
		return property.serializedObject
			.FindProperty("_instances") //Array with instances of my objects
			.GetArrayElementAtIndex(Convert.ToInt32(property.propertyPath.Split('[', ']')[1]))
			.FindPropertyRelative("SerializedMyType"); //String containing the enum name
	}

	//Find proper index using saved enum parsed to string
	private static int GetSelectedIndex(string myTypeString)
	{
		for (int i = 0; i < EnumNames.Length; i++)
		{
			if (EnumNames[i] == myTypeString)
			{
				return i;
			}
		}
		return 0; //Return default if not exist (eg. it is deleted)
	}

	private static string[] GetEnumNames()
	{
		string[] result = Enum.GetNames(typeof(ResourceTag));
		for (int i = 0; i < result.Length; i++)
		{
			result[i] = result[i].Replace("_", " / ");
		}
		return result;
	}
}