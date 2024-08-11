using System;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    [SerializeField] private int floor;
    [SerializeField] private int row;
    [SerializeField] private int col;
    [SerializeField] private Vector3 posStart;
    [SerializeField] private List<FloorDrop> lstFloorDrop = new List<FloorDrop>();


    public void FindPosCanSpawn(out bool canSpawn, out Vector3 posSpawn, out int idFloor, out int idPlace)
    {
        canSpawn = false;
        posSpawn = Vector3.zero;
        idFloor = 0;
        idPlace = 0;


        var _countFLoor = lstFloorDrop.Count;
        var _countPlace = row * col;
        for (int i = 0; i < _countFLoor; i++)
        {
            for (int j = 0; j < _countPlace; j++)
            {
                if (lstFloorDrop[i].lstPlaces[j].isFull) continue;
                lstFloorDrop[i].lstPlaces[j].isFull = true;
                posSpawn = lstFloorDrop[i].lstPlaces[j].pos;
                idFloor = i;
                idPlace = j;
                canSpawn = true;
                return;
            }
        }
    }

    public void ResetPlace(int idFloor, int idPlace)
    {
        lstFloorDrop[idFloor].lstPlaces[idPlace].isFull = false;
    }





    [ContextMenu(nameof(GenPosDrop))]
    public void GenPosDrop()
    {
        posStart = transform.position;

        lstFloorDrop = new List<FloorDrop>();
        var _placeCount = row * col;

        for (int i = 0; i < floor; i++)
        {
            lstFloorDrop.Add(new FloorDrop());
            lstFloorDrop[i].lstPlaces = new List<Place>();
            for (int m = 0; m < _placeCount; m++)
            {
                lstFloorDrop[i].lstPlaces.Add(new Place());
            }

            var _realY = posStart.y + (i * 0.2f);
            var _indexPlace = 0;
            for (int j = 0; j < row; j++)
            {
                var _realX = posStart.x - (j * 0.4f);
                for (int k = 0; k < col; k++)
                {
                    var _realZ = posStart.z - (k * 0.75f);

                    lstFloorDrop[i].lstPlaces[_indexPlace].pos = new Vector3(_realX, _realY, _realZ);
                    lstFloorDrop[i].lstPlaces[_indexPlace].isFull = false;
                    _indexPlace++;
                }
            }
        }
    }
}

[Serializable]
public class FloorDrop
{
    public List<Place> lstPlaces;

}

[Serializable]
public class Place
{
    public Vector3 pos;
    public bool isFull;
}
