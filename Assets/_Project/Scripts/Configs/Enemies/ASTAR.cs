using UnityEngine;

public class ASTAR : MonoBehaviour
{
    [SerializeReference, SubclassSelector] public BaseState BaseState;

    [SerializeField] public EnemyStateMachine enemyMachine;
}
