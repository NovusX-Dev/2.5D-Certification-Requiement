using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    [SerializeField] private GameObject _objectToActivate;
    [SerializeField] private bool _isActive;
    [SerializeField] AudioClip _clipToPlay;
    [SerializeField] Light _lightToChange;
    [SerializeField] Color _color;

    private bool _boxAtPoint = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Crate_Pushable"))
        {
            var distance = Vector3.Distance(transform.position, other.transform.position);
            
            if (distance < 0.5f && !_boxAtPoint)
            {
                GetComponent<Renderer>().material.color = Color.green;
                other.GetComponent<Rigidbody>().isKinematic = true;
                _objectToActivate.SetActive(_isActive);

                other.GetComponent<BoxCollider>().enabled = false;

                SFXManager.Instance.PlaySFX(_clipToPlay, 1, 1,0);
                if (_lightToChange != null)
                {
                    _lightToChange.color = _color;
                }
                _boxAtPoint = true;
            }
        }
    }

}//class
