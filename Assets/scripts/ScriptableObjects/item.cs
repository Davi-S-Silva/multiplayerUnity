using UnityEngine;
using UnityEngine.UI;
public class item : ScriptableObject
{
    [SerializeField]
    private string _nameItem;
    [SerializeField]
    private float _precoItem;
    [SerializeField][TextArea]
    private string _descricaoItem;
    [SerializeField]
    private Sprite _imgItem;
}
