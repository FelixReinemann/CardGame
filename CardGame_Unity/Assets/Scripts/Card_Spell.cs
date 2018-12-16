using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Cards/Spell")]
public class Card_Spell : Card_Data {
    void OnEnable() {
        type = card_types.Spell;
        range = cast_ranges.Global;
    }
    public override void Play_Card() {

    }
}



