using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProstorEnums
{
    //definira prostor u kojem je igrac, za koristit u stealthu
    public static Lokacija lokacijaIgraca = Lokacija.Poljana;
    


    public enum Lokacija {

        Poljana,//kod kotla
        Jezero ,
        TamnaSuma,
        Kamenolom,
        Spilja,
        Brdo,
        VelikoDrvo,
        Crkva,
        LovackaKuca,

    }
}
