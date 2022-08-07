using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어와의 버그를 줄이고 상호작용을 원활히 하기위해 자체 카메라 제작
public class Cam_Transform
{
    //뷰행렬과 뷰의 역행렬
    private static _matrix m_matView;
    private static _matrix m_matViewInv;

    //카메라와 플레이어 사이의 거리
    private static float m_fCam_Distance = 0f;

    //카메라 조정
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
        //플레이어의 행렬
        _matrix matPL = PL_Transform.Instance.Get_World();
        _matrix matCamRot = MM.Rotation(m_vRot.x, m_vRot.y, m_vRot.z);

        //카메라 행렬 = 뷰의 역행렬
        m_matViewInv = MM.RotAxis(MM.Get_Look(matCamRot)) *
                                    MM.Trans(MM.Get_Pos(matPL) - MM.Get_Look(matCamRot) * m_fCam_Distance);
        //}

        //카메라의 역행렬 = 뷰행렬
        m_matView = MM.Inverse(m_matViewInv);

        //자체 제작 뷰행렬 적용
        Camera.main.ResetWorldToCameraMatrix();
        Camera.main.worldToCameraMatrix = MM.ViewForUnityClient(m_matView);

    }

    //Get함수
    public static _matrix Get_View() { return m_matView; }
    public static _matrix Get_ViewInv() { return m_matViewInv; }
    public Vector3 Get_Rot() { return m_vRot; }

    //Set함수
    public void Set_Rot(Vector3 vRot) { m_vRot = vRot; }

    //싱글톤
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