using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Data : ScriptableObject {
    public string cardName;
    public card_types type;
    public int cost;
    public cast_ranges range;
    public string flavor_text;
    public Sprite card_image;

    public virtual void Play_Card() {

    }

}

public enum card_types { Unit, Spell, Trap }
public enum cast_ranges { Unit_Influence, Global }
