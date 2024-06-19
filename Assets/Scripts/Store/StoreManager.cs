using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;
    public static bool inStore;

    [SerializeField] ItemObject _currentObject;

    [Header("Screens")]
    [SerializeField] GameObject _buyScreen;
    [SerializeField] GameObject _sellScreen;
    [SerializeField] GameObject _selectBuySell;

    [Header("Items")]
    [SerializeField] List<ItemObject> _listOfObjects = new List<ItemObject>();
    [SerializeField] List<ItemObject> _listOfObjectsToSell = new List<ItemObject>();

    [Header("Buy")]
    [SerializeField] Image _buyItemDisplay;
    [SerializeField] TextMeshProUGUI _buyCostDisplay;
    [SerializeField] TextMeshProUGUI _statusButtonBuy;

    [Header("Sell")]
    [SerializeField] Image _sellItemDisplay;
    [SerializeField] TextMeshProUGUI _sellCostDisplay;
    [SerializeField] TextMeshProUGUI _statusButtonSell;

    int _currentIdexBuy = 0;
    int _currentIdexSell = 0;

    private void Start()
    {
        if (instance != null)
        {
            Debug.Log("There is already an instance of store manager");
            Destroy(gameObject);
        }

        instance = this;
        SetBuyInfo();
    }

    public void DeactivateDialogue()
    {
        _selectBuySell.SetActive(false);
        DialogueInk.instance.ExitDialogue();
    }

    /// <summary>
    /// Changes the object when selecting what to buy
    /// </summary>
    /// <param name="left"></param>
    public void ChangeObject(bool left)
    {
        if (left)
        {
            _currentIdexBuy--;
            if (_currentIdexBuy < 0)
                _currentIdexBuy = _listOfObjects.Count - 1;
        }
        else
        {
            _currentIdexBuy++;
            if (_currentIdexBuy >= _listOfObjects.Count)
                _currentIdexBuy = 0;            
        }

        if(_listOfObjects.Count == 0)
        {
            _statusButtonBuy.text = "All items sold!";
            _buyItemDisplay.sprite = null;
            _buyCostDisplay.text = "0";
            _currentObject = null;
        }
        else
        {
            SetBuyInfo();
        }
    }

    public void SetBuyInfo()
    {
        _currentObject = _listOfObjects[_currentIdexBuy];
        _buyItemDisplay.sprite = _currentObject.image;
        _buyCostDisplay.text = _currentObject.price.ToString();

        if(_listOfObjects.Count != 0)
            _statusButtonBuy.text = "Buy!";
    }

    public void BuyItem()
    {
        if (_listOfObjects.Count == 0)
            return;

        if(PlayerInventory.instance.coins > _currentObject.price)
        {
            PlayerInventory.instance.AddItem(_currentObject);
            _listOfObjects.RemoveAt(_currentIdexBuy);
            ChangeObject(true);
        }
        else if(PlayerInventory.instance.coins > _currentObject.price && _listOfObjects.Count > 0)
        {
            _statusButtonBuy.text = "You have no money!";
        }
        
        if(_listOfObjects.Count == 0)
        {
            _statusButtonBuy.text = "All items sold!";
        }
    }


    /// <summary>
    /// Closes all screns related to buying selling
    /// </summary>
    public void ExitAllScreens()
    {
        _buyScreen.SetActive(false);
        _sellScreen.SetActive(false);
        //_listOfObjectsToSell.Clear();
        _currentIdexBuy = 0;
        _currentIdexSell = 0;
        _selectBuySell.SetActive(false);
    }

    public void OpenSellScreen()
    {
        _sellScreen.SetActive(true);
        _listOfObjectsToSell = PlayerInventory.instance.GetListOfItems();

        _statusButtonSell.text = "Sell";

        SetSellInfo();
    }

    /// <summary>
    /// Changes between the objects in the sell list(the objects that the player has) in order to sell them
    /// </summary>
    /// <param name="left"></param>
    public void ChangeObjectSell(bool left)
    {
        if (left)
        {
            _currentIdexSell--;

            if (_currentIdexSell < 0)
                _currentIdexSell = _listOfObjectsToSell.Count - 1;
        }
        else
        {
            _currentIdexSell++;
            if (_currentIdexSell >= _listOfObjectsToSell.Count)
                _currentIdexSell = 0;
        }

        SetSellInfo();
    }

    public void SellItem()
    {
        if (_listOfObjectsToSell.Count == 0)
            return;

        _listOfObjects.Add(_listOfObjectsToSell[_currentIdexSell]);
        PlayerInventory.instance.SellItem(_currentIdexSell, (int)(_currentObject.price * 0.8), _listOfObjectsToSell[_currentIdexSell]);
        _currentIdexSell = 0;

        if(_listOfObjectsToSell.Count != 0)
            ChangeObjectSell(true);
        else
        {
            _sellItemDisplay.sprite = null;
            _sellCostDisplay.text = "";
            _statusButtonSell.text = "You have no items to sell";
            _currentObject = null;
        }
    }

    private void SetSellInfo()
    {
        if (_listOfObjectsToSell.Count == 0)
        {
            _statusButtonSell.text = "You have no items to sell";
            return;
        }

        _currentObject = _listOfObjectsToSell[_currentIdexSell];
        _sellItemDisplay.sprite = _currentObject.image;
        _sellCostDisplay.text = (_currentObject.price*0.8).ToString();
    }

    /// <summary>
    /// Sets active both screens to select if you are buyng or selling
    /// </summary>
    public void ActivateOptionsScreen()
    {
        _selectBuySell.SetActive(true);
    }
}
