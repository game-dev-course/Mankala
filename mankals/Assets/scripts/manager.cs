using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    // Start is called before the first frame update
    public int length;
    public int height;
    public int steps;
    public int shift;
    public GameObject prefab_hole;
    public GameObject prefab_home;
    public int stone_amount;
    GameObject[,] board;
    GameObject home_one;
    GameObject home_two;

    bool turn = true;

    void Start()
    {
        this.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = "down player turn";
        board = new GameObject[length, height];
        // Get the position of the left and right edges of the game window in world coordinates
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0f));

        // Calculate the width of the world
        float worldWidth = Vector3.Distance(leftEdge, rightEdge);

        // Get the position of the top and bottom edges of the game window in world coordinates
        Vector3 topEdge = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0f));
        Vector3 bottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f));

        // Calculate the height of the world
        float worldHeight = Vector3.Distance(topEdge, bottomEdge);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < length; j++)
            {
                GameObject hole = (GameObject)Instantiate(prefab_hole);
                board[j, i] = hole;
                Vector3 movement = new Vector3(j * steps - (worldWidth + shift) / 2, i * steps - (worldHeight) / 8, 0);
                hole.GetComponent<hole_press>().x_val = j;
                hole.GetComponent<hole_press>().y_val = i;
                hole.GetComponent<hole_press>().size = stone_amount;
                hole.GetComponent<hole_press>().manager = this;
                hole.GetComponent<Transform>().position = movement;
                hole.GetComponent<hole_press>().change_number();
            }
        }
        CreateHome(worldHeight, worldWidth);
        CreateHome2(worldHeight, worldWidth);
    }

    void CreateHome(float worldHeight, float worldWidth)
    {
        home_one = (GameObject)Instantiate(prefab_home);
        Vector3 movement = new Vector3(-2 * steps / 3 - (worldWidth + shift) / 2, (worldHeight) / 32, 0);
        home_one.GetComponent<hole_press>().x_val = -1;
        home_one.GetComponent<hole_press>().y_val = -1;
        home_one.GetComponent<hole_press>().size = 0;
        home_one.GetComponent<hole_press>().manager = this;
        home_one.GetComponent<Transform>().position = movement;
    }
    void CreateHome2(float worldHeight, float worldWidth)
    {
        home_two = (GameObject)Instantiate(prefab_home);
        Vector3 movement = new Vector3(length * steps - steps / 3 - (worldWidth + shift) / 2, (worldHeight) / 32, 0);
        home_two.GetComponent<hole_press>().x_val = -1;
        home_two.GetComponent<hole_press>().y_val = -1;
        home_two.GetComponent<hole_press>().size = 0;
        home_two.GetComponent<hole_press>().manager = this;
        home_two.GetComponent<Transform>().position = movement;

    }
    public void pressed(int i, int j, int amount)
    {
        if (j == 0 && !turn)
        {
            return;
        }
        if (j == 1 && turn)
        {
            return;
        }
        turn = !turn;
        if (!turn)
        {
            this.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = "up player turn";
        }
        else
        {
            this.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = "down player turn";
        }
        GameObject org = board[i, j];
        org.GetComponent<hole_press>().size = 0;
        org.GetComponent<hole_press>().change_number();
        Debug.Log(i + ", " + j + " amount: " + amount);
        if (j == 0)
        {
            for (int k = i + 1; k < length; k++)
            {
                if (amount != 0)
                {
                    amount--;
                    GameObject hole = board[k, j];
                    hole.GetComponent<hole_press>().size++;
                    hole.GetComponent<hole_press>().change_number();
                }
            }
            if (amount > 0)
            {
                amount--;
                home_two.GetComponent<hole_press>().size++;
                home_two.GetComponent<hole_press>().change_number();
                j = 1;
                i = length;
            }
        }
        if (j == 1)
        {
            for (int k = i - 1; k >= 0; k--)
            {
                if (amount != 0)
                {
                    amount--;
                    GameObject hole = board[k, j];
                    hole.GetComponent<hole_press>().size++;
                    hole.GetComponent<hole_press>().change_number();
                }
            }
            if (amount > 0)
            {
                amount--;
                home_one.GetComponent<hole_press>().size++;
                home_one.GetComponent<hole_press>().change_number();
                i = -1;
                j = 0;
            }
            for (int k = i + 1; k < length; k++)
            {
                if (amount != 0)
                {
                    amount--;
                    GameObject hole = board[k, j];
                    hole.GetComponent<hole_press>().size++;
                    hole.GetComponent<hole_press>().change_number();
                }
            }
            if (amount > 0)
            {
                amount--;
                home_two.GetComponent<hole_press>().size++;
                home_two.GetComponent<hole_press>().change_number();
                j = 1;
                i = length;
            }
        }
    }
}
