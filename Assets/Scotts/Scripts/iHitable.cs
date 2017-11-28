using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface iHitable{
    void Hit();
    void Hit(int dam);
    void Knockback();//Stagger
    void Knockback(Vector3 dir);//Push
}
