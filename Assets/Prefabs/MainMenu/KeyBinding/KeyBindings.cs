using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindings
{
    [Header("Keyboard")]
    [SerializeField] public static Dictionary<string, KeyCode> KeyCodes;

    [SerializeField] public static string KeyCode_Esc = "KeyCode_Esc";

    [SerializeField] public static string KeyCode_Left = "KeyCode_Left";
    [SerializeField] public static string KeyCode_Right = "KeyCode_Right";
    [SerializeField] public static string KeyCode_Up = "KeyCode_Up";
    [SerializeField] public static string KeyCode_Down = "KeyCode_Down";

    [SerializeField] public static string KeyCode_Jump = "KeyCode_Jump";

    [SerializeField] public static string KeyCode_Attack = "KeyCode_Attack";
    [SerializeField] public static string KeyCode_SecondAttack = "KeyCode_SecondAttack";
    [SerializeField] public static string KeyCode_Deflect = "KeyCode_Deflect";


    [Header("Gamepad")]
    [SerializeField] public static string KeyCode_joy_Esc = "KeyCode_joy_Esc";

}
