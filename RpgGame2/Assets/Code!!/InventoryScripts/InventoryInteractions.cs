using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.Inventory {
    public class InventoryInteractions : MonoBehaviour
    {
        public static InventoryInteractions instance;

        [SerializeField] private GameObject inventoryFrame;
        public bool isUiActive = true;

        void Awake(){
            instance = this;
        }

        public void ToggleInventory() {
            if(isUiActive) {
                    inventoryFrame.SetActive(false);
                    isUiActive = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else {
                    inventoryFrame.SetActive(true);
                    isUiActive = true;
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                }
        }
    }
}
