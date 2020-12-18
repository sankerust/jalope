using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandling : MonoBehaviour
{

    void Update()
    {
        HandleObject();
    }
    public Transform DetectObject() {
        Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit)) {
            //print("I'm looking at " + hit.transform.name);
            //print(hit.transform.tag);
            return hit.transform;
        } else {
            return null;
            }
    }

    private bool IsPickupAble() {
        if (DetectObject() != null && DetectObject().tag == "PickupAble") {
            return true;
        } else {
            return false;
        }
    }

    private void HandleObject() {
        Transform objectInHand = DetectObject();
        if (Input.GetKeyDown("f")) {
            if (IsPickupAble()) {
                objectInHand.parent = this.transform;
                objectInHand.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        if (Input.GetKeyDown("g")) {
                print("g");
                objectInHand.parent = null;
                objectInHand.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
    }
}
