using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


/// <summary>
/// Input - Key press with countdown functionality and UI display
/// For educational use in Animation and Interactivity class.
/// Connect via UnityEvents in Inspector.
/// </summary>
public class InputKeyCountdown : MonoBehaviour
{

    public KeyCode  thisKey = KeyCode.Space;
    public int countDownValue = 10;
    public UnityEvent onCountDownKey;
    public UnityEvent onCountLimitKey;
    private int originalCountDownValue;

    [SerializeField]
    private TextMeshProUGUI countDownnNumberText; // Reference to the TextMeshPro text field


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(thisKey))
        {
            //countDown the number of clicks
            if (countDownValue > 0)
            {
                //send the event
                onCountDownKey?.Invoke();

                countDownValue = countDownValue - 1;

                UpdateCountdownNumberText();

                if (countDownValue == 0)
                {
                    onCountLimitKey?.Invoke();
                }
            }
        }


    }

    private void UpdateCountdownNumberText()
    {
        if (countDownnNumberText != null)
        {
            countDownnNumberText.text = countDownValue.ToString(); // Convert int to string
        }
    }
}
