using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CA_Sample_CurrentValueText : MonoBehaviour
{

	public CA_Valve valve;
	TextMesh text; 

	void Start()
	{
		text = GetComponent<TextMesh>();
	}

    void Update()
    {
		text.text = valve.currentRot.ToString();

	}
}
