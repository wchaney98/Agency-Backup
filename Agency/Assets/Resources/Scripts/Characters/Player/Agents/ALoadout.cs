using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ALoadout
{
    public abstract AEquipment Primary { get; set; }
    public abstract AEquipment Secondary { get; set; }
}
