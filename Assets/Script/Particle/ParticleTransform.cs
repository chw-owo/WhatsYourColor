using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

[RequireComponent(typeof(Renderer))]
public class ParticleTransform : MonoBehaviour
{
    ParticleSettings m_tSettings = new ParticleSettings();

    public GameObject m_objCube;
    public GameObject[] m_comTrail;

    private Vector3 v_CreatPos;
    private Vector3 m_vColor;
    private float m_fNoise;
    private float m_fOpacity;
    //private Material m_shaderMaterial = null;

    float m_fInterationF = 0f;
    float m_fInterationB = -2f;
    Vector3 m_vMousePos;



    void Awake()
    {
        m_tSettings.Initialize();

        m_tSettings.shaderMaterial = GetComponent<MeshRenderer>().material;
        m_tSettings.comRenderer = GetComponent<MeshRenderer>();


        //m_shaderMaterial = GetComponent<MeshRenderer>().material;
        //BoxPosition = GameObject.Find("BoxSpawner").transform.position;
        //m_tSettings.vOriginPos = new Vector3(BoxPosition.x * 10f, BoxPosition.y * 10f, 0);
        //Debug.Log(m_tSettings.v_BoxPos);

        m_tSettings.vOriginPos = new Vector3(
            ParticleSpawner.Get_CubeX() * 10f, //m_tSettings.v_BoxPosx,
            ParticleSpawner.Get_CubeZ() * 10f, //m_tSettings.v_BoxPosy,
            0);

        //이걸 바꿔주니 sphere 제자리에 뜸

        m_tSettings.vPos = m_tSettings.vOriginPos;


        v_CreatPos = ParticleSpawner.Get_CreatPos();
        m_vColor = ParticleSpawner.Get_Color();
        m_fNoise = ParticleSpawner.Get_Noise();
        m_fOpacity = ParticleSpawner.Get_Opacity();


        m_tSettings.tSpline.Initialize(v_CreatPos);
        


        for (int i = 0; i < Wave_Ani0.iWaveNum; ++i)
            (m_tSettings.tWave_Ani0[i]).Initialize();
        m_comTrail = new GameObject[m_tSettings.iTrailNum];

        for (int i = 0; i < m_tSettings.iTrailNum; ++i)
        {
            m_comTrail[i] = Instantiate(m_objCube, transform); //여기
            m_comTrail[i].transform.parent = GameObject.Find("ParticleSpawner").transform; //여기

            m_comTrail[i].GetComponent<MeshRenderer>().material.SetFloat("_Red", m_vColor.x);
            m_comTrail[i].GetComponent<MeshRenderer>().material.SetFloat("_Green", m_vColor.y);
            m_comTrail[i].GetComponent<MeshRenderer>().material.SetFloat("_Blue", m_vColor.z);

            m_comTrail[i].GetComponent<MeshRenderer>().material.SetFloat("_DeltaTimeF", m_fInterationF);
            m_comTrail[i].GetComponent<MeshRenderer>().material.SetFloat("_DeltaTimeB", m_fInterationB);

            m_comTrail[i].GetComponent<MeshRenderer>().material.SetVector("_WorldPos",
                        new Vector4(0, -2f - 12f * ParticleSpawner.Get_CubeZ() * 0.5f, 0, 0));

            m_comTrail[i].GetComponent<MeshRenderer>().material.SetVector("_MousePos",
                        new Vector4(m_vMousePos.x, m_vMousePos.y, m_vMousePos.z, 0));

            m_comTrail[i].GetComponent<MeshRenderer>().material.SetFloat("_Noise", m_fNoise);
            m_comTrail[i].GetComponent<MeshRenderer>().material.SetFloat("_Opacity", m_fOpacity);

            //shared material = 적용 o 색 x // material = 적용 x 색 o
        }


        GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {
        Update_KeyInput();
        Update_Animation();
        Update_Transform();
        //Update_Shader();

        m_fInterationF += Time.deltaTime * 10f;
        m_fInterationB += Time.deltaTime * 10f;

        if (m_fInterationF > 5f * ParticleSpawner.Get_CubeZ())
        {
            m_fInterationF = -12f * ParticleSpawner.Get_CubeZ() * 0.5f;
            m_fInterationB = -2f - 12f * ParticleSpawner.Get_CubeZ() * 0.5f;
        }

        m_tSettings.shaderMaterial.SetFloat("_Red", m_vColor.x);
        m_tSettings.shaderMaterial.SetFloat("_Green", m_vColor.y);
        m_tSettings.shaderMaterial.SetFloat("_Blue", m_vColor.z);
        m_tSettings.shaderMaterial.SetFloat("_Noise", m_fNoise);
        m_tSettings.shaderMaterial.SetFloat("_Opacity", m_fOpacity);

        m_vMousePos = ParticleSpawner.Get_MousePos();

    }
    private void Update_KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            for (int i = 0; i < Wave_Ani0.iWaveNum; ++i)
            {
                m_tSettings.tWave_Ani0[i].Update_KeyInput(ParticleSpawner.Get_StartTime(i));
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_tSettings.Set_Spline();
            m_tSettings.tSpline.Initialize(v_CreatPos); // Vector3.zero, 이걸 바꿔주니 (뭐가????) 제대로 시작됨
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_tSettings.Release_Spline();
        }
    }
    private void Update_Animation()
    {
        m_tSettings.fWaveValue = 0f;

        for (int i = 0; i < Wave_Ani0.iWaveNum; ++i)
        {
            m_tSettings.tWave_Ani0[i].Update_Animation(ParticleSpawner.Get_AniStartPos(i), TimeMgr.Get_BA(), m_tSettings.vPos);
            m_tSettings.fWaveValue = m_tSettings.tWave_Ani0[i].Get_AnimationValue() > m_tSettings.fWaveValue ?
                m_tSettings.tWave_Ani0[i].Get_AnimationValue() : m_tSettings.fWaveValue;
        }

        m_tSettings.fScaleAniCur += TimeMgr.Get_BA();
        if (m_tSettings.Check_Spline())
        {

            m_tSettings.tSpline.Update_Animation(TimeMgr.Get_BA(), v_CreatPos);  //m_tSettings.vOriginPos

            m_tSettings.vPos = m_tSettings.tSpline.Get_Pos();
            m_tSettings.vDir = m_tSettings.tSpline.Get_Dir();
            m_tSettings.fScale = Mathf.Sin(m_tSettings.fScaleAniCur * 1f) * 1f + 3f; // 10f) * 0.5f + 4f
            // ����===================================================================================
        }
        else
        {
            m_tSettings.vPos = v_CreatPos; //m_tSettings.vOriginPos;
            m_tSettings.vDir = Vector3.forward;
            m_tSettings.fScale = (1f - m_tSettings.fWaveValue) * 0.5f + 0.5f;
        }
    }

    private void Update_Transform()
    {
        m_tSettings.matWorld =
            MM.RotAxis(m_tSettings.vDir) *
            MM.RotY(m_tSettings.fWaveValue * 360f) *
            MM.Scale(m_tSettings.fScale) *
            MM.Trans(m_tSettings.vPos) * MM.TransY(m_tSettings.fWaveValue * 2f);

        transform.rotation = Quaternion.LookRotation(MM.DoubleToVector(m_tSettings.matWorld._31, m_tSettings.matWorld._32, m_tSettings.matWorld._33).normalized);
        transform.position = MM.DoubleToVector(m_tSettings.matWorld._41, m_tSettings.matWorld._42, m_tSettings.matWorld._43);
        transform.localScale = MM.FloatToVector(m_tSettings.fScale, m_tSettings.fScale, m_tSettings.fScale);


        m_tSettings.fTrailCur += TimeMgr.Get_BA();
        if (m_tSettings.fTrailCur > m_tSettings.fTrailMax)
        {
            m_tSettings.fTrailCur = 0f;

            for (int i = 0; i < m_tSettings.iTrailNum; ++i)
            {
                if (i == m_tSettings.iTrailNum - 1)
                {

                    m_comTrail[i].transform.rotation = transform.rotation;
                    m_comTrail[i].transform.position = transform.position;
                    m_comTrail[i].transform.localScale = transform.localScale;

                    m_comTrail[i].GetComponent<MeshRenderer>().material.SetFloat("_DeltaTimeF", m_fInterationF);
                    m_comTrail[i].GetComponent<MeshRenderer>().material.SetFloat("_DeltaTimeB", m_fInterationB);

                    m_comTrail[i].GetComponent<MeshRenderer>().material.SetVector("_WorldPos",
                                new Vector4(0, -2f - 12f * ParticleSpawner.Get_CubeZ() * 0.5f, 0, 0));

                    m_comTrail[i].GetComponent<MeshRenderer>().material.SetVector("_MousePos",
                                new Vector4(m_vMousePos.x, m_vMousePos.y, m_vMousePos.z, 0));

                    m_comTrail[i].GetComponent<MeshRenderer>().material.SetFloat("_Noise", m_fNoise);
                    m_comTrail[i].GetComponent<MeshRenderer>().material.SetFloat("_Opacity", m_fOpacity);
                }
                else
                {

                    m_comTrail[i].transform.rotation = m_comTrail[i + 1].transform.rotation;
                    m_comTrail[i].transform.position = m_comTrail[i + 1].transform.position;
                    m_comTrail[i].transform.localScale = m_comTrail[i + 1].transform.localScale;

                    m_comTrail[i].GetComponent<MeshRenderer>().material.SetFloat("_DeltaTimeF", m_fInterationF);
                    m_comTrail[i].GetComponent<MeshRenderer>().material.SetFloat("_DeltaTimeB", m_fInterationB);

                    m_comTrail[i].GetComponent<MeshRenderer>().material.SetVector("_WorldPos",
                                new Vector4(0, -2f - 12f * ParticleSpawner.Get_CubeZ() * 0.5f, 0, 0));

                    m_comTrail[i].GetComponent<MeshRenderer>().material.SetVector("_MousePos",
                                new Vector4(m_vMousePos.x, m_vMousePos.y, m_vMousePos.z, 0));

                    m_comTrail[i].GetComponent<MeshRenderer>().material.SetFloat("_Noise", m_fNoise);
                    m_comTrail[i].GetComponent<MeshRenderer>().material.SetFloat("_Opacity", m_fOpacity);
                }
            }
        }
    }
    /*private void Update_ShaderTable()
    {
        if (Cam_Frustum.Check_Culling(MM.Get_Pos(m_tSettings.matWorld), m_tSettings.fScale))
        {
            m_tSettings.comRenderer.enabled = false;
            return;
        }
        else
        {
            m_tSettings.comRenderer.enabled = true;
        }

        //m_tSettings.shaderMaterial.SetVector("_vLightDir", MM.SetShaderVector(MM.FloatToVector(-1, -1, -1).normalized, false));
        //m_tSettings.shaderMaterial.SetVector("_vCamDir", MM.SetShaderVector(MM.Get_Look(Cam_Transform.Get_ViewInv()), false));
        //m_tSettings.shaderMaterial.SetMatrix("_matWorld", MM.SetShaderMatrix(m_tSettings.matWorld));
        //m_tSettings.shaderMaterial.SetMatrix("_matView", MM.SetShaderMatrix(Cam_Transform.Get_View()));
    }*/

}