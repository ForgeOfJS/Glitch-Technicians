using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LvL1Obj : MonoBehaviour
{
    bool[] goals = {false, false, false};

    public void GetKey(int key)
    {
        goals[key] = true;
    }

    public bool CheckWin()
    {
        for (int i = 0; i < goals.Length; i++)
        {
            if (!goals[i]) return false;
        }
        return true;
    }

}
