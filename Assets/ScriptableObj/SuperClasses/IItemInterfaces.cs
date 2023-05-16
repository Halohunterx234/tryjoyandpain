using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Attack
{
    public abstract void OnFire();
}
public interface Support
{
    protected abstract void OnSupport();
}
