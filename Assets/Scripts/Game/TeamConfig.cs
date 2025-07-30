using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TeamConfig", menuName = "Scriptable Objects/TeamConfig")]
public class TeamConfig : ScriptableObject {
    [SerializeField, Min(1)]private int maxTeamSize = 1;
    public int MaxTeamSize => maxTeamSize;

}
