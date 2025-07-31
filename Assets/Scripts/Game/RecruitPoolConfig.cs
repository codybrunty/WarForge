using UnityEngine;

[CreateAssetMenu(fileName = "RecruitPoolConfig", menuName = "Scriptable Objects/RecruitPoolConfig")]
public class RecruitPoolConfig : ScriptableObject {
    [SerializeField, Min(1)] private int incrementRecruitPoolSize = 1;
    public int IncrementRecruitPoolSize => incrementRecruitPoolSize; 
    
    [SerializeField, Min(1)] private int recruitPoolMaxSize = 1;
    public int RecruitPoolMaxSize => recruitPoolMaxSize;

}