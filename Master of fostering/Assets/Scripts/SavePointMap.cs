using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class SavePointMap
    {
        string m_Name;
        Vector2 m_Position;
        public SavePointMap(string name, Vector2 position)
        {
            m_Name = name;
            m_Position = position;
        }
        public Vector2 GetSavePointPosition()
        {
            return m_Position;
        }
        public string GetName()
        {
            return m_Name;
        }
    }
}
