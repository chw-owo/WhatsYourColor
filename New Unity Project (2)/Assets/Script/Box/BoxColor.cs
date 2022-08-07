using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

[RequireComponent(typeof(Renderer))]
public class BoxColor : MonoBehaviour
{
    ParticleSettings m_tSettings = new ParticleSettings();

    public GameObject m_Box;
    private GameObject[] m_heads;

    private Vector3 v_CreatPos;
    private float m_fNoise;
    private float m_fOpacity;

    float m_fInterationF = 0f;
    float m_fInterationB = -2f;
    Vector3 m_vMousePos;



    void Awake()
    {
        m_tSettings.Initialize();


        m_tSettings.shaderMaterial = GetComponent<MeshRenderer>().sharedMaterial;
        m_tSettings.comRenderer = GetComponent<MeshRenderer>();


        m_tSettings.vOriginPos = new Vector3(
            ParticleSpawner.Get_CubeX() * 10f, //m_tSettings.v_BoxPosx,
            ParticleSpawner.Get_CubeZ() * 10f, //m_tSettings.v_BoxPosy,
            0);

        m_tSettings.vPos = m_tSettings.vOriginPos;
        v_CreatPos = ParticleSpawner.Get_CreatPos();
        m_fNoise = ParticleSpawner.Get_Noise();
        m_fOpacity = 0.1f; //ParticleSpawner.Get_Opacity();

        m_Box.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_DeltaTimeF", m_fInterationF);
        m_Box.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_DeltaTimeB", m_fInterationB);
        
        m_Box.GetComponent<MeshRenderer>().sharedMaterial.SetVector("_WorldPos",
                    new Vector4(0, -2f - 12f * ParticleSpawner.Get_CubeZ() * 0.5f, 0, 0));

        m_Box.GetComponent<MeshRenderer>().sharedMaterial.SetVector("_MousePos",
                    new Vector4(m_vMousePos.x, m_vMousePos.y, m_vMousePos.z, 0));

        m_Box.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Noise", m_fNoise);
        m_Box.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Opacity", m_fOpacity);

    }

    void Update()
    {

        Update_Transform();
        //Update_ShaderTable();

        m_fInterationF += Time.deltaTime * 10f;
        m_fInterationB += Time.deltaTime * 10f;

        if (m_fInterationF > 5f * ParticleSpawner.Get_CubeZ())
        {
            m_fInterationF = -12f * ParticleSpawner.Get_CubeZ() * 0.5f;
            m_fInterationB = -2f - 12f * ParticleSpawner.Get_CubeZ() * 0.5f;
        }
        
        m_heads = GameObject.FindGameObjectsWithTag("Head");

        foreach (GameObject m_head in m_heads)
        {
            m_vMousePos = m_head.transform.position;
        }

    }


    private void Update_Transform()
    {

        m_tSettings.fTrailCur += TimeMgr.Get_BA();
        if (m_tSettings.fTrailCur > m_tSettings.fTrailMax)
        {
            m_tSettings.fTrailCur = 0f;

            m_Box.transform.rotation = transform.rotation;
            m_Box.transform.position = transform.position;
            m_Box.transform.localScale = transform.localScale;
            
            m_Box.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_DeltaTimeF", m_fInterationF);
            m_Box.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_DeltaTimeB", m_fInterationB);
            
            m_Box.GetComponent<MeshRenderer>().sharedMaterial.SetVector("_WorldPos",
                        new Vector4(0, -2f - 12f * ParticleSpawner.Get_CubeZ() * 0.5f, 0, 0));

            m_Box.GetComponent<MeshRenderer>().sharedMaterial.SetVector("_MousePos",
                        new Vector4(m_vMousePos.x, m_vMousePos.y, m_vMousePos.z, 0));

            m_Box.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Noise", m_fNoise);
            m_Box.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Opacity", m_fOpacity);
            
        }
    }

}