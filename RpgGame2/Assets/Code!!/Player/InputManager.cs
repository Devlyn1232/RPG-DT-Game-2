using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// purpose of this script is to put all the bindings etc. into one area.
// That way if states are needed say for playing in the world versus using inventory it can be done here. :D
namespace Player.InputHandler {
    public class InputManager : MonoBehaviour
    {
        public static InputManager instance;

        [Header("Movement Binds")]
        public KeyCode jumpKey = KeyCode.Space;
        public KeyCode sprintKey = KeyCode.LeftShift;
        public KeyCode crouchKey = KeyCode.C;

        [SerializeField] private bool toggleCrouch = false;

        [Header("Inventory Binds")]
        [SerializeField] private KeyCode toggleInventoryKey = KeyCode.I;

        [Header("Camera Controls")]
        public Vector2 cameraInput;
        public float cameraInputX;
        public float cameraInputY;
        [SerializeField] private float sensX;
        [SerializeField] private float sensY;

        void Awake() {
            instance = this;
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update() { // still need to add movement controls into the input manager but the idea is here
            HandleAllInput();
        }

        void HandleAllInput() {
            if (Input.GetKeyDown(toggleInventoryKey)) {
                Player.Inventory.InventoryInteractions.instance.ToggleInventory();
                cameraInput.x = 0;
                cameraInput.y = 0;
            }

            if (Player.Inventory.InventoryInteractions.instance.isUiActive == false) {
                if (Input.GetKeyDown(crouchKey)) {
                    Player.Movement.PlayerMovementManager.instance.Crouch(true);
                }
                if (Input.GetKeyUp(crouchKey)) {
                    Player.Movement.PlayerMovementManager.instance.Crouch(false);
                }

                // setup all camera inputs
                cameraInputX = cameraInput.x;
                cameraInputY = cameraInput.y;
                
                cameraInput.x = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
                cameraInput.y = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
            }
        }

        void LateUpdate() {
            Player.CameraManager.instance.HandleAllCameraMovement(); // make the camera do things
        }
    }
}

