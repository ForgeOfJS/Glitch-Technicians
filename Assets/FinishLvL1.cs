using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLvL1 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<LvL1Obj>().CheckWin()) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
