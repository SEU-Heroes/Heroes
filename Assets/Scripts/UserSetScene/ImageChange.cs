using UnityEngine;
using System.Collections;
using UnityEngine.Sprites;
using UnityEngine.UI;
public class ImageChange : MonoBehaviour {
    public int _BGPicture = 0;
    public Image image;
    public Image bigImage;
    public Sprite backSeu;
    public Sprite backBoat;
    static public ImageChange _instance;

    void Start()
    {
        _instance = this;
    }

    public void Down()
    {
        _BGPicture = (_BGPicture + 1) % 2;
        ChangeImage();
    }
    public void ChangeImage() {
        if (_BGPicture == 0)
        {
            image.sprite = backBoat;
            bigImage.sprite = backBoat;
        }
        else {
            image.sprite = backSeu;
            bigImage.sprite = backSeu;
        }
    }
}
