using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject Object;
    public Vector3 ObjectLocation;
    public Vector3 ObjectRotation;
    private void OnTriggerStay(Collider other) {
        // Debug.Log("enter");
        if (other.CompareTag("Player") && !other.GetComponent<PlayerController>().isHolding) {
            GameObject item = Instantiate(Object, Vector3.zero, Quaternion.Euler(0, 0, 0)) as GameObject;
            // GameObject item = Instantiate(Object, ObjectLocation, ObjectRotation) as GameObject;            
            Transform destination = other.transform.Find("gnome_model").Find("lower_body").Find("upper_body").Find("upper_arm.R").Find("lower_arm.R").Find("hand.R");         
            other.GetComponent<PlayerController>().isHolding = true; 
            item.transform.parent = destination.transform;
            item.transform.localPosition = ObjectLocation;
            item.transform.localEulerAngles = ObjectRotation;
            // Debug.Log(destination);
            // Debug.Log("Object spawned");
        }
    }
}
