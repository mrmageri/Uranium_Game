using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playMin;
    public int playSec;
    [SerializeField] private TMP_Text timeText;
    private int minLast = 0;
    private int secLast = 0;
    private string min;
    private string sec;

    private void Start()
    {
        minLast = playMin;
        secLast = playSec;
        min = minLast.ToString();
        sec = secLast.ToString();
    }

    public void CountSec()
    {
        secLast -= 1;
        if (minLast - 1 >= 0 && secLast < 0)
        {
            minLast--; 
            secLast = 59;
        }

        min = minLast.ToString();
        sec = secLast.ToString();
        if (minLast < 10) min = "0" + minLast;
        if (minLast == 0) min = "00";
        if (secLast < 10) sec = "0" + secLast;
        if (secLast == 0) sec = "00";
        timeText.text = min + ":" + sec;
    }
    
}
