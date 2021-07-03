using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Text itemName;
    public Text keyBind;
    public Text itemStatus;
    public Image abilityBarImage;

    Item item;

    // Start is called before the first frame update
    void Start()
    {
        itemName.gameObject.SetActive(false);
        itemStatus.gameObject.SetActive(false);
        abilityBarImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (item != null)
        {

            if (item.charges > 92)
            {
                itemStatus.text = "Full Charge";
                itemStatus.color = new Color(0, 255, 110, 255);
            }  else if (item.charges < 8) {
                itemStatus.color = Color.red;
                itemStatus.text = "Low Charge!";
            } else {
                itemStatus.text = (int) item.charges + 5 + "%";
                itemStatus.color = new Color(0, 255, 110, 255);
            }

            abilityBarImage.fillAmount = item.charges / 100f;

        }
    }


    public void SetItem(Item item)
    {
        this.item = item;

        itemName.text = item.itemName;

        itemName.gameObject.SetActive(true);
        itemStatus.gameObject.SetActive(true);
        abilityBarImage.gameObject.SetActive(true);

    }

    public Item GetItem()
    {
        return item;
    }
}
