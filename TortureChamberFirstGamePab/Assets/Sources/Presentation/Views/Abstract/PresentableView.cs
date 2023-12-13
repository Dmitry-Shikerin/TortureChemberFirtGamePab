using System;
using System.Collections;
using System.Collections.Generic;
using Sources.ControllersInterfaces;
using UnityEngine;

//TODO в правильной ли он папке?
//TODO сделать класс абстрактным?
public class PresentableView<T> : MonoBehaviour where T : IPresenter
{
    protected T Presenter {get; private set;}

    //TODO можно ли сделать их виртуальными?
    public void OnEnable() => 
        Presenter?.Enable();

    public void OnDisable() => 
        Presenter?.Disable();

    public void Construct(T presenter)
    {
        Hide();
        Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        Show();
    }

    private void Show() => 
        gameObject.SetActive(true);

    private void Hide() => 
        gameObject.SetActive(false);
}
