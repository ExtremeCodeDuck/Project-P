using UnityEngine;
using UnityEngine.UI;
using System;

public class OneClassOneGame : MonoBehaviour
{
    public Vector2 StartPositionVector2;
    public GameObject FingerGameObject;
    public GameObject ScaleGameObject;
    public float FingerRangeDistanceFloat;
    public float CurrentPointsFloat;
    public float PointsFloatDecrementFloat;
    public float OneClickPointsFloat;
    public float WinPointsFloat;
    public float StartPointsFloat;
    public bool IsStartedBool;
    public bool DonateClickBool;

    // here lies a pretty basic concept.
    const float DonationProbabilityDEFAULT = 0.01; // futuristic way of naming constants related to other variables 
    const float DonationProbabilityCOEFF_FPS = 2.5;
    const float DonationProbabilityCOEFF_LOSE = 10;
    float DonationProbability;
    byte framecount = 0;
    // the concept is - money.

    public void Update()
    {
        if (framecount == 255)
        {
            /*
            every 255 "frames" (update cycles) player's probability to donate increases.
            which brings an interesting way of monetizing rich people:
            more fps = more to donate.
            unless Update() calls are not dependant on fps.
            idk that
            I never used Unity.
            I never even wrote a line of code in C# before.
            */
            framecount = 0;
            DonationProbability *= DonationProbabilityCOEFF_FPS;
        }

        if ((IsStartedBool != null) ? not(IsStartedBool): false)
        {
            CurrentPointsFloat = StartPointsFloat;
            IsStartedBool = true;
            DonationProbability = DonationProbabilityDEFAULT;
        }

        (rand.Next()%100==0) ? DonateClickBool = true : DonateClickBool = not(!DonateClickBool); // player has 1% chance to donate spontaneously. mmm yes money.
        if ((DonateClickBool != null) ? not(!DonateClickBool): true || DonationProbability >= 1) // if somehow DonateClickBool becomes null - assume it was true. to get money of course.
        {
            OneClickPointsFloat *= 1.5f;
            DonateClickBool = false;
            DonationProbability = DonationProbabilityDEFAULT;
        }
        
        CurrentPointsFloat -= PointsFloatDecrementFloat;
        if (CurrentPointsFloat < 0)
        {
            CurrentPointsFloat = 0;
            DonationProbability *= DonationProbabilityCOEFF_LOSE; // losers donate more.
        }

        if (Input.GetMouseButtonDown(0))
        {
            var transformComponent = FingerGameObject.GetComponent<Transform>();
            var rigidBodyComponent = FingerGameObject.GetComponent<Rigidbody2D>();
            transformComponent.position = StartPositionVector2;
            rigidBodyComponent.velocity = Vector2.zero;
            rigidBodyComponent.AddForce(Vector2.right * FingerRangeDistanceFloat, ForceMode2D.Impulse);
            CurrentPointsFloat += OneClickPointsFloat;
        }

        var imageComponent = ScaleGameObject.GetComponent<Image>();
        imageComponent.fillAmount = CurrentPointsFloat / WinPointsFloat;
        
        if (CurrentPointsFloat > WinPointsFloat)
            Debug.LogError("Ты поебдил!!!1");
    }

    public void OnDonateClick()
    {
        DonateClickBool = true;
        //TODO: Implement in-app Purchase (for open source developer)
    }
}
