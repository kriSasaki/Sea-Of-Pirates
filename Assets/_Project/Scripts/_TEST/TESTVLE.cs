using UnityEngine;

[CreateAssetMenu]
public class TESTVLE : ScriptableObject
{
    [SerializeField] private int a = 1;

    public void Change()
    {
        Debug.Log("было " + a);
        a++;
        Debug.Log("стало " + a);

    }

    private void OnDestroy()
    {
        Debug.Log("Сдох " + a);
    }
}
