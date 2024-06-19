using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;

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

    [Header("Sell")]
    [SerializeField] Image _sellItemDisplay;
    [SerializeField] TextMeshProUGUI _sellCostDisplay;

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

        SetBuyInfo();
    }

    private void SetBuyInfo()
    {
        _currentObject = _listOfObjects[_currentIdexBuy];
        _buyItemDisplay.sprite = _currentObject.image;
        _buyCostDisplay.text = _currentObject.price.ToString();
    }

    public void BuyItem()
    {
        if(PlayerInventory.instance.coins > _currentObject.price)
        {
            PlayerInventory.instance.AddItem(_currentObject);
            _listOfObjects.RemoveAt(_currentIdexBuy);
            ChangeObject(true);
        }
    }


    /// <summary>
    /// Closes all screns related to buying selling
    /// </summary>
    public void ExitAllScreens()
    {
        _buyScreen.SetActive(false);
        _sellScreen.SetActive(false);
        _listOfObjectsToSell.Clear();
        _currentIdexBuy = 0;
        _currentIdexSell = 0;
        _selectBuySell.SetActive(false);
    }

    public void OpenSellScreen()
    {
        _sellScreen.SetActive(true);
        _listOfObjectsToSell = PlayerInventory.instance.GetListOfItems();

        Debug.Log(_currentIdexSell);
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

    private void SetSellInfo()
    {
        _currentObject = _listOfObjectsToSell[_currentIdexSell];
        _sellItemDisplay.sprite = _currentObject.image;
        _sellCostDisplay.text = _currentObject.price.ToString();
    }

    /// <summary>
    /// Sets active both screens to select if you are buyng or selling
    /// </summary>
    public void ActivateOptionsScreen()
    {
        _selectBuySell.SetActive(true);
    }
}
