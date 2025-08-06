using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to player's camera
/// </summary>
public class CA_InteractManager : MonoBehaviour
{
    public static CA_InteractManager script;

    public Transform playerController;
    [SerializeField] private float rayDistance = 5;
	[SerializeField] private InteractInput inputType;

	[HideInInspector] public GameObject hitObj;
	[HideInInspector] public bool interacting;
    
    public enum InteractInput
	{
		LeftMouse, E, F
	}

    private void Awake()
    {
        script = this;
    }

    void Update()
    {
        RaycastHit hit;
		hitObj = null;
		Reset();

		if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
		{
			hitObj = hit.transform.gameObject;

			Inputs();
		}
	}

    private void Inputs()
    {
        switch (inputType)
        {
            case InteractInput.LeftMouse:
                if (Input.GetMouseButton(0))
                    interacting = true;
                else
                    interacting = false;
                break;

            case InteractInput.E:
                if (Input.GetKey(KeyCode.E))
                    interacting = true;
                else
                    interacting = false;
                break;

            case InteractInput.F:
                if (Input.GetKey(KeyCode.F))
                    interacting = true;
                else
                    interacting = false;
                break;
        }
    }

	void Reset()
	{
		switch (inputType)
		{
			case InteractInput.LeftMouse:
				if (!Input.GetMouseButton(0))
					interacting = false;
				break;

			case InteractInput.E:
				if (!Input.GetKey(KeyCode.E))
					interacting = false;
				break;

			case InteractInput.F:
				if (!Input.GetKey(KeyCode.F))
					interacting = false;
				break;
		}
	}
}
