using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oki
{
    public class CursorManager : MonoBehaviour
    {
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}