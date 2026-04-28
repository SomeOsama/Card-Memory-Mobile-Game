using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int cardID;

    public Image cardImage;
    public Sprite frontSprite;
    public Sprite backSprite;

    private bool isFlipped = false;
    private bool isMatched = false;

    AudioSource fliped;

    private void Start()
    {
        fliped = GetComponent<AudioSource>();
    }
    public void SetCard(int id, Sprite front)
    {
        cardID = id;
        frontSprite = front;
        cardImage.sprite = backSprite;
    }

    public void OnCardClicked()
    {
        if (isFlipped || isMatched || BoardManager.Instance.IsBusy())
            return;

        FlipUp();
        BoardManager.Instance.CardFlipped(this);

        fliped.Play();
    }

    public void FlipUp()
    {
        isFlipped = true;
        cardImage.sprite = frontSprite;
    }

    public void FlipDown()
    {
        isFlipped = false;
        cardImage.sprite = backSprite;
    }

    public void SetMatched()
    {
        GetComponent<Button>().interactable = false;
        GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
    }

}

