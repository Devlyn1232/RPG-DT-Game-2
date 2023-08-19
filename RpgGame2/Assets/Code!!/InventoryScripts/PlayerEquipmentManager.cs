using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.Inventory {
    public class PlayerEquipmentManager : MonoBehaviour
    {
        [Header("Player Transforms")]
        [SerializeField] private Transform helmetAnchor;
        [SerializeField] private Transform chestAnchor;
        [SerializeField] private Transform armsAnchor;
        [SerializeField] private Transform legsAnchor;

        [Header("Ui Transforms")]
        public Transform helmetUiHolder;
        public Transform chestUiHolder;
        public Transform armsUiHolder;
        public Transform legsUiHolder;

        // gameobjects for the model components of equiped gear
        private GameObject currentHelmetObj;
        private GameObject currentChestObj;
        private GameObject currentArmsObj;
        private GameObject currentLegsObj;

        // gameobjects for the ui component of equiped gear
        private InventoryObjectButton currentHelmetUi;
        private InventoryObjectButton currentChestUi;
        private InventoryObjectButton currentArmsUi;
        private InventoryObjectButton currentLegsUi;

        public static PlayerEquipmentManager instance;

        public Object.ObjectData[] equipment;

        void Awake() {
            instance = this;

            equipment = new Object.ObjectData[5];
        }

        public void AssignGearItem(GearObjectData item, int slot, InventoryObjectButton clickedSlot) {
            switch (slot) {
            case 1:
                if (DestroyIfNotNull(currentHelmetObj)) {
                    currentHelmetUi.transform.SetParent(Game.Player.InventorySystem.instance.inventoryFrame);
                    currentHelmetUi.used = false;
                }
                currentHelmetObj = CreateNewItemInstance(item, helmetAnchor);
                Debug.Log("Instantiated helmet");
                Game.Player.PlayerManager.instance.totalDefense += item.defense;
                clickedSlot.transform.SetParent(helmetUiHolder, clickedSlot);
                clickedSlot.transform.localPosition = new Vector3(0,0,0);
                currentHelmetUi = clickedSlot;

                break;
            case 2:
                if (DestroyIfNotNull(currentChestObj)) {
                    currentChestUi.transform.SetParent(Game.Player.InventorySystem.instance.inventoryFrame);
                    currentChestUi.used = false;
                }
                currentChestObj = CreateNewItemInstance(item, chestAnchor);
                Debug.Log("Instantiated chest");
                Game.Player.PlayerManager.instance.totalDefense += item.defense;
                clickedSlot.transform.SetParent(chestUiHolder, clickedSlot);
                clickedSlot.transform.localPosition = new Vector3(0,0,0);
                currentChestUi = clickedSlot;

                break;
            case 3:
                if (DestroyIfNotNull(currentArmsObj)) {
                    currentArmsUi.transform.SetParent(Game.Player.InventorySystem.instance.inventoryFrame);
                    currentArmsUi.used = false;
                }
                currentArmsObj = CreateNewItemInstance(item, armsAnchor);
                Debug.Log("Instantiated arms");
                Game.Player.PlayerManager.instance.totalDefense += item.defense;
                clickedSlot.transform.SetParent(armsUiHolder, clickedSlot);
                clickedSlot.transform.localPosition = new Vector3(0,0,0);
                currentArmsUi = clickedSlot;

                break;
            case 4:
                if (DestroyIfNotNull(currentLegsObj)) {
                    currentLegsUi.transform.SetParent(Game.Player.InventorySystem.instance.inventoryFrame);
                    currentLegsUi.used = false;
                }
                currentLegsObj = CreateNewItemInstance(item, legsAnchor);
                Debug.Log("Instantiated arms");
                Game.Player.PlayerManager.instance.totalDefense += item.defense;
                clickedSlot.transform.SetParent(legsUiHolder, clickedSlot);
                clickedSlot.transform.localPosition = new Vector3(0,0,0);
                currentLegsUi = clickedSlot;

                break;
            default:

                break;
            }
        }

        public void UnassignGearItem(GearObjectData item, int slot) {
            switch (slot) {
            case 1:
                Destroy(currentHelmetObj);
                currentHelmetObj = null;
                currentHelmetUi.transform.SetParent(Game.Player.InventorySystem.instance.inventoryFrame);
                currentHelmetUi = null;
                Game.Player.PlayerManager.instance.totalDefense -= item.defense;
                break;
            case 2:
                Destroy(currentChestObj);
                currentChestObj = null;
                currentChestUi = null;
                Game.Player.PlayerManager.instance.totalDefense -= item.defense;
                break;
            case 3:
                Destroy(currentArmsObj);
                currentArmsObj = null;
                currentArmsUi = null;
                Game.Player.PlayerManager.instance.totalDefense -= item.defense;
                break;
            case 4:

                break;
            default:

                break;
            }
        }

        public void AssignWeaponItem(Object.ObjectData item) {

        }

        private GameObject CreateNewItemInstance(Object.ObjectData item, Transform anchor) {
            var itemInstance = Instantiate(item.prefab, anchor);
            //itemInstance.transform.localRotation = item.GetLocalRotation();
            //itemInstance.transform.localPosition = item.GetLocalPosition();
            return itemInstance;
        }

        private bool DestroyIfNotNull(GameObject obj) {
            if (obj) {
                Destroy(obj);
                return true;
            }
            return false;
        }
    }
}
