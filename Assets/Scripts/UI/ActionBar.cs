using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour
{
    private Image[] _images;

    public void Start()
    {
        _images = Enumerable.Range(0, 10).Select(x =>
        {
            var img = new GameObject("Action " + (x + 1), typeof(Image));
            img.transform.SetParent(transform);
            return img.GetComponent<Image>();
        }).ToArray();
    }

    public void ChangeImageColor(Color c, int index)
    {
        _images[index].color = c;
    }

    public void Clear()
    {
        foreach (var image in _images)
        {
            image.color = Color.white;
        }
    }
}
