using System.Collections;

using TMPro;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{

    private int m_cubePosX;
    private int m_cubePosY;

    public bool m_isLocked = false;
    public void MoveDown()
    {
        if (m_isLocked)
        {
            
        }
        //on va vérifier la position, vérifier si la place d'en bas est libre
        
        //si la case est vide on descend le cube d'un cran
        //si y == 0 alors on lock le cube
        if (m_cubePosY == 0 || GameManager.Instance.GetStatus(m_cubePosX, m_cubePosY - 1) == GameManager.Status.vide)
        {
            //on lock le cube
            m_isLocked = true;
            GameManager.Instance.CreateCube();
            return;
        }
        m_cubePosY--;
        GameManager.Instance.MoveCube(m_cubePosX, m_cubePosY, m_cubePosX, --m_cubePosY);
    }

   

    public void SetPosition(int p_x, int p_y)
    {
        m_cubePosX = p_x;
        m_cubePosY = p_y;
        
    }
}
