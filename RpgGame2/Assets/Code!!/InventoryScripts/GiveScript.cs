using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveScript : MonoBehaviour
{
    [SerializeField] private Object.ObjectData obj;
    public void GiveObject() {
        Game.Player.InventorySystem.instance.Add(obj);
    }
    public void TakeObject() {
        Game.Player.InventorySystem.instance.Remove(obj);
    }
}
