using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour {

    public bool isLinked = false;
    public ItemSlot itemSlot;
    public Image slotIcon;
    public TextMeshProUGUI slotAmount;
    private GameObject _selectedToggle;
    public Button toggleButton;
    //World world;

    private void Awake() {

        //world = GameObject.Find("World").GetComponent<World>();
        _selectedToggle = transform.GetChild(0).gameObject;
        toggleButton = GetComponent<Button>();
        toggleButton.onClick.AddListener(OnClick);
    }
    public void HasSelected (bool selected) {
        _selectedToggle.SetActive(selected);
    }
    public void OnClick () {
        if (itemSlot == null||HasItem == false)
            return;
        SlotManager.Instance.SelectItemSlot(this);
    }
    public bool HasItem {

        get {

            if (itemSlot == null)
                return false;
            else
                return itemSlot.HasItem;

        }

    }

    public void Link (ItemSlot _itemSlot) {

        itemSlot = _itemSlot;
        isLinked = true;
        itemSlot.LinkUISlot(this);
        UpdateSlot();

    }

    public void UnLink () {

        itemSlot.unLinkUISlot();
        itemSlot = null;
        UpdateSlot();

    }

    public void UpdateSlot() {

        if (itemSlot != null && itemSlot.HasItem) {

            slotIcon.sprite = WorldManager.Instance.allBockTypes[itemSlot.stack.id].icon;
            slotAmount.text = itemSlot.stack.amount.ToString();
            slotIcon.enabled = true;
            slotAmount.enabled = true;
            toggleButton.interactable = true;

        } else
            Clear();

    }

    public void Clear () {

        slotIcon.sprite = null;
        slotAmount.text = "";
        slotIcon.enabled = false;
        slotAmount.enabled = false;
        toggleButton.interactable = false;

    }

    private void OnDestroy() {

        if (itemSlot != null)
            itemSlot.unLinkUISlot();

    }

}

public class ItemSlot {

    public ItemStack stack = null;
    private UIItemSlot uiItemSlot = null;

    public bool isCreative;

    public ItemSlot (UIItemSlot _uiItemSlot) {

        stack = null;
        uiItemSlot = _uiItemSlot;
        uiItemSlot.Link(this);

    }

    public ItemSlot (UIItemSlot _uiItemSlot, ItemStack _stack) {

        stack = _stack;
        uiItemSlot = _uiItemSlot;
        uiItemSlot.Link(this);

    }

    public void LinkUISlot (UIItemSlot uiSlot) {

        uiItemSlot = uiSlot;

    }

    public void unLinkUISlot () {

        uiItemSlot = null;

    }

    public void EmptySlot () {

        stack = null;
        if (uiItemSlot != null)
            uiItemSlot.UpdateSlot();

    }

    public int Take (int amt) {

        if (amt > stack.amount) {
            int _amt = stack.amount;
            EmptySlot();
            return _amt;
        } else if (amt < stack.amount) {
            stack.amount -= amt;
            uiItemSlot.UpdateSlot();
            return amt;
        } else {
            EmptySlot();
            return amt;
        }

    }

    public ItemStack TakeAll() {

        ItemStack handOver = new ItemStack(stack.id, stack.amount);
        EmptySlot();
        return handOver;

    }

    public void InsertStack (ItemStack _stack) {

        stack = _stack;
        uiItemSlot.UpdateSlot();

    }

    public bool HasItem {

        get {

            if (stack != null)
                return true;
            else
                return false;

        }

    }

}
