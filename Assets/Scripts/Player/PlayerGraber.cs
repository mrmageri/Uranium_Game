using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerGraber : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCamera;
    public Collider playerCollider1;
    public Collider playerCollider2;
    public Transform holdPos;
    
    public GameObject heldObj; //object which we pick up

    
    
    //if you copy from below this point, you are legally required to like the video
    public float throwForce = 500f; //force at which the object is thrown at
    public float pickUpRange = 5f; //how far the player can pickup the object from
    private float rotationSensitivity = 1f; //how fast/slow the object is rotated in relation to mouse movement
    private Rigidbody heldObjRb;
    private Collider heldCol; //rigidbody of object we pick up
    private bool canDrop = true; //this is needed so we don't throw/drop object when rotating the object
    private int LayerNumber; //layer index

    private PlayerRotation playerRotationScript;

    //Reference to script which includes mouse movement of player (looking around)
    //we want to disable the player looking around when rotating the object
    //example below 
    private void Start()
    {
        LayerNumber = LayerMask.NameToLayer("HoldLayer"); //if your holdLayer is named differently make sure to change this ""
        playerRotationScript = playerCamera.GetComponent<PlayerRotation>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //change E to whichever key you want to press to pick up
        {
            if (heldObj == null) //if currently not holding anything
            {
                //perform raycast to check if player is looking at object within pickuprange
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    //make sure pickup tag is attached
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        //pass in object hit into the PickUpObject function
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if(canDrop == true)
                {
                    StopClipping(); //prevents object from clipping through walls
                    DropObject();
                }
            }
        }
        if (heldObj != null) //if player is holding object
        {
            MoveObject(); //keep object position at holdPos
            //RotateObject();
            if (Input.GetKeyDown(KeyCode.Q) && canDrop == true) //Mous0 (leftclick) is used to throw, change this if you want another button to be used)
            {
                StopClipping();
                ThrowObject();
            }

        }
    }

    public void DestroyItem()
    {
        IgnoreColliders(false);
        Destroy(heldObj);
        heldObj = null;
    }

    public void GiveItem(GameObject obj)
    {
        heldObj = Instantiate(obj, holdPos.position, Quaternion.identity, holdPos);
        PickUpObject(heldObj);
    }
    private void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.TryGetComponent(out Rigidbody rb)) //make sure the object has a RigidBody
        {
            heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = rb; //assign Rigidbody
            if (heldObj.TryGetComponent(out Collider col)) heldCol = col;
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform;
            heldObj.layer = LayerNumber; //change the object layer to the holdLayer
            for (int i = 0; i < heldObj.transform.childCount; i++)
            {
                heldObj.transform.GetChild(i).gameObject.layer = LayerNumber;
            }
            //make sure object doesnt collide with player, it can cause weird bugs
            heldObj.transform.rotation = new Quaternion(0f,0f,0f,0f);
            IgnoreColliders(true);
        }
    }
    private void DropObject()
    {
        //re-enable collision with player
        IgnoreColliders(false);
        heldObj.layer = 0; 
        for (int i = 0; i < heldObj.transform.childCount; i++)
        {
            heldObj.transform.GetChild(i).gameObject.layer = 0;
        }
        //object assigned back to default layer
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
    }
    private void MoveObject()
    {
        //keep object position the same as the holdPosition position
        heldObj.transform.position = holdPos.transform.position;
    }

    private void ThrowObject()
    {
        //same as drop function, but add force to object before undefining it
        IgnoreColliders(false);
        heldObj.layer = 0;
        for (int i = 0; i < heldObj.transform.childCount; i++)
        {
            heldObj.transform.GetChild(i).gameObject.layer = 0;
        }
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }
    /*private void RotateObject()
    {
        if (Input.GetKey(KeyCode.R))//hold R key to rotate, change this to whatever key you want
        {
            canDrop = false; //make sure throwing can't occur during rotating

            playerRotationScript.enabled = false;

            float YaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
            float XaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;
            //rotate the object depending on mouse X-Y Axis
            heldObj.transform.Rotate(Vector3.down, XaxisRotation);
            heldObj.transform.Rotate(Vector3.right, YaxisRotation);
        }
        else
        {
            playerRotationScript.enabled = true;
            canDrop = true;
        }
    }*/
    private void StopClipping() //function only called when dropping/throwing
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); //distance from holdPos to the camera
        //have to use RaycastAll as object blocks raycast in center screen
        //RaycastAll returns array of all colliders hit within the cliprange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            //change object position to camera position 
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
            //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
        }
    }

    private void IgnoreColliders(bool ignore)
    {
        Physics.IgnoreCollision(heldCol, playerCollider1, ignore);
        Physics.IgnoreCollision(heldCol, playerCollider2, ignore);
    }
}

