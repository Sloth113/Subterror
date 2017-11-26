using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface iHitable{
    void Hit();
    void Hit(int dam);
    void KnockBack();//Stagger
    void KockBack(Vector3 dir);//Push
}
