using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    Image flash;

    private void Start()
    {
        flash = GetComponent<Image>();
    }
    private void Update()
    {
        if (flash.color.a > 0)
        {
            Color invisible = new Color(flash.color.r, flash.color.g, flash.color.b, 0);
            flash.color = Color.Lerp(flash.color, invisible, 5 * Time.deltaTime);
        }
    }

    public void FlashDamage()
    {
        //czerwony, zielony, niebieski, przezroczystosc %
        flash.color = new Color(1, 0, 0, 0.4f);
    }
}
