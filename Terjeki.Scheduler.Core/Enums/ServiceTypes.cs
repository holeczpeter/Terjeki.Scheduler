namespace Terjeki.Scheduler.Core
{
   
    public enum ServiceTypes
    {
        [Description("Nincs")]
        None,

        [Description("Olajcsere")]
        OilChange,

        [Description("Műszaki vizsga")]
        Inspection,

        [Description("Egyéb")]
        Other,

        [Description("Fékellenőrzés és javítás")]
        BrakeCheck,

        [Description("Gumiabroncs csere vagy javítás")]
        TireService,

        [Description("Motor diagnosztika")]
        EngineDiagnostics,

        [Description("Futómű beállítás")]
        SuspensionAlignment,

        [Description("Lámpa- és világításellenőrzés")]
        LightingCheck,

        [Description("Szűrők cseréje (levegő, üzemanyag, pollen)")]
        FilterReplacement,

        [Description("Hűtőrendszer karbantartás")]
        CoolingSystemService,

        [Description("Klímarendszer ellenőrzés és töltés")]
        ACService,

        [Description("Általános időszakos karbantartás")]
        GeneralMaintenance,

        [Description("Vészhelyzeti javítás")]
        EmergencyRepair,

        [Description("Külső/belső tisztítás")]
        Cleaning,

        [Description("Elektronikai rendszer vizsgálat")]
        ElectricalCheck
    }
}
