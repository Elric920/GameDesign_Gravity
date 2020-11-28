using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float m_horizontal;
    Vector2 m_position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontal = Input.GetAxis("Horizontal");
        m_position = transform.position;
        m_position.x += m_horizontal * Time.deltaTime * 1.5f;
        transform.position = m_position;

        
    }
}
