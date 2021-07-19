using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsManager : MonoBehaviour
{

    public Text notificationText;

    public void SendNotification(string message, float time = 3, float delay = 0)
    {
        StartCoroutine(ProcessNotification(message, time, delay));
    }

    IEnumerator ProcessNotification(string message, float time, float delay)
    {
        yield return new WaitForSeconds(delay);

        notificationText.text = message;
        yield return new WaitForSeconds(time);
        notificationText.text = "";
    }

}
