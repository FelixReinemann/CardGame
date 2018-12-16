using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hand : MonoBehaviour {

    public Player_Behaviour myPlayer;
    public GameObject cardPrefab;

    public List<GameObject> cardObjects;
    public float cardSpace = 5;
    public float cardAngle = 15;

    public bool lookAtCam;

    void Start() {
        if (lookAtCam) {
            //transform.forward = transform.position - Camera.main.transform.position;
            transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }

        //GetCardsFromPlayerHand();
        //ArrangeCardsInHand();
    }

    public void ClearHand() {
        foreach (Card_Behaviour card in GetComponentsInChildren<Card_Behaviour>()) {
            Destroy(card.gameObject);
        }
        cardObjects.Clear();
    }

    public void GetCardsFromPlayerHand() {
        for (int i = 0; i < myPlayer.cardsInHand.Count; i++) {
            AddCardToHand(myPlayer.cardsInHand[i]);
        }
    }

    public void AddCardToHand(Card_Data cardToCreate) {
        GameObject newCard = Instantiate(cardPrefab, transform);
        newCard.GetComponent<Card_Behaviour>().SetCard(cardToCreate);
        cardObjects.Add(newCard);
    }

    public void RemoveCardFromHand(GameObject cardObject) {
        Card_Data tempCard = cardObject.GetComponent<Card_Behaviour>().myCard;
        cardObjects.Remove(cardObject);
        Destroy(cardObject);
        myPlayer.cardsInHand.Remove(tempCard);
        myPlayer.cardsInGraveyard.Add(tempCard);
    }

    public void ArrangeCardsInHand() {
        for (int i = 0; i < cardObjects.Count; i++) {
            Vector3 newPosition = Vector3.right * (((cardObjects.Count-1) - i) * (-cardSpace)) - Vector3.forward*i*0.1f;
            cardObjects[i].transform.localPosition = newPosition;
            Quaternion newRotation = Quaternion.AngleAxis(cardAngle * ((cardObjects.Count - 1) - i), Vector3.forward );
            cardObjects[i].transform.localRotation = newRotation;
        }
    }

}
