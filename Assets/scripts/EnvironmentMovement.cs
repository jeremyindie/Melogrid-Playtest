using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMovement : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public float LerpRatio;
    public Transform PlayerReferencePosition;
    public float DistanceToEnvironment;
    public float s_DistanceForMovement = 50;
    public static float DistanceForMovement = 50;
    public float DistanceToStand = 50;

    // Update is called once per frame
    void Update()
    {
        DistanceForMovement = s_DistanceForMovement;

        DistanceToEnvironment = Vector3.Distance(PlayerReferencePosition.position - new Vector3(0.5f, 0.5f, 0.0f), gameObject.transform.position);
        LerpRatio = Mathf.Pow((DistanceToEnvironment / DistanceToStand),10);
        /*if (gameObject.transform.position.z - (PlayerReferencePosition.position.z) >= 0)
        {
            LerpRatio = 0.5f - Mathf.Clamp(((DistanceToEnvironment - DistanceToStand) / DistanceForMovement), 0, 0.5f);
        }
        else if (gameObject.transform.position.z - (PlayerReferencePosition.position.z) <= 0)
        {
            LerpRatio = Mathf.Clamp((DistanceToEnvironment / DistanceForMovement), 0.5f, 1);
        }*/
        gameObject.transform.rotation = Quaternion.Lerp(StartPoint.rotation, EndPoint.rotation, LerpRatio);
    }
}
