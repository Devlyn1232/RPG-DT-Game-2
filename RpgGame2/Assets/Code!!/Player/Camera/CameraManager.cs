using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform targetTransform; //The object the camera follows
    public Transform cameraPivot;     //The object the camera uses to pivot
    public Transform cameraTransform; //The transform of the actual camera object in the scene
    public LayerMask collisionLayers; // The layers we want our camera to collide with
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition; //Cannot edit z position with transform therefore use this method

    private float defaultPosition;
    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 2f;
    public float cameraPivotSpeed = 2f;
    public float cameraCollisionRadius = 0.2f;
    public float cameraCollisionOffSet = 0.4f; //How much the camera will jump off objects it colides with
    public float minimumCollisionOffSet = 0.3f;

    public float lookAngle; // Camera look up and down
    public float pivotAngle; // Camera look left and right
    public float minimumPivotAngle = -35;
    public float maximumPivotAngle = 35;

    public Vector2 cameraInput;
    public float cameraInputX;
    public float cameraInputY;
    public float sensX;
    public float sensY;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    private void Update() {
        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;
        
        cameraInput.x = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        cameraInput.y = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        handleAllCameraMovement();
    }

    public void handleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        handleCameraCollision();
    }

    private void FollowTarget()
    {
        //Move smoothly between 2 points
        Vector3 targetPosition = Vector3.SmoothDamp
            (transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;

    }
    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;
        lookAngle = lookAngle + (cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

        //This will make the camera rotate towards the rotation made by input
        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        //local rotation is important as it refers to the game object not rotation of object in the space
        cameraPivot.localRotation = targetRotation;

    }

    private void handleCameraCollision()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit; //Gives information about the collision
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if(Physics.SphereCast
            (cameraPivot.transform.position,cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition),collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition =- (distance - cameraCollisionOffSet);
        }

        if(Mathf.Abs(targetPosition) < minimumCollisionOffSet)
        {
            targetPosition = targetPosition - minimumCollisionOffSet;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;

    }
}
