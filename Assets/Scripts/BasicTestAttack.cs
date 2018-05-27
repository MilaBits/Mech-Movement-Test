using UnityEngine;

public class BasicTestAttack : IMoveAction {
    public string debugMessage;
    public void Execute() {
        Debug.Log(debugMessage);
    }
}