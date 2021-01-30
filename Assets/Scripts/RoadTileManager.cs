using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTileManager : MonoBehaviour
{
    public List<GameObject> roadTiles;

    [SerializeField] Transform endOfTile;

    [SerializeField] GameObject currentRoadTile;
    [SerializeField] GameObject forwardRoadTile;
    [SerializeField] GameObject backRoadTile;
    [SerializeField] GameObject destroyRoadTile;

    [SerializeField] Vector3 localEulerAngles;

    [SerializeField] PlayerController playerCar;

    private void Start()
    {
        playerCar.TriggerEnter += NewTile;
    }

    void NewTile()
    {
        if (backRoadTile != null)
            if (destroyRoadTile != backRoadTile)
                Destroy(destroyRoadTile);

        destroyRoadTile = backRoadTile;
        backRoadTile = currentRoadTile;
        currentRoadTile = forwardRoadTile;
        endOfTile = currentRoadTile.transform.Find("EndOfTile");
        forwardRoadTile = Instantiate(roadTiles[Random.Range(0, roadTiles.Count)], endOfTile);
        forwardRoadTile.transform.parent = currentRoadTile.transform.parent;
    }
}
