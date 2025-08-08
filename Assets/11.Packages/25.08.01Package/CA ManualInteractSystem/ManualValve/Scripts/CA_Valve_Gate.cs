using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_Valve_Gate: MonoBehaviour
{
	public CA_Valve valve;
	[SerializeField] private MoveAxisType moveAxisType;
	public enum MoveAxisType {X, Y, Z}
	public float strength = -400;
	public AudioClip gateMoveSFX;

	Vector3 currentPos;
	bool moving;
	bool stopped;
	Coroutine co_in;
	Coroutine co_out;
	AudioSource audioSource;
	Rigidbody rb;
	Vector3 oldPos;
	Vector3 newPos;

	void Start()
	{
		//create audiosource if there isn't any
		if (GetComponent<AudioSource>() == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.loop = true;
			audioSource.playOnAwake = false;
		}
			
		else
			audioSource = GetComponent<AudioSource>();

		audioSource.clip = gateMoveSFX;
		rb = GetComponent<Rigidbody>();
		currentPos = transform.position;
		oldPos = transform.position;
	}

	Vector3 targetPos;
	public float smoothTime = 0.3F;
	Vector3 velocity = Vector3.zero;

	void Update()
	{
		Audio();

		float move = valve.currentRot / -strength;
		
		switch (moveAxisType)
		{
			case MoveAxisType.X:
				targetPos = new Vector3(currentPos.x + move, currentPos.y, currentPos.z);
				break;
			case MoveAxisType.Y:
				targetPos = new Vector3(currentPos.x, currentPos.y + move, currentPos.z);
				break;
			case MoveAxisType.Z:
				targetPos = new Vector3(currentPos.x, currentPos.y, currentPos.z + move);
				break;
		}

		//smooth closing and opening
		transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
	}

	#region Sounds
	void Audio()
	{
		if (GetVelocity() > 0.1f)
		{
			if (!audioSource.isPlaying) audioSource.Play();

			co_in = StartCoroutine(FadeInSound());

			if (co_out != null)
			{
				StopCoroutine(co_out);
				co_out = null;
			}
		}

		else if (GetVelocity() < 0.1f)
		{
			co_out = StartCoroutine(FadeOutSound());

			if (co_in != null)
			{
				StopCoroutine(co_in);
				co_in = null;
			}
		}
	}

	float GetVelocity()
	{
		//this is needed to get the velocity of a kinematic rigidbody
		newPos = transform.position;
		Vector3 m = (newPos - oldPos);
		velocity = m / Time.deltaTime;
		oldPos = newPos;
		newPos = transform.position;
		float mag = velocity.magnitude;

		return mag;
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
	#endregion

}
