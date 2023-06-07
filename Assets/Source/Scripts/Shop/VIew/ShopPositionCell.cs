using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopPositionCell : MonoBehaviour
{
    [SerializeField] private Image _rewardIcon;
    [SerializeField] private Image _recourseIcon;
    [SerializeField] private Image _tradeItemIcon;
    [SerializeField] private TMP_Text _costText;
    
    [SerializeField] private Button _showCraftButton;
    [SerializeField] private Button _buyButton;
    
    [SerializeField] private int _cost;
    [SerializeField] private ItemInfo _reward;
    [SerializeField] private ItemInfo _recourse;
    [SerializeField] private ItemInfo _tradeItem;

    public event UnityAction<ItemInfo> Bought; 

    public bool HasTradeItem { get; private set; }
    public bool HasRecourse { get; private set; }
    
    private void OnEnable()
    {
        _showCraftButton.onClick.AddListener(OnShowCraftButtonClick);
        _buyButton.onClick.AddListener(OnBuyButtonClick);
    }

    private void OnDisable()
    {
        _showCraftButton.onClick.RemoveListener(OnShowCraftButtonClick);
        _buyButton.onClick.RemoveListener(OnBuyButtonClick);
    }

    public void Init(int cost, ItemInfo reward, ItemInfo recourse, ItemInfo tradeItem, bool canBuy)
    {
        _cost = cost;
        _reward = reward;
        _recourse = recourse;
        _tradeItem = tradeItem;
        _buyButton.interactable = canBuy;
        HasTradeItem = true;
        HasRecourse = true;
        
        DrawUI();
    }

    public void Init(int cost, ItemInfo reward, bool canBuy)
    {
        _cost = cost;
        _reward = reward;
        _buyButton.interactable = canBuy;
        
        HasTradeItem = false;
        HasRecourse = false;
        
        DrawUI();
    }

    public void Init(int cost, ItemInfo reward, ItemInfo recourse, bool canBuy)
    {
        _cost = cost;
        _reward = reward;
        _recourse = recourse;
        _buyButton.interactable = canBuy;
        
        HasTradeItem = false;
        HasRecourse = true;
        
        DrawUI();
    }
    
    public void Init(ItemInfo reward, int cost, ItemInfo tradeItem, bool canBuy)
    {
        _cost = cost;
        _reward = reward;
        _tradeItem = tradeItem;
        _buyButton.interactable = canBuy;
        
        HasTradeItem = true;
        HasRecourse = false;
        
        DrawUI();
    }

    private void DrawUI()
    {
        _rewardIcon.sprite = _reward.Icon;
        _costText.text = _cost.ToString();

        if (HasRecourse)
            _recourseIcon.sprite = _recourse.Icon;
        else 
            _recourseIcon.gameObject.SetActive(false);

        if (HasTradeItem)
            _tradeItemIcon.sprite = _tradeItem.Icon;
        else 
            _tradeItemIcon.gameObject.SetActive(false);
    }
    
    private void OnShowCraftButtonClick()
    {
        
    }

    private void OnBuyButtonClick()
    {
        Bought?.Invoke(_reward);
    }
}
