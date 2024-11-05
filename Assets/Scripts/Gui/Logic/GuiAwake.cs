using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiAwake : MonoBehaviour
{
    [SerializeField] private CanvasInterfaces _canvasInterfaces;
    private InfoEdit _infoEdit = new InfoEdit();
    private HealthBarPlayer _healthBarPlayer = new HealthBarPlayer();
    private void Awake()
    {
        _infoEdit.CloseInfo(_canvasInterfaces._tableInfo.gameObject);
        _infoEdit.CloseInfo(_canvasInterfaces._gunInfo.gameObject);
        _healthBarPlayer.HealthBarEdit(_canvasInterfaces._healthBar, 100, 100);
    }
}
