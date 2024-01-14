using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMoney _money;
    [SerializeField] private PlayerLevel _level;
    [SerializeField] private BodyCarrier _bodyCarrier;

    public PlayerMoney PlayerMoney => _money;
    public PlayerLevel PlayerLevel => _level;
    public BodyCarrier BodyCarrier => _bodyCarrier;
}
