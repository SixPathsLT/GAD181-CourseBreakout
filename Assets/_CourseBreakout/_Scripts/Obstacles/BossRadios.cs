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
    public GameObject player;

    private void Start()
    {
        count.gameObject.SetActive(false);
    }
    void Update()
    {

    }
    private void OnTriggerStay()
    {
        count.text = "Deactivating.... (" + (int)time + "%)";
        if (Input.GetKey(KeyCode.E))
        {
            time += Time.deltaTime;
            count.gameObject.SetActive(true);
            if (time >= 100)
            {
                Radio.SetActive(false);
                time = 0;
                count.gameObject.SetActive(false);
                player.GetComponent<PlayerControllerScript>().score++;
            }
        }
    }
    private void OnTriggerExit()
    {
        
    }
}
