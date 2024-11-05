using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using static UnityEngine.UI.Image;

public class ParametresUnit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool IsDead = false;
    [SerializeField] CanvasInterfaces _canvasInterfaces;
    [SerializeField] GameObject _hand;
    [SerializeField] public Transform poolMov;
    [SerializeField] RuntimeAnimatorController _anim;
    [SerializeField] PostProcessVolume _profFirst;
    [SerializeField] PostProcessVolume _profthirt;

    private InfoEdit _infoEdit = new InfoEdit();
    private HealthBarPlayer _healthBarPlayer = new HealthBarPlayer();

    [SerializeField] string Name;
    [SerializeField] int Health;
    [SerializeField] int MaxHealth;

    private EnemyAi _enemyAi;
    private PlayerInput _playerInput;

    public ToolParametres Gun = null;

    public int _health => Health;
    public int _maxHealth => MaxHealth;
    public ToolParametres _gun => Gun;
    public bool _isDead => IsDead;

    public string _name => Name;

    private Animation _animation;

    private float _time = 1;

    private WaitForSeconds _ToEffectTimeStop;

    private void Start()
    {
        _ToEffectTimeStop = new WaitForSeconds(_time);
        _animation = GetComponent<Animation>();
        _enemyAi = GetComponent<EnemyAi>();
        _playerInput = GetComponent<PlayerInput>();
    }

    public void GiveHealth(int health)
    {
        if (!_isDead)
        {
            Health += health;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
            if (Health <= 0)
            {
                Health = 0;
                killUnit();
            }


            if(Name == "Player")
            {
                if(Health <= 0)
                {
                    _profFirst.isGlobal = true;
                    _profthirt.isGlobal = true;
                }
                else
                {
                    StartCoroutine(Vignette());
                }
                _healthBarPlayer.HealthBarEdit(_canvasInterfaces._healthBar, Health, MaxHealth);
            }
        }
    }

    public IEnumerator Vignette()
    {
        _profFirst.isGlobal = true;
        _profthirt.isGlobal = true;

        yield return _ToEffectTimeStop;

        _profFirst.isGlobal = false;
        _profthirt.isGlobal = false;
    }

    public void GiveGun(ToolParametres gun)
    {

        GameObject newWeapon = Instantiate(gun._gunmodel, _hand.transform);

        Gun = newWeapon.GetComponent<ToolParametres>();

        if (_enemyAi != null)
        {

            _enemyAi._animator.runtimeAnimatorController = _anim;
        }

        if (gameObject.layer == 11)
        {
            if (Gun == null)
                _infoEdit.OpenInfo(_canvasInterfaces._gunInfo.gameObject);

            if (_playerInput != null)
            {
                _playerInput._animator.runtimeAnimatorController = _anim;
            }

            _infoEdit.OpenInfo(_canvasInterfaces._gunInfo.gameObject);
            _infoEdit.GiveInfoMain(Gun._name, _canvasInterfaces._mainInfoGun);
            _infoEdit.GiveInfoDescription(Gun._damage, 0, Gun._ammo, _canvasInterfaces._descriptionInfoGun);
        }
        Destroy(gun.gameObject);

    }


    public void killUnit()
    {
        new RBKinematic().RBIsKinematic(gameObject, false);
        IsDead = true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Name != "Player")
        {
            _infoEdit.OpenInfo(_canvasInterfaces._tableInfo.gameObject);
            _infoEdit.GiveInfoMain(Name, _canvasInterfaces._mainInfoTab);
            _infoEdit.GiveInfoDescription(0, Health, 0, _canvasInterfaces._descriptionInfoTab);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(Name != "Player")
        {
            _infoEdit.CloseInfo(_canvasInterfaces._tableInfo.gameObject);
        }
    }
}
