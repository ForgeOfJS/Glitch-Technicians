using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvL2Obj : MonoBehaviour
{
    public float countDown = 180f;
    float timer;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        timer = countDown;
        PlayerPrefs.SetInt("UnlockedLevel", 2);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        text.text = timer.ToString("0.00") + "s";
        if (timer <= 0f)
        {
            SceneManager.LoadScene("Credits");
        }
    }
}
