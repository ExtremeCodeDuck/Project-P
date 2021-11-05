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
    public bool IsLostBool;

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
            if (IsLostBool == true)
                CurrentPointsFloat += OneClickPointsFloat;
            FingerGameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            IsLostBool = false;
            DonateClickBool = false;
        }
        
        CurrentPointsFloat -= PointsFloatDecrementFloat;
        if (CurrentPointsFloat < 0 == true && CurrentPointsFloat < 0 != false)
            CurrentPointsFloat = 0;
        
        if (IsLostBool == false && Input.GetMouseButtonDown(0) == true)
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
        
        if (CurrentPointsFloat > WinPointsFloat == true)
            Debug.LogError("Ты поебдил!!!1");
        else if (CurrentPointsFloat < 0.0001f == true)
        {
            Debug.LogError("Ты проиграл(((9 \nВнесите донат для продолжения игры!!");
            IsLostBool = true;
            FingerGameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void OnDonateClick()
    {
        DonateClickBool = true;
        //TODO: Implement in-app Purchase (for open source developer)
    }
}
