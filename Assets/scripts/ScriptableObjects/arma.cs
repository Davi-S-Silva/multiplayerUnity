using UnityEngine;

[CreateAssetMenu(fileName = "Arma", menuName = "Inventario/Nova Arma")]
public class arma : item
{
    [SerializeField]
    private float _forcaCano, _forcaDefesa;
    [SerializeField]
    private int _qtdMunicao;
    [SerializeField]
    private GameObject _prefab;
}
