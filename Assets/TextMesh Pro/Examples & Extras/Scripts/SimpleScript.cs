using UnityEngine;
using System.Collections;

    public class SimpleScript : MonoBehaviour
    {
        // Tham chiếu đến Canvas mà bạn muốn hiện lên
        public GameObject panel;

        // Khi Player chạm vào đối tượng này
        private void OnTriggerEnter(Collider other)
        {
            panel.SetActive(false);
        // Kiểm tra xem đối tượng chạm vào có tag là "Player" không
        if (other.CompareTag("Player"))
            {
                // Hiện Canvas lên
                panel.SetActive(true);
            }
        }
    }