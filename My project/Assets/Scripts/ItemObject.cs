using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] itemSO data;

    public int GetPoint()
    {
        return data.point;
    }
}
