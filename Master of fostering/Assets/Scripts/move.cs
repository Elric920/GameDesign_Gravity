using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public GameObject playerball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = playerball.transform.position;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0.1f);
    }
}
