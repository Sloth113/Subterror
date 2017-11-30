using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Interact interface
//Attached to doors, switches, -chests
interface iInteractable {
    void Use();
    void Use(GameObject user);
    string GetText();
    void DisplayToggle();
    
}
