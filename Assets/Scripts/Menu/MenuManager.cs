using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
//using UnityEditor.Experimental.GraphView;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    private MenuData _currentMenu;
    private Button _lastActiveButton;
    private Button _activeColorButton;
    private int colorArrayIndex = 0;


    /* The colors for the collor selector.
     * Are manipulated by the color sliders. 
     */
    public float red = 0, green = 0, blue = 0;

    [SerializeField] private List<MenuData> menus;
    [SerializeField] private List<Slider> colorSliders;
    [SerializeField] private Button _colorPickerButton;

    public MenuData CurrentMenu => _currentMenu;
    //public MenuData CurrentMenu { get { return _currentMenu; } }


    public void pickColor(Button button)
    {
        _activeColorButton = button;
        Open("Color");
    }

    public void pickColorArrayIndex(int n)
    {
        colorArrayIndex = n;
    }


    public void adjustColors()
    {
        red = colorSliders[0].value;
        green = colorSliders[1].value;
        blue = colorSliders[2].value;
        _colorPickerButton.gameObject.GetComponent<Image>().color = new Color(red, green, blue);
    }

    public void setActiveButtonColor()
    {
        Debug.Log(new Color(red, green, blue).ToString());
        Color c = new Color(red, green, blue);
        Colors.colorList[colorArrayIndex] = c;
        _activeColorButton.gameObject.GetComponent<Image>().color = c;
    }


    private void Start()
    {
        if (menus != null && menus.Count > 0)
            _currentMenu = menus[0];

        Open(_currentMenu);
    }

    /// <summary>
    /// Opens the menu with a given name.
    /// </summary>
    /// <param name="name">Name of the menu.</param>
    public void Open(string name)
    {
        if (_currentMenu != null && _currentMenu.Name == name)
            return;

        Open(GetMenuObjectByName(name));
    }


    private void Open(MenuData menuToOpen)
    {
        if (menuToOpen != null)
        {
            _currentMenu = menuToOpen;

            foreach (MenuData menu in menus)
            {
                if (menu.Menu != null)
                {
                    bool isMenuToOpen = menu == menuToOpen;
                    bool menuActive = menu.Menu.activeSelf;

                    if (menu.Menu.activeSelf && !isMenuToOpen)
                        menu.OnMenuClosed?.Invoke();

                    menu.Menu.SetActive(isMenuToOpen);

                    if (isMenuToOpen)
                        menu.OnMenuOpened.Invoke();

                    // TODO: Event Triggern, aber nur wenn das Menu offen war und schließt!
                    //       Ebenso
                }
            }
				//if (menu.Menu != null) // TODO: Hier bei Namen abgleich auf True?!
        }
    }

    private MenuData GetMenuObjectByName(string name)
    {
        // TODO: Lambda als Getter!
        foreach (MenuData menu in menus)
            if (menu.Name == name)
                return menu;

        return null;
    }
}

[Serializable]
public class MenuData
{

    [SerializeField] private string name;
    [SerializeField] private GameObject menu;
    [Space]
    //[SerializeField] private bool openAdditionally;
    [Space(20)]
    public UnityEvent OnMenuOpened;
    public UnityEvent OnMenuClosed;

    public string Name => name;
    public GameObject Menu => menu;
    //public bool OpenAdditionally => openAdditionally;
}

//#if UNITY_EDITOR
//public class MenuDataEditor : Editor
//{
    
//}
//#endif
