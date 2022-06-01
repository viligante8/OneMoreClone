using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private Block blockPrefab;
    private int playWidth = 6;
    private float blockDistance = 0.9f;
    private int rowsSpawned = 0;

    private List<Block> blocks = new List<Block>();

    private void OnEnable()
    {
        foreach(var i in Enumerable.Range(0, 1))
        {
            SpawnNewRow();
        }
    }

    public void SpawnNewRow()
    {
        foreach (var block in blocks)
        {
            if(block != null)
                block.transform.position += Vector3.down * blockDistance;
        }
        foreach (var i in Enumerable.Range(0, playWidth))
        {
            if(Random.Range(0, 100) <= 30)
            {
                var block = Instantiate(blockPrefab, GetPosition(i), Quaternion.identity);
                var hits = Random.Range(1, 3) + rowsSpawned;

                block.SetHits(hits);

                blocks.Add(block);
            }
        }
        rowsSpawned++;
    }

    private Vector3 GetPosition(int i)
    {
        var position = transform.position;
        position += Vector3.right * i * blockDistance;

        return position;
    }
}
