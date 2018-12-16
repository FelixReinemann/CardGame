using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class Unit_Data : ScriptableObject {

	public string unitName;
    public Unit_Stats unitStats;
    public Sprite unitAppearance;

}
