using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable, IInfo<GameObject>
{   
    public float openAngle = 90f;
    public float closedAngle = 0f;
    public float smooth = 65f;
    public bool isOpen = false;
    public float doorOpenDistance = 1f; //Karakterin kapýya ne kadar yakýn olmasý gerektiðini belirlemek için

    private bool _stillOpening = false;

    [SerializeField] private GameObject _infoPanel = null;
    public GameObject InfoPanel => _infoPanel;

    public void Interact()
    {
        StartCoroutine(nameof(CO_Interact));
    }

    private IEnumerator CO_Interact()
    {
        if (_stillOpening) yield break;

        _stillOpening = true;

        float _targetAngleY = isOpen ? closedAngle : openAngle;

        Vector3 _targetAngle = new(transform.eulerAngles.x, _targetAngleY, transform.eulerAngles.z);

        isOpen = !isOpen;

        while (transform.eulerAngles != _targetAngle)
        {
            transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, _targetAngle, smooth * Time.deltaTime);

            yield return null;
        }

        _stillOpening = false;
    }

    public void OpenPanel()
    {
        _infoPanel.SetActive(true);
    }
    public void OffPanel()
    {
        _infoPanel.SetActive(false);
    }
}