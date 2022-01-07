using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class inventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int slotID;

    [SerializeField] initializeCharacter initialize;
    characterEvent events;

    [SerializeField] GameObject imageContainer;
    [SerializeField] GameObject textLabelObject;
    TextMeshProUGUI textLabel;
    Image itemImage;

    [SerializeField] Sprite empty;

    public void Start()
    {
        itemImage = imageContainer.GetComponent<Image>();
        textLabel = textLabelObject.GetComponent<TextMeshProUGUI>();

        initialize.onInitializeCharacter += initializeCharacterCallback;
    }

    private void initializeCharacterCallback(characterEvent events, CharacterStatistics stats)
    {
        Debug.Log("initialized hehe");
        this.events = events;
    }

    void updateImage(Sprite itemSprite)
    {
        itemImage.sprite = itemSprite;
    }

    void updateCount(int count)
    {
        textLabel.SetText(count.ToString());
    }

    public void updateDisplay(Sprite itemSpirte, int count)
    {
        updateImage(itemSpirte);
        updateCount(count);
    }

    public void setEmpty()
    {
        updateImage(empty);
        textLabel.SetText("");
    }

    /**
     * when player click on the slot, the slot should invoke a call to the inventory system
     * where the event will be processed
     * 
     * The event call should contain the number the inventory slot clicked is
     */
    public void OnPointerClick(PointerEventData eventData)
    {

        Debug.Log("clicked at:" + slotID);

        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //right clicked
            events.callOnSlotLeftClicked(slotID);
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            //left clicked
            events.callOnSlotRightClicked(slotID);
        }
    }
}
