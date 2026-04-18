using UnityEngine;
using UnityEngine.Tilemaps;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TilemapRenderer>().enabled = false;
    }

}
