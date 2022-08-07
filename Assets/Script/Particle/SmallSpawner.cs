using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSpawner : MonoBehaviour
{
    private float f_random = 0f;
    //생성할 큐브 프리팹
    public GameObject m_objCube;

    //파동의 시작점
    private static Vector3[] m_arrAni0_StartPos = new Vector3[SmallWave_Ani0.iWaveNum];
    private static Vector3 m_vCreatPos = Vector3.zero;

    //파동 시작 시간
    private static float[] m_arrAni0_StartTime = new float[SmallWave_Ani0.iWaveNum];
    //가로 큐브 갯수
    private static int m_iTileX = 10; //20
    //세로 큐브 갯수
    private static int m_iTileZ = 10; //20
    //생성할 큐브 총합
    private static int m_iTileCnt = 0;
    public static float Get_CubeX() { return (float)((m_iTileCnt % m_iTileX - m_iTileX / 2)); }
    public static float Get_CubeZ() { return (float)((m_iTileCnt / m_iTileX - m_iTileZ / 2)); }
    public static Vector3 Get_AniStartPos(int iIndex) { return m_arrAni0_StartPos[iIndex]; }
    public static float Get_StartTime(int iIndex) { return m_arrAni0_StartTime[iIndex]; }
    public static Vector3 Get_CreatPos() { return m_vCreatPos; }

    void Start()
    {
        for (int i = 0; i < SmallWave_Ani0.iWaveNum; ++i)
        {
            m_arrAni0_StartPos[i] = MM.FloatToVector(
                    (MM.RD_Float() - 0.5f) * m_iTileX, 
                    0f,
                    (MM.RD_Float() - 0.5f) * m_iTileZ);

            m_arrAni0_StartTime[i] = MM.RD_Float() * SmallWave_Ani0.fStartRandom;
        }

        for (int i = 0; i < m_iTileX * m_iTileZ; ++i)
        {
            f_random = Random.Range(0f, 30f);
            m_vCreatPos = Vector3.zero; //new Vector3(f_random, f_random, 0);
            
            //Instantiate(m_objCube, m_vCreatPos, Quaternion.identity);
            Instantiate(m_objCube, transform);
            ++m_iTileCnt;
        }
        
    }
}