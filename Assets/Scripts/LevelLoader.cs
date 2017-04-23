using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class LevelLoader : MonoBehaviour {

    public GameObject[] LevelPrefabs;
    public int GridSize = 64;
    public TextAsset[] LevelDefinitions;
    public Transform ParentTransform;

	// Use this for initialization
	void Start () {
        LoadLevel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LoadLevel()
    {
        string[] levelDef = Regex.Split(LevelDefinitions[0].ToString(), "\r\n");
        Debug.Log(levelDef);

        // read lines in reverse order
        // start at 2 last line - last is just the blank at end
        for (int i = 2; i <= levelDef.Length; i++)
        {
            string[] tiles = Regex.Split(levelDef[levelDef.Length - i], ",");
            for (int j = 0; j < tiles.Length; j++)
            { 
                if (tiles[j] != string.Empty)
                {
                    int tile = int.Parse(tiles[j]);
                    GameObject.Instantiate(LevelPrefabs[tile], new Vector3(j * GridSize, (i-2) * GridSize, 0), Quaternion.identity, ParentTransform);
                }

            }
        }
    }
}
