using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_Transform
{
    //�������
    private _matrix m_matWorld = MM.Matrix_Identity();

    //ũ��, ����, ��ġ ����
    private Vector3 m_vScale = new Vector3(1f, 1f, 1f);
    private Vector3 m_vDir = new Vector3(0f, 0f, 0f);
    private Vector3 m_vPos = new Vector3(0f, 0f, 0f);

    //�ʱ�ȭ �Լ�
    public void Initialize(Vector3 vScale, Vector3 vRot, Vector3 vPos)
    {
        m_vScale = vScale; m_vDir = MM.Get_LookNY(MM.Rotation(vRot.x, vRot.y, vRot.z)); m_vPos = vPos;
    }

    //������Ʈ �Լ�
    public void Update_Component()
    {
        m_matWorld =
            MM.Scale(m_vScale.x, m_vScale.y, m_vScale.z) *
            MM.RotAxis(m_vDir) *
            MM.Trans(m_vPos.x, m_vPos.y, m_vPos.z);

        m_vDir = MM.Get_Look(m_matWorld);
    }

    //Get �Լ�
    public Vector3 Get_Scale() { return m_vScale; }
    public Vector3 Get_Pos() { return m_vPos; }
    public Vector3 Get_Dir() { return m_vDir; }

    //Set �Լ�
    public void Set_Pos(Vector3 vPos) { m_vPos = vPos; }
    public void Set_Dir(Vector3 vDir) { m_vDir = vDir; }
    public _matrix Get_World() { return m_matWorld; }

    //�̱���
    private static PL_Transform m_pInstance;
    public static PL_Transform Instance
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = new PL_Transform();
            }
            return m_pInstance;
        }
    }
}