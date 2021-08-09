using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossRadios : MonoBehaviour
{
    public GameObject Radio;
    float time;
    public TextMeshProUGUI count;
    public int score;
    public PlayerControllerScript playercontroller;

    private void Start()
    {
        count.gameObject.SetActive(false);
    }
    void Update()
    {

    }
    private void OnTriggerStay()
    {
        count.text = "Deactivating " + time.ToString();
        if (Input.GetKey(KeyCode.E))
        {
            time += Time.deltaTime;
            count.gameObject.SetActive(true);
            if (time >= 10)
            {
                Radio.SetActive(false);
                time = 0;
                count.gameObject.SetActive(false);
                playercontroller.score++;
            }
        }
    }
    private void OnTriggerExit()
    {
        
    }
}
