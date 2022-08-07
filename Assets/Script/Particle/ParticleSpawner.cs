using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{

    //ParticleSettings m_tSettings = new ParticleSettings();

    //생성할 큐브 프리팹
    public GameObject m_objCube;
    public GameObject v_Box;

    //public GameObject m_Box;
    public float m_Boxx;
    public float m_Boxy;

    public static Vector3 m_vColor;
    public static Vector3 Get_Color() { return m_vColor; }

    private static Vector3 m_vMousePos = Vector3.zero;
    public static Vector3 Get_MousePos() { return m_vMousePos; }

    public static float m_fNoise;
    public static float Get_Noise() { return m_fNoise; }

    public static float m_fOpacity;
    public static float Get_Opacity() { return m_fOpacity; }

    //파동의 시작점
    private static Vector3[] m_arrAni0_StartPos = new Vector3[Wave_Ani0.iWaveNum];

    //파동 시작 시간
    private static float[] m_arrAni0_StartTime = new float[Wave_Ani0.iWaveNum];
    //가로 큐브 갯수
    private static int m_iTileX = 12; //12
    //세로 큐브 갯수
    private static int m_iTileZ = 8; //8

    // 수정===================================================================================
    //생성할 큐브 총합
    private static int m_iTileCnt = 0;
    private static Vector3 m_vCreatPos = Vector3.zero;

    public static Vector3 Get_CreatPos() { return m_vCreatPos;  }

    public static float Get_CubeX() { return (float)((m_iTileCnt % m_iTileX - m_iTileX / 2)); }
    public static float Get_CubeZ() { return (float)((m_iTileCnt / m_iTileX - m_iTileZ / 2)); }
    public static Vector3 Get_AniStartPos(int iIndex) { return m_arrAni0_StartPos[iIndex]; } //여기
    public static float Get_StartTime(int iIndex) { return m_arrAni0_StartTime[iIndex]; }
    
    void Start()
    {
        //m_tSettings.v_BoxPos = m_Box.GetComponent<Transform>().position;

        //m_tSettings.v_BoxPosx = m_Boxx;
        //m_tSettings.v_BoxPosy = m_Boxy;
        m_vColor = new Vector3(1, 1, 1); // 여기같아...! - 아님
        m_fNoise = 0f;
        m_fOpacity = 0.6f; //0.3f;

        for (int i = 0; i < Wave_Ani0.iWaveNum; ++i)
        {
            m_arrAni0_StartPos[i] = MM.FloatToVector(
                (MM.RD_Float() - 0.5f) * m_iTileX,
                0f,
                (MM.RD_Float() - 0.5f) * m_iTileZ);

            m_arrAni0_StartTime[i] = MM.RD_Float() * Wave_Ani0.fStartRandom;
        }
        
        for (int i = 0; i < m_iTileX * m_iTileZ; ++i)
        {

            if (m_iTileCnt == 0 | m_iTileCnt == 105 | m_iTileCnt == 156)
            {
                m_vColor = new Vector3(0.2f, 0.2f, 0.3f); //연노랑
                //m_fNoise = 1;
                //m_fOpacity = 0.03f;


            }
            else if (m_iTileCnt == 60 | m_iTileCnt == 150 | m_iTileCnt == 200)
            {
                m_vColor = new Vector3(0.3f, 0.2f, 0.3f); //올리브

            }
            else if(m_iTileCnt == 126 | m_iTileCnt == 72 | m_iTileCnt == 7 | m_iTileCnt == 144)
            {

                m_vColor = new Vector3(0.4f, 0.4f, 0.2f); //개나리
            }


            else if(m_iTileCnt == 26 | m_iTileCnt == 27 | m_iTileCnt == 66 | m_iTileCnt == 70 |  m_iTileCnt == 250)
            {
                m_vColor = new Vector3(0.3f, 0.3f, 0.4f); //연청

            }
            else if(m_iTileCnt == 127 | m_iTileCnt == 170 | m_iTileCnt == 52)
            {
                m_vColor = new Vector3(0.4f, 0.3f, 0.4f); //보라

            }
            else if(m_iTileCnt == 36 | m_iTileCnt == 227 |  m_iTileCnt == 113)
            {
                m_vColor = new Vector3(0.4f, 0.2f, 0.4f); //연보라

            }

            else if (m_iTileCnt == 246 | m_iTileCnt == 167 | m_iTileCnt == 170 | m_iTileCnt == 203)
            {
                m_vColor = new Vector3(0.4f, 0.4f, 0.4f); //연회색

            }

            else if (m_iTileCnt == 236 | m_iTileCnt == 127 | m_iTileCnt == 270 | m_iTileCnt == 213)
            {
                m_vColor = new Vector3(0.2f, 0.2f, 0.2f); //진회색

            }


            else
            {
                m_vColor = new Vector3(0.5f, 0.5f, 0.5f); //new Vector3(0.5f, 0.5f, 0.5f);//

            }

            //m_vColor = m_vColor - new Vector3(0.2f, 0.2f, 0.2f); //Black
            //m_vColor = m_vColor + new Vector3(0.2f, 0.2f, 0.2f); //Test
            m_vColor = m_vColor + new Vector3(0.5f, 0.5f, 0.5f); //White


            m_vCreatPos = new Vector3(transform.position.x + Get_CubeX() * 12f , transform.position.y + Get_CubeZ() * 12f, 0);
            Instantiate(v_Box, m_vCreatPos, Quaternion.identity);
            Instantiate(m_objCube, transform);


            ++m_iTileCnt;
        }
    }

    void Update()
    {
        //if (Input.GetMouseButtonUp(0))
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        m_vMousePos = MM.TransformCoord(ray.origin, Cam_Transform.Get_View()) * 30f; //150f
        m_vMousePos.z = 0f;
        
    }
}

/*
NullReferenceException: Object reference not set to an instance of an object
ParticleTransform.Awake () (at Assets/Script/Particle/ParticleTransform.cs:59)

UnityEngine.Object:Instantiate(GameObject, Transform)
ParticleSpawner:Start() (at Assets/Script/Particle/ParticleSpawner.cs:37)
*/