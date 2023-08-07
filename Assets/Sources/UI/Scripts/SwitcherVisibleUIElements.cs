using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SwitcherVisibleUIElements : MonoBehaviour
{
    [SerializeField] private GameObject _objectActivate;
    [SerializeField] private List<GameObject> _objectsDeactivate = new List<GameObject>();

    private IPurchasable _purchasableActiveteObject;
    private List<IPurchasable> _purchasableDeactivateObject = new List<IPurchasable>();

    public void SwitchVisiblePanels()
    {
        _purchasableActiveteObject = _objectActivate.GetComponent<IPurchasable>();

        _purchasableDeactivateObject.Clear();

        foreach (var obj in _objectsDeactivate)
        {
            if (obj.GetComponent<IPurchasable>() != null)
                _purchasableDeactivateObject.Add(obj.GetComponent<IPurchasable>());
            else
                return;
        }

        if (_purchasableActiveteObject != null)
        {
            if (_purchasableActiveteObject.ReturnIsPurchased())
            {
                _purchasableActiveteObject.SetVisibleActivePanel(true);

                foreach (var obj in _purchasableDeactivateObject)
                    obj.SetVisibleActivePanel(false);
            }
            else
            {
                return;
            }
        }
        else
        {
            _objectActivate.SetActive(true);

            foreach (var obj in _objectsDeactivate)
                obj.SetActive(false);
        }
    }
}
