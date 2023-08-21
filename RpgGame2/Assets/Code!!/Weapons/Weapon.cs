using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    BoxCollider triggerBox;

    // Start is called before the first frame update
    void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
    }

    //To comment out a section do Ctrl + k then press c

    //private void OnTriggerEnter(Collider other)
    //{
    //    //var enemy = other.gameObject.GetComponent<>();
    //    if (enemy != null)
    //    {
    //        //take damage
    //        //if health is 0 or less destroy
    //    }
    //}

    public void EnableTriggerBox()
    {
        triggerBox.enabled = true;
    }
    
    public void DisableTriggerBox()
    {
        triggerBox.enabled = false;
    }

}
