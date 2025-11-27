using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public virtual void Interact(PlayerController player) 
    {
        Destroy(gameObject); 
    }
}