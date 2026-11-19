using UnityEngine;

public class QTEDebugController : MonoBehaviour
{
    public QTE_Controller qteController;

    public void SetQTEVisible(bool visible)
    {
        if (qteController == null)
            return;

        qteController.QTE_Objects.SetActive(visible);
        qteController.QTE_Canvas.SetActive(visible);

        if (visible)
        {
            qteController.ResetQTE();
            qteController.QTE_Active = true;
        }
        else
        {
            qteController.QTE_Active = false;
        }
    }
}
