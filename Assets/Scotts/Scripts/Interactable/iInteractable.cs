using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface iInteractable {
    void Use();
    void Use(GameObject user);
    string GetText();
    void DisplayToggle();
    
}
