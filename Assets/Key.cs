using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int key;
    public MeshRenderer meshRenderer;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            meshRenderer.enabled = false;
            other.transform.GetComponent<LvL1Obj>().GetKey(key);
            Destroy(this.gameObject, 1f);
            transform.GetComponent<AudioSource>().PlayOneShot(AudioManager.Instance.itemEquip);
        }
    }
}
