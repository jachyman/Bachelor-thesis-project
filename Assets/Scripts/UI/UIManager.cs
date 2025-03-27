using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TilemapReader tilemapReader;
    [SerializeField] private Board board;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject horizontalWallPrefab;
    [SerializeField] private GameObject verticalWallPrefab;

    [SerializeField] public TileBase horizontalWallTileBase;
    [SerializeField] public TileBase verticalWallTileBase;
    [SerializeField] public TileBase deadEnemyTileBase;

    [SerializeField] private TMP_Text turnCounterText;
    [SerializeField] private TMP_Text currentPlayer;
    [SerializeField] private TMP_Text wallsLeftText;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private Button nextLevelButton;

    public class UIWallInfo
    {
        public Vector2Int position;
        public bool isHorizontal;
        public UIWallInfo(Vector2Int position, bool isHorizontal)
        {
            this.position = position;
            this.isHorizontal = isHorizontal;
        }
    }

    private void Awake()
    {
        if (board == null)
        {
            Debug.LogError("UIManager board null");
        }
        else
        {
            GenerateWallInput();
        }
    }

    private Dictionary<GameObject, UIWallInfo> WallGameObjectPosition;

    public void MoveEnemyToTile(Enemy enemy, ITile tile)
    {
        Vector3Int from = new Vector3Int(enemy.Position.x, enemy.Position.y, 0);
        Vector3Int to = new Vector3Int(tile.Position.x, tile.Position.y, 0);

        tilemapReader.OnGroundTilemap.SetTile(to, enemy.TileBase);
        tilemapReader.OnGroundTilemap.SetTile(from, null);
    }

    public Tilemap GetOnGroundTilemap()
    {
        return tilemapReader.OnGroundTilemap;
    }
    public Tilemap GetHorizontalWallsTilemap()
    {
        return tilemapReader.HorizonatalWallTilemap;
    }
    public Tilemap GetVerticalWallsTilemap()
    {
        return tilemapReader.VerticallWallTilemap;
    }

    public void WallSetActive(Wall wall, bool active)
    {
        Tilemap tilemap;
        if (wall.IsHorizontal)
        {
            tilemap = tilemapReader.HorizonatalWallTilemap;
        }
        else
        {
            tilemap = tilemapReader.VerticallWallTilemap;
        }

        Vector3Int tilemapPosition = new Vector3Int(wall.Position.x, wall.Position.y, 0);
        tilemap.SetTile(tilemapPosition, active ? wall.TileBase : null);
    }

    private void GenerateWallInput()
    {
        WallGameObjectPosition = new Dictionary<GameObject, UIWallInfo>();

        for (int row = 0; row < board.GetRows(); row++)
        {
            float horY = 1.05f + row + (row * 0.1f);
            float verY = 0.5f + row + (row * 0.1f);
            for (int col = 0; col < board.GetCols(); col++)
            {
                float horX = 0.5f + col + (col * 0.1f);
                float verX = 1.05f + col + (col * 0.1f);

                if (row < board.GetRows() - 1)
                {
                    Vector2Int position = new Vector2Int(col, row + 1);
                    UIWallInfo wallInfo = new UIWallInfo(position, true);
                    WallGameObjectPosition[Instantiate(horizontalWallPrefab, new Vector3(horX, horY, 0), Quaternion.identity)] = wallInfo;
                }
                if (col < board.GetCols() - 1)
                {
                    Vector2Int position = new Vector2Int(col + 1, row);
                    UIWallInfo wallInfo = new UIWallInfo(position, false);
                    WallGameObjectPosition[Instantiate(verticalWallPrefab, new Vector3(verX, verY, 0), Quaternion.identity)] = wallInfo;
                }
            }
        }
    }

    public UIWallInfo GetWallInfoFromWallGameObject(GameObject wallGameObject)
    {
        return WallGameObjectPosition[wallGameObject];
    }

    public void AddWall(Wall wall)
    {
        WallSetActive(wall, true);
    }

    public Wall GenerateWall(GameObject wallGameObject)
    {
        UIWallInfo wallInfo = GetWallInfoFromWallGameObject(wallGameObject);
        Tilemap tilemap = wallInfo.isHorizontal ? GetHorizontalWallsTilemap() : GetVerticalWallsTilemap();
        TileBase tileBase = wallInfo.isHorizontal ? horizontalWallTileBase : verticalWallTileBase;

        Wall wall = new Wall(wallInfo.position, wallInfo.isHorizontal, tileBase, tilemap);
        return wall;
    }

    public void UpdateUI()
    {
        int currentTurn = gameManager.GetCurrentTurn();
        int goalTurn = gameManager.GetGoalTurnCount();
        turnCounterText.text =
            "turn " +
            currentTurn +
            " / " +
            goalTurn;

        currentPlayer.text = gameManager.IsPlayerTurn() ? "Player turn" : "Enemy turn";
        wallsLeftText.text = "walls left " + gameManager.GetWallsLeft();
    }

    public void GameOver(bool playerWon)
    {
        gameOverText.text = playerWon ? "Player won" : "Player lost";
        if (playerWon)
        {
            nextLevelButton.gameObject.SetActive(true);
        }
    }
}