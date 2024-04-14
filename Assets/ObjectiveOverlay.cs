using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveOverlay : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI taskText;

    void Start()
    {
        Invoke("DisableText", 5f);
    }

   void DisableText()
    {
        objectiveText.enabled = false;
        taskText.enabled = false;
    }
}
