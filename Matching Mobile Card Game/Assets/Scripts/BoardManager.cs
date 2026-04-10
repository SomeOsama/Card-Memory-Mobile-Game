using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    public GameObject cardPrefab;
    public Transform gridParent;
    public Sprite[] cardSprites;

    private List<Card> flippedCards = new List<Card>();
    private bool isChecking = false;
    private int matchesRemaining;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GenerateBoard(GameManager.Instance.currentLevel);
    }


    public bool IsBusy()
    {
        return isChecking;
    }

    public void GenerateBoard(int level = 1)
    {
        
        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        List<int> ids = new List<int>();

        int pairCount = GameManager.Instance.cardsPerLevel[level - 1] / 2;

        for (int i = 0; i < pairCount; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }

        Shuffle(ids);

        for (int i = 0; i < ids.Count; i++)
        {
            GameObject obj = Instantiate(cardPrefab, gridParent);
            Card card = obj.GetComponent<Card>();
            card.SetCard(ids[i], cardSprites[ids[i]]);
        }

        matchesRemaining = pairCount;
    }


    public void CardFlipped(Card card)
    {
        flippedCards.Add(card);

        if (flippedCards.Count == 2)
        {
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        isChecking = true;

        yield return new WaitForSeconds(0.5f);

        if (flippedCards[0].cardID == flippedCards[1].cardID)
        {        
            flippedCards[0].SetMatched();
            flippedCards[1].SetMatched();

            yield return new WaitForSeconds(0.5f);          

            matchesRemaining--;

            if (matchesRemaining <= 0)
            {
                GameManager.Instance.LevelComplete();
            }
        }
        else
        {
            flippedCards[0].FlipDown();
            flippedCards[1].FlipDown();
        }

        flippedCards.Clear();
        isChecking = false;
    }


    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
