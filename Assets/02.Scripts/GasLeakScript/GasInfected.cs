using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasInfected : MonoBehaviour
{
    [SerializeField]
    private GameObject defaultPerson;
    [SerializeField]
    private GameObject infectedPerson;
    [SerializeField]
    private GasFadeController gasFadeController;
    
    public void Infected()
    {
        gasFadeController.FadeOut();
        defaultPerson.gameObject.SetActive(false);
        infectedPerson.gameObject.SetActive(true);
        gasFadeController.FadeIn();
    }
}
