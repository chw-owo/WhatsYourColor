using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

[RequireComponent(typeof(Renderer))]
public class SmallTransform : MonoBehaviour
{

    private float f_random = 0f;
    //쉐이더 세팅 변수
    private MeshRenderer m_comRenderer = null;
    private Material m_shaderMaterial = null;
    //월드행렬
    private _matrix m_matWorld = MM.Matrix_Identity();
    
    //방향, 위치, 크기 변수
    private Vector3 m_vDir = Vector3.forward;
    private Vector3 m_vPos = Vector3.zero;
    private Vector3 m_vOriginPos = Vector3.zero;
    private float m_fScale = 0.01f;
    private Vector3 v_CreatPos;

    //애니메이션 수치
    private float m_fWaveValue = 0f;
    //파동 변수
    private SmallWave_Ani0[] m_tWave_Ani0 = new SmallWave_Ani0[SmallWave_Ani0.iWaveNum];
    //스플라인 애니메이션 변수
    public SmallSpline m_tSpline = new SmallSpline();
    //생성할 큐브 트레일
    public GameObject m_objCube;
    private GameObject[] m_comTrail;
    private int m_iTrailNum = 20; //20
    private float m_fTrailCur = 0f;
    private float m_fTrailMax = 0.001f;
    //파티클 상태 변수
    private const ulong PT_DEFAULT = 0b0000000000000000000000000000000000000000000000000000000000000001;
    private const ulong PT_SPLINE = 0b0000000000000000000000000000000000000000000000000000000000000010;
    private ulong m_ulState = PT_DEFAULT;
    public bool Check_Default() { return (MM.StateExist(m_ulState, PT_DEFAULT)); }
    public void Set_Default() { m_ulState |= PT_DEFAULT; }
    public bool Check_Spline() { return (MM.StateExist(m_ulState, PT_SPLINE)); }
    public void Set_Spline() { m_ulState |= PT_SPLINE; }
    public void Release_Spline() { if (Check_Spline()) m_ulState ^= PT_SPLINE; }

    void Awake()
    {
        
        f_random = Random.Range(0f, 30f);
        //셰이더 세팅
        m_shaderMaterial = GetComponent<MeshRenderer>().sharedMaterial;
        m_comRenderer = GetComponent<MeshRenderer>();
        
        //위치값 초기 세팅
        m_vOriginPos = Vector3.zero; //new Vector3(f_random, f_random, 0);
        m_vPos = m_vOriginPos;
        
        //스플라인 초기화
        v_CreatPos = SmallSpawner.Get_CreatPos();

        m_tSpline.Initialize(v_CreatPos);



        //파동 초기화
        for (int i = 0; i < SmallWave_Ani0.iWaveNum; ++i)
            m_tWave_Ani0[i].Initialize();
        //트레일 생성
        m_comTrail = new GameObject[m_iTrailNum];
        for (int i = 0; i < m_iTrailNum; ++i)
        {
            m_comTrail[i] = Instantiate(m_objCube, transform);
            m_comTrail[i].transform.parent = GameObject.Find("SmallSpawner").transform;
        }
    }
    void Update()
    {
        //키값 입력
        Update_KeyInput();
        //파티클 애니메이션 업데이트
        Update_Animation();
        //트렌스폼 업데이트
        Update_Transform();
        //쉐이더 업데이트
        //Update_ShaderTable();
    }
    private void Update_KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            for (int i = 0; i < SmallWave_Ani0.iWaveNum; ++i)
            {
                m_tWave_Ani0[i].Update_KeyInput(SmallSpawner.Get_StartTime(i));
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Set_Spline();
            m_tSpline.Initialize(v_CreatPos);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Release_Spline();
        }
    }
    private void Update_Animation()
    {
        //파동 애니메이션 값 산출
        m_fWaveValue = 0f;

        for (int i = 0; i < SmallWave_Ani0.iWaveNum; ++i)
        {
            m_tWave_Ani0[i].Update_Animation(SmallSpawner.Get_AniStartPos(i), TimeMgr.Get_BA(), m_vPos);
            m_fWaveValue = m_tWave_Ani0[i].Get_AnimationValue() > m_fWaveValue ?
                m_tWave_Ani0[i].Get_AnimationValue() : m_fWaveValue;
        }

        if (Check_Spline())
        {
            //스플라인 애니메이션 값 산출
            m_tSpline.Update_Animation(TimeMgr.Get_BA(), v_CreatPos);

            //방향, 위치, 크기 설정
            m_vPos = m_tSpline.Get_Pos();
            m_vDir = m_tSpline.Get_Dir();
            m_fScale = ((1f - m_fWaveValue) * 0.5f + 0.5f) * m_tSpline.Get_Scale();
        }
        else
        {
            m_vPos = v_CreatPos;
            m_vDir = Vector3.forward;
            m_fScale = (1f - m_fWaveValue) * 0.5f + 0.5f;
        }
    }

    private void Update_Transform()
    {
        m_matWorld =
            MM.RotAxis(m_vDir) *
            MM.RotY(m_fWaveValue * 360f) *
            MM.Scale(m_fScale) *
            MM.Trans(m_vPos) * MM.TransY(m_fWaveValue * 2f);

        transform.rotation = Quaternion.LookRotation(MM.DoubleToVector(m_matWorld._31, m_matWorld._32, m_matWorld._33).normalized);
        transform.position = MM.DoubleToVector(m_matWorld._41, m_matWorld._42, m_matWorld._43);
        transform.localScale = MM.FloatToVector(m_fScale, m_fScale, m_fScale);

        //트레일 시간 설정
        m_fTrailCur += TimeMgr.Get_BA();
        if (m_fTrailCur > m_fTrailMax)
        {
            m_fTrailCur = 0f;

            for (int i = 0; i < m_iTrailNum; ++i)
            {
                if (i == m_iTrailNum - 1)
                {
                    m_comTrail[i].transform.rotation = transform.rotation;
                    m_comTrail[i].transform.position = transform.position;
                    m_comTrail[i].transform.localScale = transform.localScale;
                }
                else
                {
                    m_comTrail[i].transform.rotation = m_comTrail[i + 1].transform.rotation;
                    m_comTrail[i].transform.position = m_comTrail[i + 1].transform.position;
                    m_comTrail[i].transform.localScale = m_comTrail[i + 1].transform.localScale;
                }
            }
        }
    }
    /*
    private void Update_ShaderTable()
    {
        if (Cam_Frustum.Check_Culling(MM.Get_Pos(m_matWorld), m_fScale))
        {
            m_comRenderer.enabled = false;
            return;
        }
        else
        {
            m_comRenderer.enabled = true;
        }

        m_shaderMaterial.SetVector("_vLightDir", MM.SetShaderVector(MM.FloatToVector(-1, -1, -1).normalized, false));
        m_shaderMaterial.SetVector("_vCamDir", MM.SetShaderVector(MM.Get_Look(Cam_Transform.Get_ViewInv()), false));
        m_shaderMaterial.SetMatrix("_matWorld", MM.SetShaderMatrix(m_matWorld));
        m_shaderMaterial.SetMatrix("_matView", MM.SetShaderMatrix(Cam_Transform.Get_View()));
    }*/
}