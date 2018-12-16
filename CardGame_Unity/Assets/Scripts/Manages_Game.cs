using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manages_Game : MonoBehaviour {
	public static Manages_Game singleton;
	void Awake() {
        if (Manages_Game.singleton == null) {
			Manages_Game.singleton = this;
        } else {
            Debug.Log("Multiple manages game");
            Destroy(this);
        }
        players = FindObjectsOfType<Player_Behaviour>();
    }

    public Player_Behaviour[] players;
    public bool drawFullHandAtTurnStart = true;
    public bool clearHandAtTurnStart = true;
    public int handSize = 5;

    public void StartPlayerTurn(Player_Behaviour player) {
        for (int i = 0; i < player.myUnits.Count; i++) {
            player.myUnits[i].exhausted = false;
        }

        if (clearHandAtTurnStart) {
            while(player.cardsInHand.Count>0) {
                player.myHand.RemoveCardFromHand(player.myHand.cardObjects[0]);
            }
        }
        if (drawFullHandAtTurnStart) {
            while (player.cardsInHand.Count < handSize) {
                player.DrawCard();
            }
        } else {
            player.DrawCard();
        }
    }

    public void ClickEndTurn() {
        StartPlayerTurn(players[0]);
    }

}
