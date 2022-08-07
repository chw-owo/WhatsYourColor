/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_State
{
    private const ulong CAM_DEFAULT = 0b0000000000000000000000000000000000000000000000000000000000000001;
    private const ulong CAM_ANIMATION = 0b0000000000000000000000000000000000000000000000000000000000000010;

    public void Initialize()
    {
        Set_Default();
    }

    public void Update_Component()
    {

    }

    public static bool Check_Default() { return (MM.StateExist(m_ulState, CAM_DEFAULT)); }
    public static void Set_Default() { m_ulState |= CAM_DEFAULT; }

    public static bool Check_Animation() { return (MM.StateExist(m_ulState, CAM_ANIMATION)); }
    public static void Set_Animation() { m_ulState |= CAM_ANIMATION; }
    public static void Release_Animation() { if (Check_Animation()) m_ulState ^= CAM_ANIMATION; }

    private static ulong m_ulState = 0;

    private static Cam_State m_pInstance;
    public static Cam_State Instance
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = new Cam_State();
            }
            return m_pInstance;
        }
    }
}*/
