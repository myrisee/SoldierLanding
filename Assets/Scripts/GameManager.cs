using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(50, 50);
    public GameObject gridCell;

    public Player playerPrefab;

    ForwardCommand forwardCommand;
    RightCommand rightCommand;
    LeftCommand leftCommand;

    Player currentPlayer;

    Stack<Command> commands = new Stack<Command>();
    Command[] cmd;

    private void Start()
    {
        StartCoroutine(GenerateGrid());
    }

    public void SpawnPlayer(Vector2 dir,Vector2 pos)
    {
        currentPlayer = (Instantiate(playerPrefab.gameObject, new Vector3(pos.x,15,pos.y), Quaternion.identity) as GameObject).GetComponent<Player>();
        currentPlayer.transform.DOMoveY(1.5f, 0.3f).SetEase(Ease.InOutBack);
        currentPlayer.SetDirection(dir);
        currentPlayer.SetPosition(pos);
        forwardCommand = new ForwardCommand(currentPlayer);
        rightCommand = new RightCommand(currentPlayer);
        leftCommand = new LeftCommand(currentPlayer);
    }

    IEnumerator Play()
    {
        Command[] cmd = commands.ToArray();
        commands.Clear();
        
        for(int i = cmd.Length - 1; i >= 0; i--)
        {
            cmd[i].Execute();
            yield return new WaitForSeconds(currentPlayer.moveTime);
            yield return null;
        }
    }

    private void Update()
    {
        if (currentPlayer == null)
            return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            commands.Push(forwardCommand);
        }else if (Input.GetKeyDown(KeyCode.A))
        {
            commands.Push(leftCommand);
        }else if (Input.GetKeyDown(KeyCode.D))
        {
            commands.Push(rightCommand);
        }else if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Play());
        }
    }

    [Sirenix.OdinInspector.Button]
    public void GenerateGridButton()
    {
        StartCoroutine(GenerateGrid());
    }

    IEnumerator GenerateGrid()
    {
        int xPos = gridSize.x / 2;
        int yPos = gridSize.y / 2;

        for(int x = -xPos; x < xPos; x++)
        {
            for(int y = -yPos; y < yPos; y++)
            {
                SpawnCell(x, y);
            }

            yield return new WaitForSeconds(0.05f);
        }

        SpawnPlayer(Vector2.up, Vector2.zero);
    }

    void SpawnCell(int x,int y)
    {
        GameObject cellItem = Instantiate(gridCell, new Vector3((float)x, -5f, (float)y), Quaternion.identity,this.transform);
        cellItem.transform.DOMoveY(0f, 0.25f).SetEase(Ease.InOutBack);
        cellItem.name = $"{x}:{y}";
    }
}