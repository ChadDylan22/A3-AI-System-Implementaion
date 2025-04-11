using UnityEngine;

public static class Extensions
{
    //Allows player script to use circle cast when finding if player is grounded
    private static LayerMask layermask = LayerMask.GetMask("Default");
   public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
   {
        if (rigidbody.isKinematic) 
        {
            return false;
        }

        float radius = 0.25f;
        float distance = 0.375f;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction, distance, layermask);
        return hit.collider != null && hit.rigidbody != rigidbody;
   }
}
