using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenToggle : MonoBehaviour // merge this with an input handler at some point and delete this script
{
    [SerializeField] private GameObject inventoryFrame;
    private bool isUiActive = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            if(isUiActive) {
                inventoryFrame.SetActive(false);
                isUiActive = false;
            }
            else {
                inventoryFrame.SetActive(true);
                isUiActive = true;
            }
            
        }
    }
}
