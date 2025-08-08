using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_Valve_MinMaxActivation : MonoBehaviour
{
	public bool enable = true;
	public GameObject GO;
	public Behaviour comp;
	

	void Update()
	{
		if (enable)
		{
			if (GO != null)
				GO.SetActive(true);
			if (comp != null)
				comp.enabled = true;
		}

		else
		{
			if (GO != null)
				GO.SetActive(false);
			if (comp != null)
				comp.enabled = false;
		}
	}

	
}
