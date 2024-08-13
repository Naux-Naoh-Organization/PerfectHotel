using UnityEngine;

public class BotCharacter : Character
{


    public void CharacterMove(Vector2 direct)
    {
        moveHandle.MoveToDirection(direct);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
