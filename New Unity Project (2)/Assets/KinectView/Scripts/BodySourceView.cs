using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

using Windows.Kinect;
using Joint = Windows.Kinect.Joint;

public class BodySourceView : MonoBehaviour 
{
    public BodySourceManager mBodySourceManager;
    public GameObject mJointObject;
    private static Vector3 m_vPos = Vector3.zero;
    public static Vector3 Get_Pos() { return m_vPos; }

    private Dictionary<ulong, GameObject> mBodies = new Dictionary<ulong, GameObject>(); 
    private List<JointType> _joints = new List<JointType>()
    {

        //JointType.HandLeft,
        //JointType.HandRight,
        JointType.Head,
    };
    
    void Update () 
    {
        #region Get Kinect data
        Body[] data = mBodySourceManager.GetData();
        if (data == null)
        {
            return;
        }
       
        List<ulong> trackedIds = new List<ulong>();

        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
              }
                
            if(body.IsTracked)
            {
                trackedIds.Add (body.TrackingId);
            }
        }

        #endregion

        #region Delete Kinect bodies

        List<ulong> knownIds = new List<ulong>(mBodies.Keys);
        
        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                Destroy(mBodies[trackingId]);
                mBodies.Remove(trackingId);
            }
        }
        #endregion
        #region Create Kinect bodies
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {

                if(!mBodies.ContainsKey(body.TrackingId))
                {
                    mBodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }
                
                UpdateBodyObject(body, mBodies[body.TrackingId]);
            }
        }
        #endregion

    }

    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        
        foreach (JointType joint in _joints)
        {
            GameObject newJoint = Instantiate(mJointObject);
            newJoint.name = joint.ToString();

            newJoint.transform.parent = body.transform;
            //m_vPos = newJoint.transform.position;
        }
        
        return body;
    }
    
    private void UpdateBodyObject(Body body, GameObject bodyObject)
    {
        foreach (JointType _joint in _joints)
        {
            Joint sourceJoint = body.Joints[_joint];
            Vector3 targetPosition = GetVector3FromJoint(sourceJoint);
            targetPosition.z = 0;

            Transform jointObject = bodyObject.transform.Find(_joint.ToString());
            jointObject.position = targetPosition;
            //m_vPos = jointObject.position;


        }
    }
    
    private Vector3 GetVector3FromJoint(Joint joint)
    {
        return new Vector3(joint.Position.X * 100, joint.Position.Y * 100, joint.Position.Z * 100);
    }
}
