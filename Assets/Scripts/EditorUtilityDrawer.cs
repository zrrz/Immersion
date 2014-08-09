#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;


namespace EditorUtilityDrawer
{

  

    /*
	public class IncrementalChange : PropertyAttribute {
		public float increment;
		public IncrementalChange(float increment) {
			this.increment = increment;
		}
	}

	[CustomPropertyDrawer( typeof( IncrementalChange ))]
	public class IncrementalChangeDrawer : PropertyDrawer
	{
		private int textHeight = 16;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			
			EditorGUI.BeginProperty( position, label, property );
			int origIndent = EditorGUI.indentLevel;

			//Retrieve the increment from the attribute.
			float increment = ((IncrementalChange)attribute).increment;

			//Hidden slider allows us to replicate the slide over name to change the value
			EditorGUI.Slider(new Rect( position.xMin, position.yMin, position.width/3f, textHeight),
			                 property, property.floatValue - 200, property.floatValue + 200);

			EditorGUI.LabelField(new Rect( position.xMin, position.yMin, position.width/3f, textHeight),
			                     GUIUtils.PrettyPrintVariableName(property.name));

			property.floatValue = EditorGUI.FloatField(
				new Rect( position.xMax/3f, position.yMin,
			    position.width - (position.width/3f),
			    textHeight), Mathf.Round(property.floatValue/increment)*increment);

			EditorGUI.indentLevel = origIndent;
			EditorGUI.EndProperty();
		}
	}


	public class PopupAttribute : PropertyAttribute {
		public string[] items;
		public PopupAttribute(params string[] values) {
			this.items = values;
		}
	}

	[CustomPropertyDrawer( typeof( PopupAttribute ))]
	public class PopupDrawer : PropertyDrawer
	{
		private int textHeight = 16;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			
			EditorGUI.BeginProperty( position, label, property );
			int origIndent = EditorGUI.indentLevel;
			string[] values = ((PopupAttribute)attribute).items;
			int index = 0;
			for(; index < values.Count()-1; index++) {
				if(values[index].Equals(property.stringValue))
					break;
			}


			EditorGUI.LabelField(new Rect( position.xMin, position.yMin, position.width/3f, textHeight),
			                     GUIUtils.PrettyPrintVariableName(property.name));

			index = EditorGUI.Popup(
				new Rect( position.xMax/3f,
			         position.yMin, position.width - (position.width/3f),
			         textHeight), index, values);

			if(index < values.Count())
				property.stringValue = values[index];

			EditorGUI.indentLevel = origIndent;
			EditorGUI.EndProperty();
		}
	}

	public class InventoryItemAttribute : PropertyAttribute {
		
	}

	[CustomPropertyDrawer( typeof( InventoryItemAttribute ))]
	public class InventoryDrawer : PropertyDrawer
	{
		private int textHeight = 16;
		private string itemName;
		
		
		
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			
			EditorGUI.BeginProperty( position, label, property );
			
			int origIndent = EditorGUI.indentLevel;
			
			SerializedProperty itemName = property.FindPropertyRelative( "itemName" );
			SerializedProperty itemType = property.FindPropertyRelative( "type" );
			SerializedProperty itemID = property.FindPropertyRelative( "ID" );
			
			string[] enumValues = Enum.GetNames(typeof(Inventory.InventoryItem.InventoryItemType));
			string longest = enumValues.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);
			//Calculate the width of the largest item. And add some padding to fit the popup control around it
			float widthOfPopup = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).label.CalcSize(
				new GUIContent(longest)).x + 25;
			//Don't go beyond the size we're alloted 
			widthOfPopup = Math.Min((position.width/2), widthOfPopup);
			
			itemName.stringValue = EditorGUI.TextArea( new Rect( position.xMin, position.yMin,
			                                                    (position.width/2), textHeight ),
			                                          			itemName.stringValue );
			itemType.enumValueIndex = EditorGUI.Popup(new Rect( (position.xMax/2f), position.yMin,
			                                                    widthOfPopup, textHeight),
			                                          			itemType.enumValueIndex,
			                                            Enum.GetNames(typeof(
														Inventory.InventoryItem.InventoryItemType)));
			
			//We don't have to limit ourselves to EditorGUI, we can use regular GUI items too
			
			if(GUI.Button(new Rect(position.width - 25, position.yMin, 25, textHeight), "-")) {
				//We can even call functions inside the script
				((Inventory)property.serializedObject.targetObject).RemoveItem(itemID.intValue);
			}
			
			EditorGUI.indentLevel = origIndent;
			EditorGUI.EndProperty();
		}
	}
     * */
}
#endif