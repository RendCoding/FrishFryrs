using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chopping_Controller : MonoBehaviour
{
    public bool ChopMechanicActive = false;

    [Header("GameObjects")]
    public Scrollbar Slider1;
    public Scrollbar Slider2;
    public Scrollbar Slider3;
    public Scrollbar Slider4;

    public GameObject Chopping_Knife;

    public GameObject ChopObjects;
    public GameObject ChopCanvas;

    private Vector3 startingPosition;
    private Vector3 targetPosition;

    [Header("Knife Movement")]
    public float KnifeDownUpDistance = 1f;
    public float KnifeSpeed = 2f;
    public float KnifeLeftDistance = 1f;

    private bool isMoving = false;

    private enum KnifeState
    {
        Idle,
        MovingDown,
        MovingUp,
        MovingLeft
    }

    private KnifeState currentState = KnifeState.Idle;
    private int currentSlider = 1;
    

    void Start()
    {
        startingPosition = Chopping_Knife.transform.position;
        targetPosition = startingPosition;

        Slider1.gameObject.SetActive(true);
        Slider2.gameObject.SetActive(false);
        Slider3.gameObject.SetActive(false);
        Slider4.gameObject.SetActive(false);
    }
    void Update()
    {
        if (!ChopMechanicActive)
            return;
        
        if (isMoving)
        {
            Chopping_Knife.transform.position = Vector3.MoveTowards(
                Chopping_Knife.transform.position,
                targetPosition,
                KnifeSpeed * Time.deltaTime
            );

            if (Vector3.Distance(
                Chopping_Knife.transform.position,
                targetPosition) < 0.001f)
            {
                Chopping_Knife.transform.position = targetPosition;
                onMovementFinished();
            }
        }

        if (!isMoving && currentState == KnifeState.Idle)
        {
            switch (currentSlider)
            {
                case 1:
                    if (Slider1.value >= 1)
                        StartChop();
                    break;
                case 2:
                    if (Slider2.value >= 1)
                        StartChop();
                    break;
                case 3:
                    if (Slider3.value >= 1)
                        StartChop();
                    break;
                case 4:
                    if (Slider4.value >= 1)
                        StartChop();
                    break;
                    
            }
        }
    }

    void StartChop()
    {
        currentState = KnifeState.MovingDown;

        targetPosition = Chopping_Knife.transform.position
            + Vector3.down * KnifeDownUpDistance;
        
        isMoving = true;

        GetCurrentSlider().interactable = false;
    }

    void onMovementFinished()
    {
        switch (currentState)
        {
            case KnifeState.MovingDown:
                currentState = KnifeState.MovingUp;

                targetPosition = Chopping_Knife.transform.position
                    + Vector3.up * KnifeDownUpDistance;
                
                isMoving = true;
                break;
            case KnifeState.MovingUp:
                currentState = KnifeState.MovingLeft;

                targetPosition = Chopping_Knife.transform.position
                    + Vector3.left * KnifeLeftDistance;
                
                isMoving = true;
                break;
            case KnifeState.MovingLeft:
                isMoving = false;
                currentState = KnifeState.Idle;

                GetCurrentSlider().gameObject.SetActive(false);

                currentSlider++;

                if (currentSlider <= 4)
                {
                    Scrollbar nextSlider = GetCurrentSlider();

                    nextSlider.value = 0f;
                    nextSlider.gameObject.SetActive(true);
                    nextSlider.interactable = true;
                }

                break;
        }
    }

    Scrollbar GetCurrentSlider()
    {
        switch (currentSlider)
        {
            case 1: return Slider1;
            case 2: return Slider2;
            case 3: return Slider3;
            case 4: return Slider4;
            default: return null;
        }
    }

        
    public void SetChopActive(bool active)
    {
        ChopMechanicActive = active;

        if (ChopObjects != null)
            ChopObjects.SetActive(active);

        if (ChopCanvas != null)
            ChopCanvas.SetActive(active);

        if (active)
        {
            ResetChopMechanic();
        }
        else
        {
            isMoving = false;
            currentState = KnifeState.Idle;
            targetPosition = startingPosition;

            if (Chopping_Knife != null)
                Chopping_Knife.transform.position = startingPosition;
        }
    }

    void ResetChopMechanic()
    {
        currentSlider = 1;
        currentState = KnifeState.Idle;
        isMoving = false;

        targetPosition = startingPosition;

        if (Chopping_Knife != null)
            Chopping_Knife.transform.position = startingPosition;

        Slider1.value = 0f;
        Slider2.value = 0f;
        Slider3.value = 0f;
        Slider4.value = 0f;

        Slider1.interactable = true;
        Slider2.interactable = true;
        Slider3.interactable = true;
        Slider4.interactable = true;

        Slider1.gameObject.SetActive(true);
        Slider2.gameObject.SetActive(false);
        Slider3.gameObject.SetActive(false);
        Slider4.gameObject.SetActive(false);
    }

}
