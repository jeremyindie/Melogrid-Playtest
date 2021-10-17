using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMovement : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    private float LerpRatio;
    private Transform player1Reference;
    private Transform player2Reference;
    private Transform playerGreyReference;
    private Transform PlayerReferencePosition;
    public float DistanceToEnvironment;
    public float s_DistanceForMovement = 50;
    public static float DistanceForMovement = 50;
    public float DistanceToStand = 50;
    private void Start()
    {
        player1Reference = PlayerTurnManager.Instance.GetPlayerOneTransform();
        player2Reference = PlayerTurnManager.Instance.GetPlayerTwoTransform();
        playerGreyReference = PlayerTurnManager.Instance.GetPlayerGreyTransform();
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerTurnManager.Instance.IsPlayerOnesTurn())
        {
            PlayerReferencePosition = player1Reference;
        }
        else if (PlayerTurnManager.Instance.IsPlayerTwosTurn())
        {
            PlayerReferencePosition = player2Reference;
        } else
        {
            PlayerReferencePosition = playerGreyReference;
        }
        DistanceForMovement = s_DistanceForMovement;

        DistanceToEnvironment = PlayerReferencePosition.position.y - 0.5f - gameObject.transform.position.y;
        DistanceToEnvironment = Mathf.Clamp(DistanceToEnvironment, 0, DistanceToEnvironment);
        // Player.Reference.Vector3.Distance(PlayerReferencePosition.position - new Vector3(0.5f, 0.5f, 0.0f), gameObject.transform.position);
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
