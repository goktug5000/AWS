using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindingManager : MonoBehaviour
{
    [SerializeField] private GameObject KeyObj;
    [SerializeField] private Transform KeysParent;

    private void Awake()
    {
        SetBindings();
        ShowKeys();
    }

    public void SetBindings()
    {
        KeyBindings.KeyCodes = new Dictionary<string, KeyCode>();
        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Esc, KeyCode.Escape);
        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Left, KeyCode.A);
        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Right, KeyCode.D);
        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Down, KeyCode.S);
        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Jump, KeyCode.W);

        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Attack, KeyCode.Mouse0);
        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_SecondAttack, KeyCode.Mouse1);
        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Deflect, KeyCode.Space);
    }

    public void ShowKeys()
    {
        int count = 0;
        foreach(var keyCode in KeyBindings.KeyCodes)
        {
            var newObj = Instantiate(KeyObj, KeysParent);
            newObj.GetComponent<UI_KeyCode>().SetUIKey(keyCode.Key, keyCode.Value);

            Vector2 newPosition = newObj.GetComponent<RectTransform>().anchoredPosition;
            newPosition.y = -120 * count;
            count++;
            newObj.GetComponent<RectTransform>().anchoredPosition = newPosition;
        }
    }
}
