using UnityEngine;
using UnityEngine.UI;

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

    public void Update()
    {
        if (IsStartedBool == false)
        {
            CurrentPointsFloat = StartPointsFloat;
            IsStartedBool = true;
        }

        if (DonateClickBool == true)
        {
            OneClickPointsFloat *= 1.5f;
            DonateClickBool = false;
        }
        
        CurrentPointsFloat -= PointsFloatDecrementFloat;
        if (CurrentPointsFloat < 0)
            CurrentPointsFloat = 0;
        
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
