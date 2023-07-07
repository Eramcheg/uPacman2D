using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

//Trida pro ghosty s 3 algoritmy. Toto je trida o chovani, kdyz ghost hledá pacmana a chce chytit ho. Mezi utoky dela scatter ( pauzy)


public class Ghost_Chase : Ghost_Behaviour
{
    public string algorithm = "Simple";
    private void OnDisable()
    {
        this.ghost.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        Node_ node = collision.GetComponent<Node_>();
       
        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            FindObjectOfType<GameManager_>();
            //Mode 1(Simple)
            if(algorithm == "Simple")
            this.ghost.movement.SetDirection(SimpleFinder(node));

            //Mode 2 (Simple+)
            else if(algorithm=="Simple+")
            this.ghost.movement.SetDirection(SimpleFinderPlus(node));

            //Mode 3 (BFS)
            else
            this.ghost.movement.SetDirection(BFS(node));

        }

    }



    //Super simple, ghosty hledaji pacmana pomoci kolmo od dosazitelnych uzlu, a ghost jde do uzlu, od ktereho nejkratsi kolmo
    private Vector2 SimpleFinder(Node_ node)
    {
        Vector2 direction = Vector2.zero;
        float minDistance = float.MaxValue;

        foreach (Vector2 availableDir in node.availableDir)
        {
            Vector3 newPos = this.transform.position + new Vector3(availableDir.x, availableDir.y, 0);
            float distance = (this.ghost.target.position - newPos).sqrMagnitude;//
            if (distance < minDistance)
            {
                direction = availableDir;
                minDistance = distance;
            }
        }
        return direction;
    }


    private Vector2 SimpleFinderPlus(Node_ node)
    {
        Vector2 direction = Vector2.zero;
        float minDistance = float.MaxValue;

        foreach (Vector2 availableDir in node.availableDir)
        {
            //LayerMask layerMask = new LayerMask();
            //RaycastHit2D hit = Physics2D.Raycast(new Vector2(this.transform.position.x + availableDir.x, this.transform.position.y + availableDir.y), Vector2.zero);
            //if (hit.collider != null)
            //{
            //    layerMask = hit.collider.gameObject.layer;
            //}
            Vector3 newPos;
            //Debug.Log(layerMask.value);
            //if (layerMask == 12)
            //{
            //    newPos = this.transform.position + new Vector3(-availableDir.x, availableDir.y, 0);
            //}
            //else
            //{
            //    newPos = this.transform.position + new Vector3(availableDir.x, availableDir.y, 0);
            //}
            //Debug.Log(this.ghost.target.transform.right);
            Vector3 targetPos = this.ghost.target.position;

            newPos = this.transform.position + new Vector3(availableDir.x, availableDir.y, 0);
            float distance = ((this.ghost.target.position + 2*this.ghost.target.transform.right) - newPos).sqrMagnitude; //Tady ghost nehleda pacmana, ale point pred nim, aby najit cestu rychelsi nez pacman
            if (distance < minDistance)
            {
                direction = availableDir;
                minDistance = distance;
            }
        }
        return direction;
    }

    public float GetValue(float number)
    {
        if (number - Math.Floor(number) == 0)
        {
            return (float)(number + 0.5);
        }
        else
        {   
            float numberAfterPoint = (float)(number - Math.Floor(number));
            if (numberAfterPoint - 0.5f == 0)
            {
                return number;
            }
            else
            {
                if (number > 0)
                {
                    return (float)((int)number) + 0.5f;
                }
                else
                {
                    return (float)((int)number) - 0.5f;
                }
            }
        }
    }

    //BFS, ghost dela cestu, a potom jde po ni. Pomoci BFS ghost nikdy nevybere spatny nod, od ktereho bude potrebovat vetsi cestu.
    public Vector2 BFS(Node_ startNode)
    {
        float finalPositionCoorX = GetValue(ghost.target.transform.position.x);
        float finalPositionCoorY = GetValue(ghost.target.transform.position.y);

        float startPositionCoorX = GetValue(startNode.transform.position.x);
        float startPositionCoorY = GetValue(startNode.transform.position.y);

        ValueTuple<int[], Vector2> startPosition = new ValueTuple<int[], Vector2>(new int[] { 0, 0 }, new Vector2(startPositionCoorX, startPositionCoorY));
        ValueTuple<int[], Vector2> finalPosition = new ValueTuple<int[], Vector2>(new int[] { 0, 0 }, new Vector2(finalPositionCoorX, finalPositionCoorY));

        //Debug.Log(ghost.target.transform.position);
        //Vector2 v = new Vector2(1, 2);

        float node_start_x = -21.5f;
        //float node_end_x = 21.5f;
        float node_start_y = 14.5f;
        //float node_end_y = -16.5f;

        ValueTuple<bool, bool, Vector2>[,] road = new ValueTuple<bool, bool, Vector2>[32, 44];


        float temp_y = node_start_y;
        for (int i = 0; i < road.GetLength(0); i++)
        {
            float temp_x = node_start_x;
            for (int j = 0; j < road.GetLength(1); j++)
            {
                if (startPosition.Item2.x == temp_x && startPosition.Item2.y == temp_y)
                {
                    startPosition.Item1[0] = i;
                    startPosition.Item1[1] = j;
                }

                if (finalPosition.Item2.x == temp_x && finalPosition.Item2.y == temp_y)
                {
                    finalPosition.Item1[0] = i;
                    finalPosition.Item1[1] = j;
                }

                LayerMask layerMask = new LayerMask();
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(temp_x, temp_y), Vector2.zero);
                if (hit.collider != null)
                {
                    layerMask = hit.collider.gameObject.layer;
                  //  Debug.Log(layerMask.value);
                }
                //Debug.Log(layerMask.value);
                if (layerMask == 11 || layerMask == 9 || layerMask == 0 || layerMask == 6 || layerMask == 12)
                {
                    road[i, j] = new ValueTuple<bool, bool, Vector2>(false, true, new Vector2(temp_x, temp_y));
                }
                else
                {
                    road[i, j] = new ValueTuple<bool, bool, Vector2>(false, false, new Vector2(temp_x, temp_y));
                }

                temp_x += 1f;
            }
            temp_y -= 1f;
        }

        Queue<ValueTuple<int[], List<int[]>>> queue = new Queue<ValueTuple<int[], List<int []>>>();

        queue.Enqueue(new ValueTuple<int[], List<int[]>>(startPosition.Item1, new List<int[]>()));

        ValueTuple<int[], List<int[]>> output = new ValueTuple<int[], List<int[]>>();

        while ( queue.Count > 0 )
        {
            ValueTuple<int[], List<int[]>> temp_point = queue.Dequeue();
            List<int[]> oldRoad = temp_point.Item2;

            if (!road[temp_point.Item1[0], temp_point.Item1[1]].Item1)
            {
                if (temp_point.Item1[0] == finalPosition.Item1[0] && temp_point.Item1[1] == finalPosition.Item1[1])
                {
                    output = temp_point;
                    break;
                }

                if (temp_point.Item1[0] + 1 < road.GetLength(0))
                {
                    if (road[temp_point.Item1[0] + 1, temp_point.Item1[1]].Item1 == false && road[temp_point.Item1[0] + 1, temp_point.Item1[1]].Item2 == true)
                    {
                        List<int[]> newRoad = new List<int[]>(oldRoad);
                        newRoad.Add(new int[] { temp_point.Item1[0] + 1, temp_point.Item1[1] });

                        queue.Enqueue(new ValueTuple<int[], List<int[]>>(new int[] { temp_point.Item1[0] + 1, temp_point.Item1[1] }, newRoad));
                    }
                }

                if (temp_point.Item1[0] - 1 > 0)
                {
                    if (road[temp_point.Item1[0] - 1, temp_point.Item1[1]].Item1 == false && road[temp_point.Item1[0] - 1, temp_point.Item1[1]].Item2 == true)
                    {
                        List<int[]> newRoad = new List<int[]>(oldRoad);
                        newRoad.Add(new int[] { temp_point.Item1[0] - 1, temp_point.Item1[1] });

                        queue.Enqueue(new ValueTuple<int[], List<int[]>>(new int[] { temp_point.Item1[0] - 1, temp_point.Item1[1] }, newRoad));
                    }
                }

                if (temp_point.Item1[1] + 1 < road.GetLength(1))
                {
                    if (road[temp_point.Item1[0], temp_point.Item1[1] + 1].Item1 == false && road[temp_point.Item1[0], temp_point.Item1[1] + 1].Item2 == true)
                    {
                        List<int[]> newRoad = new List<int[]>(oldRoad);
                        newRoad.Add(new int[] { temp_point.Item1[0], temp_point.Item1[1] + 1 });

                        queue.Enqueue(new ValueTuple<int[], List<int[]>>(new int[] { temp_point.Item1[0], temp_point.Item1[1] + 1 }, newRoad));
                    }
                }

                if (temp_point.Item1[1] - 1 > 0)
                {
                    if (road[temp_point.Item1[0], temp_point.Item1[1] - 1].Item1 == false && road[temp_point.Item1[0], temp_point.Item1[1] - 1].Item2 == true)
                    {
                        List<int[]> newRoad = new List<int[]>(oldRoad);
                        newRoad.Add(new int[] { temp_point.Item1[0], temp_point.Item1[1] - 1 });

                        queue.Enqueue(new ValueTuple<int[], List<int[]>>(new int[] { temp_point.Item1[0], temp_point.Item1[1] - 1 }, newRoad));
                    }
                }
            }

            road[temp_point.Item1[0], temp_point.Item1[1]].Item1 = true;
        }

        if (output.Item2 != null)
        {
            Vector2 outputVector = road[output.Item2[0][0], output.Item2[0][1]].Item3;
            if (startPositionCoorX > outputVector.x && startPositionCoorY == outputVector.y)
            {
                return Vector2.left;
            }
            if (startPositionCoorX < outputVector.x && startPositionCoorY == outputVector.y)
            {
                return Vector2.right;
            }
            if (startPositionCoorX == outputVector.x && startPositionCoorY > outputVector.y)
            {
                return Vector2.down;
            }
            if (startPositionCoorX == outputVector.x && startPositionCoorY < outputVector.y)
            {
                return Vector2.up;
            }

            return outputVector;
        }
        else
        {
            Debug.Log("NULL");
            //int randomNumber = new System.Random().Next(0, 4);
    
            return SimpleFinder(startNode); 
            
        }
       
        
    }


    //A*
    //public Vector2 FindPath(Node_ startNode)
    //{
    //    // Create open and closed sets to keep track of visited and unvisited nodes
    //    HashSet<Node_> openSet = new HashSet<Node_>();
    //    HashSet<Node_> closedSet = new HashSet<Node_>();

    //    // Add the start node to the open set
    //    openSet.Add(startNode);

    //    while (openSet.Count > 0)
    //    {
    //        Node_ currentNode = null;

    //        // Find the node with the lowest F cost in the open set
    //        foreach (Node_ node in openSet)
    //        {
    //            if (currentNode == null || node.FCost < currentNode.FCost)
    //            {
    //                currentNode = node;
    //            }
    //        }

    //        // Remove the current node from the open set and add it to the closed set
    //        openSet.Remove(currentNode);
    //        closedSet.Add(currentNode);


    //        // Check each neighbor of the current node
    //        foreach (Vector2 neighborPos in currentNode.availableDir)
    //        {
    //            // Get the Node_ component attached to the neighbor position
    //            Node_ neighborNode = GetNodeComponentAtPosition(neighborPos + new Vector2(currentNode.transform.position.x, currentNode.transform.position.y));

    //            // Skip if the neighbor node is null, in the closed set, or an obstacle
    //            if (neighborNode == null || closedSet.Contains(neighborNode))
    //            {
    //                continue;
    //            }

    //            // Calculate the G cost for the neighbor node
    //            float newGCost = currentNode.GCost + Vector2.Distance(currentNode.transform.position, neighborPos);

    //            // Check if the neighbor node is already in the open set
    //            if (!openSet.Contains(neighborNode))
    //            {
    //                // Add the neighbor node to the open set
    //                openSet.Add(neighborNode);
    //            }
    //            else if (newGCost >= neighborNode.GCost)
    //            {
    //                // Skip if the new G cost is not better than the previous G cost
    //                continue;
    //            }

    //            // Update the neighbor node's G and H costs and set its parent to the current node
    //            neighborNode.GCost = newGCost;
    //            neighborNode.HCost = Vector2.Distance(neighborPos, this.ghost.target.transform.position);
    //            neighborNode.Parent = currentNode;
    //        }
    //    }

    //    // No path found
    //    return Vector2.zero;
    //}

    //private Node_ GetNodeComponentAtPosition(Vector2 position)
    //{
    //    Collider2D[] colliders = Physics2D.OverlapPointAll(position);

    //    foreach (Collider2D collider in colliders)
    //    {
    //        Node_ node = collider.GetComponent<Node_>();
    //        if (node != null)
    //        {
    //            return node;
    //        }
    //    }

        
    //    return null;
    //}

    //private bool IsObstacle(Node_ node)
    //{
    //    // Add your obstacle check logic here
    //    return false;
    
    //}







    //public float detectionRadius = 5f; // set this to the proximity you want

    //public Vector2 DFSPathFind(Node_ startNode)
    //{
    //    // Create visited set
    //    HashSet<Node_> visited = new HashSet<Node_>();

    //    // Create stack for DFS and add start node to it
    //    Stack<Node_> stack = new Stack<Node_>();
    //    stack.Push(startNode);

    //    // Dictionary for path
    //    Dictionary<Node_, Node_> path = new Dictionary<Node_, Node_>();

    //    while (stack.Count > 0)
    //    {
    //        Node_ currentNode = stack.Pop();

    //        // Mark the node as visited right after popping
    //        visited.Add(currentNode);

    //        // Check if the Ghost is near enough to Pacman
    //        if (Vector2.Distance(currentNode.transform.position, ghost.target.transform.position) <= detectionRadius)
    //        {
    //            Node_ backTrackNode = currentNode;

    //            // While there is a node in the path that led to the current node
    //            while (path.ContainsKey(backTrackNode))
    //            {
    //                // Backtrack
    //                backTrackNode = path[backTrackNode];
    //            }

    //            // Return the first node's direction in the path from start to target
    //            return (backTrackNode.transform.position - startNode.transform.position).normalized;
    //        }

    //        // Get all the neighbours
    //        foreach (Vector2 direction in currentNode.availableDir)
    //        {
    //            RaycastHit2D hit = Physics2D.BoxCast(currentNode.transform.position, Vector2.one * 0.5f, 0f, direction, 1f);

    //            // If the hit is not null and is a Node_, then it's a neighbour
    //            if (hit.collider != null)
    //            {
    //                Node_ neighbour = hit.collider.gameObject.GetComponent<Node_>();

    //                // If the neighbour is not null and has not been visited
    //                if (neighbour != null && !visited.Contains(neighbour))
    //                {
    //                    stack.Push(neighbour);

    //                    // Add neighbour to path
    //                    if (!path.ContainsKey(neighbour))
    //                    {
    //                        path.Add(neighbour, currentNode);
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    // No path found
    //    return Vector2.zero;
    //}
}
