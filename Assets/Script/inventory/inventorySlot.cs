using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inventorySlot : MonoBehaviour
{
    [SerializeField] GameObject imageContainer;
    [SerializeField] GameObject textLabelObject;
    TextMeshProUGUI textLabel;
    Image itemImage;

    public void Start()
    {
        itemImage = imageContainer.GetComponent<Image>();
        textLabel = textLabelObject.GetComponent<TextMeshProUGUI>();
    }

    public void onClickAction()
    {
        Debug.Log("player clicked on" + this.name);
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
}
