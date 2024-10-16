using UnityEngine;

namespace Scripts.UI
{
    public class JoystickCanvas : MonoBehaviour
    {
        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disabe()
        {
            gameObject.SetActive(false);
        }
    }
}