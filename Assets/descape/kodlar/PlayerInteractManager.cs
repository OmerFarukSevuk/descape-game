using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractManager : MonoBehaviour
{
    [SerializeField] private KeyCode _interactKeyCode = KeyCode.E;

    [SerializeField] private Vector3 _center = Vector3.zero, _halfExtents = Vector3.zero;
    [SerializeField, Range(0, 30)] private int _interactableRayDistance = 0;
    [SerializeField] private LayerMask _whatIsInteractable = 0;
    [SerializeField] private Transform _pivot = null;

    

    private IInfo<GameObject> _lastInfoPanel = null;

    public GameObject a;

    private void Update()
    {
        

        if (Input.GetKeyDown(_interactKeyCode)) CheckInteractable();

        CheckInfo();
    }

    private void CheckInteractable()
    {
        // Physics.Raycast(_pivot.position, _pivot.forward, out _raycastHit, _interactableRayDistance, _whatIsInteractable);

        IInteractable _interactable = null;

        _interactable = GetOverlapType<IInteractable>(_pivot.position + _center, _halfExtents, transform.eulerAngles.y > 180 ? transform.eulerAngles.y - 360 : transform.eulerAngles.y);

        _interactable?.Interact();

        /*  for (int i = 0; i < _interactables.Count; i++)
              Debug.Log(_interactables[i].GetType());*/

        //if (!_raycastHit.collider) return;

        //if (_raycastHit.collider.TryGetComponent(out IInteractable interactable))
        // interactable.Interact();
    }

    private void CheckInfo()
    {
        SendRaycast(_pivot.position, _pivot.forward, out RaycastHit _infoRay, _interactableRayDistance);

        if (_lastInfoPanel != null)
            if (!_infoRay.collider)
            {
                _lastInfoPanel.OffPanel();
                _lastInfoPanel = null;
            }


        if (!_infoRay.collider) return;

        if (_infoRay.collider.TryGetComponent(out IInfo<GameObject> info))
        {
            info.OpenPanel();
            _lastInfoPanel = info;
        }
    }
    private void SendRaycast(Vector3 origin, Vector3 direction, out RaycastHit ray, LayerMask layerMask = default)
    {
        Physics.Raycast(origin, direction, out RaycastHit _ray, layerMask);

        ray = _ray;
    }

    private List<T> GetOverlapTypes<T>(Vector3 origin, Vector3 halfExtents) where T : IInteractable
    {
        Collider[] _colliders = Physics.OverlapBox(origin, halfExtents);

        List<T> _targetTypes = new();

        foreach (Collider collider in _colliders)
        {
            if (!collider.TryGetComponent(out T _t)) continue;

            _targetTypes.Add(_t);
        }

        return _targetTypes;
    }
    private T GetOverlapType<T>(Vector3 origin, Vector3 halfExtents, float forward) where T : IInteractable
    {

        Collider[] _colliders = Physics.OverlapBox(origin, halfExtents, Quaternion.Euler(new(0, forward, 0)));

        List<T> _targetTypes = new();
        foreach (Collider collider in _colliders)
        {
            if (!collider.TryGetComponent(out T _t)) continue;

            _targetTypes.Add(_t);

            break;
        }
        if (_targetTypes.Count >= 0)
        {
            Debug.Log(_targetTypes[0].GetType());
        }
        return _targetTypes[0];
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawWireCube(_pivot.position + _center, _halfExtents);
    }
}