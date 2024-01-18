using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public Text liveText;
    // Update is called once per frame
    void Update()
    {
        liveText.text = PlayerStats.Lives.ToString() + " LIVES";
    }
}