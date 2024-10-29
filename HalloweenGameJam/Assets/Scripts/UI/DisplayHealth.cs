using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField] GameObject heartCanvas;
    [SerializeField] GameObject heartOne;
    [SerializeField] GameObject heartTwo;
    [SerializeField] GameObject heartThree;
    [SerializeField] GameObject heartFour;
    [SerializeField] GameObject heartFive;

    [SerializeField] Sprite heartSprite;
    [SerializeField] Sprite emptyHeartSprite;
    private List<GameObject> hearts;
    private List<Image> heartImages;
    private int maxHearts = 5;

    public void Start()
    {
        heartImages = new List<Image>();
        hearts = new List<GameObject>() { heartOne, heartTwo, heartThree, heartFour, heartFive};
        foreach (GameObject heart in hearts)
        {
            heartImages.Add(heart.GetComponent<Image>());
        }

        heartCanvas.SetActive(true);
    }
    public void DisplayHP(int health)
    {
        heartCanvas.SetActive(true);
        for (int i = 0; i < maxHearts - health; i++)
        {
            heartImages[i].sprite = emptyHeartSprite;
        }
        for (int j = maxHearts - health; j < health; j++)
        {
            heartImages[j].sprite = heartSprite;
        }
    }

}
