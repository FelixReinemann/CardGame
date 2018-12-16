using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player_Behaviour : MonoBehaviour {

    public string playerName;
    public CardList deck;

    public List<Card_Data> cardsLeftInDeck;

    public List<Card_Data> cardsInHand;

    public List<Card_Data> cardsInGraveyard;

    public List<Unit_Behaviour> myUnits;

    public bool activePlayer;

    public int manaPool;

    public int startHandSize = 5;

    public UI_Hand myHand;

    void Start() {
        DrawStarthand(startHandSize);
    }

    public void DrawStarthand(int amount) {
        cardsLeftInDeck.Clear();
        cardsInHand.Clear();
        myHand.ClearHand();
        ShuffleDeck();
        for (int i = 0; i < startHandSize; i++) {
            DrawCard();
        }
    }

    public void DrawCard() {
        if (cardsLeftInDeck.Count >= 1) {
            cardsInHand.Add(cardsLeftInDeck[0]);
            myHand.AddCardToHand(cardsLeftInDeck[0]);
            myHand.ArrangeCardsInHand();
            cardsLeftInDeck.RemoveAt(0);
        } else if (cardsInGraveyard.Count >= 1){
            Debug.Log("Shuffle Graveyard into Cardstack");
            ShuffleGraveyardIntoCardStack();
        } else {
            Debug.Log("No Cards Left in either Cardstack or Graveyard");
        }
    }

    
    public void ShuffleGraveyardIntoCardStack() {
        Card_Data tempCard;
        while(cardsInGraveyard.Count>0) {
            tempCard = cardsInGraveyard[0];
            cardsInGraveyard.RemoveAt(0);
            cardsLeftInDeck.Add(tempCard);
        }
        ShuffleDeck();
    }

    public void ShuffleDeck() {
        cardsLeftInDeck = deck.cards.OrderBy(x => Random.value).ToList();
    }

}

[CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
public class CardList : ScriptableObject {

    public List<Card_Data> cards;

}


