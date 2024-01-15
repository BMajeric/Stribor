using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProstorEnums
{
    //definira prostor u kojem je igrac, za koristit u stealthu
    public static Lokacija lokacijaIgraca = Lokacija.Poljana;
    public static StriborProgression striborProgress = StriborProgression.NemaKljuca;
    
    

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

    public enum StriborProgression {
        //enum za stanja dolazenja do stribora za posljednji svarozic upgrade

        NemaKljuca, //pocetak igre
        SkupioKljuc, //skupio je kljuc kod svjetionika
        SkupioJelena, //nasao jelena u kamenolomu
        PricaoSaStriborom, //pricao sa striborom i skupio posljednji upgrade

    }
}
