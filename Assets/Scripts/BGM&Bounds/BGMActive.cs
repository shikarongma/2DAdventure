using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMActive : MonoBehaviour
{
    public GameObject BGM;
    private void Awake()
    {
        BGM.SetActive(true);
    }
}
