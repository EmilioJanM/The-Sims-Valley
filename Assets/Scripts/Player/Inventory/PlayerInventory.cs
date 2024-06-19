using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    [SerializeField] private GameObject _changeScreen;
    [SerializeField] public int coins;

    //Index: 0 = Hood, 1 = pelvis, 2 = left Shoulder, 3 = right shoulder, 4 = torso

    [SerializeField] private List<Image> _display = new List<Image>();
    [SerializeField] List<ItemObject> _listOfObjects = new List<ItemObject>();
    [SerializeField] TextMeshProUGUI _coinsText;

    [SerializeField] private List<SpriteRenderer> _equipmentToChange = new List<SpriteRenderer>();

    [Header("All Objects for each part")]
    [SerializeField] List<List<ItemObject>> _allListsObjectsToChange = new List<List<ItemObject>>();
    [SerializeField] List<ItemObject> _listOfHoods = new List<ItemObject>();
    [SerializeField] List<ItemObject> _listOfPelvis = new List<ItemObject>();
    [SerializeField] List<ItemObject> _listOfDholdL = new List<ItemObject>();
    [SerializeField] List<ItemObject> _listOfSholdR = new List<ItemObject>();
    [SerializeField] List<ItemObject> _listOfOTorso = new List<ItemObject>();

    [SerializeField] List<ChangeItems> _changeList = new List<ChangeItems>();


    private int _currentIndex;
    private int _currentItem;


    public void AddItem(ItemObject item)
    {
        //CheckForTypeOfItem(item);
        _listOfObjects.Add(item);

        coins -= item.price;
        _coinsText.text = coins.ToString();
    }

    

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;

        _coinsText.text = coins.ToString();



        _allListsObjectsToChange.Add(_listOfHoods);
        _allListsObjectsToChange.Add(_listOfPelvis);
        _allListsObjectsToChange.Add(_listOfDholdL);
        _allListsObjectsToChange.Add(_listOfSholdR);
        _allListsObjectsToChange.Add(_listOfOTorso);
    }

    public List<ItemObject> GetListOfItems()
    {
        return _listOfObjects;
    }

    public void SetItemsList()
    {
        for (int i = 0; i != _listOfObjects.Count; i++){
            int index = _listOfObjects[i].index;

            if(!_allListsObjectsToChange[index].Contains(_listOfObjects[i]))
                _allListsObjectsToChange[index].Add(_listOfObjects[i]);
        }

        for(int i = 0; i != _allListsObjectsToChange.Count; i++) {
            _changeList[i].SetItems(_allListsObjectsToChange[i]);
        }
    }

    public void ChangeItems(bool left)
    {
        if (left)
        {
            _currentIndex--;
            if (_currentIndex < 0)
                _currentIndex = _listOfObjects.Count - 1;
        }
        else
        {
            _currentIndex++;
            if (_currentIndex >= _listOfObjects.Count)
                _currentIndex = 0;
        }
    }

    public void SellItem(int index, int coins, ItemObject item)
    {
        _listOfObjects.RemoveAt(index);

        this.coins += coins;
        _coinsText.text = this.coins.ToString();

        CheckForTypeOfItem(item);
    }

    private void CheckForTypeOfItem(ItemObject item)
    {
        switch (item.index)
        {
            case 0:
                _listOfHoods.Remove(item);         
                break;
            case 1:
                _listOfPelvis.Remove(item);
                break;
            case 2:
                _listOfDholdL.Remove(item);
                break;
            case 3:
                _listOfSholdR.Remove(item);
                break;
            case 4:
                _listOfOTorso.Remove(item);
                break;
            default:
                Debug.Log("incorrect index");
                break;
        }
        _changeList[item.index].ResetPart();
    }
}
