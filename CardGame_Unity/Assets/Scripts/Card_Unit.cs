using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Cards/Unit")]
public class Card_Unit : Card_Data {
    public Unit_Data unit;
    public int unitCount;
    void OnEnable() {
        type = card_types.Unit;
        range = cast_ranges.Unit_Influence;
    }

    public override void Play_Card() {

    }
}
