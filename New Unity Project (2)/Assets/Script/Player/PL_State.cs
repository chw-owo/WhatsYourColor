/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_State 
{
    private const ulong PL_F    = 0b0000000000000000000000000000000000000000000000000000000000000001;
    private const ulong PL_B    = 0b0000000000000000000000000000000000000000000000000000000000000010;
    private const ulong PL_L    = 0b0000000000000000000000000000000000000000000000000000000000000100;
    private const ulong PL_R    = 0b0000000000000000000000000000000000000000000000000000000000001000;
    private const ulong PL_DROP = 0b0000000000000000000000000000000000000000000000000000000000010000;
    private const ulong PL_IDLE = 0b0000000000000000000000000000000000000000000000000000000000100000;
    private const ulong PL_WALK = 0b0000000000000000000000000000000000000000000000000000000001000000;
    private const ulong PL_JUMP = 0b0000000000000000000000000000000000000000000000000000000010000000;

    private static ulong m_ulState = 0;
    
    public void Initialize()
    {
        m_ulState = PL_DROP | PL_IDLE;
    }

    public void Update_Component()
    {
    }

    public static bool Check_Drop() { return (MM.StateExist(m_ulState, PL_DROP)); }
    public static void Set_Drop() { m_ulState |= PL_DROP; }
    public static void Release_Drop() { if(Check_Drop()) m_ulState ^= PL_DROP; }

    public static bool Check_Idle() { return (MM.StateExist(m_ulState, PL_IDLE)); }
    public static void Set_Idle() { m_ulState |= PL_DROP; }
    public static void Release_Idle() { if (Check_Idle()) m_ulState ^= PL_IDLE; }

    public static bool Check_Forward() { return (MM.StateExist(m_ulState, PL_F)); }
    public static void Set_Forward() { m_ulState |= PL_F; }
    public static void Release_Forward() { if (Check_Forward()) m_ulState ^= PL_F; }

    public static bool Check_Back() { return (MM.StateExist(m_ulState, PL_B)); }
    public static void Set_Back() { m_ulState |= PL_B; }
    public static void Release_Back() { if (Check_Back()) m_ulState ^= PL_B; }

    public static bool Check_Right() { return (MM.StateExist(m_ulState, PL_R)); }
    public static void Set_Right() { m_ulState |= PL_R; }
    public static void Release_Right() { if (Check_Right()) m_ulState ^= PL_R; }

    public static bool Check_Left() { return (MM.StateExist(m_ulState, PL_L)); }
    public static void Set_Left() { m_ulState |= PL_L; }
    public static void Release_Left() { if (Check_Left()) m_ulState ^= PL_L; }

    public static bool Check_Walk() { return (MM.StateExist(m_ulState, PL_WALK)); }
    public static void Set_Walk() { m_ulState |= PL_WALK; }
    public static void Release_Walk() { if (Check_Walk()) m_ulState ^= PL_WALK; }

    public static bool Check_Jump() { return (MM.StateExist(m_ulState, PL_JUMP)); }
    public static void Set_Jump() { m_ulState |= PL_JUMP; }
    public static void Release_Jump() { if (Check_Jump()) m_ulState ^= PL_JUMP; }
    

    private static PL_State m_pInstance;
    public static PL_State Instance
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = new PL_State();
            }
            return m_pInstance;
        }
    }
}
*/