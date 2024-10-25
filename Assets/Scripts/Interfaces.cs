using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public enum status
{
    Idle,
    Search,
    Collect,
    Return
}
interface IStatusVariable { status ChooseStatus(); }

interface IScore { void ScoreResult(TextMeshProUGUI TextScore, int Score); }

