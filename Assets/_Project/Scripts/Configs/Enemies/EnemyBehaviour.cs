using UnityEngine;

namespace Project.Configs.Enemies
{
    [CreateAssetMenu(fileName = "EnemyBehaviour", menuName = "Configs/Enemies/Behaviour")]
    public class EnemyBehaviour: ScriptableObject
    {
        [SerializeReference] public BaseState BaseState = new IdleState();
    }
}

[System.Serializable]
public class BaseState
{
    [SerializeField] float a;
}

[System.Serializable]
public class IdleState : BaseState
{
    [SerializeField] float b;
}

[System.Serializable]
public class BattleState: BaseState
{
    [SerializeField] float b;
}

[System.Serializable]
public class DeadState: BaseState
{
    [SerializeField] float b;
}
