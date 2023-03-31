using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class alerter : MonoBehaviour
{
    public float timer = 10f;
    public GameObject image;

    private void Start()
    {
        image.SetActive(true);
        
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            image.SetActive(false);
        }
    }
}