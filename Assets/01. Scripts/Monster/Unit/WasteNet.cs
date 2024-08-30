using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WasteNet : Monster
{
    [Header("PollutionAir")]
    [SerializeField] private float monsterScale = default;


    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        Flip();
    }

    private void Flip()
    {
        // Get the SpriteRenderer component attached to the same GameObject
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Check the position of the target relative to the monster
        if (targetTransform.position.x >= transform.position.x)
        {
            // If the target is on the right, flip the sprite to face right
            spriteRenderer.flipX = true;
        }
        else
        {
            // If the target is on the left, flip the sprite to face left
            spriteRenderer.flipX = false;
        }
    }
}
