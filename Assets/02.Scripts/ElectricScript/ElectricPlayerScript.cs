using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPlayerScript : MonoBehaviour
{
    public Vector3 startPosition;

    public bool isWearGlove = false;

    private void Awake()
    {
        startPosition = gameObject.transform.position;
    }

    public void EndState()
    {
        gameObject.GetComponent<PlayerMovement>().gravity = 0;
        gameObject.transform.position = startPosition;
    }
}
