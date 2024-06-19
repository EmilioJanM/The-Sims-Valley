using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeItems : MonoBehaviour
{
    [SerializeField] SpriteRenderer _part;
    [SerializeField] Image _displayPart;
    [SerializeField] List<ItemObject> _listOfItems = new List<ItemObject>();

    private int _currentIndex;

    public void SetItems(List<ItemObject> list)
    {
        _listOfItems = list;

        if(_listOfItems.Count != 0)
            _displayPart.sprite = _listOfItems[_currentIndex].image;
    }

    public void ChangeItem(bool left)
    {
        if (left)
        {
            _currentIndex--;
            if (_currentIndex < 0)
                _currentIndex = _listOfItems.Count - 1;
        }
        else
        {
            _currentIndex++;
            if (_currentIndex >= _listOfItems.Count)
                _currentIndex = 0;
        }

        _displayPart.sprite = _listOfItems[_currentIndex].image;
        _part.sprite = _listOfItems[_currentIndex].image;
    }
}