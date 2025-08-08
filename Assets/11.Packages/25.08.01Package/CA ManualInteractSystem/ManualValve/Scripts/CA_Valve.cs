using System.Collections;
using UnityEngine;


public class CA_Valve : MonoBehaviour {

	[Header("Valve setup")]
	[SerializeField] private float strength = 20;
	[SerializeField] private Transform handle;
	[SerializeField] private ForwardAxisType forwardAxisType;
	[SerializeField] private RightAxisType rightAxisType;
	public enum ForwardAxisType{ X, Y, Z, NegativeX, NegativeY, NegativeZ }
	public enum RightAxisType{ X, Y, Z, NegativeX, NegativeY, NegativeZ }
	[SerializeField] private AudioClip valveMoveSFX;

	[Header("Icons setup")]
	[SerializeField] private GameObject onIcon;
	[SerializeField] private GameObject offIcon;

	[Header ("Min&Max settings")]
	[SerializeField] private GameObject minBlock;
	[SerializeField] private GameObject maxBlock;
	[SerializeField] private bool lockOnMin;
	[SerializeField] public bool lockOnMax;
	public Vector2 minMaxValue = new Vector2 (-500, 0);
	[HideInInspector] public float currentRot;

    #region private vars
    //player set up
    private Transform player;
    private Camera playerCam;

    float newRotationValue;
	float rotValue;
	float mag;
	bool rotating;
	Rigidbody rb;
	CharacterController CC;
	CA_InteractManager interactManager;
	AudioSource audioSource;
	Coroutine co_in;
	Coroutine co_out;
	GameObject axisPivot;
	Vector3 forwardAxis;
	Vector3 rightAxis;
    #endregion

    private void Awake()
	{
        interactManager = CA_InteractManager.script;
        player = CA_InteractManager.script.transform;
        playerCam = CA_InteractManager.script.GetComponent<Camera>();

        //create audiosource if there isn't any
        if (GetComponent<AudioSource>() == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.loop = true;
			audioSource.playOnAwake = false;
		}
			
		else
			audioSource = GetComponent<AudioSource>();

		minBlock.SetActive(false);
		maxBlock.SetActive(false);

		rb = gameObject.GetComponent<Rigidbody>();
		rb.centerOfMass = new Vector3(0, 0, 0);
		CC = player.GetComponent<CharacterController>();
		audioSource.clip = valveMoveSFX;
		MakeAxisPivot();
	}

	void MakeAxisPivot()
	{
		axisPivot = new GameObject("AxisPivot");
		axisPivot.transform.position = transform.position;
		axisPivot.transform.rotation = transform.rotation;
		axisPivot.transform.parent = transform.parent;
	}
	
	void Update()
	{
		Rotation();

		CheckLimits();

		Audio();

		Icons();
	}

	void Icons()
	{
		if(interactManager.hitObj == gameObject)
		{
			if(interactManager.interacting)
			{
				offIcon.SetActive(false);
				onIcon.SetActive(true);
			}
			else
			{
				offIcon.SetActive(true);
				onIcon.SetActive(false);
			}
		}

		else
		{
			offIcon.SetActive(false);
			onIcon.SetActive(false);
		}
	}

	private void Rotation()
	{
		if (interactManager.hitObj == gameObject && interactManager.interacting)
		{
			rotating = true;

			//Add force to rotate
			rb.AddForceAtPosition(player.right * strength * Input.GetAxis("Mouse X"), handle.position);
			rb.AddForceAtPosition(player.up * strength * Input.GetAxis("Mouse Y"), handle.position);

			//Get current rotation
			newRotationValue = GetSignedAngle();
			while (newRotationValue < rotValue - 180f) newRotationValue += 360f;
			while (newRotationValue > rotValue + 180f) newRotationValue -= 360f;
			rotValue = newRotationValue;
			
			//clamp currentRot (make sure the values don't exceed set min and max values)
			currentRot = Mathf.Clamp(rotValue, minMaxValue.x, minMaxValue.y);

			DisableLook();
		}

		else
		{
			rotating = false;
			EnableLook();
		}

		if (rotating)
			if (!interactManager.interacting)
			{
				rotating = false;
				EnableLook();
			}
	}

	float GetSignedAngle()
	{
		//get forward axis
		switch (forwardAxisType)
		{
			case ForwardAxisType.X:
				forwardAxis = axisPivot.transform.right;
				break;
			case ForwardAxisType.Y:
				forwardAxis = axisPivot.transform.up;
				break;
			case ForwardAxisType.Z:
				forwardAxis = axisPivot.transform.forward;
				break;
			case ForwardAxisType.NegativeX:
				forwardAxis = -axisPivot.transform.right;
				break;
			case ForwardAxisType.NegativeY:
				forwardAxis = -axisPivot.transform.up;
				break;
			case ForwardAxisType.NegativeZ:
				forwardAxis = -axisPivot.transform.forward;
				break;
		}

		//get right axis
		switch (rightAxisType)
		{
			case RightAxisType.X:
				rightAxis = axisPivot.transform.right;
				break;
			case RightAxisType.Y:
				rightAxis = axisPivot.transform.up;
				break;
			case RightAxisType.Z:
				rightAxis = axisPivot.transform.forward;
				break;
			case RightAxisType.NegativeX:
				rightAxis = -axisPivot.transform.right;
				break;
			case RightAxisType.NegativeY:
				rightAxis = -axisPivot.transform.up;
				break;
			case RightAxisType.NegativeZ:
				rightAxis = -axisPivot.transform.forward;
				break;
		}

		Vector3 handleDir = handle.position - transform.position;
		float angle = Vector3.SignedAngle(handleDir, rightAxis, forwardAxis);

		return angle;
	}


	void DisableLook()
	{
        //put your own player controller script to disable here
        interactManager.GetComponent<CA_Sample_PlayerLook>().enabled = false;
	}
	
	void EnableLook()
	{
        // put your own player controller script to enable here
        interactManager.GetComponent<CA_Sample_PlayerLook>().enabled = true;
    }

	void CheckLimits()
	{
		//min
		if(currentRot <= minMaxValue.x)
		{
			OnMin();
			minBlock.SetActive(true);
			if (lockOnMin)
				rb.isKinematic = true;
		}
			
		else
			minBlock.SetActive(false);

		//max
		if (currentRot >= minMaxValue.y)
		{
			OnMax();
			maxBlock.SetActive(true);
			if (lockOnMax)
				rb.isKinematic = true;
		}
			
		else
			maxBlock.SetActive(false);
	}

	public void OnMin()
	{
		//put your own functions on minimum rotational value.

	}

	public void OnMax()
	{
		//put your own functions on maxmimum rotational value.

	}

	void Audio()
	{
		mag = rb.angularVelocity.magnitude;

		if (mag > 0.1f)
		{
			if(!audioSource.isPlaying) audioSource.Play();
			
			co_in = StartCoroutine(FadeInSound());

			if (co_out != null)
			{
				StopCoroutine(co_out);
				co_out = null;
			}
				
		}

		else if (mag < 0.1f)
		{
			co_out = StartCoroutine(FadeOutSound());

			if (co_in != null)
			{
				StopCoroutine(co_in);
				co_in = null;
			}
				
		}
	}

	IEnumerator FadeInSound()
	{
		while (audioSource.volume < 1)
		{
			audioSource.volume += Time.deltaTime * 1;
			yield return new WaitForFixedUpdate();
		}
	}

	IEnumerator FadeOutSound()
	{
		while (audioSource.volume > 0)
		{
			audioSource.volume -= Time.deltaTime * 1;
			yield return new WaitForFixedUpdate();
		}
	}

	
}
