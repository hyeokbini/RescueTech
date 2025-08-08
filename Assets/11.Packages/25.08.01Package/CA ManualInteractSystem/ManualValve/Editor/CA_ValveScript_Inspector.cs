using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (CA_Valve))]
public class CA_ValveScript_Inspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		CA_Valve valve = (CA_Valve)target;

		GUILayout.Label("Current Rotation   =  " + valve.currentRot.ToString());
	}
}
