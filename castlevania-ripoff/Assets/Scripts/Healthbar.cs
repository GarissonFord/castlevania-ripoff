using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public PlayerController pc;
    //Player hitpoints
    public float hitPoint = 100;
    private float maxHitPoint = 100;
    public Image currentHealthBar;
    public Text ratioText;

    // Use this for initialization
    void Start ()
    {
        UpdateHealthbar();
        pc = GetComponent<PlayerController>();
	}

    public void UpdateHealthbar()
    {
        float ratio = hitPoint / maxHitPoint;
        //Healthbar is shrunken to reflect the drop in HP 
        currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString("0");
    }
	
	private void TakeDamage(float damage)
    {
        hitPoint -= damage;
        if(hitPoint <= 0)
        {
            hitPoint = 0;
            pc.SendMessageUpwards("Die");
        }

        UpdateHealthbar();
    }
}
