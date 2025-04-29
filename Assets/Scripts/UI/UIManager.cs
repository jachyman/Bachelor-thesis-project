using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TilemapReader tilemapReader;
    [SerializeField] private Board board;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject horizontalWallPrefab;
    [SerializeField] private GameObject verticalWallPrefab;
    [SerializeField] private GameObject verticalPlayerWallPrefab;

    [SerializeField] public TileBase horizontalWallTileBase;
    [SerializeField] public TileBase verticalWallTileBase;
    [SerializeField] public TileBase deadEnemyTileBase;
    [SerializeField] private TileBase switchTileOnTileBase;
    [SerializeField] private TileBase switchTileOffTileBase;

    [SerializeField] private TMP_Text turnCounterText;
    [SerializeField] private TMP_Text enemyMovementText;
    [SerializeField] private TMP_Text currentPlayer;
    [SerializeField] private TMP_Text wallsLeftText;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private TMP_Text gameOverReasonText;
    [SerializeField] private TMP_Text hintCountText;
    //[SerializeField] private UnityEngine.UI.Button nextLevelButton;
    [SerializeField] private GameObject nextLevelButton;

    private GameObject[,] horizontalWallInputsList;
    private GameObject[,] verticalWallInputsList;
    private Color hintHighlightColor = Color.green;

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
            if (verticalPlayerWallPrefab != null)
            {
                GenerateWallsLeftUI();
            }
        }
    }

    private Dictionary<GameObject, UIWallInfo> WallGameObjectPosition;
    private List<GameObject> WallsLeftList;

    public void MoveEnemyToTile(Enemy enemy, ITile tile)
    {
        Vector3Int from = new Vector3Int(enemy.Position.x, enemy.Position.y, 0);
        Vector3Int to = new Vector3Int(tile.Position.x, tile.Position.y, 0);

        tilemapReader.OnGroundTilemap.SetTile(to, enemy.TileBase);
        tilemapReader.OnGroundTilemap.SetTile(from, null);
    }

    public void KillEnemy(Enemy enemy)
    {
        Vector3Int tilemapPosition = new Vector3Int(enemy.Position.x, enemy.Position.y, 0);
        GetOnGroundTilemap().SetTile(tilemapPosition, deadEnemyTileBase);
    }

    public void SwitchSwitchTile(SwitchTile tile)
    {
        Vector3Int tilePosition = new Vector3Int(tile.Position.x, tile.Position.y, 0);
        tilemapReader.GroundTilemap.SetTile(tilePosition, tile.IsSwitchedOn ? switchTileOnTileBase : switchTileOffTileBase);
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
        int rows = board.GetRows();
        int columns = board.GetCols();

        WallGameObjectPosition = new Dictionary<GameObject, UIWallInfo>();
        horizontalWallInputsList = new GameObject[rows - 1, columns];
        verticalWallInputsList = new GameObject[rows, columns - 1];

        for (int row = 0; row < rows; row++)
        {
            float horY = 1.05f + row + (row * 0.1f);
            float verY = 0.5f + row + (row * 0.1f);
            for (int col = 0; col < columns; col++)
            {
                float horX = 0.5f + col + (col * 0.1f);
                float verX = 1.05f + col + (col * 0.1f);

                if (row < rows - 1)
                {
                    Vector2Int position = new Vector2Int(col, row + 1);
                    UIWallInfo wallInfo = new UIWallInfo(position, true);
                    GameObject wall = Instantiate(horizontalWallPrefab, new Vector3(horX, horY, 0), Quaternion.identity);
                    WallGameObjectPosition[wall] = wallInfo;
                    horizontalWallInputsList[row, col] = wall;
                }
                if (col < columns - 1)
                {
                    Vector2Int position = new Vector2Int(col + 1, row);
                    UIWallInfo wallInfo = new UIWallInfo(position, false);
                    GameObject wall = Instantiate(verticalWallPrefab, new Vector3(verX, verY, 0), Quaternion.identity);
                    WallGameObjectPosition[wall] = wallInfo;
                    verticalWallInputsList[row, col] = wall;
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
        UpdateWallsLeftUI();
    }

    public void RemoveWall(Wall wall)
    {
        WallSetActive(wall, false);
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
        if (turnCounterText != null)
        {
            turnCounterText.text =
                currentTurn +
                " / " +
                goalTurn;
        }

        if (currentPlayer  != null)
        {
            currentPlayer.text = gameManager.IsPlayerTurn() ? "PLAYER TURN" : "ENEMY TURN";
        }

        /*
        if (wallsLeftText != null)
        {
            wallsLeftText.text = "WALLS LEFT " + gameManager.GetWallsLeft();
        }
        */

        if (verticalPlayerWallPrefab != null)
        {
            UpdateWallsLeftUI();
        }
        else if (wallsLeftText != null)
        {
            wallsLeftText.text = "WALLS LEFT " + gameManager.GetWallsLeft();
        }

        if (enemyMovementText != null)
        {
            enemyMovementText.text = "enemy movement: " + gameManager.GetEnemyMovement();
        }

        if (hintCountText != null)
        {
            hintCountText.text = gameManager.GetHintIndex().ToString();
        }
    }

    private void GenerateWallsLeftUI()
    {
        WallsLeftList = new List<GameObject>();
        float verX = -0.5f;
        float verY = 3.6f;
        for (int i = 0; i < gameManager.GetWallsPerTurn(); i++)
        {
            WallsLeftList.Add(Instantiate(verticalPlayerWallPrefab, new Vector3(verX, verY, 0), Quaternion.identity));
            verX -= 0.5f;
        }
    }

    public void UpdateWallsLeftUI()
    {
        for (int i = 0; i < WallsLeftList.Count; ++i)
        {
            if (i >= gameManager.GetWallsLeft())
            {
                WallsLeftList[i].SetActive(false);
            }
            else
            {
                WallsLeftList[i].SetActive(true);
            }
        }
    }

    public void ShowHintWall(GameManager.Hint hint)
    {
        GameObject wall;
        if (hint.wallType == GameManager.WallType.Vertical)
        {
            wall = verticalWallInputsList[hint.x, hint.y];
        }
        else
        {
            wall = horizontalWallInputsList[hint.x, hint.y];
        }

        wall.GetComponent<SpriteRenderer>().color = hintHighlightColor;
    }

    public void GameOver(GameManager.GameOver gameOver)
    {
        gameOverText.text = gameOver == GameManager.GameOver.PlayerWon ? "PLAYER WON" : "PLAYER LOST";
        if (gameOver == GameManager.GameOver.PlayerWon)
        {
            nextLevelButton.gameObject.SetActive(true);
        }
        else if (gameOverReasonText !=  null)
        {
            switch (gameOver)
            {
                case GameManager.GameOver.PlayerLostLastTurn:
                    gameOverReasonText.text = "IT WAS LAST TURN";
                    break;
                case GameManager.GameOver.PlayerLostGoalBlocked:
                    gameOverReasonText.text = "ALL GOAL TILES ARE BLOCKED";
                    break;
                case GameManager.GameOver.PlayerLostEnemyReachedGoal:
                    gameOverReasonText.text = "BUTTON HAS REACHED GOAL TILE";
                    break;

            }
        }
    }
}