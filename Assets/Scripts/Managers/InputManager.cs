using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject EcsMenuWrapper;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isActive = EcsMenuWrapper.activeSelf;
            EcsMenuWrapper.SetActive(!isActive);
        }
    }
}
