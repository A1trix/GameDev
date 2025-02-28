using UnityEngine;

// Not integrated
public class Chest : MonoBehaviour
{
  private ChestAnimation chestAnimation;
  private Coin coin;
  public int value = 5;
  private void OnTriggerEnter2D(Collider2D collision)
  {
      // Check if the player entered the trigger and the chest is not already opened
      if (collision.CompareTag("Player") && !chestAnimation.isOpened)
      {
          coin.CollectCoin(value);
          coin.UpdateCoinUI();
          // Play the chest opening animation
          chestAnimation.OpenChest();
      }
  }
}