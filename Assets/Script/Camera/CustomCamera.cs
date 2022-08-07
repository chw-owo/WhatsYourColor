using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    private void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    void Start()
    {
        //컴퍼넌트 초기화
        //Cam_KeyInput.Instance.Initialize();
        Cam_Transform.Instance.Initialize();
        //Cam_Frustum.Instance.Initialize();
        //Cam_State.Instance.Initialize();
        //Cam_Animation.Instance.Initialize();
    }

    void Update()
    {
        //컴퍼넌트 업데이트
        //Cam_KeyInput.Instance.Update_Component();
        Cam_Transform.Instance.Update_Component();
        //Cam_Frustum.Instance.Update_Component();
        //Cam_Animation.Instance.Update_Component();
        //Cam_State.Instance.Update_Component();

        //행렬 적용
        SetMatrix(Cam_Transform.Get_ViewInv());
    }

    private void SetMatrix(_matrix matViewInv)
    {
        transform.position = MM.Get_Pos(matViewInv);
        transform.rotation = Quaternion.LookRotation(MM.Get_Look(matViewInv));
    }

    //싱글톤
    private static CustomCamera m_pInstance;
    public static CustomCamera Instance
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = GameObject.Find("Camera").GetComponent<CustomCamera>();
            }
            return m_pInstance;
        }
    }
}