using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform blockPrefab;
    [SerializeField] private Transform blockHolder;
    [SerializeField] private Transform newBall;

    [SerializeField] private Canvas canvas;

    private int level = 0;

    private Transform currentBlock;

    // Start is called before the first frame update
    void Start()
    {
        nextLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void nextLevel()
    {
        level++;
        canvas.GetComponentInChildren<TMP_Text>().text = "" + level;

        var random = new System.Random();
        // harder levels spawn more blocks

        List<int> positions = new List<int>();

        for(int i = 0; i < random.Next(5) + 2; i++)
        {
            int position = random.Next(7);
            while(positions.Contains(position))
            {
                position = random.Next(7);
            }
            positions.Add(position);
        }

        spawnNewBall(positions[^1]);
        positions.RemoveAt(positions.Count - 1);
        
        foreach (int position in positions)
        {
            spawnNewBlock(level, position);
        }

        foreach (Transform block in blockHolder)
        {
            StartCoroutine(MoveDown(block));
        }
    }

    private void spawnNewBlock(int level, int pos)
    {
        currentBlock = Instantiate(blockPrefab, blockHolder);
        currentBlock.position = new Vector2(pos - 3, 5.5f);
        currentBlock.GetComponent<SpriteRenderer>().color = new Color(20f/255f, 1, 236f/255f);
        currentBlock.GetComponentInChildren<TextMeshPro>().text = "" + level;
    }

    private void spawnNewBall(int pos)
    {
        Instantiate(newBall, blockHolder).position = new Vector2(pos - 3, 5.5f);
    }

    private IEnumerator MoveDown(Transform block)
    {
        float timer = 0;
        Vector2 startPosition = block.position;
        Vector2 endPosition = new Vector2(startPosition.x, startPosition.y - 1);

        while(timer < .5f)
        {
            timer += Time.deltaTime;
            block.position = Vector2.Lerp(startPosition, endPosition, timer / .5f);
            yield return null;
        }
    }

}

