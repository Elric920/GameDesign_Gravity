using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWall : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "stableStone")
        {
            player.SetCollisionTag(0, true);
        }
            
        else if (other.tag == "semiStableStone")
        {
            player.SetCollisionTag(1, true);
        }
            
        else if (other.tag == "nonStableStone")
        {
            player.SetCollisionTag(2, true);
        }

        else if(other.tag == "door")
        {
            player.SetCollisionTag(3, true);
            player.SetNearestDoorController(other.GetComponent<DoorController>());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "stableStone")
        {
            player.SetCollisionTag(0, false, false);
        }
            
        else if (other.tag == "semiStableStone")
        {
            player.SetCollisionTag(1,  false, false);
        }
            
        else if (other.tag == "nonStableStone")
        {
            player.SetCollisionTag(2,  false, false);
        }

        else if(other.tag == "door")
        {
            player.SetCollisionTag(3,  false, false);
            player.SetNearestDoorController(null);
        }
    }
}
