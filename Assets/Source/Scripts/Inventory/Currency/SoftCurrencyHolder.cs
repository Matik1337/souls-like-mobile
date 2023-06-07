using UnityEngine;

public class SoftCurrencyHolder : CurrencyHolder
{
    public override void Load()
    {
        if (PlayerPrefs.HasKey(nameof(SoftCurrencyHolder)))
        {
            Value = PlayerPrefs.GetInt(nameof(SoftCurrencyHolder));
        }
        else
        {
            Value = 0;
        }
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(nameof(SoftCurrencyHolder), Value);
    }
}
