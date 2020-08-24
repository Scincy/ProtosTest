using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayManager : MonoBehaviour
{
    public List<string> commands;

    private void Start()
    {
        ResetCommand();
    }

    public string GetCommand()
    {
        string result = commands[Random.Range(0, commands.Count)];
        commands.Remove(result);
        return result;
    }

    public void ResetCommand()
    {
        commands = new List<string>();
        commands.Add("left");
        commands.Add("right");
        commands.Add("jump");
        commands.Add("down");
    }
}
