using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SwitcherVisibleUIElements : MonoBehaviour
{
    [SerializeField] private GameObject _objectActivate;
    [SerializeField] private List<GameObject> _objectsDeactivate;

    private IPurchasable _purchasableActiveteObject;
    private List<IPurchasable> _purchasableDeactivateObject;

    private void Start()
    {
        if (_objectsDeactivate == null)
            _objectsDeactivate = new List<GameObject>();

        if (_purchasableDeactivateObject == null)
            _purchasableDeactivateObject = new List<IPurchasable>();
    }

    public void SwitchVisiblePanels()
    {
        if (_objectActivate != null)
            _purchasableActiveteObject = _objectActivate.GetComponent<IPurchasable>();

        _purchasableDeactivateObject.Clear();

        foreach (var obj in _objectsDeactivate)
        {
            if (obj.GetComponent<IPurchasable>() != null)
                _purchasableDeactivateObject.Add(obj.GetComponent<IPurchasable>());
        }

        if (_purchasableActiveteObject != null)
        {
            if (_purchasableActiveteObject.ReturnIsPurchased())
            {
                _purchasableActiveteObject.SetVisibleActivePanel(true);

                foreach (var obj in _purchasableDeactivateObject)
                    obj.SetVisibleActivePanel(false);
            }
        }
        else
        {
            if (_objectActivate != null)
                _objectActivate.SetActive(true);

            foreach (var obj in _objectsDeactivate)
            {
                if (obj != null)
                    obj.SetActive(false);
            }
        }
    }
}
