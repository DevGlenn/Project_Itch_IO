using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PogoBar : MonoBehaviour
{
    [SerializeField] Image pogoChargeBar;
   
    private float startingCharge;
    private float currentCharge;

    private void Start()
    {

    }

    private void Update()
    {
        ChargeBar();
    }
    private void ChargeBar() 
    {
        pogoChargeBar.fillAmount = Mathf.Lerp(pogoChargeBar.fillAmount, currentCharge / startingCharge, 1.5f * Time.deltaTime); // fill the bar, in 1.5 seconds to symbolize the charge up timer
    }
}
