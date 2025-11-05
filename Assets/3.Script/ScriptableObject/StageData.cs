using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObject/Stage/StageData", order = 1)]
public class StageData : ScriptableObject
{
    [SerializeField] private Vector2 _LimitMin;
    [SerializeField] private Vector2 _LimitMax;

    public Vector2 LimitMin => _LimitMin;
    public Vector2 LimitMax => _LimitMax;
}
