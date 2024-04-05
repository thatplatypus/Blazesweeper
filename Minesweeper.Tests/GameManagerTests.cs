using Xunit;
using Minesweeper;

public class GameManagerTests
{
    [Fact]
    public void TestGameInitialization()
    {
        var gameManager = new GameManager(10, 10);
        Assert.Equal(GameStatus.Ongoing, gameManager.GetStatus());
    }

    [Fact]
    public void TestClickMine()
    {
        var gameManager = new GameManager(10, 1);
        var game = gameManager.Reset();
        for(int i = 0; i < 10; i ++)
        {
            for(int j = 0; j < 10; j++)
            {
                if (game[i][j].IsMine)
                {
                    Assert.Equal(GameStatus.Lost, gameManager.ClickTile(i, j));
                }
            }
        }

    }

    [Fact]
    public void TestClickEmptyTile()
    {
        var gameManager = new GameManager(10, 1);
        var game = gameManager.Reset();
        for(int i = 0; i < 10; i++)
        {
            if (!game[i][0].IsMine)
            {
                Assert.Equal(GameStatus.Ongoing, gameManager.ClickTile(i, 0));
                return;
            }
        }
    }

    [Fact]
    public void TestWinGame()
    {
        var gameManager = new GameManager(2, 1);
        var game = gameManager.Reset();
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                if (!game[i][j].IsMine)
                {
                   gameManager.ClickTile(i, j);
                }
            }
        }

        Assert.Equal(GameStatus.Won, gameManager.GetStatus());
    }
}