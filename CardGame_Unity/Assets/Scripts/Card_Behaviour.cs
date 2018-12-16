using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card_Behaviour : MonoBehaviour {

    public Card_Data myCard;
    public TextMeshPro cost_text, name_text, type_text, abilities_text, flavor_text;
    public SpriteRenderer picture;
    public bool mouseOver;
    public Transform visualTransform;
    //public int indexInHand;
    float Y_OnMouseOver=1;
    Vector3 myPosition;
    Collider myCol;

    public void SetCard(Card_Data _card) {
        myCard = _card;
        cost_text.text = _card.cost.ToString();
        name_text.text = _card.cardName;
        type_text.text = _card.type.ToString();
        //abilities_text.text = _card.
        flavor_text.text = _card.flavor_text;
        picture.sprite = _card.card_image;
    }

    public void PlayCard() {
        Debug.Log("Played Card " + myCard.cardName);
        transform.parent.GetComponent<UI_Hand>().RemoveCardFromHand(this.gameObject);
        transform.parent.GetComponent<UI_Hand>().ArrangeCardsInHand();
    }

    void Start() {
        myPosition = visualTransform.localPosition;
        myCol = GetComponent<Collider>();
    }

    void Update() {
        if (mouseOver) {
            visualTransform.localPosition = myPosition + Vector3.up * Y_OnMouseOver;
        } else {
            visualTransform.localPosition = myPosition;
        }

    }

}





