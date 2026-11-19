
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FryerMechanicController : MonoBehaviour
{
    public bool FryerMechanicActive = false;

    public QTE_Controller qteController;
    public GameObject Fryer_Cage;
    public Scrollbar bar;

    public bool FryerOn = false;
    public bool Allowed_CageUp = false;

    [Header("Cage Movement")]
    public float CageDownDistance = 1f;
    public float CageSpeed = 2f;

    [Header("Catching Timer")]
    public float FryerTimer = 5f;
    private float startingFryerTimer;

    private Vector3 startingPosition;
    private Vector3 targetPosition;

    [Header("Fryer Objects")]
    public GameObject Fryer_Objects;
    public GameObject Fryer_Canvas;

    [Header("Fryer UI")]
    public TMP_Text FryerText;

    void Start()
    {
        startingPosition = Fryer_Cage.transform.position;
        targetPosition = startingPosition;

        startingFryerTimer = FryerTimer;

        SetFryerActive(false);
    }

    void Update()
    {
        if (!FryerMechanicActive)
            return;

        if (!FryerOn && !Allowed_CageUp && bar != null && bar.value >= 1f)
        {
            targetPosition = startingPosition + Vector3.down * CageDownDistance;
        }

        Fryer_Cage.transform.position = Vector3.MoveTowards(
            Fryer_Cage.transform.position,
            targetPosition,
            CageSpeed * Time.deltaTime
        );
        if (!FryerOn && !Allowed_CageUp &&
            Mathf.Abs(Fryer_Cage.transform.position.y - targetPosition.y) < 0.001f &&
            targetPosition.y < startingPosition.y)
        {
            FryerOn = true;
            Debug.Log("Fryer timer started!");
        }

        if (FryerOn && FryerTimer > 0f)
        {
            FryerTimer -= Time.deltaTime;

            if (FryerTimer <= 0f)
            {
                FryerTimer = 0f;
                Allowed_CageUp = true;
                Debug.Log("Cage can now move up!");
            }
        }

        if (Allowed_CageUp)
        {
            targetPosition = startingPosition;
        }

        if (FryerText != null)
        {
            FryerText.text = FryerTimer.ToString("0.00");
        }
    }

    
    public void SetFryerActive(bool active)
    {
        FryerMechanicActive = active;

        if (Fryer_Objects != null)
            Fryer_Objects.SetActive(active);

        if (Fryer_Canvas != null)
            Fryer_Canvas.SetActive(active);

        if (!active)
        {
            FryerOn = false;
            Allowed_CageUp = false;

            FryerTimer = 5f;
            targetPosition = startingPosition;

            if (Fryer_Cage != null)
                Fryer_Cage.transform.position = startingPosition;
        }
    }

}
