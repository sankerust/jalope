using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectHandling : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ObjectInfo;
    [SerializeField] float thrust = 3f;
    Transform objectInHand;

    bool isHolding = false;

    void Update()
    {
        HandleObject();
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

    private bool IsPickupAble() {
        if (RaycastObject() != null && RaycastObject().tag == "PickupAble") {
            //ObjectInfo.text = "f to pick up " + RaycastObject().name;
            return true;
        } else {
            return false;
        }
    }

    private bool IsUsable() {
        if (RaycastObject() != null && RaycastObject().tag == "Usable") {
            return true;
        } else {
            return false;
        }
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
            print("fire");
        }

        if (IsPickupAble()) {
            if (Input.GetKeyDown("f") && !isHolding)
            {
                PickupObject();
                return;
            }
        }

        if (IsUsable()) {
            print("usable item");
        }
    }

    private void PickupObject()
    {
        isHolding = true;
        objectInHand.parent = this.transform;
        objectInHand.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void DropObject()
    {
        isHolding = false;
        objectInHand.parent = null;
        objectInHand.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
