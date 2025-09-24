using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
public enum itemType
{
    FOOD, RAGE, CLEAN, SNAILS, NONE
}
public class PlayerMovement : MonoBehaviour
{
    Camera mainCam;
    private InputAction moveInputs;
    private InputAction interactInput;
    public GameObject GrabIndicator;
    public GameObject UseIndicator;
    private Rigidbody rb;
    public float speed = 5f;
    private GameObject heldItem = null;
    private itemType heldItemType = itemType.NONE;
    private GameObject snailsItem;

    private void Start()
    {
        moveInputs = InputSystem.actions.FindAction("Move");
        interactInput = InputSystem.actions.FindAction("Interact");
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        snailsItem = GameObject.FindGameObjectWithTag("Snails");
    }
    private void Update()
    {
        GrabIndicator.SetActive(false);
        UseIndicator.SetActive(false);
        Movement();

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out RaycastHit hit, 5f))
        {
            if (heldItem == null)
            {
                if (hit.collider.CompareTag("Can") || hit.collider.CompareTag("Snails"))
                {
                    // Highlight object or show UI prompt
                    GrabIndicator.SetActive(true);
                }
                else
                {
                    GrabIndicator.SetActive(false);
                }
            }
            else
            {
                if (hit.collider.CompareTag("FishTank"))
                {
                    UseIndicator.SetActive(true);
                }
                else
                {
                    UseIndicator.SetActive(false);
                }
            }
            if (interactInput.WasPressedThisFrame())
            {
                Interact(hit);
            }
        }
        else if (interactInput.WasPressedThisFrame())
        {
            DropItem();
        }

    }

    void Movement()
    {
        Vector3 move = moveInputs.ReadValue<Vector2>();
        Vector3 camForward = mainCam.transform.forward;
        camForward.y = 0; // Keep movement on the XZ plane
        Vector3 camRight = mainCam.transform.right;
        Vector3 newPos = transform.position + (camForward * move.y + camRight * move.x) * speed * Time.deltaTime;
        rb.MovePosition(newPos);
    }

    void Interact(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Can"))
        {
            GameObject can = hit.collider.gameObject;       
            if (heldItemType == itemType.NONE)
            {
                can.GetComponent<CanHandler>().EquipCan();
                heldItemType = (itemType)can.GetComponent<CanHandler>().CanType;
                heldItem = can;
            }
            else
            {
                DropItem();
                can.GetComponent<CanHandler>().EquipCan();
                heldItemType = (itemType)can.GetComponent<CanHandler>().CanType;
                heldItem = can;
            }
        }
        else if (hit.collider.CompareTag("Snails"))
        {
            GameObject snails = hit.collider.gameObject;
            if (heldItemType == itemType.NONE)
            {
                snails.GetComponent<Snails>().Equip();
                heldItemType = itemType.SNAILS;
                heldItem = snails;
            }
            else
            {
                DropItem();
                snails.GetComponent<Snails>().Equip();
                heldItemType = itemType.SNAILS;
                heldItem = snails;
            }
        }
        else if (hit.collider.CompareTag("FishTank"))
        {
            FishTank tank = hit.collider.GetComponent<FishTank>();
            switch (heldItemType)
            {
                case itemType.FOOD:
                    tank.FeedFish(25);
                    ConsumeItem();
                    break;
                case itemType.CLEAN:
                    tank.DetoxifyTank(25);
                    ConsumeItem();
                    break;
                case itemType.RAGE:
                    tank.RageFish();
                    ConsumeItem();
                    break;
                case itemType.SNAILS:
                    tank.ToggleSnails();
                    ConsumeItem();
                    break;
                default:
                    if (tank.hasSnails)
                    {
                        DropItem();
                        tank.ToggleSnails();
                        snailsItem.SetActive(true);
                        heldItemType = itemType.SNAILS;
                        heldItem = snailsItem;
                        heldItem.GetComponent<Snails>().Equip();
                    }
                    else
                    {
                        DropItem();
                    }
                    break;
            }
        }
        else
        {
            DropItem();
        }
    }
    void DropItem()
    {
        // Drop item logic here
        if (heldItem != null)
        {
            if (heldItemType == itemType.SNAILS)
            {
                heldItem.GetComponent<Snails>().Unequip();
                heldItemType = itemType.NONE;
                heldItem = null;
            }
            else
            {
                heldItem.GetComponent<CanHandler>().UnequipCan();
                heldItemType = itemType.NONE;
                heldItem = null;
            }
        }
    }

    void ConsumeItem()
    {
        if (heldItem != null && heldItemType == itemType.SNAILS)
        {
            heldItem.GetComponent<Snails>().Unequip();
            heldItemType = itemType.NONE;
            heldItem.SetActive(false);
            heldItem = null;
        }
        if (heldItemType != itemType.NONE)
        {
            heldItemType = itemType.NONE;
            Destroy(heldItem, 0.1f);
            heldItem = null;
        }
    }
}
