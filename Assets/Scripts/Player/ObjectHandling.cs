using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectHandling : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ObjectInfo;
    [SerializeField] float thrust = 3f;
    [SerializeField] float handsLenght = 2f;
    [SerializeField] AudioClip pickupSound;
    Transform objectInHand;
    AudioSource audioSource;

    bool isHolding = false;
    float distanceToObject;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(ObjectInfo) {
            ObjectInfo.text = null;
        }
        
        
        if(IsInRange()) {
            ObjectInfo.text = RaycastObject().gameObject.name;
            HandleObject();
        }
        
    }
    public Transform RaycastObject() {
        Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit)) {
            return hit.transform;
        } else {
            return null;
            }
    }
    private bool IsInRange() {
        if(RaycastObject() == null) {return false;}
        distanceToObject = Vector3.Distance(this.transform.position, RaycastObject().position);
        return (distanceToObject <= handsLenght);
    }

    private bool IsPickupAble() {
        if (RaycastObject() != null && RaycastObject().tag == "PickupAble") {
            //ObjectInfo.text = "f to pick up " + RaycastObject().name;
            return true;
        } else {
            return false;
        }
    }

    private void UseObject() {
        if (RaycastObject() != null) {
            IUsable usable = RaycastObject().GetComponent<IUsable>();
            if (Input.GetKeyDown("e") && !isHolding && usable != null) {
                usable.Use();
            }
        } return;
    }

    private void HandleObject() {
        objectInHand = RaycastObject();
        if (Input.GetKeyDown("f") && isHolding)
        {
            DropObject();
            return;
        }

        if (isHolding && Input.GetButtonDown("Fire1")) {
            DropObject();
            objectInHand.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * thrust);
        }

        if (IsPickupAble()) {
            if (Input.GetKeyDown("f") && !isHolding)
            {
                PickupObject();
                return;
            }
        }
        UseObject();
    }

    private void PickupObject()
    {
        isHolding = true;
        objectInHand.parent = this.transform;
        objectInHand.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        audioSource.PlayOneShot(pickupSound);
    }

    private void DropObject()
    {
        isHolding = false;
        objectInHand.parent = null;
        objectInHand.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        audioSource.PlayOneShot(pickupSound);
    }
}
