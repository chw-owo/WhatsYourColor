/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Frustum
{
    //�簢���� 5���� ������
    private static Vector3 m_vFrustumPoint_0;
    private static Vector3 m_vFrustumPoint_1;
    private static Vector3 m_vFrustumPoint_2;
    private static Vector3 m_vFrustumPoint_3;
    private static Vector3 m_vFrustumPoint_4;
    //�簢���� �ظ��� ������ �����
    private static Plane m_tPlane_0;
    private static Plane m_tPlane_1;
    private static Plane m_tPlane_2;
    private static Plane m_tPlane_3;
    //���� ������ ���� �� ���⿡ ���� ����ü��
    private static Ray m_tRayUp;
    private static Ray m_tRayRight;
    private static Ray m_tRayDown;
    private static Ray m_tRayLeft;
    //���� �Լ��� ������ ������� �ʴ� ���ڰ�
    private static float m_fRayLength = 0f;
    //�ش� ��ü�� �� �����̽������� ��ġ
    private static Vector3 m_vViewPos = Vector3.zero;
    //ī�޶� Ʈ���������� ������ �����
    private static _matrix m_matView;
    //��ü�� �������� ���� ���� ��ü�� �缱 ����
    private static float m_fDiagonal = 0f;
    //����ü�� ���� ����
    private float m_fFov = 60f;
    //����ü�� ������� ����
    private float m_fHeight = 0f;
    //����ü�� ������� �ʺ�
    private float m_fWidth = 0f;
    //����ü�� �����
    private static float m_fNear = 0.1f;
    //����ü�� �����
    private static float m_fFar = 1000f;

    public static bool Check_Culling(Vector3 vPos, float fRadius)
    {
        //��ü�� ��ġ�� �� �����̽��� ��ȯ
        m_vViewPos = MM.TransformCoord(vPos, m_matView);
        //������ ������� ���� 1������
        if (m_vViewPos.z < m_fNear && m_vViewPos.z > m_fFar)
            return true;
        //��ü�� �밢���� ����
        m_fDiagonal = fRadius * Mathf.Sqrt(3);
        //�������� ������ ������ �ϱ����� ������ �������� ����
        m_tRayUp.origin = MM.Vec3_AdjustY(m_vViewPos, -m_fDiagonal);
        m_tRayRight.origin = MM.Vec3_AdjustX(m_vViewPos, -m_fDiagonal);
        m_tRayDown.origin = MM.Vec3_AdjustY(m_vViewPos, +m_fDiagonal);
        m_tRayLeft.origin = MM.Vec3_AdjustX(m_vViewPos, +m_fDiagonal);
        //����ĳ��Ʈ�� ���� 2�� ���� 
        if ((m_tPlane_0.Raycast(m_tRayUp, out m_fRayLength) && m_tPlane_2.Raycast(m_tRayDown, out m_fRayLength)) &&
            (m_tPlane_1.Raycast(m_tRayRight, out m_fRayLength) && m_tPlane_3.Raycast(m_tRayLeft, out m_fRayLength)))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void Initialize()
    {
        //���� ����ü�� ���� ���� ����
        m_tRayRight.direction = Vector3.right;
        m_tRayUp.direction = Vector3.up;
        m_tRayDown.direction = Vector3.down;
        m_tRayLeft.direction = Vector3.left;
    }
    public void Update_Component()
    {
        //ź��Ʈ30 = ���� / �غ� (�غ��� 1(�ӽ� z��))
        m_fHeight = Mathf.Tan(Mathf.PI * m_fFov * 0.5f / 180f) * m_fFar;
        m_fWidth = m_fHeight * 16 / 9;

        //����ü �������� ����

        m_vFrustumPoint_0.x = 0f;
        m_vFrustumPoint_0.y = 0f;
        m_vFrustumPoint_0.z = 0f;

        m_vFrustumPoint_1.x = -m_fWidth;
        m_vFrustumPoint_1.y = +m_fHeight;
        m_vFrustumPoint_1.z = m_fFar;

        m_vFrustumPoint_2.x = +m_fWidth;
        m_vFrustumPoint_2.y = +m_fHeight;
        m_vFrustumPoint_2.z = m_fFar;

        m_vFrustumPoint_3.x = +m_fWidth;
        m_vFrustumPoint_3.y = -m_fHeight;
        m_vFrustumPoint_3.z = m_fFar;

        m_vFrustumPoint_4.x = -m_fWidth;
        m_vFrustumPoint_4.y = -m_fHeight;
        m_vFrustumPoint_4.z = m_fFar;

        //����ü �������� �̿��� ����ü ����� ����
        m_tPlane_0.Set3Points(m_vFrustumPoint_0, m_vFrustumPoint_1, m_vFrustumPoint_2);
        m_tPlane_1.Set3Points(m_vFrustumPoint_0, m_vFrustumPoint_2, m_vFrustumPoint_3);
        m_tPlane_2.Set3Points(m_vFrustumPoint_0, m_vFrustumPoint_3, m_vFrustumPoint_4);
        m_tPlane_3.Set3Points(m_vFrustumPoint_0, m_vFrustumPoint_4, m_vFrustumPoint_1);

        //�� ����� ������Ʈ
        m_matView = Cam_Transform.Get_View();
    }

    //�̱���
    private static Cam_Frustum m_pInstance;
    public static Cam_Frustum Instance
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = new Cam_Frustum();
            }
            return m_pInstance;
        }
    }
}*/