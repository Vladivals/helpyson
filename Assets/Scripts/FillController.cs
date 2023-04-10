using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class FillController : MonoBehaviour
{
    public float fillSpeed = 0.5f;
    public Image homeImage;
    public Image schoolImage;
    public Image galleryImage;
    public TMP_Text coinsText;
    private int coinsAmount = 0;

    public float maxTime = 10f;
    public float timeLeft;

    void Start()
    {
        timeLeft = maxTime;
        
        homeImage.fillAmount = 0f;
        schoolImage.fillAmount = 0f;
        galleryImage.fillAmount = 0f;
        
       

    }

    void Update() 
    {
        addCoin();
        string coinsTextString = coinsAmount.ToString();
        coinsText.SetText(coinsTextString);

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            homeImage.fillAmount = timeLeft / maxTime;
        } 
    }


    public void addCoin()
    {
        if(homeImage.fillAmount >= 1.0f){
            coinsAmount++;
            homeImage.fillAmount = 0f;
        }
    }

    public void homeImageFilling()
    {
        homeImage.fillAmount += 0.2f;
        timeLeft += 0.8f;
    }
    
    public void schoolImageFilling()
    {
        schoolImage.fillAmount += 0.2f;
    }
    
    public void galleryImageFilling()
    {
        galleryImage.fillAmount += 0.2f;
    }

    
}