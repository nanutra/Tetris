using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    
    private float m_elapsedTime = 0;
    [SerializeField, Tooltip("temps entre chaques mouvement de cube")] private float m_tickRate = 1;
    [SerializeField] private GameObject m_cubePrefab;

    [SerializeField] private int m_boardHeight = 25;
    [SerializeField] private int m_boardWidth = 8;

    private List<CubeBehavior> m_cubes = new List<CubeBehavior>();
    

    public enum Status 
    {
        vide = 0,
        plein = 1,
        error = 10
    }
    
    private Status[,] m_board= new Status[8,25];

    private void Start()
    {
        ClearBoard();
        CreateCube();
    }

    private void Update()
    {
        //timer, si le temps est écoulé, la curent brique va déscendre
        m_elapsedTime += Time.deltaTime;

        if (m_elapsedTime >= m_tickRate)
        {
            MoveDown();
        }
    }
    
    private void ClearBoard()
    {
        m_board = new Status[8, 25];
        m_cubes.Clear();
    }
    
    protected override string GetSingletonName()
    {
        return "GameManager";
    }

    

    private void MoveDown()
    {
        //remet le timer a 0
        m_elapsedTime = 0; 
        
        //déplace la curent brique vers le bas
        for (int i = 0; i < m_cubes.Count; i++)
        {
            //call le move down des cubes
            m_cubes[i].MoveDown();
        }
    }

    public void MoveCube(int p_xOrigin, int p_yOrigin, int p_xDest, int p_yDest)
    {
        m_board[p_xOrigin, p_yOrigin] = Status.vide;
        m_board[p_xDest, p_yDest] = Status.plein;
    }

    public void CreateCube()
    {
        int xPos = Random.Range(0,m_boardWidth);
        Vector3 cubePos = new Vector3(xPos, m_boardHeight);
        if (m_board[xPos, m_boardWidth] == Status.plein)
        {
            //game Over
            Debug.Log("le jeu est terminé");
            return;
        }

        m_board[xPos, m_boardWidth] = Status.plein;
        GameObject go = Instantiate(m_cubePrefab, cubePos, Quaternion.identity);
        CubeBehavior cube = go.GetComponent<CubeBehavior>();
        m_cubes.Add(cube);//pas fini
        cube.SetPosition(xPos, m_boardHeight);
    }

    public Status GetStatus(int p_x, int p_y)
    {
        if (p_x < 0 || p_x >= m_boardWidth || p_y < 0 || p_y >m_boardHeight)
        {
            Debug.LogError("position n'existe pas");
            return Status.error;
        }

        return m_board[p_x, p_y];
    }
    
}
