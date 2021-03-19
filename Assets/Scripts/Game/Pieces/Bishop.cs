using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override List<Vector3Int> SelectAvaliableSquares()
    {
        avaliableMoves.Clear();

        //need to make which ones of these don't go if it's on a certain face
        //split into different loops. to do: how to make detect if it's a wrap?
        //if is in this y and this x, it is an edge. could make detect overall.
        //actually looks like matt already did this on calculate rotation
        //from coords


        //I think the farthest a bishop could travel without hitting a barrier
        //is 13 squares 
        for (int i = 0; i < 13; i++)
        {
            avaliableMoves.Add(occupiedSquare + new Vector3Int(i, i, 0));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(-i, -i, 0));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(0, i, i));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(0, -i, -i));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(i, 0, i));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(-i, 0, -i));
        }



        // Directions Always Acceptable:

        /* 
         * I copied what matt wrote to use as an example. thanks matt 
         * very cool
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 0, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 0, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 0, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 0, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 1, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, -1, 0));
        */

        base.SelectAvaliableSquares();

        return avaliableMoves;
    }
}
