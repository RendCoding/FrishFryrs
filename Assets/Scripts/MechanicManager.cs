
using UnityEngine;

public class MechanicManager : MonoBehaviour
{
    [Header("Mechanic Controllers")]
    public QTEDebugController qteDebugController;
    public FryerMechanicController fryerController;
    public Chopping_Controller choppingController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ActivateQTE();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            ActivateFryer();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            ActivateChop();
    }

    public void ActivateQTE()
    {
        DisableAllMechanics();

        if (qteDebugController != null)
            qteDebugController.SetQTEVisible(true);
        else
            Debug.LogError("QTE Debug Controller is not assigned!");

        Debug.Log("Mechanic 1: QTE");
    }

    public void ActivateFryer()
    {
        DisableAllMechanics();

        if (fryerController != null)
            fryerController.SetFryerActive(true);
        else
            Debug.LogError("Fryer Controller is not assigned!");

        Debug.Log("Mechanic 2: Fryer");
    }

    public void ActivateChop()
    {
        DisableAllMechanics();

        if (choppingController != null)
            choppingController.SetChopActive(true);
        else
            Debug.LogError("Chopping Controller is not assigned!");

        Debug.Log("Mechanic 3: Chopping");
    }

    public void DisableAllMechanics()
    {
        if (qteDebugController != null)
            qteDebugController.SetQTEVisible(false);

        if (fryerController != null)
            fryerController.SetFryerActive(false);

        if (choppingController != null)
            choppingController.SetChopActive(false);
    }
}
