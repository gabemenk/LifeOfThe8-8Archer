using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OneHS : MonoBehaviour
{
    [SerializeField] TMP_Text tex;
    // Start is called before the first frame update
    void Start()
    {
        tex.text = "" + (Mathf.FloorToInt(MainManager.Instance.timeRemaining) + (-5 * Mathf.FloorToInt(MainManager.Instance.reindeerHits)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
