using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Crate_Pushable"))
        {
            var distance = Vector3.Distance(transform.position, other.transform.position);
            
            if (distance < 2.5f)
            {
                GetComponent<Renderer>().material.color = Color.green;
                other.GetComponent<Rigidbody>().isKinematic = true;
                objectToActivate.SetActive(true);
            }
        }
    }
}//class
