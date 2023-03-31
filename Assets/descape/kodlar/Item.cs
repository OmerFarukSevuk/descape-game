using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Destroy(gameObject);
    }
}