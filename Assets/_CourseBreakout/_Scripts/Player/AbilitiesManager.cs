using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{

    public GameObject grapplingHook;
    public GameObject companion;

    PlayerControllerScript player;

    enum Ability
    {
        GRAPPLING_HOOK,
        HEAL,
        SHIELD,
        BOOSTED_JUMP
    }

    Ability currentAbility;
    InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerControllerScript>();
        inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        InventorySlot slot = inventoryManager.GetSelectedSlot();

        if (slot == null)
            return;
        
        switch (slot.itemName.text) {
            case "Grapple Hook":
                currentAbility = Ability.GRAPPLING_HOOK;
                break;
            case "Heal":
                currentAbility = Ability.HEAL;
                break;
            case "Shield":
                currentAbility = Ability.SHIELD;
                break;
            case "Boosted Jump":
                currentAbility = Ability.BOOSTED_JUMP;
                break;
        }

        
        if (Input.GetMouseButtonDown(1)) {
            switch (currentAbility) {
                case Ability.GRAPPLING_HOOK:
                    grapplingHook.GetComponent<GrapplingGun>().StartGrapple();
                    break;
                case Ability.HEAL:
                    if (UseFullCharge(slot))
                        companion.GetComponent<Companion>().ActivateHeal();
                    break;
                case Ability.SHIELD:
                    if (UseFullCharge(slot))
                        companion.GetComponent<Companion>().ActivateShield();
                    break;
                case Ability.BOOSTED_JUMP:
                    if (UseFullCharge(slot)) {
                        player.jumpForce *= 1.6f;
                        player.maxHeight *= 1.6f;
                        GameObject.FindObjectOfType<NotificationsManager>().SendNotification("Your jump has been boosted for 10 seconds!");
                        Invoke("ResetBoostedJump", 10);
                    }             
                    break;
            }
        } else if (Input.GetMouseButtonUp(1)) {
            switch (currentAbility) {
                case Ability.GRAPPLING_HOOK:
                    grapplingHook.GetComponent<GrapplingGun>().StopGrapple();
                    break;
            }
        }
    }

    bool UseFullCharge(InventorySlot slot) {
        if (slot.GetItem().charges >= 100) {
            slot.GetItem().charges -= 100;
            return true;
        } else
            GameObject.FindObjectOfType<NotificationsManager>().SendNotification(slot.itemName.text + " requires 100% charge.");

        return false;
    }

    void ResetBoostedJump() {
        player.jumpForce /= 1.5f;
        player.maxHeight /= 1.5f;
    }
}
