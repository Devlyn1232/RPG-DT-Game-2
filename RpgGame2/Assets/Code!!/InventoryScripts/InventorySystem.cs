using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEditor;
using TMPro;

namespace Player // add more namespace at beginning when committed to project
{

    [System.Serializable] public class InventoryUiItem { // wrapper class for an array of the UI components
        public GameObject itemUI { get; private set; }
        public TMP_Text itemUIStackText { get; private set; }
        public GameObject itemUiIcon { get; private set; }

        public InventoryUiItem(GameObject uiPrefab, GameObject textPrefab, GameObject iconPrefab){
            itemUI = uiPrefab;
            itemUIStackText = textPrefab.GetComponent<TMP_Text>();
            itemUiIcon = iconPrefab;
        }
    }

    [System.Serializable] public class InventoryItem { // wrapper class for item in inventory (ui comps, stack size obj data)
        public int StackSize { get; private set; }
        public Object.ObjectData data { get; private set; }
        public List<InventoryUiItem> itemUiList { get; private set; }

        public InventoryItem(Object.ObjectData source, InventoryUiItem uiItemPrefabs){
            itemUiList = new List<InventoryUiItem>();
            data = source;
            itemUiList.Add(uiItemPrefabs);

            foreach(InventoryUiItem i in itemUiList){
                i.itemUiIcon.GetComponent<Image>().sprite = data.icon;
            }

            AddToStack();
        }

        public void AddToStack()
        {
            StackSize++;
            foreach(InventoryUiItem i in itemUiList){
                i.itemUIStackText.text = StackSize.ToString();
            }
            
        }

        public void RemoveFromStack(){
            StackSize--;
            foreach(InventoryUiItem i in itemUiList){
                i.itemUIStackText.text = StackSize.ToString();
            }
        }

        public void UnstackableAdd(InventoryUiItem uiItemPrefabs){
            StackSize++;
            itemUiList.Add(uiItemPrefabs);

            foreach(InventoryUiItem i in itemUiList){
                i.itemUiIcon.GetComponent<Image>().sprite = data.icon;
            }
        }

        public void UnstackableRemove() {
            StackSize--;
            InventoryUiItem itemToRemove = itemUiList[0];
            itemUiList.Remove(itemToRemove);
            Player.InventorySystem.instance.Destroyer(itemToRemove);
        }
    }

    public class InventorySystem : MonoBehaviour
    {
        public static InventorySystem instance; // singleton reference

        [SerializeField] private GameObject itemUI;
        [SerializeField] private GameObject itemStackUI;
        [SerializeField] private GameObject itemIconUI;

        [SerializeField] private Transform inventoryFrame;

        public List<InventoryItem> inventory {get; private set;} // the inventory list
        private Dictionary<Object.ObjectData, InventoryItem> m_itemDictionary; // dictionary for items in inventory (stack size, ui elements etc.)

        private void Awake() {
            instance = this; // setup reference
            inventory = new List<InventoryItem>(); // setup list & dictionary
            m_itemDictionary = new Dictionary<Object.ObjectData, InventoryItem>();
        }

        public void Add(Object.ObjectData referenceData) { // add item to inventory
            if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value)){ // is it in the inventory already?
                if (referenceData.stackable == true){
                    value.AddToStack(); // increase stack size

                } else{ // create new ui item for unstackable items (TBD)...
                    GameObject newItemUI = Instantiate(itemUI, itemUI.transform.position, Quaternion.identity, inventoryFrame);
                    GameObject newItemIconUI = Instantiate(itemIconUI, itemIconUI.transform.position, Quaternion.identity, newItemUI.transform);
                    GameObject newItemStackUI = Instantiate(itemStackUI, itemStackUI.transform.position, Quaternion.identity, newItemUI.transform);

                    InventoryUiItem newUiItem = new InventoryUiItem(newItemUI, newItemStackUI, newItemIconUI);

                    value.UnstackableAdd(newUiItem);
                }
            }
            else{ // else create a new element in the list for the item.
                GameObject newItemUI = Instantiate(itemUI, itemUI.transform.position, Quaternion.identity, inventoryFrame);
                GameObject newItemIconUI = Instantiate(itemIconUI, itemIconUI.transform.position, Quaternion.identity, newItemUI.transform);
                GameObject newItemStackUI = Instantiate(itemStackUI, itemStackUI.transform.position, Quaternion.identity, newItemUI.transform);

                InventoryUiItem newUiItem = new InventoryUiItem(newItemUI, newItemStackUI, newItemIconUI);
                InventoryItem newItem = new InventoryItem(referenceData, newUiItem);
                inventory.Add(newItem);
                m_itemDictionary.Add(referenceData, newItem);
                // add creation of ui element here...
            }
        }
        
        public void Remove(Object.ObjectData referenceData) {
            if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value)){
                if (referenceData.stackable == true) {
                    value.RemoveFromStack();
                }
                else {
                    value.UnstackableRemove();
                }

                if(value.StackSize == 0){
                    inventory.Remove(value); // next 2 lines remove item
                    m_itemDictionary.Remove(referenceData);
                }
            }
        }

        public InventoryItem Get(Object.ObjectData referenceData){
            if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value)){
                return value;
            }
            return null;
        }

        public void Destroyer(InventoryUiItem itemToDestroy) {
            
            Destroy(itemToDestroy.itemUI);
        }
    }

}