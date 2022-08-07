using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾���� ���׸� ���̰� ��ȣ�ۿ��� ��Ȱ�� �ϱ����� ��ü ī�޶� ����
public class Cam_Transform
{
    //����İ� ���� �����
    private static _matrix m_matView;
    private static _matrix m_matViewInv;

    //ī�޶�� �÷��̾� ������ �Ÿ�
    private static float m_fCam_Distance = 0f;

    //ī�޶� ����
    private static Vector3 m_vRot = new Vector3(0, 0, 0);

    public void Initialize()
    {

    }

    public void Update_Component()
    {
        /*if (Cam_State.Check_Animation())
        {
            m_matViewInv = MM.RotAxis(Cam_Animation.Get_Dir()) *
                                    MM.Trans(Cam_Animation.Get_Pos());
        }
        else
        {*/
        //�÷��̾��� ���
        _matrix matPL = PL_Transform.Instance.Get_World();
        _matrix matCamRot = MM.Rotation(m_vRot.x, m_vRot.y, m_vRot.z);

        //ī�޶� ��� = ���� �����
        m_matViewInv = MM.RotAxis(MM.Get_Look(matCamRot)) *
                                    MM.Trans(MM.Get_Pos(matPL) - MM.Get_Look(matCamRot) * m_fCam_Distance);
        //}

        //ī�޶��� ����� = �����
        m_matView = MM.Inverse(m_matViewInv);

        //��ü ���� ����� ����
        Camera.main.ResetWorldToCameraMatrix();
        Camera.main.worldToCameraMatrix = MM.ViewForUnityClient(m_matView);

    }

    //Get�Լ�
    public static _matrix Get_View() { return m_matView; }
    public static _matrix Get_ViewInv() { return m_matViewInv; }
    public Vector3 Get_Rot() { return m_vRot; }

    //Set�Լ�
    public void Set_Rot(Vector3 vRot) { m_vRot = vRot; }

    //�̱���
    private static Cam_Transform m_pInstance;
    public static Cam_Transform Instance
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = new Cam_Transform();
            }
            return m_pInstance;
        }
    }
}