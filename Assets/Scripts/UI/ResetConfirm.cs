using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetConfirm : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        UIManager.SetComponentResetConfirm(gameObject);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ConfirmReset()
    {
        UIManager.Instance.InvokeResetAction();
    }

}
